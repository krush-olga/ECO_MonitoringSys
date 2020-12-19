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

namespace Maps
{
    public partial class TestNewMap : Form
    {
        private readonly GMap.NET.PointLatLng defaultMapStartCoord;
        private readonly string defaultImageName;
        private readonly Role expert;
        private readonly int userId;

        private bool isLoading;
        private bool moveMode;
        private bool markerAddingMode;
        private bool polygonAddingMode;
        private bool routeAddingMode;
        private DBManager dBManager;

        private Keys pressedKey;

        private Button StartDrawButton;
        private Button EndDrawButton;
        private GroupBox DrawButtonGroupBox;

        private ReworkedMap reworkedMap;
        private DrawContext drawContext;
        private HelpWindows.ItemConfigurationWindow itemConfigurationWindow;

        private object _lock;

        public TestNewMap(Role expert, int userId)
        {
            defaultMapStartCoord = new GMap.NET.PointLatLng(50.449173, 30.457578);
            defaultImageName = "missingImage";

            _lock = new object();

            InitializeComponent();
            InitialiazeAdditionalComponent();

            reworkedMap = new ReworkedMap(gMapControl);

            this.userId = userId;
            this.expert = expert;
            this.moveMode = false;

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

            StartDrawButton = new Button()
            {
                Text = "Начать",
                Size = PolygonButton.Size,
                Location = new Point(5, 30)
            };
            EndDrawButton = new Button()
            {
                Text = "Закончить",
                Size = PolygonButton.Size,
                Location = new Point(StartDrawButton.Location.X, StartDrawButton.Location.Y + StartDrawButton.Height + 5),
                Enabled = false
            };

            StartDrawButton.Click += StartDrawButton_Click;
            EndDrawButton.Click += EndDrawButton_Click;

            DrawButtonGroupBox = new GroupBox()
            {
                Text = "Кнопки рисования",
                Size = new Size(StartDrawButton.Width + 10, StartDrawButton.Height * 2 + 40),
                Visible = false
            };

            DrawButtonGroupBox.TabIndex = 5;

            this._ElementsSideMenuPanel.Controls.Add(DrawButtonGroupBox);

            DrawButtonGroupBox.Controls.Add(StartDrawButton);
            DrawButtonGroupBox.Controls.Add(EndDrawButton);

            FindSideMenuPanel.Visible = false;
            FiltrationSideMenuPanel.Visible = false;
            CompareSideMenuPanel.Visible = false;
            ElementsSideMenuPanel.Visible = false;

            cbSearch.SelectedIndex = 0;

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

        private void MarkerButton_Click(object sender, EventArgs e)
        {
            MarkerButton.Enabled = false;
            PolygonButton.Enabled = true;
            TubeButton.Enabled = true;

            TubeButton.Location = new Point(PolygonButton.Location.X, PolygonButton.Location.Y + PolygonButton.Height + 6);
            LoadButton.Location = new Point(TubeButton.Location.X, TubeButton.Location.Y + TubeButton.Height + 6);


            DrawButtonGroupBox.Visible = false;
        }
        private void PolygonButton_Click(object sender, EventArgs e)
        {
            MarkerButton.Enabled = true;
            PolygonButton.Enabled = false;
            TubeButton.Enabled = true;

            DrawButtonGroupBox.Location = new Point(PolygonButton.Location.X - 5,
                                                    PolygonButton.Location.Y + PolygonButton.Height + 6);
            TubeButton.Location = new Point(DrawButtonGroupBox.Location.X + 5,
                                            DrawButtonGroupBox.Location.Y + DrawButtonGroupBox.Height + 6);
            LoadButton.Location = new Point(TubeButton.Location.X,
                                            TubeButton.Location.Y + TubeButton.Height + 6);

            DrawButtonGroupBox.Visible = true;
        }
        private void TubeButton_Click(object sender, EventArgs e)
        {
            MarkerButton.Enabled = true;
            PolygonButton.Enabled = true;
            TubeButton.Enabled = false;

            TubeButton.Location = new Point(PolygonButton.Location.X,
                                            PolygonButton.Location.Y + PolygonButton.Height + 6);
            DrawButtonGroupBox.Location = new Point(TubeButton.Location.X - 5,
                                                    TubeButton.Location.Y + TubeButton.Height + 6);
            LoadButton.Location = new Point(DrawButtonGroupBox.Location.X + 5,
                                            DrawButtonGroupBox.Location.Y + DrawButtonGroupBox.Height + 6);

            DrawButtonGroupBox.Visible = true;
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

        private void StartDrawButton_Click(object sender, EventArgs e)
        {
            StartDrawButton.Enabled = false;
            EndDrawButton.Enabled = true;

            if (!PolygonButton.Enabled)
            {
                if (string.IsNullOrEmpty(LayoutTextBox.Text))
                {
                    drawContext = reworkedMap.StartDrawPolygon(Color.Red, 30, "");
                }
                else
                {
                    drawContext = reworkedMap.StartDrawPolygon(Color.Red, 30, "", LayoutTextBox.Text);
                }
            }
            else if (!TubeButton.Enabled)
            {
                if (string.IsNullOrEmpty(LayoutTextBox.Text))
                {

                    drawContext = reworkedMap.StartDrawRoute(Color.Blue, "");
                }
                else
                {
                    drawContext = reworkedMap.StartDrawRoute(Color.Blue, "", LayoutTextBox.Text);
                }
            }
        }
        private void EndDrawButton_Click(object sender, EventArgs e)
        {
            StartDrawButton.Enabled = true;
            EndDrawButton.Enabled = false;

            if (!PolygonButton.Enabled)
            {
                reworkedMap.EndPolygonDraw();
            }
            else if (!TubeButton.Enabled)
            {
                reworkedMap.EndRouteDraw();
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!polygonAddingMode && !routeAddingMode)
            {
                string[] columns = new string[] { "Coord_Lat", "Coord_Lng" };
                string[] values = new string[] { reworkedMap.SelectedMarker.Position.Lat.ToString().Replace(',', '.'),
                                                 reworkedMap.SelectedMarker.Position.Lng.ToString().Replace(',', '.') };

                try
                {
                    dBManager.DeleteFromDB("poi", columns, values);

                    reworkedMap.RemoveMarker(reworkedMap.SelectedMarker);

                    MessageBox.Show("Видалення маркеру успішне.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка при видаленні маркеру.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
#if DEBUG
                    DebugLog(ex);
#endif
                }
            }

            //if (!MarkerButton.Enabled)
            //{
            //    reworkedMap.RemoveMarker(reworkedMap.SelectedMarker);
            //}
            //else if (!StartDrawButton.Enabled && drawContext != null)
            //{
            //    drawContext.RemoveCoord(reworkedMap.SelectedMarker.Position);
            //}
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            reworkedMap.ClearMap();

            StartDrawButton.Enabled = true;
            EndDrawButton.Enabled = false;
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
                if (polygonAddingMode || routeAddingMode || expert == Role.Admin)
                {
                    MapObjectContextMenuStrip.Items[0].Visible = true;
                }
                else
                {
                    MapObjectContextMenuStrip.Items[0].Visible = false;
                }

                MapObjectContextMenuStrip.Show(gMapControl, e.Location);
            }
        }
        private void gMapControl_OnPolygonClick(GMap.NET.WindowsForms.GMapPolygon item, MouseEventArgs e)
        {
            if (polygonAddingMode)
            {
                return;
            }

            reworkedMap.SelectedPolygon = item;
        }
        private void gMapControl_OnRouteClick(GMap.NET.WindowsForms.GMapRoute item, MouseEventArgs e)
        {
            if (routeAddingMode)
            {
                return;
            }

            reworkedMap.SelectedRoute = item;
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
                var addedMarkers = reworkedMap.GetMarkersByLayoutOrNull("__AddMarkerLayout__");

                if (addedMarkers == null || addedMarkers.Count() == 0)
                {
                    reworkedMap.AddMarker(cursorLocation, 
                                          (Bitmap)MarkerPictureBox.Image, 
                                          "", "__AddMarkerLayout__");
                }
                else
                {
                    addedMarkers.First().Position = gMapControl.FromLocalToLatLng(cursorLocation.X, cursorLocation.Y);
                }
            }
            else if ((polygonAddingMode || routeAddingMode) && drawContext != null)
            {
                drawContext.AddCoord(gMapControl.FromLocalToLatLng(cursorLocation.X, cursorLocation.Y));
            }
        }
        private void gMapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveMode && pressedKey == Keys.ControlKey)
            {
                moveMode = true;
                reworkedMap.SelectedMarker = reworkedMap.GetMarkerByCoordsOrNull(e.Location);
            }
        }
        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (moveMode)
            {
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
                else if ((polygonAddingMode || routeAddingMode) && drawContext != null)
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

        private void HideButton_Click(object sender, EventArgs e)
        {
            reworkedMap.HideLayoutById(LayoutTextBox.Text);
        }
        private void ShowButton_Click(object sender, EventArgs e)
        {
            reworkedMap.ShowLayoutById(LayoutTextBox.Text);
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            if (!MarkerButton.Enabled)
            {
                CurrentActionStatusLabel.Text = "Завантаження з бази даних";
                MainToolStripProgressBar.Visible = true;
                MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

                try
                {
                    var result = await Task.Run(() =>
                    {
                        return dBManager.GetRows("poi", "Coord_Lat, Coord_Lng, Description", "");
                    });

                    foreach (var row in result)
                    {
                        reworkedMap.AddMarker(new GMap.NET.PointLatLng((double)row[0], (double)row[1]),
                                              (Bitmap)Image.FromFile($@"{System.Environment.CurrentDirectory}\Resources\Images\noimage.png"),
                                              row[2].ToString());
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Помилка при завантаженні.");
                }
                finally
                {
                    CurrentActionStatusLabel.Text = "";

                    MainToolStripProgressBar.Visible = false;
                    MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Continuous;
                }


            }
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

        private void EconomicActivityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EconomicActivityComboBox.SelectedValue != null)
            {
                string imageName = EconomicActivityComboBox.SelectedValue.ToString();

                MarkerPictureBox.Image = LoadImage(imageName);

                if (markerAddingMode)
                {
                    var markers = reworkedMap.GetMarkersByLayoutOrNull("__AddMarkerLayout__");

                    if (markers != null && markers.Count() != 0)
                    {
                        var marker = markers.First();
                        reworkedMap.RemoveMarker(marker);
                        reworkedMap.AddMarker(marker.Position, (Bitmap)MarkerPictureBox.Image,
                                              marker.ToolTipText, "__AddMarkerLayout__");
                    }
                }

                DebugToolStripStatusLabel.Text = imageName;
            }
        }

        private void AddItemTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAddItemButtons();
        }
        private void ResetAddItemButtons()
        {
            HelpStatusLabel.Text = "";

            markerAddingMode = false;
            polygonAddingMode = false;
            routeAddingMode = false;

            MarkerSettingsButton.Enabled = false;
            SaveMarkerButton.Enabled = false;

            PolygonSettingsButton.Enabled = false;
            PolygonSaveButton.Enabled = false;
            PolygonColorPictureBox.Enabled = false;

            AddMarkerButton.Text = "Додати";
            PolygonDrawButton.Text = "Почати";

            if (reworkedMap != null)
            {
                reworkedMap.RemoveLayout("__AddMarkerLayout__");
                reworkedMap.CancelPolygonDraw();
                reworkedMap.CancelRouteDraw();
            }

            if (itemConfigurationWindow != null)
            {
                itemConfigurationWindow.Dispose();
            }
            itemConfigurationWindow = null;
        }

