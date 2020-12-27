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
using Maps.Core;

namespace Maps
{
    public partial class MapWindow : Form
    {
        private static readonly string defaultImageName;
        private static readonly string addTempLayoutName;

        private readonly GMap.NET.PointLatLng defaultMapStartCoord;
        private readonly Role expert;
        private readonly int userId;

        private bool isLoading;
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

        private object _lock;

        static MapWindow()
        {
            defaultImageName = "missingImage";
            addTempLayoutName = "__AddTempLayout__";
        }

        public MapWindow(Role expert, int userId)
        {
            defaultMapStartCoord = new GMap.NET.PointLatLng(50.449173, 30.457578);

            _lock = new object();

            this.userId = userId;
            this.expert = expert;
            this.moveMode = false;

            InitializeComponent();
            InitialiazeAdditionalComponent();

            reworkedMap = new Map(gMapControl);
            dBManager = new DBManager();

            itemConfigurationWindow = null;
            drawContext = null;

            LoadCommonThings();
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
            itemInfo.SubscribeAdditionalInfoClickEvent(AdditionalInfo);

            if (expert != Role.Admin)
            {
                itemInfo.HideDeleteButton();
            }

            this.Controls.Add(itemInfo);
            this.Controls.SetChildIndex(itemInfo, 0);

            this.Height = 450;

            PolygonColorTypeComboBox.SelectedIndex = 0;

            Binding envCheckBinding = new Binding("Enabled", EnvironmentsCheckBox, "Checked");
            Binding issuesCheckBinding = new Binding("Enabled", IssuesCheckBox, "Checked");

            EnvironmentsGroupBox.DataBindings.Add(envCheckBinding);
            IssuesGroupBox.DataBindings.Add(issuesCheckBinding);
        }
        private async void LoadCommonThings()
        {
            CurrentActionStatusLabel.Text = "Йде завантаження головних даних.";
            MainToolStripProgressBar.Visible = true;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

            await FillCheckBoxFromBDAsync(CitiesComboBox, "cities", "*", "",
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
                                              CitiesComboBox.Text = "Не вдалось завантажити міста.";
                                              GoToCityButton.Enabled = false;
                                          });

            await FillCheckBoxFromBDAsync(EnvironmentComboBox, "environment", "*", "",
                                          r => EnvironmentMapper.Map(r),
                                          displayComboBoxMember: "Name",
                                          valueComboBoxMember: "Id",
                                          falultAction: c =>
                                          {
                                              c.DataSource = new List<Data.Entity.Environment>
                                              {
                                                  new Data.Entity.Environment { Id = -1, Name = "Зобразити усі" }
                                              };
                                          });

            await FillCheckBoxFromBDAsync(IssuesComboBox, "issues", "issue_id, name", "",
                                          r =>
                                          {
                                              int id;
                                              int.TryParse(r[0].ToString(), out id);

                                              return new
                                              {
                                                  Id = id,
                                                  Name = r[1].ToString()
                                              };
                                          },
                                          displayComboBoxMember: "Name",
                                          valueComboBoxMember: "Id",
                                          falultAction: c => c.Items.Add("Не вдалось завантажити задачі."));

            await FillCheckBoxFromBDAsync(OwnershipTypeComboBox, "owner_types", "*", "",
                                          r =>
                                          {
                                              int id;
                                              int.TryParse(r[0].ToString(), out id);

                                              return new
                                              {
                                                  Id = id,
                                                  Type = r[1].ToString()
                                              };
                                          },
                                          displayComboBoxMember: "Type",
                                          valueComboBoxMember: "Id",
                                          falultAction: c => c.Items.Add("Не вдалось завантажити типи власності"));

            await FillCheckBoxFromBDAsync(EconomicActivityComboBox, "type_of_object", "Id, Name, Image_Name", "",
                                          r =>
                                          {
                                              int id;
                                              int.TryParse(r[0].ToString(), out id);

                                              return new Data.Entity.TypeOfObject
                                              {
                                                  Id = id,
                                                  Name = r[1].ToString(),
                                                  ImageName = r[2].ToString()
                                              };
                                          },
                                          displayComboBoxMember: "Name",
                                          valueComboBoxMember: "ImageName",
                                          falultAction: c => c.Items.Add("Не вдалось завантажити вид економічної діяльності"));

