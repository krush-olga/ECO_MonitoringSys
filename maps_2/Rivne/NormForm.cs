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
    public partial class NormForm : Form
    {
        Data.DBManager db;
        GMap.NET.WindowsForms.GMapMarker _item;
        public NormForm(Data.DBManager db, GMap.NET.WindowsForms.GMapMarker _item)
        {
            InitializeComponent();
            this.db = db;
            this._item = _item;
            FillGrid();
        }
        void FillGrid()
        {
            var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
            var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
            List<List<Object>> listElements;
            if (idPoi != null)
                listElements = db.GetRows("norm_result", "valueAvg, valueMax", "idMarker = " + idPoi);
            else
                listElements = db.GetRows("norm_result", "valueAvg, valueMax", "idPolygon = " + idPoligon);

            for (int i = 0; i < listElements.Count; i++)
            {
                dataGridView1.Rows.Add(listElements[i][0], listElements[i][1]);

            }
        }
    }
}
