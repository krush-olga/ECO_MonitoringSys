using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odessa
{
    public partial class Risks : Form
    {
        Data.DBManager db;
        GMap.NET.WindowsForms.GMapMarker _item;
        List<List<Object>> ids;
        List<List<Object>> listElements = new List<List<Object>>();
        public Risks(Data.DBManager db, GMap.NET.WindowsForms.GMapMarker _item, List<List<Object>> listElements)
        {
            this.db = db;
            this._item = _item;
            this.listElements = listElements;
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            Double sum = 0;
            List<Object> names = new List<Object>();
            label2.Text = db.GetValue("poi", "Name_Object", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.')).ToString();

            var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));// ПОЛУЧАЕМ КОДЫ МАРКЕРА И ПОЛИГОНА
            var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));

            if (idPoi != null)
            {
                ids = db.GetRows("emissions_on_map", "idElement", "idPoi = '" + idPoi + "'");
                for (int i = 0; i < ids.Count; i++)
                {
                    names.Add(db.GetValue("elements", "short_name", "code = '" + ids[i][0] + "'"));
                }
            }
            else
            {
                ids = db.GetRows("emissions_on_map", "idElement", "idPoligon = '" + idPoligon + "'");
                for (int i = 0; i < ids.Count; i++)
                    names.Add(db.GetValue("elements", "short_name", "code = '" + ids[i][0] + "'"));
            }
            for (int i = 0; i < names.Count; i++)
            {
                if (db.GetValue("gdk", "mpc_avrg_d", $"code = '{ids[i][0]}'") != null)
                {
                    dataGridView1.Rows.Add(names[i], db.GetValue("emissions_on_map", "ValueAvg", $"id = '{listElements[i][4]}'"), db.GetValue("gdk", "mpc_avrg_d", $"code = '{ids[i][0]}'").ToString());
                    var hq = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    dataGridView1.Rows[i].Cells[3].Value = hq;
                    sum += hq;
                }
                else
                {
                    dataGridView1.Rows.Add(names[i], db.GetValue("emissions_on_map", "ValueAvg", $"id = '{listElements[i][4]}'"), "ГДК ВІДСУТНЄ");
                    dataGridView1.Rows[i].Cells[3].Value = 0;
                    sum += 0;
                }
            }
            dataGridView1.Rows.Add("Сумарний ризик", sum);
        }
    }
}