            EconomicActivityComboBox.SelectedIndex = -1;
            EconomicActivityComboBox.SelectedIndex = 0;

            CurrentActionStatusLabel.Text = string.Empty;
            MainToolStripProgressBar.Visible = false;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Continuous;
        }

        private Task FillCheckBoxFromBDAsync<TResult>(ComboBox comboBox, string table, string columns,
                                                      string condition, Func<List<object>, TResult> func,
                                                      string displayComboBoxMember = null,
                                                      string valueComboBoxMember = null,
                                                      Action<ComboBox> falultAction = null)
        {
            if (comboBox == null || string.IsNullOrEmpty(table) || columns == null ||
                condition == null || func == null)
            {
                return null;
            }

            Action<ComboBox, string, string, Action<ComboBox>, List<TResult>> syncAction = SyncAction;

            comboBox.Items.Add("Йде завантаження...");
            comboBox.SelectedIndex = 0;

            try
            {
                return Task.Run(() =>
                {
                    lock (_lock)
                    {
                        isLoading = true;
                    }

                    var result = dBManager.GetRows(table, columns, condition)
                                          .Select(func)
                                          .ToList();

                    comboBox.Invoke(syncAction, comboBox, displayComboBoxMember, valueComboBoxMember, falultAction, result);
                });
            }
            catch (Exception ex)
            {
                if (falultAction != null)
                {
                    falultAction(comboBox);
                }
#if DEBUG
                DebugLog(ex);
#endif
                return null;
            }
            finally
            {
                isLoading = false;
            }
        }

        private Bitmap LoadImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return null;
            }

            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory,
                                                 @"Resources\Images",
                                                 imageName);
            Bitmap img = null;

            if (System.IO.File.Exists(path))
            {
                if (Helpers.ImageCache.ContainsKey(imageName))
                {
                    img = Helpers.ImageCache.GetImage(imageName);
                }
                else
                {
                    img = (Bitmap)Image.FromFile(path);
                    Helpers.ImageCache.Add(imageName, img);
                }
            }
            else
            {
                if (!Helpers.ImageCache.ContainsKey(defaultImageName))
                {
                    Helpers.ImageCache.Add(defaultImageName,
                                           (Bitmap)Image.FromFile(
                                               System.IO.Path.Combine(System.Environment.CurrentDirectory,
                                                                      @"Resources\Images\noimage.png")
                                               )
                                           );
                }

                img = Helpers.ImageCache.GetImage(defaultImageName);
            }

            return img;
        }

        private void SyncAction<TResult>(ComboBox comboBox, string displayComboBoxMember,
                                         string valueComboBoxMember, Action<ComboBox> falultAction,
                                         List<TResult> result)
        {
            comboBox.Items.Clear();

            if (result.Count != 0)
            {
                comboBox.DataSource = result;
                comboBox.DisplayMember = displayComboBoxMember;
                comboBox.ValueMember = valueComboBoxMember;
            }
            else if (result != null)
            {
                falultAction(comboBox);
            }
        }

