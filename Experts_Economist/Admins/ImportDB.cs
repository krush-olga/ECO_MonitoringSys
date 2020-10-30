using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;
using System.IO;
using System.Text;

namespace Experts_Economist
{
    public partial class ImportDB : Form
    {
        const char TAB_CHAR = '\t';
        
        private DBManager db = new DBManager();
        private Parser csvParser;
        private ParserToDB csvToDB;
        private List<string> tableColumsList = new List<string>();
        private List<string> userColumnsList = new List<string>();
        private int tik = 0;
        

        public ImportDB()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private List<List<object>> getTableColumnsName(string schema, string table)
        {
            //SELECT COLUMN_NAME FROM information_schema.columns WHERE table_schema='experts' AND table_name='test'

            return db.GetRows("information_schema.columns", "COLUMN_NAME", $"table_schema='{schema}' AND table_name='{table}'"); ;
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(openFileDialog1.FileName);
                if(fileExtensionIsEqualTo(file,".csv") || fileExtensionIsEqualTo(file,".txt"))
                {
                    createCsvParser(delimeterTB.Text[0]);
                }
                else
                    MessageBox.Show("Файл такого типу не підтримується!\nПотрібен файл з розширенням .csv або .txt","Помилка!");
            }
        }

        private void createCsvParser(char delimeter)
        {
            try
            {
                pathTB.Text = openFileDialog1.FileName;
                csvParser = new Parser(openFileDialog1.FileName, delimeter, Encoding.Default);
                userColumnsListView.Items.Clear();
                loadDataToCheckedListBox(csvParser.Headers, ref userColumnsListView);
            }
            catch (IOException)
            {
                MessageBox.Show("Оберіть файл!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непередбачувана помилка\n{ex.Message}");
            }
        }

        private bool fileExtensionIsEqualTo(FileInfo f,string extension)
        {
            return f.Extension.Equals(extension);
        }

        private void loadDataToCheckedListBox(List<object> loadData,ref ListBox list)
        {
            foreach (var record in loadData)
            {
                list.Items.Add(record);
            }
        }
        private void loadDataToCheckedListBox(List<List<object>> loadData,ref ListBox list)
        {
            foreach (var columnName in loadData)
            {
                loadDataToCheckedListBox(columnName, ref list);
            }
        }

        private void loadData(object sender, EventArgs e)
        {
            tableColumnsListView.Items.Clear();
            loadDataToCheckedListBox(getTableColumnsName("h34471c_KPI_KEEM", tableCB.Text), ref tableColumnsListView);
        }

        private void ImportDB_Load(object sender, EventArgs e)
        {
            Dovidka.populateComboBox(ref tableCB, db.connectionString, "information_schema.tables", "table_name", "table_schema ='h34471c_KPI_KEEM'");
            tableCB.SelectedIndexChanged += loadData;
            doneButton.Enabled = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            csvToDB = new ParserToDB(csvParser, tableCB.Text, tableColumsList.ToArray(), userColumnsList.ToArray());

            try
            {
                
                csvToDB.uploadDataAsync();
                //MessageBox.Show("DONE!");
                
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних\n" + ex.Message, "Помилка");
                timer1.Stop();
            }
        }

        private void delimeterTB_TextChanged(object sender, EventArgs e)
        {
            createCsvParser(delimeterTB.Text[0]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tableColumnsListView.SelectedItem != null && userColumnsListView.SelectedItem != null)
            {
                if (doneButton.Enabled == false) doneButton.Enabled = true;

                tableColumsList.Add(tableColumnsListView.SelectedItem.ToString());
                userColumnsList.Add(userColumnsListView.SelectedItem.ToString());
                mapList.AppendText($"{tableColumnsListView.SelectedItem}  ->  {userColumnsListView.SelectedItem};\n");
            }
            else
            {
                MessageBox.Show("Потрібно обрати два значення","Увага!");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            doneButton.Enabled = false;
            mapList.Clear();
            tableColumsList.Clear();
            userColumnsList.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mapList.Clear();
            if (csvToDB.isDoneUploading)
            {
                mapList.AppendText($"Дані завантажено!");
                timer1.Stop();
            }
            else
            {
                if (tik >= 3)
                {
                    mapList.AppendText($"\nЗавантаження");
                    tik = 0;
                }
                else
                {
                    mapList.AppendText($"\n{mapList.Text}.");
                    
                }
                tik++;

                //switch (tik++)
                //{
                //    case 0:
                //        {
                //            mapList.AppendText($"\nЗавантаження.");
                //            break;
                //        }
                //    case 1:
                //        {
                //            mapList.AppendText($"\nЗавантаження..");
                //            break;
                //        }
                //    case 2:
                //        {
                //            mapList.AppendText($"\nЗавантаження..."); 
                //            break;
                //        }
                //    case 3:
                //        {
                //            mapList.AppendText($"\nЗавантаження....");
                //            tik = 0;
                //            break;
                //        }
                //}

            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            delimeterTB.Enabled = true;
            createCsvParser(delimeterTB.Text[0]);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                delimeterTB.Enabled = false;
                createCsvParser(TAB_CHAR);
            }
            else
            {
                delimeterTB.Enabled = true;
            }
        }
    }
}
