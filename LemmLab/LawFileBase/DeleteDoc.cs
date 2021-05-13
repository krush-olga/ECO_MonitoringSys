using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileBase;

namespace LawFileBase
{
    public partial class DeleteDoc : Form
    {
        public FileBaseManager LawBaseManager = new FileBaseManager(".\\FB");
        SearchManager SM = new SearchManager();
        string[] countOfDocs = { };

        public DeleteDoc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var deletingDoc = comboBox1.SelectedItem.ToString();
            
            // видалення з _listOfFiles
            IEnumerable<string> newList = countOfDocs.Where(x => !(x.Split(' ')[0] +" "+ x.Split(' ')[1]).Equals(deletingDoc));
            LawBaseManager.WriteToFile("_listOfFiles", newList.ToArray());

            //видалення файлу з атрибутами
            File.Delete(".\\FB\\Attributes\\" + deletingDoc);

            //видалення самого документу
            File.Delete(".\\FB\\" + deletingDoc + ".html");

            //видалення назви документу з файлів індексації
            bool a = true;
            var i = 0;
            IEnumerable<string> b;
            while (a)
            {
                try
                {
                    var content = LawBaseManager.GetWordFile(i.ToString());
                    b = content.Where(x => !(x.Split(' ')[0] + " " + x.Split(' ')[1]).Equals(deletingDoc));
                    LawBaseManager.WriteToFile("\\TF\\" + i, b.ToArray());
                    i++;
                }

                catch (FileNotFoundException)
                {
                    a = false;
                }
            }

            reload();

        }
        private void reload()
        {
            countOfDocs = LawBaseManager.GetListOfFiles();
            comboBox1.Items.Clear();

            var resList = new Dictionary<string, int>();
            foreach (var doc in countOfDocs)
            {
                resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = SM.Numbers(doc.Split(' ')[1]);
            }
            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;

            foreach (var line in sortedOneList)
            {
                comboBox1.Items.Add(line.Split(' ')[0] + " " + line.Split(' ')[1]);
            }
            webBrowser1.DocumentText = "";
        }

        private void DeleteDoc_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            reload();
        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SuppressScriptErrorsOnly(webBrowser1); // Mine
            webBrowser1.DocumentText = SM.GetPage(comboBox1.SelectedItem.ToString());
        }

        // для ігнорування помилок, які відкриваються у діалогових вікнах
        private void SuppressScriptErrorsOnly(WebBrowser browser) //Mine 
        {
            browser.ScriptErrorsSuppressed = true;
        }
    }
    
}
