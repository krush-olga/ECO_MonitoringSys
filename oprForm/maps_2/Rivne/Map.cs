using Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Maps
{
    //класс отображения карты и вызова методов для роботы  с ней 
    public partial class Map : Form
    {

        private NewMap newMap;
        public int id_of_exp;
        private DBManager db = new DBManager();
        private double[] LatLng;
        private EmissionsForm emissionsForm;
        private List<List<Object>> list_towns;
        private List<List<Object>> list_towns_cord;

        //иниц. карты
        public Map()
        {
            InitializeComponent();

            list_towns = db.GetRows("cities", "Name", "");
            cbTownSearch.Items.Add("");
            for(int i = 0; i<list_towns.Count; i++)
            {
                cbTownSearch.Items.Add(list_towns[i][0].ToString());
            }
            newMap = new NewMap(gMapControl);
            newMap.MapInitialize();
        }

        //событие двойного нажатия на карту
        private void gMapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //если включена кнопка "Начать" на вкладке Полигон
            if (btnStartPointPolygon.Enabled == false)
            {
                newMap.AddMarkerOnMap(comboBox1.Text, e.X, e.Y, null, false);
                btnSavePolygon.Enabled = true;
                btnAddMarker.Tag = "0";

            }
            //если включена кнопка "Добавить" на вкладке Полигон
            if (btnAddMarker.Enabled == false)
            {
                btnStartPointPolygon.Tag = "0";
                LatLng = newMap.AddMarkerOnMap(comboBox1.Text, e.X, e.Y, (Bitmap)pictureBox1.Image, false);
                btnSaveMarker.Enabled = true;
            }
            //если включена кнопка "Начать" на вкладке Водопровод
            if (btnStartTube.Enabled == false)
            {
                newMap.AddPointTube(e.X, e.Y);
                if (newMap._pointsTube.Count == 2)
                {
                    newMap.DrawTube();
                    newMap._pointsTube.RemoveAt(0);
                }
                btnSaveTube.Enabled = true;
            }
        }
        //Собития вкладки Маркер
        ////////////////////////////////////////////////////////////////////////////////////////
        //настраивает кнопки других вкладок при нажатие на кнопку "добавить" на вкладке маркеров
        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            btnAddMarker.Enabled = false;
            btnCancelMarker.Enabled = true;
            btnDeleteMarker.Enabled = false;
            btnClearMarker.Enabled = false;

        }
        //кнопка сохранения маркера 
        private void btnSaveMarker_Click(object sender, EventArgs e)
        {
            Calculations fc = new Calculations(id_of_exp, comboBox1.Text, comboBox2.SelectedIndex + 1, LatLng[0], LatLng[1], newMap);
            fc.ShowDialog();
            if (fc.Save() == true)
            {
                btnAddMarker.Enabled = true;
                btnSaveMarker.Enabled = false;
                btnCancelMarker.Enabled = false;
                btnDeleteMarker.Enabled = true;
                btnClearMarker.Enabled = true;
                newMap.OverlayClear();
            }
        }
        //кнопка отмены
        private void btnCancelMarker_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
            btnAddMarker.Enabled = true;
            btnSaveMarker.Enabled = false;
            btnCancelMarker.Enabled = false;
            btnDeleteMarker.Enabled = true;
            btnClearMarker.Enabled = true;
        }
        //настройка кнопки удаления для удаления маркера
        private void btnDeleteMarker_Click(object sender, EventArgs e)
        {
            btnDeleteMarker.Enabled = false;
        }
        //кнопка очистки
        private void btnClearMarker_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
            btnShowAllMarkers.Enabled = true;
            btnShowExpertsMarkers.Enabled = true;
        }
        //отрисовует все маркеры загрязнения 
        private void btnShowAllMarkers_Click(object sender, EventArgs e)
        {
            newMap.AddObjectFromDB();
            gMapControl.Zoom += 1;
            gMapControl.Zoom -= 1;
            btnShowAllMarkers.Enabled = false;
            btnShowExpertsMarkers.Enabled = true;
        }
        //отрисовует все маркеры загрязнения поточного эксперта
        private void btnShowExpertsMarkers_Click(object sender, EventArgs e)
        {
            newMap.AddObjectFromDB(id_of_exp.ToString());
            gMapControl.Zoom += 1;
            gMapControl.Zoom -= 1;
            btnShowExpertsMarkers.Enabled = false;
            btnShowAllMarkers.Enabled = true;
        }
        ///////////////////////////////////////////////////////////////////////////////////////

        //Собития вкладки Полигон
        ///////////////////////////////////////////////////////////////////////////////////////
        //настройка других кнопок при включении кнопки "начать" на вкладке полигонов
        private void btnStartPointPolygon_Click(object sender, EventArgs e)
        {
            btnStartPointPolygon.Enabled = false;
            btnCancelPolygon.Enabled = true;
            btnShowAllPolygons.Enabled = false;
            btnShowExpertsPolygons.Enabled = false;
            btnShowPolygon.Enabled = true;
        }
        //сохранение координат полигона в БД
        private void btnSavePolygon_Click(object sender, EventArgs e)
        {
            Calculations fc = new Calculations(id_of_exp, colorDialog1, newMap, db);
            fc.ShowDialog();
            if (fc.Save() == true)
            {
                btnSavePolygon.Enabled = false;
                btnCancelPolygon.Enabled = false;
                btnStartPointPolygon.Enabled = true;
                btnShowAllPolygons.Enabled = true;
                btnShowExpertsPolygons.Enabled = true;
                btnShowPolygon.Enabled = false;
                newMap.OverlayClear();
            }
        }
        //настраивает кнопки других вкладок при нажатие на кнопку "Отмена" на вкладке полигонов
        private void btnCancelPolygon_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
            newMap._points.Clear();
            btnStartPointPolygon.Enabled = true;
            btnSavePolygon.Enabled = false;
            btnCancelPolygon.Enabled = false;
            btnShowExpertsPolygons.Enabled = true;
            btnShowPolygon.Enabled = false;
        }
        //отображает полигон который только что был нарисован(без сохранений) кнопка "изобразить" на вкладке полигонов
        private void btnShowPolygon_Click(object sender, EventArgs e)
        {
            newMap.DrawPolygon(numericUpDown1, colorDialog1);
            btnShowPolygon.Enabled = false;
        }
        //отображает все полигоны на карте
        private void btnAllShowPolygons_Click(object sender, EventArgs e)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, description", "type = 'poligon'");

            for (int i = 0; i < listPoligon.Count; i++)
            {
                var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                for (int j = 0; j < poligonCount.Count; j++)
                {
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                    points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                }
                listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                newMap.AddMarkerOnMap(listPoligon[i][6].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][5].ToString(), null, listPoligon[i][4].ToString());
                newMap.DrawPolygon(Convert.ToInt16(listPoligon[i][1]), Convert.ToInt16(listPoligon[i][2]), Convert.ToInt16(listPoligon[i][3]), points);
                points.Clear();
            }
            btnShowAllPolygons.Enabled = false;
            btnShowExpertsPolygons.Enabled = true;
        }
        //выбор цвета для полигона
        private void btnColorPolygon_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
        //отрисовует все полигоны поточного эксперта
        private void btnShowExpertsPolygons_Click(object sender, EventArgs e)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, type", "id_of_expert = " + id_of_exp.ToString());

            for (int i = 0; i < listPoligon.Count; i++)
            {
                if (listPoligon[i][6].ToString().ToLower() == "poligon".ToLower())
                {

                    var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                    for (int j = 0; j < poligonCount.Count; j++)
                    {
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                        points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                    }
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                    newMap.AddMarkerOnMap(listPoligon[i][4].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][5].ToString(), null, "");
                    newMap.DrawPolygon(Convert.ToInt16(listPoligon[i][1]), Convert.ToInt16(listPoligon[i][2]), Convert.ToInt16(listPoligon[i][3]), points);
                    points.Clear();
                }
            }
            btnShowAllPolygons.Enabled = true;
            btnShowExpertsPolygons.Enabled = false;
        }
        //настройка кнопки удаления для удаления полигона
        private void btnDeletePolygon_Click(object sender, EventArgs e)
        {
            btnDeletePolygon.Enabled = false;
            btnDeleteMarker.Enabled = true;
        }
        //кнопка очистки карты 
        private void btnClearPolygons_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
            btnShowAllPolygons.Enabled = true;
        }
        ///////////////////////////////////////////////////////////////////////////////////////

        //Собития вкладки Водопровод
        ///////////////////////////////////////////////////////////////////////////////////////
        //настройка кнопки для начала построения трубопровода
        private void btnStartTube_Click(object sender, EventArgs e)
        {
            btnStartTube.Enabled = false;
            btnCancelTube.Enabled = true;
            btnShowAllTubes.Enabled = false;
            btnShowExpertsTubes.Enabled = false;
            btnClearTube.Enabled = false;
            btnDeleteTube.Enabled = false;
        }
        //сохранения трубопровода
        private void btnSaveTube_Click(object sender, EventArgs e)
        {
            Calculations fc = new Calculations(id_of_exp, newMap);
            fc.ShowDialog();
            if (fc.Save() == true)
            {
                btnStartTube.Enabled = true;
                btnSaveTube.Enabled = false;
                btnCancelTube.Enabled = false;
                btnShowAllTubes.Enabled = true;
                btnShowExpertsTubes.Enabled = true;
                btnClearTube.Enabled = true;
                btnDeleteTube.Enabled = true;
            }
        }
        //кнопка отмены и сброс настроек 
        private void btnCancelTube_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
            newMap._points.Clear();
            newMap._pointsTube.Clear();
            btnStartTube.Enabled = true;
            btnSaveTube.Enabled = false;
            btnCancelTube.Enabled = false;
            btnShowAllTubes.Enabled = true;
            btnShowExpertsTubes.Enabled = true;
            btnClearTube.Enabled = true;
            btnDeleteTube.Enabled = true;
        }
        //отображает все трубопроводы 
        private void btnShowAllTubes_Click(object sender, EventArgs e)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            listPoligon = db.GetRows("poligon", "Id_of_poligon, name, id_of_expert, description", "type = 'tube'");

            for (int i = 0; i < listPoligon.Count; i++)
            {
                var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                for (int j = 0; j < poligonCount.Count; j++)
                {
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                    points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                    newMap.AddPointTube(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][2].ToString(), listPoligon[i][1].ToString(), listPoligon[i][3].ToString());
                    if (points.Count == 2)
                    {
                        newMap.DrawTube();
                        points.RemoveAt(0);
                        newMap._pointsTube.RemoveAt(0);


                    }
                }
                points.Clear();
                newMap._pointsTube.Clear();
                newMap._points.Clear();


            }
        }
        //отображает поточный водопровод
        private void btnShowExpertsTubes_Click(object sender, EventArgs e)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            listPoligon = db.GetRows("poligon", "Id_of_poligon, name, id_of_expert, description", $"type = 'tube' AND id_of_expert = {id_of_exp}");

            for (int i = 0; i < listPoligon.Count; i++)
            {
                var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                for (int j = 0; j < poligonCount.Count; j++)
                {
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                    points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                    newMap.AddPointTube(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[0][2].ToString(), listPoligon[0][1].ToString(), listPoligon[0][3].ToString());
                    if (points.Count == 2)
                    {
                        newMap.DrawTube();
                        points.RemoveAt(0);
                        newMap._pointsTube.RemoveAt(0);
                    }
                }
                points.Clear();
                newMap._pointsTube.Clear();
                newMap._points.Clear();
            }
        }
        //очистка карты
        private void btnClearTube_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
        }
        //настройка кнопки для удаления трубопровода
        private void btnDeleteTube_Click(object sender, EventArgs e)
        {
            btnDeleteTube.Enabled = false;
            btnDeletePolygon.Enabled = true;
            btnDeleteMarker.Enabled = true;

        }
        ///////////////////////////////////////////////////////////////////////////////////////

        private void ZoomAdd_Click(object sender, EventArgs e)
        {
            newMap.ZoomAdd();
        }

        private void ZoomMinus_Click(object sender, EventArgs e)
        {
            newMap.ZoomMinus();
        }

        //Загрузка названий картинок в combobox2 на вкладке маркеров
        private void UpLoadNameImg()
        {
            List<List<Object>> listImg;
            listImg = db.GetRows("type_of_object", "Name", "");

            for (int i = 0; i < listImg.Count; i++)
            {
                comboBox2.Items.Add(listImg[i][0]);
            }
            comboBox2.SelectedIndex = 0;

        }

        private void FillCheckListBox(string table, string col, CheckedListBox checkedList, string condition)
        {
            List<List<Object>> list;
            list = db.GetRows(table, col, condition);
            for (int i = 0; i < list.Count; i++)
            {
                checkedList.Items.Add(list[i][0].ToString());
                checkedList.SetItemChecked(i, true);
            }
        }

        private void FillCheckListBoxForPoligon(string table, string col, CheckedListBox checkedList, string condition)
        {
            List<List<Object>> list;
            list = db.GetRows(table, col, condition);
            for (int i = 0; i < list.Count; i++)
            {
                checkedList.Items.Add(list[i][0] + " - Id:" + list[i][1].ToString());
                checkedList.SetItemChecked(i, true);
            }
        }

        private void Map_Load(object sender, EventArgs e)
        {
            UpLoadNameImg();
            comboBox1.SelectedIndex = 0;

            btnCancelPolygon.Enabled = false;

            CollapseButton.Location = new Point(gMapControl.Size.Width - 25, 2);
            ZoomAdd.Location = new Point(gMapControl.Size.Width - 25, 31);
            ZoomMinus.Location = new Point(gMapControl.Size.Width - 25, 60);
            panel1.Size = new Size(346, tabControl1.Size.Height + label2.Location.Y + tabControl1.Location.Y + label2.Location.Y + label6.Location.Y);
        }

        //код был до меня
        private void button3_Click(object sender, EventArgs e)
        {
            var a = newMap.cArea();
            //button3.Text = a.ToString();
        }

        private void gMapControl_OnPolygonClick(GMap.NET.WindowsForms.GMapPolygon item, MouseEventArgs e)
        {
            double pLat = item.From.Value.Lat;
            MessageBox.Show(pLat.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            list_towns_cord = db.GetRows("cities", "Latitude, Longitude", "Name = '" + list_towns[cbTownSearch.SelectedIndex -1][0].ToString() + "'");
            gMapControl.Position = new GMap.NET.PointLatLng(Convert.ToDouble(list_towns_cord[0][0]), Convert.ToDouble(list_towns_cord[0][1]));
        }

        //при клике на любой маркер на картке(обычный маркер, полигон, трубы)
        private void gMapControl_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            string description = "null";
            string id = null;
            newMap.currentMarker = item;

            if (btnStartPointPolygon.Enabled == true && btnDeleteMarker.Enabled == true && btnAddMarker.Enabled == true && btnDeletePolygon.Enabled == true && btnDeleteTube.Enabled == true && btnStartTube.Enabled == true)
            {
                ///////////////////////////////////////////////////////////
                ///25,09 ИЗМЕНЕНО
                emissionsForm = new EmissionsForm(newMap.currentMarker);
                Int32 indexx = item.ToolTipText.IndexOf("Опис");
                emissionsForm.Text = item.ToolTipText.Remove(indexx);
                emissionsForm.ShowDialog();
                ///////////////////////////////////////////////////////////////
            }

            if (btnDeleteMarker.Enabled == false && btnAddMarker.Enabled == true && tabControl1.SelectedIndex == 0)
            {
                try
                {
                    description = (string)db.GetValue("poi", "Description", "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.'));
                    string nameObject = (string)db.GetValue("poi", "Name_Object", "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.'));
                    var typePoi = (int)db.GetValue("poi", "Type", "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.'));
                    string typeMarker = (string)db.GetValue("type_of_object", "Name", "Id = " + typePoi);

                    id = (string)db.GetValue("poi", "Name", "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.'));
                    var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.'));

                    DeleteForm deleteForm = new DeleteForm(description, nameObject, typeMarker);
                    if (id == id_of_exp.ToString())
                    {
                        deleteForm.ShowDialog();
                        if (deleteForm.CloseSelect() == true)
                        {
                            db.DeleteFromDB("emissions_on_map", "idPoi", idPoi.ToString());
                            db.DeleteFromDB("poi", "Coord_Lat", item.Position.Lat.ToString().Replace(',', '.'));
                            newMap.OverlayClear();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Данний експерт не може видаляти маркери. Маркери може видаляти експерт з ід: {id}");
                    }
                    btnDeleteMarker.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Це не маркер");
                }
            }
            else if (btnDeletePolygon.Enabled == false && btnStartPointPolygon.Enabled == true && tabControl1.SelectedIndex == 1)
            {
                btnDeleteMarker.Enabled = true;
                try
                {

                    string idPointPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + item.Position.Lat.ToString().Replace(',', '.') + " AND order123=" + 1.ToString()).ToString();
                    id = db.GetValue("poligon", "id_of_expert", "id_of_poligon = " + idPointPoligon).ToString();
                    DeleteForm deleteForm = new DeleteForm("", "", "");
                    if (id == id_of_exp.ToString())
                    {
                        deleteForm.ShowDialog();
                        if (deleteForm.CloseSelect() == true)
                        {
                            newMap.OverlayClear();

                            //.db.DeleteFromDB("emissions_on_map", "idPoligon", idPointPoligon.ToString());
                            db.DeleteFromDB("poligon_calculations_description", "id_poligon", idPointPoligon.ToString());
                            db.DeleteFromDB("point_poligon", "Id_of_poligon", idPointPoligon.ToString());
                            db.DeleteFromDB("poligon", "id_of_poligon", idPointPoligon.ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show($"Данний експерт не може видаляти полігони. Полігони може видаляти експерт з ід: {id}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Це не полігон");
                }
            }
            else if (btnDeleteTube.Enabled == false && btnStartTube.Enabled == true && tabControl1.SelectedIndex == 2)
            {
                btnDeleteTube.Enabled = true;
                try
                {
                    string idPointPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + item.Position.Lat.ToString().Replace(',', '.')).ToString();
                    id = db.GetValue("poligon", "id_of_expert", "id_of_poligon = " + idPointPoligon).ToString();
                    DeleteForm deleteForm = new DeleteForm("", "", "");
                    if (id == id_of_exp.ToString())
                    {
                        deleteForm.ShowDialog();
                        if (deleteForm.CloseSelect() == true)
                        {
                            db.DeleteFromDB("emissions_on_map", "idPoligon", idPointPoligon.ToString());
                            db.DeleteFromDB("point_poligon", "Id_of_poligon", idPointPoligon.ToString());
                            db.DeleteFromDB("poligon", "id_of_poligon", idPointPoligon.ToString());


                            newMap.OverlayClear();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Данний експерт не може видаляти трубопровід. Трубопровід може видаляти експерт з ід: {id}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Це не полігон");
                }
            }
            else
            {
                int? mID = null;
                if (newMap.currentMarker != null)
                {
                    mID = Convert.ToInt32(newMap.currentMarker.Tag);
                    for (int i = 0; i < newMap._points.Count; i++)
                    {
                        if (newMap.currentMarker.Position.Lat == newMap._points[i].Lat && newMap.currentMarker.Position.Lng == newMap._points[i].Lng)
                        {
                            newMap._points.RemoveAt(i);
                            break;
                        }
                    }
                    newMap.markersOverlay.Markers.Remove(newMap.currentMarker);
                    newMap.currentMarker = null;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //кнопка поискак по координатам
        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "" && textBox4.Text != "")
                {

                    gMapControl.Position = new GMap.NET.PointLatLng(Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox4.Text));
                    textBox2.Text = "";
                    textBox4.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Пасхалочка");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //отображения выбраной картинки при выборе параметра в комбобоксе2 на вкладке маркеры
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])db.GetValue("type_of_object", "Image", "Id = " + (comboBox2.SelectedIndex + 1)));
            Image img = Image.FromStream(image_stream);

            pictureBox1.Image = img;
        }

        //заполнения условий фильтраций по трубам
        private void FillTube(int exp, string name)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            string id = name.Substring(name.IndexOf("Id:"));
            id = id.Trim(new char[] { 'I', 'd', ':' });
            name = name.Substring(0, name.Length - 7);
            listPoligon = db.GetRows("poligon", "Id_of_poligon, name, id_of_expert, description", $"type = 'tube' AND id_of_expert = {exp} AND name = '{name}'" + " AND Id_of_poligon = " + id);

            for (int i = 0; i < listPoligon.Count; i++)
            {
                var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                for (int j = 0; j < poligonCount.Count; j++)
                {
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                    points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                    newMap.AddPointTube(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[0][2].ToString(), listPoligon[0][1].ToString(), listPoligon[0][3].ToString());
                    if (points.Count == 2)
                    {
                        newMap.DrawTube();
                        points.RemoveAt(0);
                        newMap._pointsTube.RemoveAt(0);
                    }
                }
                points.Clear();
                newMap._pointsTube.Clear();
                newMap._points.Clear();
            }
        }

        //заполнения условий фильтраций по полигонам

        private void FillPoligons(int exp, string name)
        {
            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            string id = name.Substring(name.IndexOf("Id:"));
            id = id.Trim(new char[] { 'I', 'd', ':' });
            name = name.Substring(0, name.Length - 7);
            listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, type, description", "name = '" + name + "'" + " AND id_of_expert =" + exp + " AND Id_of_poligon = " + id);

            for (int i = 0; i < listPoligon.Count; i++)
            {
                if (listPoligon[i][6].ToString().ToLower() == "poligon".ToLower())
                {

                    var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                    for (int j = 0; j < poligonCount.Count; j++)
                    {
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                        points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                    }
                    listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                    newMap.AddMarkerOnMap(listPoligon[i][7].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][5].ToString(), null, listPoligon[i][4].ToString());
                    newMap.DrawPolygon(Convert.ToInt16(listPoligon[i][1]), Convert.ToInt16(listPoligon[i][2]), Convert.ToInt16(listPoligon[i][3]), points);
                    points.Clear();
                }
            }
        }
        //заполнения условий фильтраций по маркерам
        private void FillMarkers(int exp, int type)
        {
            List<List<Object>> listMarkers;


            listMarkers = db.GetRows("poi", "Description, Coord_Lat, Coord_Lng, Name, Name_Object, Type", "Type = " + (type + 1) + " AND Name = '" + exp + "'");
            if (listMarkers.Count != 0)
            {
                System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])db.GetValue("type_of_object", "Image", "Id = " + listMarkers[0][5].ToString()));
                Image img = Image.FromStream(image_stream);
                newMap.AddMarkerOnMap(listMarkers[0][0].ToString(), Convert.ToDouble(listMarkers[0][1]), Convert.ToDouble(listMarkers[0][2]), listMarkers[0][3].ToString(), (Bitmap)img, listMarkers[0][4].ToString());
                listMarkers.Clear();
            }

        }
        //очистка карты
        private void button10_Click(object sender, EventArgs e)
        {
            newMap.OverlayClear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //ввод координат (юзер может ввести только цыфры и ,)
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44)
            {
                e.Handled = true;
            }
        }

        private void Map_SizeChanged(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                CollapseButton.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 2);
                ZoomAdd.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 31);
                ZoomMinus.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 60);
            }
            else if (panel1.Visible == false)
            {
                CollapseButton.Location = new Point(gMapControl.Size.Width - 25, 0);
                ZoomAdd.Location = new Point(gMapControl.Size.Width - 25, 29);
                ZoomMinus.Location = new Point(gMapControl.Size.Width - 25, 58);
            }
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
            {
                CollapseButton.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 0);
                ZoomAdd.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 29);
                ZoomMinus.Location = new Point(gMapControl.Size.Width - panel1.Size.Width + 5, 58);
                panel1.Visible = true;
            }
            else if (panel1.Visible == true)
            {
                CollapseButton.Location = new Point(gMapControl.Size.Width - 25, 0);
                ZoomAdd.Location = new Point(gMapControl.Size.Width - 25, 29);
                ZoomMinus.Location = new Point(gMapControl.Size.Width - 25, 58);
                panel1.Visible = false;
            }
        }
        //Переключение между вкладками в меню карты 
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)// вкладка Маркер
            {
                if (btnCancelPolygon.Enabled == true || btnCancelTube.Enabled == true)
                {
                    newMap.OverlayClear();
                    if (btnCancelPolygon.Enabled == true)
                    {
                        btnDeletePolygon.Enabled = true;
                        btnClearPolygons.Enabled = true;
                        btnShowAllPolygons.Enabled = true;
                        btnShowExpertsPolygons.Enabled = true;
                        btnSearch.Enabled = true;
                    }
                    if (btnCancelTube.Enabled == true)
                    {
                        btnDeleteTube.Enabled = true;
                        btnClearTube.Enabled = true;
                    }
                }
                btnAddMarker.Enabled = true;
                btnSaveMarker.Enabled = false;
                btnCancelMarker.Enabled = false;
                btnStartPointPolygon.Enabled = true;
                btnSavePolygon.Enabled = false;
                btnCancelPolygon.Enabled = false;
                btnStartTube.Enabled = true;
                btnSaveTube.Enabled = false;
                btnCancelTube.Enabled = false;
            }
            if (tabControl1.SelectedIndex == 1)// вкладка Полигон
            {
                if (btnCancelMarker.Enabled == true || btnCancelTube.Enabled == true)
                {
                    newMap.OverlayClear();
                    if (btnCancelMarker.Enabled == true)
                    {
                        btnDeleteMarker.Enabled = true;
                        btnClearMarker.Enabled = true;
                    }
                    if (btnCancelTube.Enabled == true)
                    {
                        btnDeleteTube.Enabled = true;
                        btnClearTube.Enabled = true;
                        btnShowExpertsTubes.Enabled = true;
                        btnShowAllTubes.Enabled = true;
                    }
                }
                btnStartPointPolygon.Enabled = true;
                btnSavePolygon.Enabled = false;
                btnCancelPolygon.Enabled = false;
                btnAddMarker.Enabled = true;
                btnSaveMarker.Enabled = false;
                btnCancelMarker.Enabled = false;
                btnStartTube.Enabled = true;
                btnSaveTube.Enabled = false;
                btnCancelTube.Enabled = false;
            }
            if (tabControl1.SelectedIndex == 2)// вкладка Водопровод
            {
                if (btnCancelMarker.Enabled == true || btnCancelPolygon.Enabled == true)
                {
                    newMap.OverlayClear();
                    if (btnCancelMarker.Enabled == true)
                    {
                        btnDeleteMarker.Enabled = true;
                        btnClearMarker.Enabled = true;
                    }
                    if (btnCancelPolygon.Enabled == true)
                    {
                        btnDeletePolygon.Enabled = true;
                        btnClearPolygons.Enabled = true;
                        btnShowAllPolygons.Enabled = true;
                        btnShowExpertsPolygons.Enabled = true;
                        btnSearch.Enabled = true;
                    }
                }
                btnStartTube.Enabled = true;
                btnSaveTube.Enabled = false;
                btnCancelTube.Enabled = false;
                btnAddMarker.Enabled = true;
                btnSaveMarker.Enabled = false;
                btnCancelMarker.Enabled = false;
                btnStartPointPolygon.Enabled = true;
                btnSavePolygon.Enabled = false;
                btnCancelPolygon.Enabled = false;
            }
        }

        private void gMapControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
                newMap.ZoomAdd();
            if (e.KeyCode == Keys.Subtract)
                newMap.ZoomMinus();
        }

        private void cbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSearch.SelectedIndex != -1)
                btnSearch.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<List<Object>> listPoligon = null;
            List<List<Object>> listPointPoligon = null;
            List<List<Object>> listIdPoligons = null;
            List<List<Object>> listSearch = null;
            List<List<Object>> list = null;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            if (cbSearch.SelectedIndex == 0)// поиск по назве
            {
                if (tabControl1.SelectedIndex == 0)// маркер
                {
                    list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "Name_Object = '" + tbSearch.Text + "'");
                    if(list.Count == 0)
                        MessageBox.Show("По запиту \"" + tbSearch.Text + "\" нічого не знайдено");
                    for (int i = 0; i < list.Count; i++)
                    {
                        System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])db.GetValue("type_of_object", "Image", "Id = " + list[i][3]));
                        Image img = Image.FromStream(image_stream);
                        newMap.AddMarkerOnMap(list[i][2].ToString(), Convert.ToDouble(list[i][0]), Convert.ToDouble(list[i][1]), list[i][4].ToString(), (Bitmap)img, list[i][5].ToString());
                    }
                }

                if (tabControl1.SelectedIndex == 1)// полигон
                {
                    listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, description", "type = 'poligon' AND name = '" + tbSearch.Text + "'");
                    if (listPoligon.Count == 0)
                        MessageBox.Show("По запиту \"" + tbSearch.Text + "\" нічого не знайдено");
                    for (int i = 0; i < listPoligon.Count; i++)
                    {
                        var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                        for (int j = 0; j < poligonCount.Count; j++)
                        {
                            listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                            points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                        }
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                        newMap.AddMarkerOnMap(listPoligon[i][6].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][5].ToString(), null, listPoligon[i][4].ToString());
                        newMap.DrawPolygon(Convert.ToInt16(listPoligon[i][1]), Convert.ToInt16(listPoligon[i][2]), Convert.ToInt16(listPoligon[i][3]), points);
                        points.Clear();
                    }
                }

                if(tabControl1.SelectedIndex == 2)// водопровод
                {
                    listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, description", "type = 'tube' AND name = '" + tbSearch.Text + "'");
                    if (listPoligon.Count == 0)
                        MessageBox.Show("По запиту \"" + tbSearch.Text + "\" нічого не знайдено");
                    for (int i = 0; i < listPoligon.Count; i++)
                    {
                        var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                        for (int j = 0; j < poligonCount.Count; j++)
                        {
                            listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                            points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                        }
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                        newMap.AddMarkerOnMap(listPoligon[i][6].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][5].ToString(), null, listPoligon[i][4].ToString());
                        newMap.DrawPolygon(Convert.ToInt16(listPoligon[i][1]), Convert.ToInt16(listPoligon[i][2]), Convert.ToInt16(listPoligon[i][3]), points);
                        points.Clear();
                    }
                }
            }
            else
            {
                try
                {
                    if (cbSearch.SelectedIndex == 1)//поиск по задачи
                    {
                        if (tbSearch.Text != "")
                        {
                            listSearch = db.GetRows("issues", "issue_id", "name = '" + tbSearch.Text + "'");
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "id_of_issue = '" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "id_of_issue = '" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "id_of_issue = '" + listSearch[0][0] + "'");
                        }
                        else
                        {
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "id_of_issue = '0'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "id_of_issue = '0'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "id_of_issue = '0'");
                        }
                    }
                    else if (cbSearch.SelectedIndex == 2)// поиск по расчету
                    {
                        if (tbSearch.Text != "")
                        {
                            listSearch = db.GetRows("calculations_description", "calculation_number", "calculation_name = '" + tbSearch.Text + "'");
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "calculations_desription_number '" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "calculations_description_number = '" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "calculations_description_number = '" + listSearch[0][0] + "'");
                        }
                        else
                        {
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "calculations_desription_number '0'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "calculations_description_number = '0'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "calculations_description_number = '0'");
                        }
                    }
                    else if (cbSearch.SelectedIndex == 3)
                    {
                        if (tbSearch.Text != "")// поиск по формуле
                        {
                            listSearch = db.GetRows("formulas", "id_of_formula", "name_of_formula = '" + tbSearch.Text + "'");
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "id_of_formula ='" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "id_of_formula = '" + listSearch[0][0] + "'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "id_of_formula = '" + listSearch[0][0] + "'");
                        }
                        else
                        {
                            if (tabControl1.SelectedIndex == 0)//маркер
                                list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, Id, Name_Object", "id_of_formula ='0'");
                            if (tabControl1.SelectedIndex == 1)//полигон
                                listIdPoligons = db.GetRows("poligon_calculations_description", "id_poligon", "id_of_formula = '0'");
                            if (tabControl1.SelectedIndex == 2)//водопрод
                                listIdPoligons = db.GetRows("tubes", "id_of_tube", "id_of_formula = '0'");
                        }
                    }
                    if (tabControl1.SelectedIndex == 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])db.GetValue("type_of_object", "Image", "Id = " + list[i][3]));
                            Image img = Image.FromStream(image_stream);
                            newMap.AddMarkerOnMap(list[i][2].ToString(), Convert.ToDouble(list[i][0]), Convert.ToDouble(list[i][1]), list[i][4].ToString(), (Bitmap)img, list[i][5].ToString());
                        }
                    }
                    for (int i = 0; i < listIdPoligons.Count; i++)
                    {
                        listPoligon = db.GetRows("poligon", "Id_of_poligon, brush_color_r, bruch_color_g, brush_color_b, name, id_of_expert, description", "type = 'poligon' AND Id_of_poligon = '" + listIdPoligons[i][0] + "'");
                        var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[0][0]);
                        for (int j = 0; j < poligonCount.Count; j++)
                        {
                            listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[0][0] + " AND order123 = " + (j + 1).ToString());
                            points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                        }
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[0][0] + " AND order123 = 1");
                        newMap.AddMarkerOnMap(listPoligon[0][6].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[0][5].ToString(), null, listPoligon[0][4].ToString());
                        newMap.DrawPolygon(Convert.ToInt16(listPoligon[0][1]), Convert.ToInt16(listPoligon[0][2]), Convert.ToInt16(listPoligon[0][3]), points);
                        points.Clear();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("По запиту \"" + tbSearch.Text + "\" нічого не знайдено");
                }
            }
            newMap.ZoomAdd();
            newMap.ZoomMinus();
        }
    }
}