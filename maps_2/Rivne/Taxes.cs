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
    public partial class Taxes : Form
    {
        Data.DBManager db;
        GMap.NET.WindowsForms.GMapMarker _item;
        List<Object> danger_c = new List<Object>();
        List<List<Object>> listElements = new List<List<Object>>();
        double tax_sum = 0;
        String date = "";
        private RadioButton oldValue = new RadioButton();
            List<List<Object>> ids;
        public Taxes(Data.DBManager db, GMap.NET.WindowsForms.GMapMarker _item)
        {
            InitializeComponent();
            this.db = db;
            this._item = _item;
            FillGrid();
        }
        private void FillGrid()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            List<List<Object>> elements = db.GetRows("taxes_on_map", "", "");
            List<Object> names = new List<Object>();
            var nameObj = db.GetValue("poi", "Name_Object", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
            label2.Text = nameObj.ToString();

            var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));// ПОЛУЧАЕМ КОДЫ МАРКЕРА И ПОЛИГОНА
            var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
            if (idPoi != null)// ЗАПОЛНЯЕМ МАССИВЫ ДЛЯ ДАЛЬНЕЙШЕГО ЗАПОЛНЕНИЯ ТАБЛИЦ И РАБОТЫ С ЭТИМИ МАССИВАМИ
            {
                ids = db.GetRows("emissions_on_map", "distinct idElement", "idPoi = '" + idPoi + "'");
                for (int i = 0; i < ids.Count; i++)
                {
                    var tmp = db.GetRows("emissions_on_map", "id, idPoi, idElement, idEnvironment, Year, Month, Day", $"idElement = '{ids[i][0]}' AND idPoi = '{idPoi}'");
                    listElements.Add(tmp[0]);
                    names.Add(db.GetValue("elements", "short_name", "code = '" + ids[i][0] + "'"));
                    danger_c.Add(db.GetValue("gdk", "danger_class", "code = '" + ids[i][0] + "'"));
                    elements = db.GetRows("taxes_on_map", "", $"idEmission = {listElements[i][0]} AND idMarker = '{idPoi}'");
                }
            }
            else
            {
                ids = db.GetRows("emissions_on_map", "distinct idElement", "idPoligon = '" + idPoligon + "'");
                for (int i = 0; i < ids.Count; i++)
                {
                    var tmp = db.GetRows("emissions_on_map", "id, idPoi, idElement, idEnvironment, Year, Month, Day", $"idElement = '{ids[i][0]}' AND idPoi = '{idPoi}'");
                    listElements.Add(tmp[0]);
                    names.Add(db.GetValue("elements", "short_name", "code = '" + ids[i][0] + "'"));
                    danger_c.Add(db.GetValue("gdk", "danger_class", "code = '" + ids[i][0] + "'"));
                    elements = db.GetRows("taxes_on_map", "", $"idEmission = {listElements[i][0]} AND idMarker = '{idPoi}'");
                }
            }
            if (elements.Count != 0)// ЕСЛИ У НАС УЖЕ ЕСТЬ ЗНАЧЕНИЯ НАЛОГОВ, ТО ЗАПОЛНЯЕМ ВСЮ ТАБЛИЦУ
                for (int i = 0; i < names.Count; i++)
                {
                    comboBox1.Items.Add(names[i]);
                    dataGridView1.Rows.Add(names[i]);
                    if (db.GetValue("tax_values", "tax", $"id_of_element = '{ids[i][0]}'") == null)
                    {
                        if (danger_c[i] == null || danger_c[i].ToString() == "-")
                        {
                            if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) <= 0.0001)
                                dataGridView1.Rows[i].Cells[1].Value = 738187.86;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.0001 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.001)
                                dataGridView1.Rows[i].Cells[1].Value = 63247.95;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.001 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.01)
                                dataGridView1.Rows[i].Cells[1].Value = 8737.07;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.01 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.1)
                                dataGridView1.Rows[i].Cells[1].Value = 2451.84;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.1)
                                dataGridView1.Rows[i].Cells[1].Value = 92.37;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 1)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 17536.42;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 2)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 4016.11;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 3)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 598.4;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 4)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 138.57;
                        }
                    }
                    else
                        dataGridView1.Rows[i].Cells[1].Value = db.GetValue("tax_values", "tax", $"id_of_element = '{ids[i][0]}'");
                    dataGridView1.Rows[i].Cells[2].Value = db.GetValue("taxes_on_map", "valueOfEmission", $"idElement = '{ids[i][0]}' AND idMarker = '{idPoi}'");
                    dataGridView1.Rows[i].Cells[3].Value = db.GetValue("taxes_on_map", "tax_to_pay", $"idElement = '{ids[i][0]}' AND idMarker = '{idPoi}'");
                    tax_sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                    label4.Text = tax_sum.ToString();

                }
            else// ЕСЛИ НЕТ, ТО ЗАПОЛНЯЕМ ТОЛЬКО НАЗВАНИЯ И СТАВКИ
                for (int i = 0; i < names.Count; i++)
                {
                    comboBox1.Items.Add(names[i]);
                    dataGridView1.Rows.Add(names[i]);
                    if (db.GetValue("tax_values", "tax", $"id_of_element = '{ids[i][0]}'") == null)
                    {
                        if (danger_c[i].ToString() == "-")
                        {
                            if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) <= 0.0001)
                                dataGridView1.Rows[i].Cells[1].Value = 738187.86;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.0001 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.001)
                                dataGridView1.Rows[i].Cells[1].Value = 63247.95;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.001 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.01)
                                dataGridView1.Rows[i].Cells[1].Value = 8737.07;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.01 && Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][0] + "'")) <= 0.1)
                                dataGridView1.Rows[i].Cells[1].Value = 2451.84;
                            else if (Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'")) > 0.1)
                                dataGridView1.Rows[i].Cells[1].Value = 92.37;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 1)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 17536.42;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 2)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 4016.11;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 3)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 598.4;
                        }
                        else if (Convert.ToInt32(danger_c[i]) == 4)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 138.57;
                        }
                    }
                    else
                        dataGridView1.Rows[i].Cells[1].Value = db.GetValue("tax_values", "tax", $"id_of_element = '{ids[i][0]}'");
                }
            tax_sum = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<List<Object>> dateResult;
            String[] taxes = new String[dataGridView1.Rows.Count - 1];
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                date = listElements[i][4].ToString() + "-" + listElements[i][5].ToString() + "-" + listElements[i][6].ToString();
                dateResult = db.GetRows("taxes_on_map", "", "idEmission = " + listElements[i][0] + " AND date = '" + date + "'");
                if(dateResult.Count == 0)
                {
                    if(db.GetValue("tax_values", "tax", $"id_of_element = '{listElements[i][2]}'") != null)// ЕСЛИ ЕСТЬ СТАВКА
                    {
                        taxes[i] = count_Tax(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), Convert.ToDouble(db.GetValue("tax_values", "tax", $"id_of_element = '{ listElements[i][2]}'"))).ToString();
                    }
                    else if(db.GetValue("tax_values", "tax", $"id_of_element = '{listElements[i][2]}'") == null && db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'") != null && danger_c[i] != null)//ЕСЛИ НЕТ СТАВКИ, НО ЕСТЬ ГДК
                    {
                        taxes[i] = search_Tax(danger_c[i].ToString(), Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'"))).ToString();
                    }
                    else// ЕСЛИ НЕТ НИ СТАВКИ НИ ГДК
                    {
                        taxes[i] = search_Tax("-", Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), 0).ToString();
                    }
                    //if (db.GetValue("tax_values", "tax", $"id_of_element = '{listElements[i][2]}'") == null  && db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'") != null && danger_c[i] != null)
                      //  taxes[i] = search_Tax(danger_c[i].ToString(), Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'"))).ToString();
                    //else if(db.GetValue("tax_values", "tax", $"id_of_element = '{listElements[i][2]}'") == null && db.GetValue("gdk", "tsel", "code = '" + listElements[i][2] + "'") == null && danger_c[i] == null)
                      //  taxes[i] = search_Tax("-", Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), 0).ToString();
                    //else taxes[i] = count_Tax(Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value), Convert.ToDouble(db.GetValue("tax_values", "tax", $"id_of_element = '{ listElements[i][2]}'"))).ToString();
                    ///////////////////
                    var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
                    var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
                    try
                    {
                        var id = db.GetRows("taxes_on_map", "id", "");
                        if (id.Count != 0)
                        {
                            if (idPoi != null)// ДЛЯ МАРКЕРА
                                db.InsertToBD("taxes_on_map", $"'{Convert.ToInt32(id.Last()[0]) + 1}', '{Convert.ToInt32(listElements[i][0])}', '{Convert.ToInt32(idPoi)}', '{0}','{Convert.ToInt32(listElements[i][2])}'," +
                                $"'{dataGridView1.Rows[i].Cells[1].Value}','{Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value)}','{taxes[i].Replace(",", ".")}','{ date }'");
                            else if (idPoligon != null)// ДЛЯ ПОЛИГОНА
                                db.InsertToBD("taxes_on_map", $"'{Convert.ToInt32(id.Last()[0]) + 1}', '{Convert.ToInt32(listElements[i][0])}', '{0}', '{Convert.ToInt32(idPoligon)}','{Convert.ToInt32(listElements[i][2])}'," +
                                $"'{dataGridView1.Rows[i].Cells[1].Value}','{Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value)}','{taxes[i].Replace(",", ".")}','{ date }'");
                        }
                        else
                        {
                            if (idPoi != null)// ДЛЯ МАРКЕРА
                                db.InsertToBD("taxes_on_map", $"'{0}', '{Convert.ToInt32(listElements[i][0])}', '{Convert.ToInt32(idPoi)}', '{0}','{Convert.ToInt32(listElements[i][2])}'," +
                                $"'{dataGridView1.Rows[i].Cells[1].Value}','{Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value)}','{taxes[i].Replace(",", ".")}','{ date }'");
                            else if (idPoligon != null)// ДЛЯ ПОЛИГОНА
                                db.InsertToBD("taxes_on_map", $"'{0}', '{Convert.ToInt32(listElements[i][0])}', '{0}', '{Convert.ToInt32(idPoligon)}','{Convert.ToInt32(listElements[i][2])}'," +
                                $"'{dataGridView1.Rows[i].Cells[1].Value}','{Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value)}','{taxes[i].Replace(",", ".")}','{ date }'");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            FillGrid();
        }

        private double count_Tax(Double value, Double tax)// ПОДСЧЁТ ДЛЯ ЭЛЕМЕНТОВ СО СТАВКОЙ
        {
            Double res = value * tax;
            return res;
        }

        private double search_Tax(String danger_Class, double value, double TSEL)// ПОИСК ЧЕРЕЗ КЛАСС ОПАСНОСТИ
        {
            double res = 0;
            if (danger_Class == "-")
            {
                res = search_by_TSEL(TSEL, value);
            }
            else if(danger_Class == "1")
            {
                res = 17536.42 * value;
            }
            else if(danger_Class == "2")
            {
                res = 4016.11 * value;
            }
            else if(danger_Class == "3")
            {
                res = 598.4 * value;
            }
            else if(danger_Class == "4")
            {
                res = 138.57 * value;
            }
            return res;
        }

        private double search_by_TSEL(double TSEL, double value)// ПОИСК ЧЕРЕЗ ОБУВ
        {
            double res = 0;
            if (TSEL <= 0.0001)
                res = 738187.86 * value;
            else if (TSEL > 0.0001 && TSEL <= 0.001)
                res = 63247.95 * value;
            else if (TSEL > 0.001 && TSEL <= 0.01)
                res = 8737.07 * value;
            else if (TSEL > 0.01 && TSEL <= 0.1)
                res = 2451.84 * value;
            else if (TSEL > 0.1)
                res = 92.37 * value;
            return res;
        }

        private void button2_Click(object sender, EventArgs e)// КНОПКА УДАЛЕНИЯ
        {
            String[] cols = { "idElement", "idMarker", "idEmission" };
            var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
            var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String[] vals = { ids[i][0].ToString(), idPoi.ToString(), listElements[i][0].ToString() };
                    db.DeleteFromDB("taxes_on_map", cols, vals);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Щось пішло не так, спробуйте ще раз");
            }
            FillGrid();
        }

        private void button3_Click(object sender, EventArgs e)// КНОПКА РЕДАКТИРОВАНИЯ
        {
            try
            {
                if(comboBox1.SelectedItem != null)
                {
                    if(db.GetValue("tax_values", "tax", $"id_of_element = '{db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'").ToString()}'") != null)
                    {
                        var taxes = count_Tax(Convert.ToDouble(textBox1.Text), Convert.ToDouble(db.GetValue("tax_values", "tax", $"id_of_element = '{db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'").ToString()}'")));
                        db.SetValue("taxes_on_map", "valueOfEmission", textBox1.Text, "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                        db.SetValue("taxes_on_map", "tax_to_pay", "'" + taxes + "'", "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                    }
                    else if (db.GetValue("gdk", "danger_class", "code = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'") != null)// ЕСЛИ ЕСТЬ ГДК
                    {
                        var taxes = search_Tax(db.GetValue("gdk", "danger_class", "code = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'").ToString(),
                            Convert.ToDouble(textBox1.Text), Convert.ToDouble(db.GetValue("gdk", "tsel", "code = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'"))).ToString();
                        db.SetValue("taxes_on_map", "valueOfEmission", textBox1.Text, "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                        db.SetValue("taxes_on_map", "tax_to_pay", "'" + taxes + "'", "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                    }
                    else// ЕСЛИ НЕТ ГДК
                    {
                        var taxes = search_Tax("-", Convert.ToDouble(textBox1.Text), 0).ToString();
                        db.SetValue("taxes_on_map", "valueOfEmission", textBox1.Text, "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                        db.SetValue("taxes_on_map", "tax_to_pay", "'" + taxes + "'", "idElement = '" + db.GetValue("elements", "code", "short_name = '" + comboBox1.Text + "'") + "'");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            FillGrid();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selected = (RadioButton)sender;

            if (oldValue.Text == selected.Text) return;

                    label5.Visible = false;
                    label6.Visible = false;
                    comboBox1.Visible = false;
                    comboBox1.Enabled = false;
                    textBox1.Visible = false;
                    textBox1.Enabled = false;
                    button1.Enabled = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button3.Visible = false;

            switch (selected.Text)// РЕЖИМ РАБОТЫ
            {
                case "Додавання":
                    {
                        button1.Enabled = true;
                        button1.Visible = true;
                        break;
                    }
                case "Видалення":
                    {
                        button2.Visible = true;
                        button2.Enabled = true;
                        break;
                    }
                case "Редагування":
                    {
                        label5.Visible = true;
                        label6.Visible = true;
                        comboBox1.Visible = true;
                        comboBox1.Enabled = true;
                        textBox1.Visible = true;
                        textBox1.Enabled = true;
                        button3.Enabled = true;
                        button3.Visible = true;
                        break;
                    }
                    
            }
            oldValue = selected;
        }
    }
}
