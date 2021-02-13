using Data;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMap.Core;
using UserMap.Helpers;

namespace UserMap
{
    public partial class MapWindow : Form
    {
        private static readonly string defaultImageName;
        private static readonly string addTempLayoutName;

        private readonly GMap.NET.PointLatLng defaultMapStartCoord;
        private readonly Role expert;
        private readonly int userId;
#if !DEBUG
        private readonly Services.ILogger logger;
#endif

        private bool moveMode;
        private bool markerAddingMode;
        private bool polygonAddingMode;
        private bool tubeAddingMode;
        private DBManager dBManager;

        private Keys pressedKey;

        private Map reworkedMap;
        private DrawContext drawContext;

        private HelpWindows.ItemConfigurationWindow itemConfigurationWindow;
        private UserControls.ItemInfo itemInfo;

        static MapWindow()
        {
            defaultImageName = "missingImage";
            addTempLayoutName = "__AddTempLayout__";
        }

        public MapWindow(Role expert, int userId)
        {
            MapCache.Add("images", new Dictionary<string, Bitmap>());

            defaultMapStartCoord = new GMap.NET.PointLatLng(50.449173, 30.457578);

            this.userId = userId;
            this.expert = expert;
            this.moveMode = false;

            InitializeComponent();
            InitialiazeAdditionalComponent();

            reworkedMap = new Map(gMapControl);
            dBManager = new DBManager();

            itemConfigurationWindow = null;
            drawContext = null;

#if !DEBUG
            logger = new Services.FileLogger();
#endif
        }

