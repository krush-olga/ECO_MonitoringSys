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
        string[] listOfFi = { };
        public DeleteDoc()
        {
            InitializeComponent();
        }

        private void reload(string [] listOfFi)
        {
          
            listBox1.Items.Clear();
            foreach (var g in listOfFi)
            {
                listBox1.Items.Add(SM.GetPrewiew(g));
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


        private void AllDocs_Click(object sender, EventArgs e)
        {
            listOfFi = SM.SearchAll();
            reload(listOfFi);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            listOfFi = SM.SearchAll();
            listOfFi = SM.SearchLine(textBox1.Text, listOfFi);

            if (listOfFi.Count() == 0)
            {
                label2.Text = "Нічого не знайдено";
            }
            else
            {
                label2.Text = "Знайдені документи";
            }

            reload(listOfFi);
        }

        private void Del_Click(object sender, EventArgs e)
        {
            var deletingDoc = listOfFi[listBox1.SelectedIndex];

            var docsWithWords = LawBaseManager.GetListOfFiles();

            // видалення з _listOfFiles
            IEnumerable<string> newList = docsWithWords.Where(x => !(x.Split(' ')[0] + " " + x.Split(' ')[1]).Equals(deletingDoc));
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

            listOfFi = SM.SearchAll();
            webBrowser1.DocumentText = "";
            reload(listOfFi);
        }
    }
}
