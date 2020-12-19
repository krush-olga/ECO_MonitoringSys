﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace FileBase
{
    public class FileBaseManager
    {
       
        /// <summary>
        /// Шлях до дерикторії бази.
        /// </summary>
        private string location;
        /// <summary>
        /// Конструктор классу FileBaseManager.
        /// </summary>
        public FileBaseManager()
        {
            
            location = "";
        }
        /// <summary>
        /// Конструктор классу FileBaseManager.
        /// </summary>
        /// <param name="path">Шлях до дерикторії бази.</param>
        public FileBaseManager(string path)
        {
            if (path[path.Length - 1] != '\\')
            {
                this.location = path + '\\';
            }
            else
            {
                this.location = path;
            }
        }
        /// <summary>
        /// Встановлення Шляху до дерикторії бази.
        /// </summary>
        /// <param name="path">Шлях.</param>
        public void SetLocation(string path)
        {
            if(path[path.Length-1] != '\\')
            {
                this.location = path + '\\';
            } else
            {
                this.location = path;
            }

        }
        /// <summary>
        /// Отримання змісту файлу.
        /// </summary>
        /// <param name="name">Ім'я файлу.</param>
        /// <returns>Зміст файлу</returns>
        public string[] GetFile(string name)
        {
            return File.ReadAllLines(location + name, Encoding.Unicode);
        }
        /// <summary>
        /// Отримання змісту файлу зі списком документів та їх TF-індексів.
        /// </summary>
        /// <param name="name">Ім'я файлу.</param>
        /// <returns>Зміст файлу.</returns>
        public string[] GetWordFile(string name)
        {
            return File.ReadAllLines(location + "TF\\" + name, Encoding.Unicode);
        }
        /// <summary>
        /// Отримання словнику всіх доступних слів та їх індексів.
        /// </summary>
        /// <returns>Список індексів та відповідних слів.</returns>
        public string[] GetDictionary()

        {
           
            return File.ReadAllLines(location + "_dictionary", Encoding.Unicode);
            
        }
		/// <summary>
		/// Отримання словнику всіх доступних слів та їх індексів.
		/// </summary>
		/// <returns>Список індексів та відповідних слів.</returns>
		public Dictionary<string,int> GetDictionaryInDictionaryForm()
		{
			var strM = File.ReadAllLines(location + "_dictionary", Encoding.Unicode);
			Dictionary<string, int> res = new Dictionary<string, int>();
			foreach(var str in strM)
			{
				var pair = str.Split(' ');
				res.Add(pair[1], int.Parse(pair[0]));
			}
			return res;
		}
		/// <summary>
		/// Встановлення словнику.
		/// </summary>
		public void SetDictionary(Dictionary<string, int> dic)
		{
			var res = new List<string>();
			foreach (var str in dic)
			{
				res.Add(str.Value +" "+ str.Key);
			}
			File.WriteAllLines(location + "_dictionary", res, Encoding.Unicode);
		}
		/// <summary>
		/// Оголошення кількості слова в документі
		/// </summary>
		/// <param name="dic"></param>
		public void AddFileToWord(int wordCode, string filename, int amount)
		{
			Dictionary<string, int> docs = new Dictionary<string, int>();
            ///try
            //{

            if (File.Exists(location + "//TF//" + wordCode)) // mine
            { 
                var text = File.ReadAllLines(location + "//TF//" + wordCode);
                foreach (var line in text)
                {
                    var temp = line.Split(' ');

                    docs.Add(temp[0] + " " + temp[1], int.Parse(temp[2])); // якщо назва скл. з №_імя
                   // docs.Add(temp[0], int.Parse(temp[1])); // якщо назва скл. з 1 строки
                }
                }
			//}
			//catch(Exception) { }

            if (docs.ContainsKey(filename))
                docs[filename] = amount;
            else
            {
                docs.Add(filename, amount);
                File.WriteAllLines(location + "//TF//" + wordCode, (from a in docs select a.Key + " " + a.Value).ToArray(), Encoding.Unicode);

            }
		}
		
		/// <summary>
		/// Отримання змісту файлу з росширенням ".htm".
		/// </summary>
		/// <param name="name">Ім'я файлу без розширення ".htm".</param>
		/// <returns>Зміст файлу.</returns>
		public string[] GetHtm(string name)
        {
            return File.ReadAllLines(location + name + ".htm", Encoding.Unicode); // ".html"
        }
        /// <summary>
        /// Отримання списку імен всіх зареєстрованих файлів.
        /// </summary>
        /// <returns>Список імен файлів.</returns>
        public string[] GetListOfFiles()
        {
            return File.ReadAllLines(location + "_listOfFiles", Encoding.Unicode);
        }

		/// <summary>
		/// Зареєстровування документу за назвою.
		/// </summary>
		public void AddToListOfFiles(string name, int wordCount)
		{
            
            using (StreamWriter dic = new StreamWriter(location + "_listOfFiles", true))
            {
                dic.WriteLine(name.Trim().Replace(".html", "") + " " + wordCount);
            }
        }

        /// <summary>
        /// Перевіряє наявність назви документу який добавляється у "_listOfFiles", 
        /// </summary>
        public bool CheckForAvailability(string name)
        {
            var text = File.ReadAllLines(location + "_listOfFiles", Encoding.Unicode);
            foreach (var line in text)
            {
                var temp = line.Split(' ');

                if (temp[0] + " " + temp[1] + ".html" == name  )
                {
                    MessageBox.Show("Такий документ вже добавлений у базу ", "Помилка !");
                    return false ; 
                }

            }
            return true;
        }
        /// <summary>
        /// Записує данні в файл з назвою "_out".
        /// Використовуюється при потребі перевести великі масиви данних до зручної для зміни форми. 
        /// </summary>
        /// <param name="st">Масив стрічок, які потрібно записати.</param>
        public void WriteDef(string[] st)
        {
            File.WriteAllLines(location + "_out", st, Encoding.Unicode);
        }
		/// <summary>
		/// Записує данні в файл.
		/// </summary>
		/// <param name="name">Ім'я файлу.</param>
		/// <param name="st">Масив стрічок, які потрібно записати.</param>
		public void WriteToFile(string name ,string[] st)
        {
            File.WriteAllLines(location + name, st, Encoding.Unicode);
        }
		/// <summary>
		/// Записує данні в файл.
		/// </summary>
		/// <param name="name">Ім'я файлу.</param>
		/// <param name="st">Стрічка, яку потрібно записати.</param>
		public void WriteToFile(string name, string st)
		{
			File.WriteAllLines(location + name, st.Split('\n'), Encoding.Unicode);
		}
		/// <summary>
		/// Отримання назви законодавчого документу.
		/// </summary>
		/// <param name="name">Ім'я файлу документу.</param>
		/// <returns>Назву документу.</returns>
		public string GetName(string name)
        {
            var mFile = GetHtm(name);
            var re1 = new Regex("<span class=rvts70>(.*)</span>");
            var re2 = new Regex("<span class=rvts66>(.*)</span>");
            var re3 = new Regex("<span class=rvts23>(.*)</span>");
            string res = "";
            foreach(var g in mFile)
            {
                if(re1.IsMatch(g))
                {
                    res += (re1.Match(g).Groups[1].Value) + " ";
                }
                if (re2.IsMatch(g))
                {
                    res += (re2.Match(g).Groups[1].Value) + " ";
                }
                if (re3.IsMatch(g))
                {
                    res += (re3.Match(g).Groups[1].Value) + " ";
                    return res;
                }
            }
            return "none";
        }
		public void doit()
		{
			string[] a;
			for (var i = 0; i < 122; i++)
			{
				a = GetFile("//TF//" + i);
				for(var j = 0; j<a.Length;j++)
				{
					a[j]=a[j].Replace(".htm", "");
				}
				WriteToFile("//TF//" + i, a);
			}
		}
	}
}