        private void InitialiazeAdditionalComponent()
        {
            gMapControl.DragButton = MouseButtons.Left;
            gMapControl.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            gMapControl.Position = defaultMapStartCoord;
            gMapControl.MaxZoom = 18;
            gMapControl.MinZoom = 2;
            gMapControl.Zoom = 12;

            FindSideMenuPanel.Visible = false;
            FiltrationSideMenuPanel.Visible = false;
            CompareSideMenuPanel.Visible = false;
            ElementsSideMenuPanel.Visible = true;

            cbSearch.SelectedIndex = 0;

            itemInfo = new UserControls.ItemInfo();
            itemInfo.Visible = false;
            itemInfo.Location = new Point(this.Width - itemInfo.Width - 15, (this.Height - itemInfo.Height - MainStatusStrip.Height) / 2);
            itemInfo.Anchor = AnchorStyles.Right;
            itemInfo.SubscriChangeItemClickEvent(ChangeObjectSettings);
            itemInfo.SubscribeAdditionInfoClickEvent(AdditionalInfo);

            if (expert != Role.Admin)
            {
                itemInfo.HideDeleteButton();
                TubeDrawButton.Enabled = false;
            }

            this.Controls.Add(itemInfo);
            this.Controls.SetChildIndex(itemInfo, 0);

            this.Height = 450;

            PolygonColorTypeComboBox.SelectedIndex = 0;

            Binding envCheckBinding = new Binding("Enabled", EnvironmentsCheckBox, "Checked");
            Binding issuesCheckBinding = new Binding("Enabled", IssuesCheckBox, "Checked");
            Binding economycActivityCheckBinding = new Binding("Enabled", EconomicActivityCheckBox, "Checked");

            EnvironmentsGroupBox.DataBindings.Add(envCheckBinding);
            IssuesGroupBox.DataBindings.Add(issuesCheckBinding);
            EconomicActivityGroupBox.DataBindings.Add(economycActivityCheckBinding);

#if DEBUG
            DebugToolStripStatusLabel.Visible = true;
#endif
        }
        private async Task LoadCommonThings()
        {
            this.Invoke((Action<string, bool, ProgressBarStyle>)ChangeProgressBar,
                        "Йде завантаження головних даних.",
                        true, ProgressBarStyle.Marquee);

            Task citiesTask = CitiesComboBox.FillComboBoxFromBDAsync(dBManager, "cities", "*", "",
                                          r =>
                                          {
                                              if (r[0].ToString() == "Камянець-Подільський")
                                              {
                                                  r[0] = "Кам'янець-Подільський";
                                              }
                                              else if (r[0].ToString() == "Камянське")
                                              {
                                                  r[0] = "Кам'янське";
                                              }
                                              else if (r[0].ToString() == "Словянськ")
                                              {
                                                  r[0] = "Слов'янськ";
                                              }

                                              return CityMapper.Map(r);
                                          },
                                          displayComboBoxMember: "Name",
                                          falultAction: c =>
                                          {
                                              c.Items.Clear();
                                              c.Text = "Не вдалось завантажити міста.";
                                              GoToCityButton.Enabled = false;
                                          });

            Task environmentTask = FillCheckedListBoxFromDBAsync(EnvironmentCheckedListBox, "environment", "*", "",
                                          r => EnvironmentMapper.Map(r),
                                          falultAction: c =>
                                          {
                                              c.DataSource = new List<Data.Entity.Environment>
                                              {
                                                  new Data.Entity.Environment { Id = -1, Name = "Помилка при завантаженні середовищ." }
                                              };

                                              EnvironmentsCheckBox.Checked = false;
                                          });

            Task issuesTask = FillCheckedListBoxFromDBAsync(IssueCheckedListBox, "issues", "*", "",
                                          r => IssueMapper.Map(r),
                                          falultAction: c => c.Items.Add(new Issue(-1) { Name = "Не вдалось завантажити задачі." })); ;

            Task ownershipTask = OwnershipTypeComboBox.FillComboBoxFromBDAsync(dBManager, "owner_types", "*", "",
                                          r => Data.Helpers.Mapper.Map<OwerType>(r),
                                          displayComboBoxMember: "Type",
                                          valueComboBoxMember: "Id",
                                          falultAction: c => c.Items.Add("Не вдалось завантажити типи власності"));

            Task economicActivityTask = EconomicActivityComboBox.FillComboBoxFromBDAsync(dBManager, "type_of_object", "Id, Name, Image_Name", "",
                                          r =>
                                          {
                                              int id;
                                              int.TryParse(r[0].ToString(), out id);

                                              return new TypeOfObject
                                              {
                                                  Id = id,
                                                  Name = r[1].ToString(),
                                                  ImageName = r[2].ToString()
                                              };
                                          },
                                          displayComboBoxMember: "Name",
                                          valueComboBoxMember: "ImageName",
                                          falultAction: c => c.Items.Add("Не вдалось завантажити вид економічної діяльності"));

            await Task.WhenAll(citiesTask, environmentTask, issuesTask, ownershipTask, economicActivityTask)
                      .ContinueWith(_task =>
                      {
                          EconomicActivityComboBox.SelectedIndex = -1;
                          EconomicActivityComboBox.SelectedIndex = 0;

                          if (!economicActivityTask.IsFaulted)
                          {
                              foreach (var typeOfObject in (IEnumerable<TypeOfObject>)EconomicActivityComboBox.DataSource)
                              {
                                  EconomicActivityCheckedListBox.Items.Add(typeOfObject);
                              }
                          }

                          ChangeProgressBar(string.Empty, false, ProgressBarStyle.Continuous);

                          if (!environmentTask.IsFaulted)
                          {
                              MapCache.Add("environments", EnvironmentCheckedListBox.Items
                                                                                        .OfType<Data.Entity.Environment>()
                                                                                        .ToList());
                          }
                      }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private async Task FillCheckedListBoxFromDBAsync<TResult>(CheckedListBox checkedListBox, string table, string columns,
                                                                  string condition, Func<List<object>, TResult> func,
                                                                  Action<CheckedListBox> falultAction = null)
        {
            if (checkedListBox == null || string.IsNullOrEmpty(table) ||
                columns == null || condition == null || func == null)
            {
                return;
            }

            try
            {
                var results = (await dBManager.GetRowsAsync(table, columns, condition))
                                                   .Select(func)
                                                   .ToList();

                checkedListBox.DataSource = results;
            }
            catch (Exception ex)
            {
                if (falultAction != null)
                {
                    checkedListBox.Invoke(falultAction, checkedListBox);
                }

#if DEBUG
                DebugLog(ex);
#else
                logger.Log(ex);
#endif
            }


        }

        private Bitmap LoadImage(string imageName)
        {
            imageName = imageName ?? string.Empty;

            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory,
                                                 @"Resources\Images",
                                                 imageName);

            Bitmap img = null;
            var images = (Dictionary<string, Bitmap>)Helpers.MapCache.GetItem("images");

            if (System.IO.File.Exists(path))
            {
                if (images.ContainsKey(imageName))
                {
                    img = images[imageName];
                }
                else
                {
                    img = (Bitmap)Image.FromFile(path);
                    images.Add(imageName, img);
                }
            }
            else
            {
                if (!images.ContainsKey(defaultImageName))
                {
                    images.Add(defaultImageName,
                               (Bitmap)Image.FromFile(
                                   System.IO.Path.Combine(System.Environment.CurrentDirectory, @"Resources\Images\noimage.png"))
                               );
                }

                img = images[defaultImageName];
            }

            return img;
        }

        private void ChangeProgressBar(string text, bool visible, ProgressBarStyle progressBarStyle)
        {
            CurrentActionStatusLabel.Text = text;
            MainToolStripProgressBar.Visible = visible;
            MainToolStripProgressBar.ProgressBar.Style = progressBarStyle;
        }

#if DEBUG
        private void DebugLog(Exception ex)
        {
            MessageBox.Show($"Message:\n{ex.Message}\n\nStack trace: \n{ex.StackTrace}",
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
#endif

        private async void MapWindow_Shown(object sender, EventArgs e)
        {
            await LoadCommonThings();
        }

        private void HideContextMenuStripItems(ContextMenuStrip contextMenuStrip)
        {
            foreach (ToolStripItem item in contextMenuStrip.Items)
            {
                item.Visible = false;
            }
        }
        private void ShowContextMenuStripItems(ContextMenuStrip contextMenuStrip)
        {
            foreach (ToolStripItem item in contextMenuStrip.Items)
            {
                item.Visible = true;
            }
        }

        #region Side panel methods
        private void HideAllSidePanels()
        {
            foreach (var control in PanelSideMenu.Controls)
            {
                if (control is Panel)
                {
                    if (((Panel)control).Name[0] == '_')
                        continue;

                    ((Panel)control).Visible = false;

                }
            }
        }
        private void ShowSidePanel(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            if (panel.Visible == false)
            {
                HideAllSidePanels();
                panel.Visible = true;
            }
            else
            {
                panel.Visible = false;
            }
        }
        private void HideAndShowMainPanel()
        {
            PanelSideMenu.Visible = !PanelSideMenu.Visible;

            if (PanelSideMenu.Visible)
            {
                CollapseButton.Location = new Point(PanelSideMenu.Width + 3, 1);
                ZoomPlus.Location = new Point(CollapseButton.Location.X, CollapseButton.Location.Y + CollapseButton.Height + 2);
                ZoomMinus.Location = new Point(ZoomPlus.Location.X, ZoomPlus.Location.Y + ZoomPlus.Height + 2);
                gMapControl.Dock = DockStyle.None;
            }
            else
            {
                CollapseButton.Location = new Point(5, 1);
                ZoomPlus.Location = new Point(CollapseButton.Location.X, CollapseButton.Location.Y + CollapseButton.Height + 2);
                ZoomMinus.Location = new Point(ZoomPlus.Location.X, ZoomPlus.Location.Y + ZoomPlus.Height + 2);
                gMapControl.Dock = DockStyle.Top;
            }
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            HideAndShowMainPanel();
        }
        private void ZoomPlus_Click(object sender, EventArgs e)
        {
            reworkedMap.ZoomPlus();
        }
        private void ZoomMinus_Click(object sender, EventArgs e)
        {
            reworkedMap.ZoomMinus();
        }

        private void SideMenuButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = (Button)sender;

                switch (button.Tag?.ToString())
                {
                    case "1":
                        ShowSidePanel(FindSideMenuPanel);
                        break;
                    case "2":
                        ShowSidePanel(CompareSideMenuPanel);
                        break;
                    case "3":
                        ShowSidePanel(FiltrationSideMenuPanel);
                        break;
                    case "4":
                        ShowSidePanel(ElementsSideMenuPanel);
                        break;
                    default:
                        HideAllSidePanels();
                        break;
                }
            }
        }
        #endregion

        private async Task ChangeObject(string objectType)
        {
            if (itemConfigurationWindow != null && reworkedMap.SelectedMarker != null)
            {
                var marker = reworkedMap.SelectedMarker as NamedGoogleMarker;

                if (marker == null)
                {
                    return;
                }

                var (changedObjName, changedObjDescription, changedSeries, changedEmissions) = itemConfigurationWindow;

                marker.Name = changedObjName;
                marker.Description = changedObjDescription;

                bool changeAsEmission = changedEmissions != null;

                string objTableName = null;
                string objCondColumn = null;
                string emissionColumn = null;
                string[] objColumns = null;
                string[] objValues = null;

                if (objectType == "Маркер")
                {
                    objTableName = "poi";
                    objCondColumn = "Id";
                    emissionColumn = "idPoi";

                    objColumns = new string[] { "Description", "Name_Object", "id_of_issue",
                                                "calculations_description_number", "idEnvironment" };
                    objValues = new string[] { DBUtil.AddQuotes(changedObjName), DBUtil.AddQuotes(changedObjDescription),
                                               //changedIssue != null ? changedIssue.Id.ToString() : "0",
                                               //changedSeries != null ? changedSeries.Id.ToString() : "0",
                                              /* changedEnvironment != null ? changedEnvironment.Id.ToString() : */"NULL" };
                }
                else if (objectType == "Полігон" || objectType == "Трубопровід")
                {
                    objTableName = "map_object_dependencies";
                    objCondColumn = "id_poligon";
                    emissionColumn = "idPoligon";

                    objColumns = new string[] { "id_of_issue", "calculations_description_number" };
                    //objValues = new string[] { changedIssue != null ? changedIssue.Id.ToString() : "0",
                    //                           changedSeries != null ? changedSeries.Id.ToString() : "0" };
                    objValues = null;
                }



                try
                {
                    dBManager.StartTransaction();
                    await dBManager.UpdateRecordAsync(objTableName, objColumns, objValues, objCondColumn + " = " + marker.Id.ToString());

                    if (changeAsEmission)
                    {
                        string[] emissionColumns = { "idElement", "idEnvironment", "ValueAvg", "ValueMax",
                                                     "Year", "Month", "day", "Measure" };
                        //string[] emissionValues = { changedEmission.Element.Code.ToString(), changedEmission.Environment.Id.ToString(),
                        //                            changedEmission.AvgValue.ToString().Replace(',', '.'), 
                        //                            changedEmission.MaxValue.ToString().Replace(',', '.'), 
                        //                            changedEmission.Year.ToString(), changedEmission.Month.ToString(),
                        //                            changedEmission.Day.ToString(), DBUtil.AddQuotes(changedEmission.Element.Measure) };
                        string[] emissionValues = null;

                        await dBManager.UpdateRecordAsync("emissions_on_map", emissionColumns, emissionValues, emissionColumn + " = " + marker.Id.ToString());
                    }

                    dBManager.CommitTransaction();

                    MessageBox.Show("Дані успішно оновлені.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dBManager.RollbackTransaction();

#if DEBUG
                    DebugLog(ex);
#else
                    MessageBox.Show("Помилка при оновлені даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Log(ex);
#endif
                }
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!polygonAddingMode && !tubeAddingMode)
            {
                if (expert == Role.Admin)
                {
                    IDescribable describableItem = reworkedMap.SelectedMarker as IDescribable;

                    if (describableItem.Type == "Маркер")
                    {
                        DeleteMarker(null, null);
                    }
                    else if (describableItem.Type == "Полігон")
                    {
                        DeletePolyline(null, null);
                    }
                    else if (describableItem.Type == "Трубопровід")
                    {
                        DeletePolyline(null, null);
                    }
                }
            }
            else if (drawContext != null)
            {
                drawContext.RemoveCoord(reworkedMap.SelectedMarker.Position);
            }
        }
        private async void ChangeObjectSettings(object sender, EventArgs e)
        {
            var marker = reworkedMap.SelectedMarker as NamedGoogleMarker;

            if (marker == null)
            {
                MessageBox.Show("Не вдалось відкрити вікно для налаштування об'єкту",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string markerType = ((IDescribable)marker).Type;
            string emissionObjIdColumn = string.Empty;

            IEnumerable<Emission> emissions = null;
            IDictionary<Issue, IEnumerable<CalculationSeries>> series = null;

            string dependenciesTables = "poi, map_object_dependencies, issues, calculations_description";
            string dependenciesColumns = " poi.Id, issues.name, calculations_description.calculation_name, " +
                                         "calculations_description.issue_id";
            string dependenciesJoinCondition = "poi.Id = map_object_dependencies.id_of_object, " +
                                               "map_object_dependencies.type_rel = 0 AND " +
                                               "map_object_dependencies.type_obj = 0 AND " +
                                               "map_object_dependencies.id_of_ref = issues.issue_id";
            string dependenciesCondition = "type_obj = " + (markerType == "Маркер" ? "0" : "1");
            string emissionTables = "poi, emissions_on_map, environment";
            string emissionColumns = "DISTINCT environment.name, poi.Id, emissions_on_map.ValueAvg, " +
                                     "emissions_on_map.ValueMax, emissions_on_map.Year, emissions_on_map.ValueAvg.";
            string emissionJoinCondition = "emissions_on_map.idPoi = poi.Id, emissions_on_map.idEnvironment = environment.id";

            try
            {


                //string emissionTables = "emissions_on_map, elements, environment";
                //string emissionColumns = "elements.code, emissions_on_map.ValueAvg, emissions_on_map.ValueMax, " +
                //                         "emissions_on_map.Year, emissions_on_map.Month, emissions_on_map.day, " +
                //                         "environment.id, environment.name";
                //string emissionJoinCond = "emissions_on_map.idElement = elements.code, " +
                //                          "emissions_on_map.idEnvironment = environment.id";
                //string emissionCond = emissionObjIdColumn + " = " + marker.Id.ToString();

                //var emissionResult = await dBManager.GetRowsUsingJoinAsync(emissionTables, emissionColumns, emissionJoinCond, emissionCond, JoinType.LEFT);

                //foreach (var emissionItem in emissionResult)
                //{
                //    emission = new Emission()
                //    {
                //        //Element = new Element()
                //        //{
                //        //    Code = (int)emissionItem[0],
                //        //},
                //        //Environment = new Data.Entity.Environment()
                //        //{
                //        //    Id = (int)emissionItem[6],
                //        //    Name = emissionItem[7].ToString()
                //        //},
                //        AvgValue = (double)emissionItem[1],
                //        MaxValue = (double)emissionItem[2],
                //        Year = (int)emissionItem[3],
                //        Month = (int)emissionItem[4],
                //        Day = (int)emissionItem[5],
                //    };
                //}
            }
            catch (Exception ex)
            {
#if DEBUG
                DebugLog(ex);
#else
                MessageBox.Show("Помилка при відкритті вікна для налаштування об'єкту.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
#endif
            }

            itemConfigurationWindow = new HelpWindows.ItemConfigurationWindow(expert);

            //if (environment != null)
            //{
            //    itemConfigurationWindow.SetEnvironment(environment);
            //}
            //else if (emission != null && emission.Environment != null)
            //{
            //    itemConfigurationWindow.SetEnvironment(emission.Environment);
            //}

            //itemConfigurationWindow.SetIssue(issue);
            //itemConfigurationWindow.SetEmission(emission);
            //itemConfigurationWindow.SetSeries(series);
            itemConfigurationWindow.SetObjName(marker.Name);
            itemConfigurationWindow.SetObjDescritpion(marker.Description);

            if (itemConfigurationWindow.ShowDialog() == DialogResult.OK)
            {
                await ChangeObject(markerType);
            }

            itemInfo.SetData(marker);

            itemConfigurationWindow.Dispose();
            itemConfigurationWindow = null;
        }
        private void AdditionalInfo(object sender, EventArgs e)
        {
            var marker = (NamedGoogleMarker)reworkedMap.SelectedMarker;

            HelpWindows.MultiBindingObjectEditor multiBindingObjectEditor = new HelpWindows.MultiBindingObjectEditor(marker.Id, marker);
            multiBindingObjectEditor.ShowDialog();
            multiBindingObjectEditor.Dispose();
        }

        #region Map methods
        private void SetMarkerPosNearExistMarker(Point existMarkerPoint)
        {
            var marker = reworkedMap.GetMarkerByCoordsOrNull(existMarkerPoint);

            if (marker != null && reworkedMap.SelectedMarker != null && !object.ReferenceEquals(marker, reworkedMap.SelectedMarker))
            {
                reworkedMap.SelectedMarker.Position = new GMap.NET.PointLatLng(marker.Position.Lat - 0.0008, marker.Position.Lng - 0.0008);
            }
        }

        private void gMapControl_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            if (markerAddingMode)
            {
                return;
            }

            reworkedMap.SelectedMarker = item;

            if (e.Button == MouseButtons.Right && !markerAddingMode)
            {
                if (polygonAddingMode || tubeAddingMode || expert == Role.Admin)
                {
                    MapObjectContextMenuStrip.Items[MapObjectContextMenuStrip.Items.Count - 1].Visible = true;
                }

                MapObjectContextMenuStrip.Show(gMapControl, e.Location);
            }
            else if (!markerAddingMode && !polygonAddingMode && !tubeAddingMode)
            {
                IDescribable describableItem = item as IDescribable;

                if (item != null)
                {
                    itemInfo.SetData(describableItem);
                }

                if (expert == Role.Admin)
                {
                    itemInfo.DescribeDeleteItemClickEvent(DeleteMarker);
                    itemInfo.DescribeDeleteItemClickEvent(DeletePolyline);

                    if (describableItem.Type == "Маркер")
                    {
                        itemInfo.SubscribeDeleteItemClickEvent(DeleteMarker);
                    }
                    else if (describableItem.Type == "Полігон")
                    {
                        itemInfo.SubscribeDeleteItemClickEvent(DeletePolyline);
                    }
                    else if (describableItem.Type == "Трубопровід")
                    {
                        itemInfo.SubscribeDeleteItemClickEvent(DeletePolyline);
                    }
                }

                if (!itemInfo.Visible)
                {
                    itemInfo.Visible = true;
                }
            }
        }
        private void gMapControl_DoubleClick(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button != MouseButtons.Left)
            {
                return;
            }

            Point cursorLocation = ((MouseEventArgs)e).Location;

            if (markerAddingMode)
            {
                var addedMarkers = reworkedMap.GetMarkersByLayoutOrNull(addTempLayoutName);

                if (addedMarkers == null || addedMarkers.Count() == 0)
                {
                    reworkedMap.AddMarker(cursorLocation,
                                          (Bitmap)MarkerPictureBox.Image,
                                          addTempLayoutName);
                }
                else
                {
                    addedMarkers.First().Position = gMapControl.FromLocalToLatLng(cursorLocation.X, cursorLocation.Y);
                    SetMarkerPosNearExistMarker(cursorLocation);
                }
            }
            else if ((polygonAddingMode || tubeAddingMode) && drawContext != null)
            {
                drawContext.AddCoord(gMapControl.FromLocalToLatLng(cursorLocation.X, cursorLocation.Y));
            }
        }
        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveMode && pressedKey == Keys.ControlKey)
            {
                moveMode = true;

                if (!markerAddingMode && drawContext != null)
                {
                    reworkedMap.SelectedMarker = reworkedMap.GetMarkerByCoordsInLayoutOrNull(e.Location, drawContext.Overlay.Id);
                }
            }
        }
        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (moveMode)
            {
                if (markerAddingMode)
                {
                    SetMarkerPosNearExistMarker(e.Location);
                }

                moveMode = false;
            }
        }
        private void gMapControl_MouseMove(object sender, MouseEventArgs e)
        {

            if (moveMode && reworkedMap.SelectedMarker != null)
            {
                if (markerAddingMode)
                {
                    reworkedMap.SelectedMarker.Position = gMapControl.FromLocalToLatLng(e.Location.X, e.Location.Y);
                }
                else if ((polygonAddingMode || tubeAddingMode) && drawContext != null)
                {
                    if (int.TryParse(reworkedMap.SelectedMarker.ToolTipText, out int res))
                    {
                        drawContext.MoveCoord(gMapControl.FromLocalToLatLng(e.Location.X, e.Location.Y), res);
                    }
                }
            }
        }
        private void gMapControl_KeyDown(object sender, KeyEventArgs e)
        {
            pressedKey = e.KeyCode;
        }
        private void gMapControl_KeyUp(object sender, KeyEventArgs e)
        {
            pressedKey = Keys.None;
        }
        #endregion

        #region Finding method group
        private void GoToCityButton_Click(object sender, EventArgs e)
        {
            if (CitiesComboBox.SelectedValue != null && CitiesComboBox.SelectedValue is City)
            {
                City city = (City)CitiesComboBox.SelectedValue;

                gMapControl.Position = new GMap.NET.PointLatLng(city.Latitude, city.Longitude);
            }
            else if (string.IsNullOrEmpty(CitiesComboBox.Text) && CitiesComboBox.DataSource is IEnumerable<City>)
            {
                IEnumerable<City> cities = (IEnumerable<City>)CitiesComboBox.DataSource;
                City city = cities.FirstOrDefault(c => c.Name.ToLower() == CitiesComboBox.Text.ToLower());

                if (city != default)
                {
                    gMapControl.Position = new GMap.NET.PointLatLng(city.Latitude, city.Longitude);
                }
            }
        }
        private void FindByLngLtdButton_Click(object sender, EventArgs e)
        {
            double lng, lat;

            if (string.IsNullOrEmpty(LongitudeTextBox.Text) || !double.TryParse(LongitudeTextBox.Text, out lng))
            {
                MessageBox.Show("Не правильно введена широта.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(LattituteTextBox.Text) || !double.TryParse(LattituteTextBox.Text, out lat))
            {
                MessageBox.Show("Не правильно введена довгота.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gMapControl.Position = new GMap.NET.PointLatLng(lat, lng);
        }
        #endregion

        #region Filtering method group
        private void ShowLayoutButton_Click(object sender, EventArgs e)
        {
            FiltrationInfoStripStatusLabel.Text = "Ввімкнена фільтрація на мапі";
            FiltrationSideMenuButton.Text = "Фільтрація (ввімкнена)";
            FiltrationSideMenuButton.BackColor = Color.DarkGray;

            reworkedMap.HideAllLayout();

            if (EnvironmentsCheckBox.Checked && EnvironmentCheckedListBox.CheckedItems.Count != 0)
            {
                foreach (Data.Entity.Environment checkedItem in EnvironmentCheckedListBox.CheckedItems)
                {
                    reworkedMap.ShowLayoutById(checkedItem.Name);
                }
            }

            if (IssuesCheckBox.Checked)
            {
                foreach (Issue checkedItem in IssueCheckedListBox.CheckedItems)
                {
                    reworkedMap.ShowLayoutById(checkedItem.Name);
                }
            }

            if (EconomicActivityCheckBox.Checked)
            {
                foreach (TypeOfObject checkedItem in EconomicActivityCheckedListBox.CheckedItems)
                {
                    reworkedMap.ShowLayoutById(checkedItem.Name);
                }
            }

            if (RegionCheckBox.Checked)
            {
                reworkedMap.ShowLayoutById("region");
            }

            if (TubeCheckBox.Checked)
            {
                reworkedMap.ShowLayoutById("tube");
            }
        }

        private void HideAllLayoutButton_Click(object sender, EventArgs e)
        {
            FiltrationInfoStripStatusLabel.Text = "Усі елементи сховані";
            FiltrationSideMenuButton.Text = "Фільтрація (ввімкнена)";
            FiltrationSideMenuButton.BackColor = Color.DarkGray;

            reworkedMap.HideAllLayout();
        }
        private void ShowAllLayoutButton_Click(object sender, EventArgs e)
        {
            FiltrationInfoStripStatusLabel.Text = "Фільтрація вимкнена";
            FiltrationSideMenuButton.Text = "Фільтрація";
            FiltrationSideMenuButton.BackColor = Control.DefaultBackColor;

            reworkedMap.ShowAllLayout();
        }
        #endregion

        private void AddItemTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAddItemButtons();
        }
        private void ResetAddItemButtons()
        {
            HelpStatusLabel.Text = "";

            markerAddingMode = false;
            polygonAddingMode = false;
            tubeAddingMode = false;

            MarkerSettingsButton.Enabled = false;
            SaveMarkerButton.Enabled = false;

            PolygonSettingsButton.Enabled = false;
            PolygonSaveButton.Enabled = false;
            PolygonColorPictureBox.Enabled = false;

            TubeSaveButton.Enabled = false;
            TubeNameTextBox.Enabled = false;
            TubeDescriptionTextBox.Enabled = false;
            TubeNameTextBox.Text = string.Empty;
            TubeDescriptionTextBox.Text = string.Empty;

            AddMarkerButton.Text = "Додати";
            PolygonDrawButton.Text = "Почати";
            TubeDrawButton.Text = "Почати";

            if (reworkedMap != null)
            {
                reworkedMap.RemoveLayout(addTempLayoutName);
                reworkedMap.CancelPolygonDraw();
                reworkedMap.CancelRouteDraw();

                drawContext = null;
            }

            if (itemConfigurationWindow != null)
            {
                itemConfigurationWindow.Dispose();
            }
            itemConfigurationWindow = null;

            ShowContextMenuStripItems(MapObjectContextMenuStrip);
        }

        #region Marker methods
        private void EconomicActivityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EconomicActivityComboBox.SelectedValue != null)
            {
                string imageName = EconomicActivityComboBox.SelectedValue.ToString();

                MarkerPictureBox.Image = LoadImage(imageName);

                if (markerAddingMode)
                {
                    var markers = reworkedMap.GetMarkersByLayoutOrNull(addTempLayoutName);

                    if (markers != null && markers.Count() != 0)
                    {
                        NamedGoogleMarker marker = (NamedGoogleMarker)markers.First();
                        reworkedMap.RemoveMarker(marker);
                        reworkedMap.AddMarker(marker.Position, (Bitmap)MarkerPictureBox.Image,
                                              addTempLayoutName, marker.Format, marker.Name, marker.Description);
                    }
                }

                //DebugToolStripStatusLabel.Text = imageName;
            }
        }
        private void AddMarkerButton_Click(object sender, EventArgs e)
        {
            HideContextMenuStripItems(MapObjectContextMenuStrip);
            markerAddingMode = !markerAddingMode;

            if (markerAddingMode)
            {
                HelpStatusLabel.Text = "Натисніть два рази на карту для встановлення маркеру.";

                MarkerSettingsButton.Enabled = true;
                SaveMarkerButton.Enabled = true;

                AddMarkerButton.Text = "Відміна";
            }
            else
            {
                ResetAddItemButtons();
            }

            reworkedMap.SelectedMarker = null;
        }
        private void MarkerSettingsButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                itemConfigurationWindow = new HelpWindows.ItemConfigurationWindow(expert);
            }
            itemConfigurationWindow.ShowDialog();
        }
        private async void SaveMarkerButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                MessageBox.Show("Не можливо зберегти об'єкт без його налаштування.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (objName, objDescription, series, emissions) = itemConfigurationWindow;

            if (string.IsNullOrEmpty(objName))
            {
                MessageBox.Show("Не можливо зберегти маркер без назви.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var markers = reworkedMap.GetMarkersByLayoutOrNull(addTempLayoutName);

            if (markers != null && markers.Count() != 0)
            {
                NamedGoogleMarker marker = (NamedGoogleMarker)markers.First();

                Data.Entity.TypeOfObject typeOfObject = EconomicActivityComboBox.SelectedItem as TypeOfObject;

                string[] columns = { "id_of_user", "Type", "owner_type", "Coord_Lat", "Coord_Lng",
                                     "Description", "Name_Object"};
                string[] values = { userId.ToString(), typeOfObject.Id.ToString(), OwnershipTypeComboBox.SelectedValue.ToString(),
                                    marker.Position.Lat.ToString().Replace(',', '.'), marker.Position.Lng.ToString().Replace(',', '.'),
                                    string.IsNullOrEmpty(objDescription) ? "'Опис відсутній.'" : Data.DBUtil.AddQuotes(objDescription),
                                    Data.DBUtil.AddQuotes(objName) };

                try
                {
                    dBManager.StartTransaction();
                    marker.Id = await dBManager.InsertToBDAsync("poi", columns, values);

                    if (emissions.Any())
                    {
                        StringBuilder emissionQuery = new StringBuilder("INSERT INTO emissions_on_map (idElement, idEnvironment, ValueAvg," +
                                                                        " ValueMax, Year, Month, day, Measure, idPoi) VALUES ");
                        var invarianCulture = System.Globalization.CultureInfo.InvariantCulture;

                        foreach (var emission in emissions)
                        {
                            emissionQuery.Append("( ");
                            emissionQuery.Append(emission.Element.Code.ToString() + ", ");
                            emissionQuery.Append(emission.Environment.Id.ToString() + ", ");
                            emissionQuery.Append(emission.AvgValue.ToString(invarianCulture) + ", ");
                            emissionQuery.Append(emission.MaxValue.ToString(invarianCulture) + ", ");
                            emissionQuery.Append(emission.Year.ToString() + ", ");
                            emissionQuery.Append(emission.Month.ToString() + ", ");
                            emissionQuery.Append(emission.Day.ToString() + ", ");
                            emissionQuery.Append(DBUtil.AddQuotes(emission.Element.Measure.ToString()) + ", ");
                            emissionQuery.Append(marker.Id.ToString());
                            emissionQuery.Append("), ");
                        }
                        emissionQuery.Remove(emissionQuery.Length - 2, 1);

                        await dBManager.InsertToBDAsync(emissionQuery.ToString());
                    }

                    if (series.Any())
                    {
                        StringBuilder mapObjectDependenciesQuery = new StringBuilder("INSERT INTO map_object_dependencies " +
                                                                                     "(id_of_object, type_obj, id_of_ref, type_rel) VALUES");

                        foreach (var keyValuePair in series)
                        {
                            mapObjectDependenciesQuery.Append("( ");
                            mapObjectDependenciesQuery.Append(marker.Id);
                            mapObjectDependenciesQuery.Append(", 0, ");
                            mapObjectDependenciesQuery.Append(keyValuePair.Key.Id);
                            mapObjectDependenciesQuery.Append(", 0 ");
                            mapObjectDependenciesQuery.Append("), ");

                            foreach (var _series in keyValuePair.Value)
                            {
                                mapObjectDependenciesQuery.Append("( ");
                                mapObjectDependenciesQuery.Append(marker.Id);
                                mapObjectDependenciesQuery.Append(", 0, ");
                                mapObjectDependenciesQuery.Append(_series.Id);
                                mapObjectDependenciesQuery.Append(", 1 ");
                                mapObjectDependenciesQuery.Append("), ");
                            }
                        }
                        mapObjectDependenciesQuery.Remove(mapObjectDependenciesQuery.Length - 2, 1);

                        await dBManager.InsertToBDAsync(mapObjectDependenciesQuery.ToString());
                    }

                    dBManager.CommitTransaction();

                    marker.Format = "Назва: {0}\nОпис: {1}";
                    marker.Name = objName;
                    marker.Description = (string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : objDescription);
                    marker.CreatorFullName = "Потрібне перезавантаження маркеру";
                    marker.CreatorRole = expert;
                    ((IDescribable)marker).Type = "Маркер";

                    reworkedMap.RemoveMarker(marker);               //Убирает маркер из слоя для добавления

                    foreach (var _series in series)
                    {
                        reworkedMap.AddMarker(marker, _series.Key.Name);
                    }
                    foreach (var _emission in emissions)
                    {
                        reworkedMap.AddMarker(marker, _emission.Environment.Name);
                    }

                    if (!(emissions.Any() && series.Any()))
                    {
                        reworkedMap.AddMarker(marker);
                    }

                    MessageBox.Show("Маркер успішно збережений до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetAddItemButtons();
                }
                catch (Exception ex)
                {
                    dBManager.RollbackTransaction();
#if DEBUG
                    DebugLog(ex);
#else 
                    MessageBox.Show("Не вдалось зберегти маркер до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Log(ex);
#endif
                }
            }
            else
            {
                MessageBox.Show("Немає встановленої точки на карті.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ShowAllMarkersButton_Click(object sender, EventArgs e)
        {
            await LoadMarkers();
        }
        private async void ShowAllExpertMarkerButton_Click(object sender, EventArgs e)
        {
            await LoadMarkers(condition: "user.id_of_expert = " + ((int)expert).ToString());
        }
        private async void ShowCurrentUserMarkerButton_Click(object sender, EventArgs e)
        {
            await LoadMarkers(condition: "user.id_of_user = " + userId.ToString());
        }
        private async Task LoadMarkers(string additionalTables = null, string additionalJoinCond = null, string condition = null)
        {
            CurrentActionStatusLabel.Text = "Завантаження усіх маркерів.";
            MainToolStripProgressBar.Visible = true;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

            string _tables = "poi, type_of_object, user";
            string _columns = "poi.Coord_Lat, poi.Coord_Lng, poi.Description, poi.Name_Object, " +
                              "type_of_object.Image_Name, user.description, user.id_of_expert, poi.Id, type_of_object.Name";
            string _joinCond = "type_of_object.Id = poi.Type, poi.id_of_user = user.id_of_user";
            string _condition = condition ?? string.Empty;
            string dependenciesTables = "poi, map_object_dependencies, issues";
            string dependenciesColumns = " poi.Id, issues.name";
            string dependenciesJoinCondition = "poi.Id = map_object_dependencies.id_of_object, " +
                                               "map_object_dependencies.type_rel = 0 AND " +
                                               "map_object_dependencies.type_obj = 0 AND " +
                                               "map_object_dependencies.id_of_ref = issues.issue_id";
            string dependenciesCondition = "type_obj = 0 ";
            string emissionTables = "poi, emissions_on_map, environment";
            string emissionColumns = "DISTINCT environment.name, poi.Id";
            string emissionJoinCondition = "emissions_on_map.idPoi = poi.Id, emissions_on_map.idEnvironment = environment.id";

            List<List<object>> result = null;
            List<List<object>> mapObjectDependencies = null;
            List<List<object>> emissions = null;

            reworkedMap.ClearAllMarkers();

            if (!string.IsNullOrEmpty(additionalTables))
            {
                _tables += ", " + additionalTables;
                dependenciesTables += ", " + additionalTables;
            }
            if (!string.IsNullOrEmpty(additionalJoinCond))
            {
                _joinCond += ", " + additionalJoinCond;
                dependenciesJoinCondition += ", " + additionalJoinCond;
            }

            try
            {
                result = await dBManager.GetRowsUsingJoinAsync(_tables, _columns, _joinCond, _condition, JoinType.LEFT);
                mapObjectDependencies = await dBManager.GetRowsUsingJoinAsync(dependenciesTables, dependenciesColumns,
                                                                              dependenciesJoinCondition, dependenciesCondition, new JoinType[] { JoinType.LEFT, JoinType.INNER });
                emissions = await dBManager.GetRowsUsingJoinAsync(emissionTables, emissionColumns,
                                                                  emissionJoinCondition, string.Empty, JoinType.LEFT);

                if (result.Count == 0)
                {
                    MessageBox.Show("Результат відсутній.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var row in result)
                {
                    NamedGoogleMarker marker = reworkedMap.AddMarker(new GMap.NET.PointLatLng((double)row[0], (double)row[1]),
                                                       LoadImage(row[4].ToString()),
                                                       "Назва: {0}\nОпис: {1}", row[3].ToString(), row[2].ToString()) as NamedGoogleMarker;
                    marker.Id = (int)row[7];
                    marker.CreatorFullName = row[5] is DBNull ? "Дані відсутні" : row[5].ToString();
                    marker.CreatorRole = (Role)((int)row[6]);
                    ((IDescribable)marker).Type = "Маркер";

                    var dependencies = mapObjectDependencies.Where(_row => !(_row[0] is DBNull) && (int)_row[0] == marker.Id);
                    foreach (var dependenciesRow in dependencies)
                    {
                        reworkedMap.AddMarker(marker, dependenciesRow[1].ToString());
                    }

                    var environment = emissions.Where(_row => !(_row[0] is DBNull) && (int)_row[1] == marker.Id);
                    foreach (var env in environment)
                    {
                        reworkedMap.AddMarker(marker, env[0].ToString());
                    }

                    bool emptyTypeOfObject = row[8] is DBNull;

                    if (!emptyTypeOfObject)
                    {
                        reworkedMap.AddMarker(marker, row[8].ToString());
                    }

                    if (!dependencies.Any() && !environment.Any() && emptyTypeOfObject)
                    {
                        reworkedMap.AddMarker(marker);
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                DebugLog(ex);
#else
                MessageBox.Show("Помилка при завантаженні усіх маркерів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
#endif
            }
            finally
            {

                CurrentActionStatusLabel.Text = string.Empty;
                MainToolStripProgressBar.Visible = false;
                MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Continuous;
            }
        }

        private void ClearAllMarkersButton_Click(object sender, EventArgs e)
        {
            reworkedMap.ClearAllMarkers();
        }
        private void DeleteMarker(object sender, EventArgs e)
        {
            if (reworkedMap.SelectedMarker != null && ((IDescribable)reworkedMap.SelectedMarker).Type == "Маркер")
            {
                if (MessageBox.Show($"Ви впевненні, що хочете видалити цей маркер?", "Увага!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                {
                    return;
                }

                var marker = (NamedGoogleMarker)reworkedMap.SelectedMarker;

                try
                {
                    dBManager.StartTransaction();
                    dBManager.DeleteFromDB("emissions_on_map", "idPoi", marker.Id.ToString());
                    dBManager.DeleteFromDB("map_object_dependencies",
                                           new string[] { "id_of_object", "type_obj" },
                                           new string[] { marker.Id.ToString(), "0" });
                    dBManager.DeleteFromDB("poi", "Id", marker.Id.ToString());
                    dBManager.CommitTransaction();

                    reworkedMap.RemoveMarker(reworkedMap.SelectedMarker);
                    itemInfo.ClearData();

                    MessageBox.Show("Видалення маркеру успішне.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dBManager.RollbackTransaction();
#if DEBUG
                    DebugLog(ex);
#else
                    MessageBox.Show("Сталась помилка при видаленні маркеру.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Log(ex);
#endif
                }
            }
        }
        #endregion

        private async Task SavePolyline(IList<GMap.NET.PointLatLng> points, Color fill, Color stroke, string polylineType)
        {
            if (string.IsNullOrEmpty(polylineType))
            {
                throw new ArgumentException("Тип фигуры должен быть явно указан.", "polylineType");
            }

            string _type = string.Empty;
            if (polylineType == "polygon")
            {
                _type = "Полігон";
            }
            else if (polylineType == "tube")
            {
                _type = "Трубопровід";
            }

            if (itemConfigurationWindow == null)
            {
                return;
            }

            var (objName, objDescription, series, emissions) = itemConfigurationWindow;

            string[] polygonColumns = { "Id_of_poligon", "brush_color_r", "bruch_color_g", "brush_color_b", "brush_alfa", "line_collor_r",
                                            "line_color_g", "line_color_b", "line_alfa", "line_thickness", "name", "id_of_user", "type", "description"};
            string[] polygonValues = { "0", fill.R.ToString(), fill.G.ToString(), fill.B.ToString(), fill.A.ToString(),
                                            stroke.R.ToString(), stroke.G.ToString(), stroke.B.ToString(), stroke.A.ToString(),
                                            "2", DBUtil.AddQuotes(objName), userId.ToString(), DBUtil.AddQuotes(polylineType),
                                            (string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : DBUtil.AddQuotes(objDescription))};

            try
            {
                object lastId = dBManager.GetValue("poligon", "MAX(Id_of_poligon)", "");

                if (lastId != null && !(lastId is DBNull))
                {
                    polygonValues[0] = ((int)lastId + 1).ToString();
                }

                dBManager.StartTransaction();
                await dBManager.InsertToBDAsync("poligon", polygonColumns, polygonValues);

                var _points = points;

                StringBuilder pointQuery = new StringBuilder("INSERT INTO point_poligon (longitude, latitude, Id_of_poligon, order123) VALUES ");

                for (int i = 0; i < _points.Count; i++)
                {
                    string tempLng = _points[i].Lng.ToString().Replace(',', '.');
                    string tempLat = _points[i].Lat.ToString().Replace(',', '.');
                    string tempOrderNumber = (i + 1).ToString();

                    pointQuery.AppendFormat(" ({0}, {1}, {2}, {3}), ", tempLat, tempLng, polygonValues[0], tempOrderNumber);
                }
                pointQuery.Remove(pointQuery.Length - 2, 1);

                await dBManager.InsertToBDAsync(pointQuery.ToString());

                if (emissions.Any())
                {
                    StringBuilder emissionQuery = new StringBuilder("INSERT INTO emissions_on_map (idElement, idEnvironment, ValueAvg," +
                                                                    " ValueMax, Year, Month, day, Measure, idPoligon) VALUES ");
                    var invarianCulture = System.Globalization.CultureInfo.InvariantCulture;

                    foreach (var emission in emissions)
                    {
                        emissionQuery.Append("( ");
                        emissionQuery.Append(emission.Element.Code.ToString() + ", ");
                        emissionQuery.Append(emission.Environment.Id.ToString() + ", ");
                        emissionQuery.Append(emission.AvgValue.ToString(invarianCulture) + ", ");
                        emissionQuery.Append(emission.MaxValue.ToString(invarianCulture) + ", ");
                        emissionQuery.Append(emission.Year.ToString() + ", ");
                        emissionQuery.Append(emission.Month.ToString() + ", ");
                        emissionQuery.Append(emission.Day.ToString() + ", ");
                        emissionQuery.Append(DBUtil.AddQuotes(emission.Element.Measure.ToString()) + ", ");
                        emissionQuery.Append(polygonValues[0]);
                        emissionQuery.Append("), ");
                    }
                    emissionQuery.Remove(emissionQuery.Length - 2, 1);

                    await dBManager.InsertToBDAsync(emissionQuery.ToString());
                }

                if (series.Any())
                {
                    StringBuilder mapObjectDependenciesQuery = new StringBuilder("INSERT INTO map_object_dependencies " +
                                                                                 "(id_of_object, type_obj, id_of_ref, type_rel) VALUES");

                    foreach (var keyValuePair in series)
                    {
                        mapObjectDependenciesQuery.Append("( ");
                        mapObjectDependenciesQuery.Append(polygonValues[0]);
                        mapObjectDependenciesQuery.Append(", 1, ");
                        mapObjectDependenciesQuery.Append(keyValuePair.Key.Id);
                        mapObjectDependenciesQuery.Append(", 0 ");
                        mapObjectDependenciesQuery.Append("), ");

                        foreach (var _series in keyValuePair.Value)
                        {
                            mapObjectDependenciesQuery.Append("( ");
                            mapObjectDependenciesQuery.Append(polygonValues[0]);
                            mapObjectDependenciesQuery.Append(", 1, ");
                            mapObjectDependenciesQuery.Append(_series.Id);
                            mapObjectDependenciesQuery.Append(", 1 ");
                            mapObjectDependenciesQuery.Append("), ");
                        }
                    }
                    mapObjectDependenciesQuery.Remove(mapObjectDependenciesQuery.Length - 2, 1);

                    await dBManager.InsertToBDAsync(mapObjectDependenciesQuery.ToString());
                }

                dBManager.CommitTransaction();

                var polyMarker = new NamedGoogleMarker(points[0],
                                                       GMap.NET.WindowsForms.Markers.GMarkerGoogleType.arrow,
                                                       "Назва: {0}\nОпис: {1}", objName,
                                                       (string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : objDescription));
                polyMarker.Id = ((int)lastId + 1);
                polyMarker.CreatorFullName = "Потрібне перезавантаження маркеру";
                polyMarker.CreatorRole = expert;
                polyMarker.IsDependent = true;

                if (polylineType == "polygon")
                {
                    ((IDescribable)polyMarker).Type = _type;
                }
                else if (polylineType == "tube")
                {
                    ((IDescribable)polyMarker).Type = _type;
                }

                Action<IDictionary<Issue, List<CalculationSeries>>, IEnumerable<Emission>, Map> action = (_issues, _emissions, rwMap) =>
                {
                    bool added = false;

                    if (_emissions != null)
                    {
                        foreach (var _emission in _emissions)
                        {
                            rwMap.AddMarker(polyMarker, _emission.Environment.Name);
                        }
                        added = true;
                    }
                    if (_issues != null)
                    {
                        foreach (var _issue in _issues)
                        {
                            rwMap.AddMarker(polyMarker, _issue.Key.Name);
                        }
                        added = true;
                    }

                    if (!added)
                    {
                        rwMap.AddMarker(polyMarker);
                    }
                };

                this.Invoke(action, null, null, reworkedMap);

                MessageBox.Show($"{_type} успішно збережений до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                dBManager.RollbackTransaction();
#if DEBUG
                DebugLog(ex);
#else
                MessageBox.Show($"Не вдалось зберегти {_type} до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
#endif
            }
        }
        private async Task LoadPolylines(string additionalTables = null, string additionalJoinCond = null,
                                         string additionalCondition = null, string polylineType = "polygon")
        {
            Action<string, bool, ProgressBarStyle> changeProgressBar = ChangeProgressBar;

            this.Invoke(changeProgressBar, "Завантаження усіх маркерів.", true, ProgressBarStyle.Marquee);

            string _tables = "poligon, emissions_on_map, user";
            string _columns = "poligon.Id_of_poligon, poligon.brush_color_r, " +
                              "poligon.bruch_color_g, poligon.brush_color_b, " +
                              "poligon.brush_alfa, poligon.line_collor_r, " +
                              "poligon.line_color_g, poligon.line_color_b, " +
                              "poligon.line_alfa, poligon.line_thickness, " +
                              "poligon.name, poligon.description, " +
                              "user.description, user.id_of_expert, emissions_on_map.idEnvironment";
            string _joinCond = "poligon.Id_of_poligon = emissions_on_map.idPoligon, poligon.id_of_user = user.id_of_user";
            string _condition = "poligon.type = " + DBUtil.AddQuotes(polylineType);

            string _polygonPointTables = "poligon, point_poligon";
            string _polygonPointColumns = "point_poligon.longitude, point_poligon.latitude, point_poligon.Id_of_poligon";
            string _polygonPointJoinConditions = "poligon.Id_of_poligon = point_poligon.Id_of_poligon";
            string _polygonPointCondition = " WHERE true ORDER BY `Id_of_poligon`, `order123`";
            string dependenciesTables = "poi, map_object_dependencies, issues";
            string dependenciesColumns = " poi.Id, issues.name";
            string dependenciesJoinCondition = "poi.Id = map_object_dependencies.id_of_object, " +
                                               "map_object_dependencies.type_rel = 0 AND " +
                                               "map_object_dependencies.type_obj = 0 AND " +
                                               "map_object_dependencies.id_of_ref = issues.issue_id";
            string dependenciesCondition = "type_obj = 0 ";
            string emissionTables = "poi, emissions_on_map, environment";
            string emissionColumns = "DISTINCT environment.name, poi.Id";
            string emissionJoinCondition = "emissions_on_map.idPoi = poi.Id, emissions_on_map.idEnvironment = environment.id";

            List<List<object>> result = null;
            List<IGrouping<int, List<object>>> pointsResult = null;
            List<List<object>> mapObjectDependencies = null;
            List<List<object>> emissions = null;

            GMap.NET.MapRoute figure = null;

            if (!string.IsNullOrEmpty(additionalCondition))
            {
                _condition += " AND " + additionalCondition;
            }
            if (!string.IsNullOrEmpty(additionalTables))
            {
                _tables += ", " + additionalTables;
            }
            if (!string.IsNullOrEmpty(additionalJoinCond))
            {
                _joinCond += ", " + additionalJoinCond;
            }

            if (polylineType == "polygon")
            {
                reworkedMap.ClearAllPolygons();
            }
            else if (polylineType == "tube")
            {
                reworkedMap.ClearAllRoutes();
                TubeCheckBox.Enabled = true;
            }

            string format = null;
            string _type = null;

            try
            {
                result = await dBManager.GetRowsUsingJoinAsync(_tables, _columns, _joinCond, _condition, JoinType.LEFT);
                pointsResult = (await dBManager.GetRowsUsingJoinAsync(_polygonPointTables,
                                                                      _polygonPointColumns,
                                                                      _polygonPointJoinConditions,
                                                                      _polygonPointCondition, JoinType.LEFT))
                                               .GroupBy(row => (row[2] is DBNull ? -1 : (int)row[2]))
                                               .ToList();
                mapObjectDependencies = await dBManager.GetRowsUsingJoinAsync(dependenciesTables, dependenciesColumns,
                                                                              dependenciesJoinCondition, dependenciesCondition, new JoinType[] { JoinType.LEFT, JoinType.INNER });
                emissions = await dBManager.GetRowsUsingJoinAsync(emissionTables, emissionColumns,
                                                                  emissionJoinCondition, string.Empty, JoinType.LEFT);


                if (result.Count == 0)
                {
                    MessageBox.Show("Результат відсутній.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var row in result)
                {
                    int polygonId = (int)row[0];

                    var _points = pointsResult.Where(group => group.Key == polygonId)
                                              .Select(group => group.Select(_row => new GMap.NET.PointLatLng((double)_row[0], (double)_row[1]))
                                                                    .ToList())
                                              .FirstOrDefault();

                    if (_points == null)
                    {
                        continue;
                    }

                    Color fill = Color.FromArgb(Convert.ToInt32(row[4]),
                                               Convert.ToInt32(row[1]),
                                               Convert.ToInt32(row[2]),
                                               Convert.ToInt32(row[3]));

                    Color stroke = Color.FromArgb(Convert.ToInt32(row[8]),
                                                  Convert.ToInt32(row[5]),
                                                  Convert.ToInt32(row[6]),
                                                  Convert.ToInt32(row[7]));

                    if (polylineType == "polygon" || polylineType == "region")
                    {
                        var polygon = new GMap.NET.WindowsForms.GMapPolygon(_points, row[10].ToString());
                        polygon.Fill = new SolidBrush(fill);
                        polygon.Stroke = new Pen(stroke, Convert.ToInt32(row[9]));
                        figure = polygon;

                        if (polylineType == "polygon")
                        {
                            format = "Назва полігону: {0}\nОпис полігону: {1}";
                            _type = "Полігон";
                        }
                        else
                        {
                            format = "Назва області: {0}\nОпис області: {1}";
                            _type = "Область";
                        }

                    }
                    else if (polylineType == "tube")
                    {
                        var route = new GMap.NET.WindowsForms.GMapRoute(_points, row[10].ToString());
                        route.Stroke = new Pen(stroke, Convert.ToInt32(row[9]));

                        format = "Назва полігону: {0}\nОпис полігону: {1}";
                        figure = route;

                        _type = "Трубопровід";
                    }

                    NamedGoogleMarker marker = reworkedMap.AddMarker(_points.First(), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.arrow,
                                                                     format, row[10].ToString(), row[11].ToString()) as NamedGoogleMarker;
                    marker.Id = polygonId;
                    marker.IsDependent = true;
                    marker.CreatorFullName = row[12] is DBNull ? "Дані відсутні" : row[14].ToString();
                    marker.CreatorRole = (Role)((int)row[13]);

                    ((IDescribable)marker).Type = _type;

                    bool isAdded = false;
                    string layout = string.Empty;

                    if (polylineType == "polygon")
                    {
                        if (!(row[14] is DBNull))
                        {
                            int envId = (int)row[14];
                            var env = EnvironmentCheckedListBox.Items.OfType<Data.Entity.Environment>().Where(_env => _env.Id == envId)
                                                                                                       .FirstOrDefault();

                            if (env != null)
                            {
                                layout = env.Name;
                                reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, layout);
                                reworkedMap.AddMarker(marker, layout);

                                isAdded = true;
                            }
                        }

                        var dependencies = mapObjectDependencies.Where(_row => !(_row[0] is DBNull) && (int)_row[0] == marker.Id);
                        foreach (var dependenciesRow in dependencies)
                        {
                            var tempDependecies = dependenciesRow[1].ToString();

                            reworkedMap.AddMarker(marker, tempDependecies);
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, tempDependecies);
                        }

                        var environment = emissions.Where(_row => !(_row[0] is DBNull) && (int)_row[1] == marker.Id);
                        foreach (var env in environment)
                        {
                            var tempEnvironment = env[0].ToString();

                            reworkedMap.AddMarker(marker, tempEnvironment);
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, tempEnvironment);
                        }

                        if (!dependencies.Any() && !environment.Any())
                        {
                            reworkedMap.AddMarker(marker);
                        }
                    }
                    else
                    {
                        if (polylineType == "region")
                        {
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, polylineType);
                        }
                        else
                        {
                            reworkedMap.AddRoute((GMap.NET.WindowsForms.GMapRoute)figure, polylineType);
                        }

                        reworkedMap.AddMarker(marker, polylineType);

                        isAdded = true;
                    }

                    if (!isAdded)
                    {
                        if (polylineType == "polygon")
                        {
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure);
                        }
                    }
                    else
                    {
                        reworkedMap.RemoveMarkerFromLayout(marker);
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                DebugLog(ex);
#else
                MessageBox.Show($"Помилка при завантаженні усіх {_type}.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
#endif
            }
            finally
            {

                this.Invoke(changeProgressBar, string.Empty, false, ProgressBarStyle.Continuous);
            }
        }
        private async void DeletePolyline(object sender, EventArgs e)
        {
            if (reworkedMap.SelectedMarker != null)
            {
                string _type = ((IDescribable)reworkedMap.SelectedMarker).Type;

                if (MessageBox.Show($"Ви впевненні, що хочете видалити {_type}?", "Увага!", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                {
                    return;
                }

                CurrentActionStatusLabel.Text = "Видалення об'єкту.";
                MainToolStripProgressBar.Visible = true;
                MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

                NamedGoogleMarker polyMarker = (NamedGoogleMarker)reworkedMap.SelectedMarker;

                try
                {
                    dBManager.StartTransaction();
                    await dBManager.DeleteFromDBAsync("map_object_dependencies",
                                                       new string[] { "id_of_object", "type_obj" },
                                                       new string[] { polyMarker.Id.ToString(), "1" });
                    await dBManager.DeleteFromDBAsync("point_poligon", "Id_of_poligon", polyMarker.Id.ToString());
                    await dBManager.DeleteFromDBAsync("emissions_on_map", "idPoligon", polyMarker.Id.ToString());
                    await dBManager.DeleteFromDBAsync("poligon", "Id_of_poligon", polyMarker.Id.ToString());
                    dBManager.CommitTransaction();

                    if (_type == "Полігон")
                    {
                        reworkedMap.RemovePolygon(polyMarker.Name);
                    }
                    else if (_type == "Трубопровід")
                    {
                        reworkedMap.RemoveRoute(polyMarker.Name);
                    }

                    reworkedMap.RemoveMarker(polyMarker);
                    itemInfo.ClearData();

                    MessageBox.Show("Видалення об'єкту успішне.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dBManager.RollbackTransaction();
#if DEBUG
                    DebugLog(ex);
#else 
                    MessageBox.Show("Сталась помилка при видаленні об'єкта.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Log(ex);
#endif
                }
                finally
                {
                    CurrentActionStatusLabel.Text = string.Empty;
                    MainToolStripProgressBar.Visible = false;
                    MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Continuous;
                }
            }
        }

        #region Polygon methods
        private void ChangePictureBoxColor()
        {
            if (drawContext == null)
            {
                return;
            }

            switch (PolygonColorTypeComboBox.SelectedIndex)
            {
                case 0:
                    PolygonColorPictureBox.BackColor = ((PolygonContext)drawContext).GetFill();
                    break;
                case 1:
                    PolygonColorPictureBox.BackColor = ((PolygonContext)drawContext).GetStroke();
                    break;
                default:
                    break;
            }

            gMapControl.Refresh();
        }

        private void PolygonColorPictureBox_Click(object sender, EventArgs e)
        {
            if (!polygonAddingMode && drawContext != null)
            {
                MessageBox.Show("Не можливо змінити колір.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                switch (PolygonColorTypeComboBox.SelectedIndex)
                {
                    case 0:
                        ((PolygonContext)drawContext).SetFill(Color.FromArgb((int)TransparentNumericUpDown.Value, colorDialog.Color));
                        break;
                    case 1:
                        ((PolygonContext)drawContext).SetStroke(Color.FromArgb((int)TransparentNumericUpDown.Value, colorDialog.Color));
                        break;
                    default:
                        break;
                }

                PolygonColorPictureBox.BackColor = Color.FromArgb((int)TransparentNumericUpDown.Value, colorDialog.Color);
            }

            gMapControl.Refresh();

            colorDialog.Dispose();
        }
        private void PolygonColorTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangePictureBoxColor();
        }

        private void PolygonDrawButton_Click(object sender, EventArgs e)
        {
            HideContextMenuStripItems(MapObjectContextMenuStrip);
            polygonAddingMode = !polygonAddingMode;

            if (polygonAddingMode)
            {
                HelpStatusLabel.Text = "Натискайте два рази на карту для встановлення точки полігону.";

                PolygonSaveButton.Enabled = true;
                PolygonSettingsButton.Enabled = true;
                PolygonColorPictureBox.Enabled = true;

                PolygonDrawButton.Text = "Відміна";

                drawContext = reworkedMap.StartDrawPolygon(Color.Red, 100, "__newPolygon__", addTempLayoutName);
                ChangePictureBoxColor();
                TransparentNumericUpDown.Value = 100;
            }
            else
            {
                ResetAddItemButtons();
            }
        }
        private void PolygonSettingsButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                itemConfigurationWindow = new HelpWindows.ItemConfigurationWindow(expert);
            }
            itemConfigurationWindow.ShowDialog();

            string tempObjName = itemConfigurationWindow.GetObjName();
            if (drawContext != null && !string.IsNullOrEmpty(tempObjName))
            {
                drawContext.SetFigureName(itemConfigurationWindow.GetObjName());
            }
        }

        private void TransparentNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (drawContext != null)
            {
                if (PolygonColorTypeComboBox.SelectedIndex != 0)
                {
                    PolygonColorTypeComboBox.SelectedIndex = 0;
                }

                ((PolygonContext)drawContext).SetOpacity((int)TransparentNumericUpDown.Value);
                ChangePictureBoxColor();
            }
        }

        private async void PolygonSaveButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                MessageBox.Show("Не можливо зберегти об'єкт без його налаштування.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (objName, _, series, emissions) = itemConfigurationWindow;

            if (string.IsNullOrEmpty(objName))
            {
                MessageBox.Show("Не можливо зберегти полігон без назви.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var polygon = reworkedMap.GetPolygonByNameOrNull(objName);

            if (polygon != null)
            {
                Color polyFill = ((SolidBrush)polygon.Fill).Color;
                Color polyStroke = polygon.Stroke.Color;

                await SavePolyline(polygon.Points, polyFill, polyStroke, "polygon");

                reworkedMap.EndPolygonDraw();
                drawContext = null;

                var newPolygon = new GMap.NET.WindowsForms.GMapPolygon(polygon.Points, polygon.Name);
                newPolygon.Fill = polygon.Fill;
                newPolygon.Stroke = polygon.Stroke;

                reworkedMap.RemovePolygon(polygon);
                reworkedMap.RemoveLayout(addTempLayoutName);

                foreach (var _series in series)
                {
                    reworkedMap.AddPolygon(newPolygon, _series.Key.Name);
                }

                foreach (var emission in emissions)
                {
                    reworkedMap.AddPolygon(newPolygon, emission.Environment.Name);
                }

                if (!series.Any() && !emissions.Any())
                {
                    reworkedMap.AddPolygon(newPolygon);
                }

                if (itemConfigurationWindow != null)
                {
                    itemConfigurationWindow.Dispose();
                }
                itemConfigurationWindow = null;

                polygonAddingMode = false;

                PolygonDrawButton.Text = "Почати";

                PolygonSettingsButton.Enabled = false;
                PolygonSaveButton.Enabled = false;
                PolygonColorPictureBox.Enabled = false;
            }
            else
            {
                MessageBox.Show("Немає намальованого полігона на карті або відсутня його назва.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ShowAllPolygonsButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines();
        }
        private async void ShowCurrentUserPolygonsButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines(additionalCondition: "poligon.id_of_user = " + userId.ToString());
        }
        private async void ShowAllExpertPolygonsButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines(additionalCondition: "user.id_of_expert = " + ((int)expert).ToString());
        }

        private void ClearAllPolygons_Click(object sender, EventArgs e)
        {
            foreach (var overlay in gMapControl.Overlays)
            {
                foreach (var polygon in overlay.Polygons)
                {
                    DeletePolygonMarker(overlay, polygon.Points.First());
                }
            }

            void DeletePolygonMarker(GMap.NET.WindowsForms.GMapOverlay overlay, GMap.NET.PointLatLng coord)
            {
                GMap.NET.WindowsForms.GMapMarker marker = null;

                foreach (var _marker in overlay.Markers)
                {
                    if (_marker.Position == coord)
                    {
                        marker = _marker;
                    }
                }

                overlay.Markers.Remove(marker);
            }

            reworkedMap.ClearAllPolygons();
        }
        #endregion

        #region Tube methods
        private void TubeDrawButton_Click(object sender, EventArgs e)
        {
            HideContextMenuStripItems(MapObjectContextMenuStrip);
            tubeAddingMode = !tubeAddingMode;

            if (tubeAddingMode)
            {
                HelpStatusLabel.Text = "Натискайте два рази на карту для встановлення точки трубопроводу.";

                TubeSaveButton.Enabled = true;
                TubeNameTextBox.Enabled = true;
                TubeDescriptionTextBox.Enabled = true;

                TubeDrawButton.Text = "Відміна";
                itemConfigurationWindow = new HelpWindows.ItemConfigurationWindow(expert);

                drawContext = reworkedMap.StartDrawRoute(Color.Blue, "__newRoute__", addTempLayoutName, GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_dot);
            }
            else
            {
                ResetAddItemButtons();
            }
        }

        private async void TubeSaveButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                MessageBox.Show("Не можливо зберегти об'єкт без його налаштування.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            itemConfigurationWindow.SetObjName(TubeNameTextBox.Text);
            itemConfigurationWindow.SetObjDescritpion(TubeDescriptionTextBox.Text);
            string objName = itemConfigurationWindow.GetObjName();

            if (string.IsNullOrEmpty(objName))
            {
                MessageBox.Show("Не можливо зберегти трубопровід без назви.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var route = reworkedMap.GetRouteByNameOrNull(objName);

            if (route != null)
            {
                Color polyStroke = route.Stroke.Color;

                await SavePolyline(route.Points, Color.Transparent, polyStroke, "tube");

                reworkedMap.EndRouteDraw();
                drawContext = null;

                var newRoute = new GMap.NET.WindowsForms.GMapRoute(route.Points, route.Name);
                newRoute.Stroke = route.Stroke;

                reworkedMap.RemoveRoute(route);
                reworkedMap.RemoveLayout(addTempLayoutName);

                reworkedMap.AddRoute(newRoute, "tube");

                if (itemConfigurationWindow != null)
                {
                    itemConfigurationWindow.Dispose();
                }
                itemConfigurationWindow = null;

                tubeAddingMode = false;

                TubeDrawButton.Text = "Почати";
                TubeSaveButton.Enabled = false;
                TubeNameTextBox.Enabled = false;
                TubeDescriptionTextBox.Enabled = false;
                TubeNameTextBox.Text = string.Empty;
                TubeDescriptionTextBox.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Немає намальованого трубопроводу на карті або відсутня його назва.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ShowAllTubesButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines(polylineType: "tube");
        }
        private async void ShowCurrentUserTubesButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines(additionalCondition: "poligon.id_of_user = " + userId.ToString(), polylineType: "tube");
        }
        private async void ShowCurrentExpertTubesButton_Click(object sender, EventArgs e)
        {
            await LoadPolylines(additionalCondition: "user.id_of_expert = " + ((int)expert).ToString(), polylineType: "tube");
        }

        private void ClearAllTubesButton_Click(object sender, EventArgs e)
        {
            TubeCheckBox.Checked = false;
            TubeCheckBox.Enabled = false;

            foreach (var overlay in gMapControl.Overlays)
            {
                foreach (var route in overlay.Routes)
                {
                    DeleteRouteMarker(overlay, route.Points.First());
                }
            }

            void DeleteRouteMarker(GMap.NET.WindowsForms.GMapOverlay overlay, GMap.NET.PointLatLng coord)
            {
                GMap.NET.WindowsForms.GMapMarker marker = null;

                foreach (var _marker in overlay.Markers)
                {
                    if (_marker.Position == coord)
                    {
                        marker = _marker;
                    }
                }

                overlay.Markers.Remove(marker);
            }

            reworkedMap.ClearAllRoutes();
        }
        #endregion

        #region Comprasion methods
        private void ComparsionSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reworkedMap.SelectedMarker != null)
            {
                NamedGoogleMarker marker = (NamedGoogleMarker)reworkedMap.SelectedMarker;

                if (!ComprasionItemsComboBox.Items.Contains(marker))
                {
                    ComprasionItemsComboBox.Items.Add(marker);
                }

                ComprasionItemsComboBox.SelectedIndex = ComprasionItemsComboBox.Items.Count - 1;
            }
        }
        private void DeleteCompareItemButton_Click(object sender, EventArgs e)
        {
            if (ComprasionItemsComboBox.SelectedIndex != -1)
            {
                ComprasionItemsComboBox.Items.RemoveAt(ComprasionItemsComboBox.SelectedIndex);
            }
        }
        private void CompareButton_Click(object sender, EventArgs e)
        {
            if (ComprasionItemsComboBox.Items.Count == 0)
            {
                MessageBox.Show("Об'єкти для порівння відсутні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CompareSettings compareSettings = new CompareSettings(ComprasionItemsComboBox.Items.OfType<IDescribable>().ToList());
            compareSettings.ShowDialog();
        }

        private void ComprasionItemsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            NamedGoogleMarker marker = e.ListItem as NamedGoogleMarker;

            if (marker != null)
            {
                e.Value = $"{marker.Name} ({((IDescribable)marker).Type})";
            }
        }
        #endregion

        private async void RegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string region = "region";

            if (!reworkedMap.LayoutExist(region))
            {
                await LoadPolylines(polylineType: region);
                reworkedMap.HideLayoutById(region);
            }
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = sender as ComboBox;

            if (e.Index < 0 || comboBox == null)
            {
                return;
            }
            string text = comboBox.GetItemText(comboBox.Items[e.Index]);

            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, br, e.Bounds);
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && text.Length * e.Font.SizeInPoints > e.Bounds.Width)
            {

                ComboBoxTextToolTip.Show(text, comboBox, e.Bounds.Left + comboBox.Width - 10, e.Bounds.Top + 4);
            }
            else
            {
                ComboBoxTextToolTip.Hide(comboBox);
            }

            e.DrawFocusRectangle();
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            HideComboBoxToolTip(sender as ComboBox);
        }
        private void ComboBox_MouseLeave(object sender, EventArgs e)
        {
            HideComboBoxToolTip(sender as ComboBox);
        }

        private void HideComboBoxToolTip(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                ComboBoxTextToolTip.Hide(comboBox);
            }
        }
    }
}