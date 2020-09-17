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
    public partial class EmissionsForm : Form
    {
        Data.DBManager db = new Data.DBManager();

        GMap.NET.WindowsForms.GMapMarker _item;
        int? idEnviroment = null;
        Elements formElements;
        Environment formEnvironment;
        string _nameElement;
        public EmissionsForm(GMap.NET.WindowsForms.GMapMarker item)
        {
            InitializeComponent();
            _item = item;

        }

        private void EmissionsForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            dataGridView1.Rows.Clear();
            var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
            var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
            List<List<Object>> listElements;
            if(idPoi != null)
                listElements = db.GetRows("emissions_on_map", "idElement, idEnvironment, ValueAvg, ValueMax, id, Year, Month, Day, Measure", "idPoi = " + idPoi);
            else 
                listElements = db.GetRows("emissions_on_map", "idElement, idEnvironment, ValueAvg, ValueMax, id, Year, Month, Day, Measure", "idPoligon = " + idPoligon);
            
            for (int i = 0; i < listElements.Count; i++)
            {
                string dateParse = ""; 
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                dateParse = listElements[i][5].ToString();
                if (listElements[i][6] != null)
                    dateParse += "-" + listElements[i][6];
                if (listElements[i][7] != null)
                    dateParse += "-" + listElements[i][7];
                var elem = db.GetValue("elements", "name", "code = " + listElements[i][0]);
                row.Cells[0].Value = (elem);
                var environ = db.GetValue("environment", "name", "id = " + listElements[i][1]);
                row.Cells[1].Value = (environ);
                row.Cells[2].Value = (listElements[i][2]);
                row.Cells[3].Value = (listElements[i][3]);
                row.Cells[4].Value = (listElements[i][8]);
                row.Cells[5].Value = (dateParse);
                row.Cells[6].Value = (listElements[i][4]);

                dataGridView1.Rows.Add(row);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formElements = new Elements();
            formElements.ShowDialog();
            _nameElement = formElements.CloseOK();
            textBox1.Text = _nameElement;
            var measure = db.GetValue("elements", "measure", "name = '" + _nameElement + "'");
            if (measure != null)
                textBox5.Text = measure.ToString();
            else
                textBox5.Text = "";
            RefreshValueGDK();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formEnvironment = new Environment();
            formEnvironment.ShowDialog();
            var row = formEnvironment.CloseOK();
            if (row != null)
            {
                textBox2.Text = row.Cells[0].Value.ToString();
                idEnviroment = Convert.ToInt32(row.Cells[1].Value);
                
            }
            else
                textBox2.Text = "";

            RefreshValueGDK();


        }
        private void RefreshValueGDK()
        {
            List<List<Object>> listGDK;
            label8.Text = "Не задано!";
            label9.Text = "Не задано!";

            if (textBox1.Text != "" && textBox2.Text != "" && idEnviroment != null)
            {
                var codeElemnt = db.GetValue("elements", "code", "name = '" + _nameElement + "'");
                listGDK = db.GetRows("gdk", "mpc_m_ot, mpc_avrg_d", "environment = " + idEnviroment + " AND code = " + codeElemnt);
                if(listGDK.Count > 0)
                {
                    label8.Text = listGDK[0][1].ToString();
                    label9.Text = listGDK[0][0].ToString();


                }
            }
        }

        private bool parseTextYear(string text)
        {
            bool response = true;
            for (int i = 0; i<text.Length; i++)
            {
                if ((text[i].ToString() == "".ToString() || text[i].ToString() == " ".ToString()) || text.Length != 4)
                {
                    response =  true;
                    break;
                }
                else
                    response = false;
                    
            }
            return response;
        }

        private void RecordDB(bool record)
        {
            string[] fieldName;
            string[] fieldValue;
            if (textBox3.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Задайте кiлькiсть викидiв");
            }
            else if (textBox5.Text == "")
                MessageBox.Show("Задайте одиниці виміру");
            else if (parseTextYear(maskedTextBox1.Text) == true)
                MessageBox.Show("Задайте повний рік");
            else if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "")
            {
                if (textBox3.Text == "")
                    textBox3.Text = "0";
                else if (textBox4.Text == "")
                    textBox4.Text = "0";
                var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
                var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
                var idEnviroment = db.GetValue("environment", "id", "name = '" + textBox2.Text+"'");

                var codeElemnt = db.GetValue("elements", "code", "name = '" + textBox1.Text + "'");
                if (idPoi != null)
                {

                    if (comboBox1.Text == "" && comboBox2.Text != "")
                    {
                        fieldName = new string[] { "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Measure" };
                        fieldValue = new string[] { idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, "'" + textBox5.Text + "'" };
                    }
                    else if (comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        fieldName = new string[] { "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Measure" };
                        fieldValue = new string[] { idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, "'" + textBox5.Text + "'" };

                    }
                    else
                    {

                        fieldName = new string[] { "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Day", "Measure" };
                        fieldValue = new string[] { idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, comboBox1.Text, "'" + textBox5.Text + "'" };
                    }


                }
                else
                {

                    if (comboBox1.Text == "" && comboBox2.Text != "")
                    {
                        fieldName = new string[] { "idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Measure" };
                        fieldValue = new string[] { idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, "'" + textBox5.Text + "'"};
                    }
                    else if (comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        fieldName = new string[] { "idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Measure" };
                        fieldValue = new string[] { idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, "'" + textBox5.Text + "'" };

                    }
                    else
                    {
                        fieldName = new string[] { "idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Day", "Measure" };
                        fieldValue = new string[] { idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, comboBox1.Text, "'" + textBox5.Text + "'" };
                    }
                }
                try
                {
                    string[] s1 = fieldName;
                    if(record == true)
                        db.InsertToBDWithoutId("emissions_on_map", fieldName, fieldValue);
                    else
                        db.UpdateRecord("emissions_on_map", fieldName, fieldValue);
                    FillGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                MessageBox.Show("Введiть всi поля");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordDB(true);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                db.DeleteFromDB("emissions_on_map", "id", dataGridView1.CurrentRow.Cells[6].Value.ToString());
                FillGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (number != 8)
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button6.Visible = true;
            button7.Visible = true;
            comboBox1.Text = "";
            comboBox2.Text = "";
            if(dataGridView1.Rows.Count != 1)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string _date = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string buff = "";
                    buff = _date.Substring(0, 4);
                    maskedTextBox1.Text = buff;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string _id = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            string[] fieldName;
            string[] fieldValue;
            if (textBox3.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Задайте кiлькiсть викидiв");
            }
            else if (textBox5.Text == "")
                MessageBox.Show("Задайте одиниці виміру");
            else if (parseTextYear(maskedTextBox1.Text) == true)
                MessageBox.Show("Задайте повний рік");
            else if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "")
            {
                if (textBox3.Text == "")
                    textBox3.Text = "0";
                else if (textBox4.Text == "")
                    textBox4.Text = "0";
                var idPoi = db.GetValue("poi", "id", "Coord_Lat = " + _item.Position.Lat.ToString().Replace(',', '.') + " AND " + "Coord_Lng = " + _item.Position.Lng.ToString().Replace(',', '.'));
                var idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " + _item.Position.Lat.ToString().Replace(',', '.'));
                var idEnviroment = db.GetValue("environment", "id", "name = '" + textBox2.Text + "'");

                var codeElemnt = db.GetValue("elements", "code", "name = '" + textBox1.Text + "'");
                if (idPoi != null)
                {

                    if (comboBox1.Text == "" && comboBox2.Text != "")
                    {
                        fieldName = new string[] { "id", "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Measure" };
                        fieldValue = new string[] { _id, idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, "'" + textBox5.Text + "'" };
                    }
                    else if (comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        fieldName = new string[] { "id", "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Measure" };
                        fieldValue = new string[] { _id, idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, "'" + textBox5.Text + "'" };

                    }
                    else
                    {

                        fieldName = new string[] { "id", "idPoi", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Day", "Measure" };
                        fieldValue = new string[] { _id, idPoi.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, comboBox1.Text, "'" + textBox5.Text + "'" };
                    }


                }
                else
                {

                    if (comboBox1.Text == "" && comboBox2.Text != "")
                    {
                        fieldName = new string[] { "id","idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Measure" };
                        fieldValue = new string[] { _id, idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, "'" + textBox5.Text + "'"};
                    }
                    else if (comboBox2.Text == "" && comboBox1.Text == "")
                    {
                        fieldName = new string[] { "id","idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Measure" };
                        fieldValue = new string[] { _id, idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, "'" + textBox5.Text + "'"};

                    }
                    else
                    {
                        fieldName = new string[] { "id","idPoligon", "idElement", "idEnvironment", "ValueAvg", "ValueMax", "Year", "Month", "Day", "Measure" };
                        fieldValue = new string[] { _id, idPoligon.ToString(), codeElemnt.ToString(), idEnviroment.ToString(), textBox3.Text, textBox4.Text, maskedTextBox1.Text, comboBox2.Text, comboBox1.Text, "'" + textBox5.Text + "'" };
                    }
                }
                try
                {
                    db.UpdateRecord("emissions_on_map", fieldName, fieldValue);
                    FillGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                MessageBox.Show("Введiть всi поля");

            }

            button6.Visible = false;
            button7.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            button6.Visible = false;
            button7.Visible = false;


            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            maskedTextBox1.Text = "";

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 46)
            {
                e.Handled = true;
            }
        }
    }
}