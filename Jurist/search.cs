using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LawFileBase;
using FileBase;
using System.Globalization;

namespace experts_jurist
{
    public partial class search : Form
    {

        SearchManager SM;
        public FileBaseManager FBM = new FileBaseManager(".\\FB");
        string[] listOfFi = { };


        public search()
        {
            SM = new SearchManager();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listOfFi = SM.SearchAll();

            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedIndex != 0)
            {
                listOfFi = SM.FindType(comboBox1.Text, listOfFi); // тип
            }

            if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedIndex != 0)
            {
                listOfFi = SM.FindPublish(comboBox2.Text, listOfFi); // орган, що видав
            }

            bool checkYear = comboBox3.SelectedIndex <= comboBox4.SelectedIndex;
            bool checkMonth = comboBox6.SelectedIndex <= comboBox8.SelectedIndex;
            bool checkDay = comboBox5.SelectedIndex <= comboBox7.SelectedIndex;

            if (checkYear == false || (checkYear && checkMonth == false) || (checkYear && checkMonth && checkDay == false))
            {
                MessageBox.Show("Перевірте межі дат \"Від\" і \"До\" !");
                return;
            }
            else
            {
                List<string> firstDate = new List<string>();
                List<string> lastDate = new List<string>();

                firstDate.Add(comboBox5.Text);
                firstDate.Add(comboBox6.Text);
                firstDate.Add(comboBox3.Text);

                lastDate.Add(comboBox7.Text);
                lastDate.Add(comboBox8.Text);
                lastDate.Add(comboBox4.Text);

                listOfFi = SM.FindDate(firstDate, lastDate, listOfFi); // дата 
            }


            if (textBox1.Text != "")
            {
                listOfFi = SM.SearchLine(textBox1.Text, listOfFi);
            }