        #region Markers methods
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
                MessageBox.Show("Не можливо зберегти об'єкт його налаштування.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var markers = reworkedMap.GetMarkersByLayoutOrNull("__AddMarkerLayout__");

            if (markers != null && markers.Count() != 0)
            {
                var marker = markers.First();

                var (issue, environment, series, objName, objDescription) = itemConfigurationWindow;

                Data.Entity.TypeOfObject typeOfObject = EconomicActivityComboBox.SelectedItem as TypeOfObject;

                string[] columns = { "id_of_user", "Type", "owner_type", "Coord_Lat", "Coord_Lng",
                                     "Description", "Name_Object", "id_of_issue", "calculations_description_number", "idEnvironment"};
                string[] values = { userId.ToString(), typeOfObject.Id.ToString(), OwnershipTypeComboBox.SelectedValue.ToString(),
                                    marker.Position.Lat.ToString().Replace(',', '.'), marker.Position.Lng.ToString().Replace(',', '.'),
                                    string.IsNullOrEmpty(objDescription) ? "'Опис відсутній.'" : Data.DBUtil.AddQuotes(objDescription),
                                    Data.DBUtil.AddQuotes(objName), issue != null ? issue.id.ToString() : "0",
                                    series != null ? series.id.ToString() : "0",
                                    environment != null ? environment.Id.ToString() : "0"};

                try
                {
                    dBManager.InsertToBD("poi", columns, values);

                    marker.ToolTipText = $"Назва: {objName}\nОпис: {(string.IsNullOrEmpty(objDescription) ? "Опис відсутній." : objDescription)}";

                    reworkedMap.RemoveMarker(marker);               //Убирает маркер из слоя для добавления

                    if (issue != null)
                    {
                        reworkedMap.AddMarker(marker, issue.name);
                    }
                    if (environment != null)
                    {
                        reworkedMap.AddMarker(marker, environment.Name);
                    }

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
            LoadMarkers(string.Empty, string.Empty, string.Empty, string.Empty);
        }
        private void ShowAllExpertMarkerButton_Click(object sender, EventArgs e)
        {
            LoadMarkers("user", string.Empty, 
                        "poi.id_of_user = user.id_of_user",
                        "user.id_of_expert = " + ((int)expert).ToString());
        }
        private void ShowCurrentUserMarkerButton_Click(object sender, EventArgs e)
        {
            LoadMarkers(string.Empty, string.Empty, string.Empty, "id_of_user = " + userId.ToString());
        }
        private async void LoadMarkers(string additionalTables, string additionalColumns, string additionalJoinCond, string condition)
        {
            CurrentActionStatusLabel.Text = "Завантаження усіх маркерів.";
            MainToolStripProgressBar.Visible = true;
            MainToolStripProgressBar.ProgressBar.Style = ProgressBarStyle.Marquee;

            string _tables = "poi, type_of_object, issues, environment";
            string _columns = "poi.Coord_Lat, poi.Coord_Lng, poi.Description, poi.Name_Object, " +
                              "type_of_object.Image_Name, issues.name, environment.name";
            string _joinCond = "type_of_object.Id = poi.Type, poi.id_of_issue = issues.issue_id, " +
                               "poi.idEnvironment = environment.id";
            string _condition = condition ?? string.Empty;
            List<List<object>> result = null;

            reworkedMap.ClearAllMarkers();

            if (!string.IsNullOrEmpty(additionalTables))
            {
                _tables += ", " + additionalTables;
            }
            if (!string.IsNullOrEmpty(additionalColumns))
            {
                _columns += ", " + additionalColumns;
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
                    var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng((double)row[0], (double)row[1]),
                                                                                 LoadImage(row[4].ToString()));
                    bool isAdded = false;

                    marker.ToolTipText = $"Назва: {row[2]}\nОпис: {row[3]}";

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

                    if (!isAdded)
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
        #endregion

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

                PolygonColorPictureBox.BackColor = colorDialog.Color;
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

                drawContext = reworkedMap.StartDrawPolygon(Color.Red, 100, "__newPolygon__", "__AddPolygonLayout__");
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
                ((PolygonContext)drawContext).SetOpacity((int)TransparentNumericUpDown.Value);
                ChangePictureBoxColor();
            }
        }
    }
}
    