#if DEBUG
        private void DebugLog(Exception ex)
        {
            MessageBox.Show($"Message:\n{ex.Message}\n\nStack trace: \n{ex.StackTrace}",
                            "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
#endif

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

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!polygonAddingMode && !tubeAddingMode)
            {
                if (expert == Role.Admin)
                {
                    IDescribable describableItem = reworkedMap.SelectedMarker as IDescribable;

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
            }
            else if (drawContext != null)
            {
                drawContext.RemoveCoord(reworkedMap.SelectedMarker.Position);
            }
        }

        private void AdditionalInfo(object sender, EventArgs e)
        {
            var marker = reworkedMap.SelectedMarker;

            if (marker != null)
            {
                var emissionsForm = new OldMap.EmissionsForm(reworkedMap.SelectedMarker, (int)expert);
                Int32 indexx = marker.ToolTipText.IndexOf("Опис");
                emissionsForm.Text = marker.ToolTipText.Remove(indexx);
                emissionsForm.ShowDialog();
                emissionsForm.FormClosed += EmissionsForm_FormClosed;
            }
        }

        private void EmissionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var disposableObj = sender as IDisposable;
            if (disposableObj != null)
            {
                disposableObj.Dispose();
            }
        }

        #region Map methods
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
                else
                {
                    MapObjectContextMenuStrip.Items[MapObjectContextMenuStrip.Items.Count - 1].Visible = false;
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
                    var marker = reworkedMap.GetMarkerByCoordsOrNull(e.Location);

                    if (marker != null && reworkedMap.SelectedMarker != null && !object.ReferenceEquals(marker, reworkedMap.SelectedMarker))
                    {
                        reworkedMap.SelectedMarker.Position = new GMap.NET.PointLatLng(marker.Position.Lat - 0.008, marker.Position.Lng - 0.008);
                    }
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
            reworkedMap.HideAllLayout();

            if (EnvironmentsCheckBox.Checked)
            {
                if (EnvironmentComboBox.Text == "Зобразити усі")
                {
                    if (EnvironmentComboBox.DataSource is IList<Data.Entity.Environment>)
                    {
                        foreach (var environment in (IList<Data.Entity.Environment>)EnvironmentComboBox.DataSource)
                        {
                            if (environment.Id != -1)
                            {
                                reworkedMap.ShowLayoutById(environment.Name);
                            }
                        }
                    }
                }
                else
                {
                    reworkedMap.ShowLayoutById(EnvironmentComboBox.Text);
                }
            }

            if (IssuesCheckBox.Checked)
            {
                reworkedMap.ShowLayoutById(IssuesComboBox.Text);
            }
        }

        private void HideAllLayoutButton_Click(object sender, EventArgs e)
        {
            reworkedMap.HideAllLayout();
        }
        private void ShowAllLayoutButton_Click(object sender, EventArgs e)
        {
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
            TubeSettingsButton.Enabled = false;

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

                DebugToolStripStatusLabel.Text = imageName;
            }
        }

        private void AddMarkerButton_Click(object sender, EventArgs e)
        {
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
        private void SaveMarkerButton_Click(object sender, EventArgs e)
        {
            if (itemConfigurationWindow == null)
            {
                MessageBox.Show("Не можливо зберегти об'єкт без його налаштування.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var markers = reworkedMap.GetMarkersByLayoutOrNull(addTempLayoutName);

            if (markers != null && markers.Count() != 0)
            {
                NamedGoogleMarker marker = (NamedGoogleMarker)markers.First();

                var (issue, environment, series, objName, objDescription) = itemConfigurationWindow;

                Data.Entity.TypeOfObject typeOfObject = EconomicActivityComboBox.SelectedItem as TypeOfObject;

                string[] columns = { "id_of_user", "Type", "owner_type", "Coord_Lat", "Coord_Lng",
                                     "Description", "Name_Object", "id_of_issue", "calculations_description_number", "idEnvironment"};
                string[] values = { userId.ToString(), typeOfObject.Id.ToString(), OwnershipTypeComboBox.SelectedValue.ToString(),
                                    marker.Position.Lat.ToString().Replace(',', '.'), marker.Position.Lng.ToString().Replace(',', '.'),
                                    string.IsNullOrEmpty(objDescription) ? "'Опис відсутній.'" : Data.DBUtil.AddQuotes(objDescription),
                                    Data.DBUtil.AddQuotes(objName), issue != null ? issue.id.ToString() : "0",
                                    series != null ? series.id.ToString() : "0",
                                    environment != null ? environment.Id.ToString() : "NULL"};

                try
                {
                    marker.Id = dBManager.InsertToBD("poi", columns, values);

                    marker.Format = "Назва: {0}\nОпис: {1}";
                    marker.Name = objName;
                    marker.Description = (string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : objDescription);
                    marker.CreatorFullName = "Потрібне перезавантаження маркеру";
                    marker.CreatorRole = expert;
                    ((IDescribable)marker).Type = "Маркер";

                    reworkedMap.RemoveMarker(marker);               //Убирает маркер из слоя для добавления

                    if (issue != null)
                    {
                        reworkedMap.AddMarker(marker, issue.name);
                    }
                    if (environment != null)
                    {
                        reworkedMap.AddMarker(marker, environment.Name);
                    }

                    if (issue == null && environment == null)
                    {
                        reworkedMap.AddMarker(marker);
                    }

                    MessageBox.Show("Маркер успішно збережений до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetAddItemButtons();
                }
                catch (Exception ex)
                {
#if DEBUG
                    DebugLog(ex);
#else 
                    MessageBox.Show("Не вдалось зберегти маркер до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                }
            }
            else
            {
                MessageBox.Show("Немає встановленої точки на карті.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAllMarkersButton_Click(object sender, EventArgs e)
        {
            LoadMarkers();
        }
        private void ShowAllExpertMarkerButton_Click(object sender, EventArgs e)
        {
            LoadMarkers(condition: "user.id_of_expert = " + ((int)expert).ToString());
        }
        private void ShowCurrentUserMarkerButton_Click(object sender, EventArgs e)
        {
            LoadMarkers(condition: "user.id_of_user = " + userId.ToString());
        }
        private async void LoadMarkers(string additionalTables = null, string additionalJoinCond = null, string condition = null)
        {
            CurrentActionStatusLabel.Text = "Завантаження усіх маркерів.";
            MainToolStripProgressBar.Visible = true;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

            string _tables = "poi, type_of_object, issues, environment, user";
            string _columns = "poi.Coord_Lat, poi.Coord_Lng, poi.Description, poi.Name_Object, " +
                              "type_of_object.Image_Name, issues.name, environment.name, user.description, user.id_of_expert, poi.Id";
            string _joinCond = "type_of_object.Id = poi.Type, poi.id_of_issue = issues.issue_id, " +
                               "poi.idEnvironment = environment.id, poi.id_of_user = user.id_of_user";
            string _condition = condition ?? string.Empty;
            List<List<object>> result = null;

            reworkedMap.ClearAllMarkers();

            if (!string.IsNullOrEmpty(additionalTables))
            {
                _tables += ", " + additionalTables;
            }
            if (!string.IsNullOrEmpty(additionalJoinCond))
            {
                _joinCond += ", " + additionalJoinCond;
            }

            try
            {
                result = await Task.Run(() =>
                {
                    lock (_lock)
                    {
                        isLoading = true;
                    }

                    return dBManager.GetRowsUsingJoin(_tables, _columns, _joinCond, _condition, JoinType.LEFT);
                });

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
                    marker.Id = (int)row[9];
                    marker.CreatorFullName = row[7] is DBNull ? "Дані відсутні" : row[7].ToString();
                    marker.CreatorRole = (Role)((int)row[8]);
                    ((IDescribable)marker).Type = "Маркер";


                    bool isAdded = false;

                    if (!(row[5] is DBNull))
                    {
                        reworkedMap.AddMarker(marker, row[5].ToString());
                        isAdded = true;
                    }
                    if (!(row[6] is DBNull))
                    {
                        reworkedMap.AddMarker(marker, row[6].ToString());
                        isAdded = true;
                    }

                    if (isAdded)
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
                MessageBox.Show("Помилка при завантаженні усіх маркерів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            finally
            {
                isLoading = false;

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

                try
                {
                    dBManager.DeleteFromDB("poi", "Id", ((NamedGoogleMarker)reworkedMap.SelectedMarker).Id.ToString());

                    reworkedMap.RemoveMarker(reworkedMap.SelectedMarker);
                    itemInfo.ClearData();

                    MessageBox.Show("Видалення маркеру успішне.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
#if DEBUG
                    DebugLog(ex);
#else
                MessageBox.Show("Сталась помилка при видаленні маркеру.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
                }
            }
        }
        #endregion

        private void SavePolyline(IList<GMap.NET.PointLatLng> points, Color fill, Color stroke, string polylineType)
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

            var (issue, _, series, objName, objDescription) = itemConfigurationWindow;

            string[] polygonColumns = { "Id_of_poligon", "brush_color_r", "bruch_color_g", "brush_color_b", "brush_alfa", "line_collor_r",
                                            "line_color_g", "line_color_b", "line_alfa", "line_thickness", "name", "id_of_user", "type", "description"};
            string[] polygonValues = { "0", fill.R.ToString(), fill.G.ToString(), fill.B.ToString(), fill.A.ToString(),
                                            stroke.R.ToString(), stroke.G.ToString(), stroke.B.ToString(), stroke.A.ToString(),
                                            "2", DBUtil.AddQuotes(objName), userId.ToString(), DBUtil.AddQuotes(polylineType),
                                            (string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : DBUtil.AddQuotes(objDescription))};

            string[] polygonDescColumns = { "id_poligon", "id_of_issue", "calculations_description_number" };
            string[] polygonDescValues = { polygonValues[0], issue != null ? issue.id.ToString() : "0",
                                               series != null ? series.id.ToString() : "0"};

            try
            {
                object lastId = dBManager.GetValue("poligon", "MAX(Id_of_poligon)", "");

                if (lastId != null && !(lastId is DBNull))
                {
                    polygonValues[0] = ((int)lastId + 1).ToString();
                    polygonDescValues[0] = polygonValues[0];
                }

                dBManager.StartTransaction();
                dBManager.InsertToBD("poligon", polygonColumns, polygonValues);

                var _points = points;

                string[] _columns = { "longitude", "latitude", "Id_of_poligon", "order123" };
                string[] _values = new string[4];
                _values[2] = polygonValues[0];


                for (int i = 0; i < _points.Count; i++)
                {
                    _values[1] = _points[i].Lng.ToString().Replace(',', '.');
                    _values[0] = _points[i].Lat.ToString().Replace(',', '.');
                    _values[3] = (i + 1).ToString();

                    dBManager.InsertToBD("point_poligon", _columns, _values);
                }

                dBManager.InsertToBD("poligon_calculations_description", polygonDescColumns, polygonDescValues);
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

                if (issue != null)
                {
                    reworkedMap.AddMarker(polyMarker, issue.name);
                }
                //if (environment != null)
                //{
                //    reworkedMap.AddPolygon(polygon, environment.Name);
                //    reworkedMap.AddMarker(polyMarker, environment.Name);
                //}

                if (issue == null /*&& environment == null*/)
                {
                    reworkedMap.AddMarker(polyMarker);
                }

                MessageBox.Show($"{_type} успішно збережений до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                dBManager.RollbackTransaction();
#if DEBUG
                DebugLog(ex);
#else
                    MessageBox.Show($"Не вдалось зберегти {_type} до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
        private async void LoadPolylines(string additionalTables = null, string additionalJoinCond = null,
                                         string additionalCondition = null, string polylineType = "polygon")
        {
            CurrentActionStatusLabel.Text = "Завантаження усіх маркерів.";
            MainToolStripProgressBar.Visible = true;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

            string _tables = "poligon, poligon_calculations_description, issues, calculations_description, user";
            string _columns = "poligon.Id_of_poligon, poligon.brush_color_r, " +
                              "poligon.bruch_color_g, poligon.brush_color_b, " +
                              "poligon.brush_alfa, poligon.line_collor_r, " +
                              "poligon.line_color_g, poligon.line_color_b, " +
                              "poligon.line_alfa, poligon.line_thickness, " +
                              "poligon.name, poligon.description, " +
                              "issues.name, calculations_description.calculation_name, " +
                              "user.description, user.id_of_expert";
            string _joinCond = "poligon.Id_of_poligon = poligon_calculations_description.id_poligon, " +
                               "poligon_calculations_description.id_of_issue = issues.issue_id, " +
                               "poligon_calculations_description.calculations_description_number = " +
                               "calculations_description.calculation_number, " +
                               "poligon.id_of_user = user.id_of_user";
            string _condition = "poligon.type = " + DBUtil.AddQuotes(polylineType);

            string _polygonPointTables = "poligon, point_poligon";
            string _polygonPointColumns = "point_poligon.longitude, point_poligon.latitude, point_poligon.Id_of_poligon";
            string _polygonPointJoinConditions = "poligon.Id_of_poligon = point_poligon.Id_of_poligon";
            string _polygonPointCondition = " WHERE true ORDER BY `Id_of_poligon`, `order123`";

            List<List<object>> result = null;
            List<IGrouping<int, List<object>>> pointsResult = null;

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
            else if (polylineType == "polygon")
            {
                reworkedMap.ClearAllRoutes();
            }

            string format = null;
            string _type = null;

            try
            {
                await Task.Run(() =>
                {
                    lock (_lock)
                    {
                        isLoading = true;
                    }

                    var _generalRes = dBManager.GetRowsUsingJoin(_tables, _columns, _joinCond, _condition, JoinType.LEFT);
                    var _pointsRes = dBManager.GetRowsUsingJoin(_polygonPointTables,
                                                                _polygonPointColumns,
                                                                _polygonPointJoinConditions,
                                                                _polygonPointCondition, JoinType.LEFT)
                                              .GroupBy(row => (row[2] is DBNull ? -1 : (int)row[2]))
                                              .ToList();

                    lock (_lock)
                    {
                        result = _generalRes;
                        pointsResult = _pointsRes;
                    }
                });

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
                        break;
                    }

                    Color fill = Color.FromArgb(Convert.ToInt32(row[4]),
                                               Convert.ToInt32(row[1]),
                                               Convert.ToInt32(row[2]),
                                               Convert.ToInt32(row[3]));

                    Color stroke = Color.FromArgb(Convert.ToInt32(row[8]),
                                                  Convert.ToInt32(row[5]),
                                                  Convert.ToInt32(row[6]),
                                                  Convert.ToInt32(row[7]));

                    if (polylineType == "polygon")
                    {
                        var polygon = new GMap.NET.WindowsForms.GMapPolygon(_points, row[10].ToString());
                        polygon.Fill = new SolidBrush(fill);
                        polygon.Stroke = new Pen(stroke, Convert.ToInt32(row[9]));

                        format = "Назва полігону: {0}\nОпис полігону: {1}";
                        figure = polygon;

                        _type = "Полігон";
                    }
                    else
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
                    marker.CreatorFullName = row[14] is DBNull ? "Дані відсутні" : row[14].ToString();
                    marker.CreatorRole = (Role)((int)row[15]);

                    ((IDescribable)marker).Type = _type;

                    bool isAdded = false;

                    if (!(row[12] is DBNull))
                    {
                        reworkedMap.AddMarker(marker, row[12].ToString());

                        if (polylineType == "polygon")
                        {
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, row[12].ToString());
                        }
                        else if (polylineType == "tube")
                        {
                            reworkedMap.AddRoute((GMap.NET.WindowsForms.GMapRoute)figure, row[12].ToString());
                        }

                        isAdded = true;
                    }
                    if (!(row[13] is DBNull))
                    {
                        reworkedMap.AddMarker(marker, row[13].ToString());

                        if (polylineType == "polygon")
                        {
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure, row[13].ToString());
                        }
                        else if (polylineType == "tube")
                        {
                            reworkedMap.AddRoute((GMap.NET.WindowsForms.GMapRoute)figure, row[13].ToString());
                        }

                        isAdded = true;
                    }

                    if (!isAdded)
                    {
                        if (polylineType == "polygon")
                        {
                            reworkedMap.AddPolygon((GMap.NET.WindowsForms.GMapPolygon)figure);
                        }
                        else if (polylineType == "tube")
                        {
                            reworkedMap.AddRoute((GMap.NET.WindowsForms.GMapRoute)figure);
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
#endif
            }
            finally
            {
                isLoading = false;

                CurrentActionStatusLabel.Text = string.Empty;
                MainToolStripProgressBar.Visible = false;
                MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Continuous;
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
                    await Task.Run(() =>
                    {
                        while (isLoading)
                        {
                            System.Threading.Thread.Sleep(500);
                        }

                        lock (_lock)
                        {
                            isLoading = true;
                        }

                        dBManager.StartTransaction();
                        dBManager.DeleteFromDB("poligon_calculations_description", "id_poligon", polyMarker.Id.ToString());
                        dBManager.DeleteFromDB("point_poligon", "Id_of_poligon", polyMarker.Id.ToString());
                        dBManager.DeleteFromDB("poligon", "Id_of_poligon", polyMarker.Id.ToString());
                        dBManager.CommitTransaction();
                    });

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
#endif
                }
                finally
                {
                    isLoading = false;

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

            if (drawContext != null)
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

            var (issue, _, _, objName, _) = itemConfigurationWindow;
            var polygon = reworkedMap.GetPolygonByNameOrNull(objName);

            if (polygon != null)
            {
                Color polyFill = ((SolidBrush)polygon.Fill).Color;
                Color polyStroke = polygon.Stroke.Color;

                isLoading = true;

                await Task.Run(() => SavePolyline(polygon.Points, polyFill, polyStroke, "polygon"));

                isLoading = false;

                reworkedMap.EndPolygonDraw();
                drawContext = null;

                var newPolygon = new GMap.NET.WindowsForms.GMapPolygon(polygon.Points, polygon.Name);
                newPolygon.Fill = polygon.Fill;
                newPolygon.Stroke = polygon.Stroke;

                reworkedMap.RemovePolygon(polygon);
                reworkedMap.RemoveLayout(addTempLayoutName);

                if (issue != null)
                {
                    reworkedMap.AddPolygon(newPolygon, issue.name);
                }
                //if (environment != null)
                //{
                //    reworkedMap.AddPolygon(polygon, environment.Name);
                //    reworkedMap.AddMarker(polyMarker, environment.Name);
                //}

                if (issue == null /*&& environment == null*/)
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

        private void ShowAllPolygonsButton_Click(object sender, EventArgs e)
        {
            LoadPolylines();
        }
        private void ShowCurrentUserPolygonsButton_Click(object sender, EventArgs e)
        {
            LoadPolylines(additionalCondition: "poligon.id_of_user = " + userId.ToString());
        }
        private void ShowAllExpertPolygonsButton_Click(object sender, EventArgs e)
        {
            LoadPolylines(additionalCondition: "user.id_of_expert = " + ((int)expert).ToString());
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
            tubeAddingMode = !tubeAddingMode;

            if (tubeAddingMode)
            {
                HelpStatusLabel.Text = "Натискайте два рази на карту для встановлення точки трубопроводу.";

                TubeSaveButton.Enabled = true;
                TubeSettingsButton.Enabled = true;

                TubeDrawButton.Text = "Відміна";

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

            var (issue, _, _, objName, _) = itemConfigurationWindow;
            var route = reworkedMap.GetRouteByNameOrNull(objName);

            if (route != null)
            {
                Color polyStroke = route.Stroke.Color;

                isLoading = true;

                await Task.Run(() => SavePolyline(route.Points, Color.Transparent, polyStroke, "tube"));

                isLoading = false;

                reworkedMap.EndRouteDraw();
                drawContext = null;

                var newRoute = new GMap.NET.WindowsForms.GMapRoute(route.Points, route.Name);
                newRoute.Stroke = route.Stroke;

                reworkedMap.RemoveRoute(route);
                reworkedMap.RemoveLayout(addTempLayoutName);

                if (issue != null)
                {
                    reworkedMap.AddRoute(newRoute, issue.name);
                }
                //if (environment != null)
                //{
                //    reworkedMap.AddPolygon(polygon, environment.Name);
                //    reworkedMap.AddMarker(polyMarker, environment.Name);
                //}

                if (issue == null /*&& environment == null*/)
                {
                    reworkedMap.AddRoute(newRoute);
                }

                if (itemConfigurationWindow != null)
                {
                    itemConfigurationWindow.Dispose();
                }
                itemConfigurationWindow = null;

                tubeAddingMode = false;

                TubeDrawButton.Text = "Почати";
                TubeSettingsButton.Enabled = false;
                TubeSaveButton.Enabled = false;
            }
            else
            {
                MessageBox.Show("Немає намальованого трубопроводу на карті або відсутня його назва.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAllTubesButton_Click(object sender, EventArgs e)
        {
            LoadPolylines(polylineType: "tube");
        }
        private void ShowCurrentUserTubesButton_Click(object sender, EventArgs e)
        {
            LoadPolylines(additionalCondition: "poligon.id_of_user = " + userId.ToString(), polylineType: "tube");
        }
        private void ShowCurrentExpertTubesButton_Click(object sender, EventArgs e)
        {
            LoadPolylines(additionalCondition: "user.id_of_expert = " + ((int)expert).ToString(), polylineType: "tube");
        }

        private void ClearAllTubesButton_Click(object sender, EventArgs e)
        {
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

        private void ComparsionSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reworkedMap.SelectedMarker != null)
            {
                NamedGoogleMarker marker = (NamedGoogleMarker)reworkedMap.SelectedMarker;

                if (!ComprasionItemsComboBox.Items.Contains(marker.ToString()))
                {
                    ComprasionItemsComboBox.Items.Add(marker.ToString());
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

            CompareSettings compareSettings = new CompareSettings(ComprasionItemsComboBox.Items.OfType<string>().ToList());
            compareSettings.ShowDialog();
        }
    }
}