            if (listOfFi.Count() == 0)
            {
                label1.Text = "Нічого не знайдено";
            }
            else
            {
                label1.Text = "Знайдені документи";
            }
            Reload();
        }

        private void Reload()
        {

            listBox1.Items.Clear();
            foreach (var g in listOfFi)
            {
                listBox1.Items.Add(SM.GetPrewiew(g));
            }
            if (listOfFi.Length > 0)
            {
                listBox1.SelectedIndex = 0;
                webBrowser1.DocumentText = SM.GetPage(listOfFi[0]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SuppressScriptErrorsOnly(webBrowser1); // Mine
            webBrowser1.DocumentText = SM.GetPage(listOfFi[listBox1.SelectedIndex]);

        }

        // для ігнорування помилок, які відкриваються у діалогових вікнах
        private void SuppressScriptErrorsOnly(WebBrowser browser) //Mine 
        {
            browser.ScriptErrorsSuppressed = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listOfFi = SM.SearchAll();
            var tempList = SM.SearchLine(textBox1.Text, listOfFi);

            if (tempList.Count() > 0)
            {
                listOfFi = tempList;
                Reload();
            }

        }

        private void search_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = comboBox3.Items.Count - 1;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;

            comboBox2.Text = "Орган, що видав документ";
            comboBox2.ForeColor = Color.Gray;
            comboBox1.Text = "Тип документу";
            comboBox1.ForeColor = Color.Gray;

            comboBox1.Items.Add("Всі");
            comboBox2.Items.Add("Всі");

            listOfFi = SM.SearchAll();

            List<string> listOfTypes = new List<string>();
            List<string> listOfPublishing = new List<string>(); ;

            for (int i = 0; i < listOfFi.Length; i++)
            {
                try
                {
                    var a = listOfFi[i].ToString();
                    var temp1 = FBM.GetAttributes(a, 2).ToString();
                    var temp2 = FBM.GetAttributes(a, 3).ToString();

                    if (listOfTypes.Contains(temp1) == false)
                    {
                        comboBox1.Items.Add(temp1.ToString());
                        listOfTypes.Add(temp1.ToString());
                    }

                    if (listOfPublishing.Contains(temp2) == false)
                    {
                        comboBox2.Items.Add(temp2.ToString());
                        listOfPublishing.Add(temp2.ToString());
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("\n=================================");
                    System.Diagnostics.Debug.WriteLine(ex);
                    System.Diagnostics.Debug.WriteLine("=================================\n");
                }

            }

            if (listOfFi.Count() < 0)
            {
                label1.Text = "Не знайдено документів в базі";
            }
            else
            {
                label1.Text = "Знайдені документи";
            }
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.ForeColor = Color.Black;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.Text = "Тип документу";
                comboBox1.ForeColor = Color.Gray;
                textBox1.Focus();
            }
            else
            {
                return;
            }
        }

        private void comboBox2_MouseEnter(object sender, EventArgs e)
        {
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.ForeColor = Color.Black;
        }

        private void comboBox2_MouseLeave(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox2.Text = "Орган, що видав документ";
                comboBox2.ForeColor = Color.Gray;
                textBox1.Focus();
            }
            else
            {
                return;
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button2, "Сортуванняя за зростанням");
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button3, "Сортуванняя за спаданням");

        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button4, "Сортуванняя за зростанням");
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button5, "Сортуванняя за спаданням");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var resList = new Dictionary<string, int>();
            foreach (var doc in listOfFi)
            {
                resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = SM.Numbers(doc.Split(' ')[1]);
            }

            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;
            listOfFi = sortedOneList.ToArray();
            Reload();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var resList = new Dictionary<string, int>();
            foreach (var doc in listOfFi)
            {
                resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = SM.Numbers(doc.Split(' ')[1]);
            }

            var sortedOneList = from pair in resList
                                orderby pair.Value descending
                                select pair.Key;
            listOfFi = sortedOneList.ToArray();
            Reload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var resList = new Dictionary<string, DateTime>();

            foreach (var doc in listOfFi)
            {
                var all = FBM.GetAttributes(doc, 4).ToString();
                DateTime t1 = DateTime.ParseExact(all, "d.M.yyyy", CultureInfo.CurrentCulture);
                resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = t1;
            }

            //foreach (var doc in listOfFi)
            //{
            //    var a = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[0]);
            //    var b = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[1]);
            //    var c = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[2]);
            //    var all = "";

            //    all = String.Join(".", a, b, c);

            //    if (a > 0 && a < 10)
            //    {
            //        all = String.Join(".", "0" + a, b, c);
            //    }
            //    if (b > 0 && b < 10)
            //    {
            //        all = String.Join(".", a, "0" + b, c);
            //    }
            //    if ((a > 0 && a < 10) && (b > 0 && b < 10))
            //    {
            //        all = String.Join(".", "0" + a, "0" + b, c);
            //    }

            //    DateTime t1 = DateTime.ParseExact(all, "dd.mm.yyyy", CultureInfo.InvariantCulture);

            //    resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = t1;

            //}

            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;
            listOfFi = sortedOneList.ToArray();
            Reload();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var resList = new Dictionary<string, DateTime>();

            foreach (var doc in listOfFi)
            {
                var a = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[0]);
                var b = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[1]);
                var c = Convert.ToInt32(FBM.GetAttributes(doc, 4).ToString().Split('.')[2]);
                var all = "";

                all = String.Join(".", a, b, c);

                if (a > 0 && a < 10)
                {
                     all = String.Join(".","0"+a, b, c);
                }
                if (b > 0 && b < 10)
                {
                     all = String.Join(".", a,"0"+ b, c);
                }
                if ((a > 0 && a < 10) && (b > 0 && b < 10))
                {
                    all = String.Join(".", "0" + a, "0" + b, c);
                }

                DateTime t1 = DateTime.ParseExact(all, "dd.mm.yyyy", CultureInfo.InvariantCulture);

                resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = t1;

            }

            var sortedOneList = from pair in resList
                                orderby pair.Value descending
                                select pair.Key;
            listOfFi = sortedOneList.ToArray();
            Reload();
        }
    
    }
}
