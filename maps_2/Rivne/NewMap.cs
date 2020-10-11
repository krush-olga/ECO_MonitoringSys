using Data;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GMap.NET.MapProviders;


namespace Maps
{
    public class NewMap
    {
        public GMapMarker currentMarker;
        public GMapOverlay markersOverlay { private set; get; }
        public List<PointLatLng> _points { private set; get; }
        public List<PointLatLng> _pointsTube { private set; get; }

        private GMapControl gMapControl;
        private double R = 6378.137;
        private DBManager db = new DBManager();


        //public void ForTest(NumericUpDown numericUpDown1, ColorDialog colorDialog1)
        //{
        //    if(_points.Count == 2)
        //    {
        //        DrawPolygon(numericUpDown1, colorDialog1);
        //    }
        //    _points.RemoveAt(0);
        //}



        public NewMap(GMapControl gm)
        {
            gMapControl = gm;
        }

        public void MapInitialize()
        {
            gMapControl.DragButton = MouseButtons.Left;
            
            gMapControl.MapProvider = GMapProviders.GoogleMap;
            gMapControl.Position =  new GMap.NET.PointLatLng(Convert.ToDouble(50.449173), Convert.ToDouble(30.457578));
            gMapControl.MaxZoom = 18;
            gMapControl.MinZoom = 2;
            gMapControl.Zoom = 12;
            _points = new List<PointLatLng>();
            _pointsTube = new List<PointLatLng>();
            markersOverlay = new GMapOverlay();
        }
        
        //метод для выборки типа маркера(стандартный для отрисовки полигона или с картинкой для объекта)
        private GMapMarker TypeOfMarker(Bitmap img, PointLatLng localPoint)
        {
            GMapMarker marker;
            if (img != null)
                marker = new GMarkerGoogle(localPoint, img); 
            else
                marker = new GMarkerGoogle(localPoint, GMarkerGoogleType.arrow);
            return marker;

        }
        //отрисовка(добавления) маркеров загрязнения на карту
        public void AddMarkerOnMap(string description, double X, double Y, string id, Bitmap img, string NameObject)
        {
            PointLatLng localPoint = new PointLatLng(X, Y);
            GMapOverlay localOverlay = new GMapOverlay("experts");
            GMapMarker marker = TypeOfMarker(img, localPoint);
            currentMarker = marker;
            List<List<Object>> userName; 
            userName = db.GetRows("user", "user_name", $"id_of_user = '{id}'");
            marker.ToolTipText = "Експерт: "+ userName[0][0] +"\nНазва:" + NameObject + "\nОпис:" + description;
            var toolTip = new GMapToolTip(marker);
            toolTip.Fill = new SolidBrush(Color.Black);
            toolTip.Foreground = new SolidBrush(Color.White);
            marker.ToolTip = toolTip;

            localOverlay.Markers.Add(marker);
            gMapControl.Overlays.Add(localOverlay);
         
        }
        public double [] AddMarkerOnMap(string description, int X, int Y, Bitmap img, bool tube)
        {
            var point = gMapControl.FromLocalToLatLng(X, Y);
            double lat = point.Lat;
            double lng = point.Lng;
            double [] pointMarker = { lat, lng};
            var marker = TypeOfMarker(img, point);
            currentMarker = marker;
            _points.Add(new PointLatLng(lat, lng));//
            var toolTip = new GMapToolTip(marker);
            toolTip.Fill = new SolidBrush(Color.Black);
            toolTip.Foreground = new SolidBrush(Color.White);
            marker.ToolTip = toolTip;
            markersOverlay.Markers.Add(marker);
            gMapControl.Overlays.Add(markersOverlay);
            MapReload();
            return pointMarker;
        }
        //добавления маркеров водопровода на карту
        public void AddPointTube(int X, int Y)
        {
            var point = gMapControl.FromLocalToLatLng(X, Y);
            double lat = Convert.ToDouble(point.Lat);
            double lng = Convert.ToDouble(point.Lng);
            var marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);

            _pointsTube.Add(new PointLatLng(lat, lng));
            _points.Add(new PointLatLng(lat, lng));//
            var toolTip = new GMapToolTip(marker);
            toolTip.Fill = new SolidBrush(Color.Black);
            toolTip.Foreground = new SolidBrush(Color.White);
            marker.ToolTip = toolTip;
            markersOverlay.Markers.Add(marker);
            gMapControl.Overlays.Add(markersOverlay);
            MapReload();

        }
        //добавления маркеров водопровода на карту

