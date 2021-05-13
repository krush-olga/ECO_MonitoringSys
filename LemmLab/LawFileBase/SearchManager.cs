using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemmLab;
using FileBase;
namespace LawFileBase
{
    public class SearchManager
    {
        /// <summary>
        /// Об'єкт классу LemmManager, що використовується для нормалізації слів.
        /// </summary>
        private LemmManager e = new LemmManager();
        /// <summary>
        /// Об'єкт классу FileBaseManager, що використовується для доступу до файлової бази данних.
        /// </summary>
        public FileBaseManager LawBaseManager = new FileBaseManager(".\\FB");
        /// <summary>
        /// Отримання списку TF-індексів та назв документів в яких є задане слово.
        /// </summary>
        /// <param name="word">Слово, по якому потрібно проводити пошук. Повинне бути в словнику.</param>
        /// <returns>Список TF-індексів та назв документів.</returns>
        public string[] GetDocsWithWord(string word)
        {
            string[] li = LawBaseManager.GetDictionary();
            string filename = "";
            foreach (string g in li)
            {
                if ((g.Split(' '))[1] == word) 
                { 
                    filename = (g.Split(' '))[0]; 
                    break;
                }
            }
            return LawBaseManager.GetWordFile(filename);
        }
        /// <summary>
        /// Отримання списку TF/IDF-індексів та назв документів в яких є задане слово.
        /// </summary>
        /// <param name="word">Слово, по якому потрібно проводити пошук. Повинне бути в словнику.</param>
        /// <returns>Список TF/IDF-індексів та назв документів.</returns>
        public Dictionary<string, double> GetDocsWithWordTFIDF(string word, string[] listOfFiles)
        {
            string[] li = LawBaseManager.GetDictionary();
            string filename = "";

            foreach (string g in li)
            {
                if ((g.Split(' '))[1] == word)
                {
                    filename = (g.Split(' '))[0];
                    break;
                }
            }
            var res = new Dictionary<string, double>();
            if (filename != "")
            {
                li = LawBaseManager.GetWordFile(filename);
                string[] all = LawBaseManager.GetListOfFiles();

                var temp1 = 0;

                // сортування документів за номером
                var resList = new Dictionary<string, int>(); 
                foreach (var t in all)
                {
                    resList[t.Split(' ')[0] + " " + t.Split(' ')[1] + " " + t.Split(' ')[2]] = Numbers(t.Split(' ')[1]);
                }
                var sortedOneList = from pair in resList
                                    orderby pair.Value ascending
                                    select pair.Key;
                sortedOneList = sortedOneList.ToArray();

                if (listOfFiles.Length != all.Length)
                {
                    foreach (string doc in sortedOneList)
                    {
                        for (var i = temp1; i < listOfFiles.Length; i++)
                        {
                            if (listOfFiles[i] == doc.Split(' ')[0] + " " + doc.Split(' ')[1])
                            {
                                listOfFiles[i] += " " + doc.Split(' ')[2];
                                break;
                            }

                        }
                    }
                }
                else
                {
                    listOfFiles = all;
                }
                
                foreach (var g in li)
                {
                    try // mine
                    { 
                      
                        for (int i = 0; i < listOfFiles.Length; i++)
                        {
                            var z = listOfFiles[i].Split(' ').ToArray();
                            if (z[0] + " " + z[1] == g.Split(' ')[0] + " " + g.Split(' ')[1])
                            {
                                res.Add(g.Split(' ')[0] + " " + g.Split(' ')[1], Convert.ToDouble(g.Split(' ')[2]));
                            }
                        }
                    }
                    catch (FormatException) // mine
                    {
                        break;
                    }
                }

                foreach (var g in listOfFiles)
                {
                    if (res.ContainsKey(g.Split(' ')[0] + " " + g.Split(' ')[1])) 
                    {
                        res[g.Split(' ')[0] + " " + g.Split(' ')[1]] *= (Math.Log((double)all.Length / (double)li.Length) + 0.01) / Convert.ToDouble(g.Split(' ')[2]);
                    }
                }

            }
            return res;
        }
        /// <summary>
        /// Додавання до існуючого списку з назв та IDF/TF-індексів документів ще одного такого списку шляхом додавання значень індексів IDF/TF, що належать одному і тому ж документу.
        /// </summary>
        /// <param name="a">Список з імен та IDF/TF-індексів документів.</param>
        /// <param name="b">Список з імен та IDF/TF-індексів документів.</param>
        /// <returns>Результат додавання.</returns>
        private Dictionary<string, double> CocQueris(Dictionary<string, double> a, Dictionary<string, double> b)
        {
            foreach (var g in b)
            {
                if (a.ContainsKey(g.Key))
                {
                    a[g.Key] += g.Value;
                }
                else
                {
                    a.Add(g.Key, g.Value);
                }
            }
            return a;
        }
        /// <summary>
        /// Проводить пошук за заданою стрічкою по документам.
        /// </summary>
        /// <param name="searchLine">Стрічка, по якій проводиться пошук.</param>
        /// <returns>Список з імен документів.</returns>
        public string[] SearchLine(string searchLine, string[] listOfFiles)
        {
			var resList = new Dictionary<string, double>();
			var wordsList = e.ToWords(searchLine);
			var lemmList = new List<string>();
			foreach (var g in wordsList)
			{
				lemmList.Add(e.ToLemm(g));
			}
			foreach (var g in lemmList)
			{
				resList = CocQueris(resList, GetDocsWithWordTFIDF(g, listOfFiles));
			}
			var sortedOneList = from pair in resList
								orderby pair.Value descending
								select pair.Key;
			return sortedOneList.ToArray();

        }
		public string[] SearchAll()
        {
            var resList = new Dictionary<string, int>();
            var AllList = LawBaseManager.GetListOfFiles();
			foreach (var t in AllList)
			{
                resList[t.Split(' ')[0] + " " + t.Split(' ')[1]] = Numbers(t.Split(' ')[1]);
            }
            var sortedOneList = from pair in resList
								orderby pair.Value ascending
                                select pair.Key;
			return sortedOneList.ToArray();
		}
        