        public void AddPointTube(double lat, double lng, string id, string name, string description)
        {
            PointLatLng point = new PointLatLng(lat, lng);
            var marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);

            _pointsTube.Add(new PointLatLng(lat, lng));
            _points.Add(new PointLatLng(lat, lng));//
            var toolTip = new GMapToolTip(marker);
            List<List<Object>> userName;
            userName = db.GetRows("user", "user_name", $"id_of_user = '{id}'");
            marker.ToolTipText = "Експерт: " + userName[0][0] + "\nНазва:" + name + "\nОпис:" + description;
            toolTip.Fill = new SolidBrush(Color.Black);
            toolTip.Foreground = new SolidBrush(Color.White);
            marker.ToolTip = toolTip;
            markersOverlay.Markers.Add(marker);
            gMapControl.Overlays.Add(markersOverlay);
            MapReload();

        }

       //отрисовка полигона на карте
        public void DrawPolygon(NumericUpDown numericUpDown1, ColorDialog colorDialog1)
        {
            int opacityValue = 30;
            if (numericUpDown1.Value <= 100 && numericUpDown1.Value >= 1)
                opacityValue = Convert.ToInt16(numericUpDown1.Value);
            var polygon = new GMapPolygon(_points, "My Area")
            {
                Stroke = new Pen(Color.Black),
                Fill = new SolidBrush(Color.FromArgb(opacityValue, colorDialog1.Color))
                
            };
            markersOverlay.Polygons.Add(polygon);
            gMapControl.Overlays.Add(markersOverlay);
            MapReload();
        }

        //отрисовка водопровода на карте
        public void DrawTube()
        {
            var polygon = new GMapPolygon(_pointsTube, "My Area")
            {
                Stroke = new Pen(Color.Black, 4),
                Fill = new SolidBrush(Color.FromArgb(0, Color.Black))
            };
            markersOverlay.Polygons.Add(polygon);
            gMapControl.Overlays.Add(markersOverlay);
            MapReload();
        }

        //отрисовка полигона на карте

        public void DrawPolygon(int r, int g, int b, List<PointLatLng> p)
        {
            GMapOverlay localOverlay = new GMapOverlay("experts");

            int opacityValue = 25;
            var polygon = new GMapPolygon(p, "My Area")
            {
                Stroke = new Pen(Color.Black),
                Fill = new SolidBrush(Color.FromArgb(opacityValue, r,g,b))
            };
            localOverlay.Polygons.Add(polygon);
            gMapControl.Overlays.Add(localOverlay);
            MapReload();
        }
        //заполнения серии пармаметров
        public void FillFormulas(int id_expert, ComboBox cbForm)
        {

            var formulas = db.GetRows("formulas", "description_of_formula",
               "id_of_expert=" + id_expert +
               "");

            for (int i = 0; i < formulas.Count; i++)
            {
                cbForm.Items.Add(Convert.ToString(formulas[i][0]));
            }
        }
        //сохранения водопровода на карту 
        public void SaveTubeToDB(string id_of_user, string descriptionText, string name, string idOfIssue, string calNum, string idOfFormula)
        {
            int maXNum;
            //get the max poligon id0
            var res_points = db.GetRows("poligon", "max(Id_of_poligon)", "");
            if (res_points[0][0] is DBNull)
                maXNum = 1;
            else
                maXNum = Convert.ToInt16(res_points[0][0]) + 1;
            if (_points.Count >= 2)
            {

                string[] fields = { "Id_of_poligon", "brush_color_r", "bruch_color_g", "brush_color_b", "brush_alfa", "line_collor_r", "line_color_g", "line_color_b",
                "line_alfa", "line_thickness", "name","id_of_user", "description", "type"};
                string[] val = { Convert.ToString(maXNum), "0", "0", "0", "250", "0", "250", "2", "21", "2", $"'{name}'", id_of_user, $"'{descriptionText}'", $"'tube'" };
                db.InsertToBD("poligon", fields, val);
                //points
                string[] fields_1 = { "longitude", "latitude", "Id_of_poligon", "order123" };
                int count = 1;
                foreach (PointLatLng points in _points)
                {
                    string[] va1 = { points.Lat.ToString().Replace(',', '.'), points.Lng.ToString().Replace(',', '.'), Convert.ToString(maXNum), Convert.ToString(count) };
                    db.InsertToBD("point_poligon", fields_1, va1);
                    count++;
                }
                string[] fields2 = { "id_of_tube", "id_of_issue", "calculations_description_number", "id_of_formula" };
                string[] val2 = { Convert.ToString(maXNum), idOfIssue, calNum, idOfFormula };
                db.InsertToBD("tubes", fields2, val2);
            }
        }
        //сохранения полигона
        public void SaveCoordinateToDataBase(string id_issue, string id_calc, string id_formula, string id_of_user, ColorDialog colorDialog, string descriptionText, string name)
        {
            int maXNum;
            //get the max poligon id0
            var res_points = db.GetRows("poligon", "max(Id_of_poligon)", "");
            if (res_points[0][0] is DBNull)
                maXNum = 1;
            else
                maXNum = Convert.ToInt16(res_points[0][0]) + 1;
            if (descriptionText == "")
                descriptionText = "Полiгон опису не мав";
            if (name == "")
                name = "Полігон без назви";
            if(_points.Count != 0)
            {

            string[] fields = { "Id_of_poligon", "brush_color_r", "bruch_color_g", "brush_color_b", "brush_alfa", "line_collor_r", "line_color_g", "line_color_b",
                "line_alfa", "line_thickness", "name", "id_of_user", "description", "type"};
            string[] val = { Convert.ToString(maXNum), colorDialog.Color.R.ToString(), colorDialog.Color.G.ToString(), colorDialog.Color.B.ToString(), "250", "0", "250", "2", "21", "2", $"'{name}'", id_of_user, $"'{descriptionText}'", $"'poligon'" };
            db.InsertToBD("poligon", fields, val);
            //points
            string[] fields_1 = { "longitude", "latitude", "Id_of_poligon", "order123" };
            int count = 1;
            foreach (PointLatLng points in _points)
            {
                string[] va1 = { points.Lat.ToString().Replace(',', '.'),points.Lng.ToString().Replace(',', '.'), Convert.ToString(maXNum), Convert.ToString(count) };
                db.InsertToBD("point_poligon", fields_1, va1);
                count++;
            }

            string[] fields2 = { "id_poligon", "id_of_issue" ,"calculations_description_number", "id_of_formula" };
            string[] val2 = { Convert.ToString(maXNum), id_issue, id_calc, id_formula };
            db.InsertToBD("poligon_calculations_description", fields2, val2);
            }

        }

        //Добавить в БД объект
        public void AddObjectToDB(string id_of_user, int type, int owner_type, double lat, double lng, string descriptions, string NameObject, string idOfIssue, string calNumber, string idOfFormula)
        {
            string[] field = { "id_of_user", "Type", "owner_type", "Coord_Lat", "Coord_Lng", "Description",
                "Name_Object", "id_of_issue", "calculations_description_number", "id_of_formula"};
            string[] values = { $"'{id_of_user}'", type.ToString(), owner_type.ToString(), lat.ToString().Replace(',', '.'),
                lng.ToString().Replace(',', '.'), "'"+descriptions+"'", "'" + NameObject + "'",
                                "'" + idOfIssue + "'", "'" + calNumber + "'", "'" + idOfFormula + "'"};
            db.InsertToBDWithoutId("poi", field, values);
        }
        //Рисует маркеры загрязнения с БД
        public void AddObjectFromDB()
        {
            List<List<Object>> list;
            gMapControl.Overlays.Clear();
            gMapControl.Show();
            int i = 0;
            list = db.GetRows("poi", "Coord_Lat, Coord_Lng, Description, Type, id_of_user, Name_Object", "");
            try
            {
                for (i = 0; i < list.Count; i++)
                {
                    var imageObj = db.GetValue("type_of_object", "Image", "Id = " + list[i][3]);
                    Image img;

                    if (imageObj != null && imageObj != DBNull.Value)
                    {
                        System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])imageObj);
                        img = Image.FromStream(image_stream);
                    }
                    else
                    {
                        img = Image.FromFile($@"{System.Environment.CurrentDirectory}\Resources\noimage.png");
                    }
                    AddMarkerOnMap(list[i][2].ToString(), Convert.ToDouble(list[i][0]), Convert.ToDouble(list[i][1]), list[i][4].ToString(), (Bitmap)img, list[i][5].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Помилка при зображенні об'єкта");
            }
        }
        //выгрузка маркеров загрязнения с БД
        public void AddObjectFromDB(string condition, int userId)
        {
            List<List<Object>> list;

            gMapControl.Overlays.Clear();
            gMapControl.Show();

            list = db.GetRows("poi, user", "poi.Coord_Lat, poi.Coord_Lng, poi.Description, poi.Type, poi.Name_Object", condition);

            for (int i = 0; i < list.Count; i++)
            {
                var imageObj = db.GetValue("type_of_object", "Image", "Id = " + list[i][3]);
                Image img;

                if (imageObj != null && imageObj != DBNull.Value)
                {
                    System.IO.MemoryStream image_stream = new System.IO.MemoryStream((byte[])imageObj);
                    img = Image.FromStream(image_stream);
                }
                else
                {
                    img = Image.FromFile($@"{System.Environment.CurrentDirectory}\Resources\noimage.png");
                }

                AddMarkerOnMap(list[i][2].ToString(), Convert.ToDouble(list[i][0]), Convert.ToDouble(list[i][1]), userId.ToString(), (Bitmap)img, list[i][4].ToString());
            }
        }
        //очистка карты
        public void OverlayClear()
        {
            _points.Clear();
            markersOverlay.Clear();
            gMapControl.Overlays.Clear();
            gMapControl.Show();
            MapReload();
        }

        public void RemoveItem(GMapMarker currentMarker)
        {
            for(int i = 0; i < gMapControl.Overlays.Count; i++)
            {
                if (gMapControl.Overlays[i].Equals(currentMarker))
                    gMapControl.Overlays[i].Clear();

            }
        }

        public void ZoomMinus()
        {
            gMapControl.Zoom -= 1;
        }
        public void ZoomAdd()
        {
            gMapControl.Zoom += 1;
        }

        public void highlight_polygon_from_table(string calc_id, string param_id, int zoom, int expert_id)
        {
            //очищать полигон перед отрисовкой
            OverlayClear();
            //!!!


            List<List<Object>> listPoligon;
            List<List<Object>> listPointPoligon;
            List<GMap.NET.PointLatLng> points = new List<GMap.NET.PointLatLng>();
            try{
                listPoligon = db.GetRows("poligon, poligon_calculations_description", "Id_of_poligon, name, id_of_expert, description", "poligon_calculations_description.calculations_description_number=" + calc_id +
                    " and  poligon.Id_of_poligon=poligon_calculations_description.id_poligon and poligon_calculations_description.id_of_formula=" + param_id);

                if(listPoligon.Count > 0)
                {
                    for (int i = 0; i < listPoligon.Count; i++)
                    {
                        var poligonCount = db.GetRows("point_poligon", "latitude", "Id_of_poligon = " + listPoligon[i][0]);
                        for (int j = 0; j < poligonCount.Count; j++)
                        {
                            listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = " + (j + 1).ToString());
                            points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0])));
                        }
                        listPointPoligon = db.GetRows("point_poligon", "latitude, longitude", "Id_of_poligon = " + listPoligon[i][0] + " AND order123 = 1");
                        AddMarkerOnMap(listPoligon[i][3].ToString(), Convert.ToDouble(listPointPoligon[0][1]), Convert.ToDouble(listPointPoligon[0][0]), listPoligon[i][2].ToString(), null, listPoligon[i][1].ToString());
                        DrawPolygon(0, 75, 65, points);
                        points.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Для обраної серії розрахунків та параметру відсутня інформація про зони забруднення! Або задана неповна інформація.");
                }
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        
        public void MapReload()
        {
            ZoomMinus();
            ZoomAdd();
        }


        //старый чей-то код, говорили оставить 
        public double ToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public double cArea()
        {
            var over = from a in gMapControl.Overlays where a.Id == "polygons" select a;
            if (over.Count() == 0) return 0;
            double area1 = 0;
            var polygons = over.First().Polygons;
            foreach (var polygon in polygons)
            {
                double area = 0;
                var points = polygon.Points;
                if (points.Count > 2)
                {
                    for (var i = 0; i < points.Count - 1; i++)
                    {
                        var p1 = points[i];
                        var p2 = points[i + 1];
                        area += ToRad(p2.Lng - p1.Lng) * (2 + Math.Sin(ToRad(p1.Lat))
                           + Math.Sin(ToRad(p2.Lat)));
                    }
                    var p3 = points.Last();
                    var p4 = points.First();
                    area += ToRad(p4.Lng - p3.Lng) * (2 + Math.Sin(ToRad(p3.Lat))
                       + Math.Sin(ToRad(p4.Lat)));

                    area = area * R * R / 2;
                }
                area1 = Math.Abs(area);
            }
            return area1;
        }
    }
}