        public string[] FindType(string type, string[] listOfFiles)
        {
            var resList = new Dictionary<string, int>();
            foreach (var doc in listOfFiles)
            {
                var typeOfDoc = LawBaseManager.GetAttributes(doc, 2).ToString();
                if (typeOfDoc == type)
                {
                    resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = Numbers(doc.Split(' ')[1]);
                }
            }
            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;
            return sortedOneList.ToArray();
        }

        public string[] FindPublish(string publish, string[] listOfFiles)
        { 
            var resList = new Dictionary<string, int>();
            foreach (var doc in listOfFiles)
            {
                var publishOfDoc = LawBaseManager.GetAttributes(doc, 3).ToString();
                if (publishOfDoc == publish)
                {
                    resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = Numbers(doc.Split(' ')[1]);
                }
            }
            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;
            return sortedOneList.ToArray();
        }

        public string[] FindDate (List <string> firstdate, List<string> lastdate, string[] listOfFiles)
        {
            var resList = new Dictionary<string, int>();
            var date1 = firstdate.ToArray();
            var date2 = lastdate.ToArray();

            foreach (var doc in listOfFiles)
            {
                var dateOfDoc = LawBaseManager.GetAttributes(doc, 4).ToString();
                var dayMonthYear = dateOfDoc.Split('.');
                var date = new DateTime(Convert.ToInt32(dayMonthYear[2]), Convert.ToInt32(dayMonthYear[1]), Convert.ToInt32(dayMonthYear[0]));

                var startDate = new DateTime(Convert.ToInt32(date1[2]), Convert.ToInt32(date1[1]), Convert.ToInt32(date1[0]));
                var endDate = new DateTime(Convert.ToInt32(date2[2]), Convert.ToInt32(date2[1]), Convert.ToInt32(date2[0]));

                var checkInIsValid = date >= startDate && date < endDate;
                if(checkInIsValid)
                {
                    resList[doc.Split(' ')[0] + " " + doc.Split(' ')[1]] = Numbers(doc.Split(' ')[1]);
                }
            }
            var sortedOneList = from pair in resList
                                orderby pair.Value ascending
                                select pair.Key;
            return sortedOneList.ToArray();
        }


        /// <summary>
        /// Очищує номер документу в назві від зайвих символів, залишаючи тільки числа
        /// </summary>
        /// <param name="str">Стрічка для перевірки</param>
        /// <returns></returns>
        public int Numbers(string str)
        {
            List<char> clearStr = new List<char>();

            foreach (var c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    clearStr.Add(c);
                }
                else
                {
                    return Convert.ToInt32(String.Join("", clearStr));
                }
            }
            return Convert.ToInt32(String.Join("", clearStr));
        }

        /// <summary>
        /// Отримання назви законодавчого документу.
        /// </summary>
        /// <param name="name">Назва файлу документу.</param>
        /// <returns>Назва законодавчого документу.</returns>
        public string GetPrewiew(string name)
        {
            return LawBaseManager.GetName(name);
        }
        /// <summary>
        /// Отримання змісту законодавчого документу.
        /// </summary>
        /// <param name="name">Ім'я файлу законодавчого документу.</param>
        /// <returns>Зміст законодавчого документу.</returns>
        public string GetPage(string name)
        {
            var list = LawBaseManager.GetHtml(name);
            var page = Formatting(list);

            if (page == "")
            {
                return LawBaseManager.GetHtml_(name);
            }
            return page;
        }

        /// <summary>
        /// Додає абзаци в місцях некоректного виведення документів
        /// </summary>
        /// <param name="list">Масив стрічок документу</param>
        /// <returns></returns>
        public string Formatting(string[] list)
        {
            string page = "";
            bool temp = false;
            foreach (var g in list)
            {
                bool tt = false;
                if (temp)
                {
                    if (g.Contains("-----"))
                    {
                        page += "<br>";
                        tt = true;
                    }
                    if (g.Contains("   |"))
                    {
                        page += "<br>";
                    }
                    if (g.Contains("______"))
                    {
                        page += "<br>";
                    }
                    page += g;

                    if (tt)
                    {
                        page += "<br>";
                    }
                }

                if (g.Contains("<div class=\"clearfix\">"))
                {
                    temp = false;
                    continue;
                }
                if (g.Contains("</aside>"))
                {
                    temp = true;
                }
                if (g.Contains("<div id=\"orfoWindow\" class=\"modal fade\">"))
                {
                    temp = false;
                }
            }
            return page;
        }

        public void doit()
		{
			//LawBaseManager.doit();
		}
    }
}
