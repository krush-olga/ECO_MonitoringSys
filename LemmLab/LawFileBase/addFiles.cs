using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LemmLab;
using FileBase;
using System.IO;

namespace LawFileBase
{
	public partial class addFiles : Form
	{
		LemmManager LM = new LemmManager();
		FileBaseManager FBM = new FileBaseManager();
		OpenFileDialog FileOpen = new OpenFileDialog();
		HashSet<string> words = new HashSet<string>();
		Boolean chooseWords = false;
		HashSet<string> choosedLemms = new HashSet<string>();
		Dictionary<string, int> sortedWords = new Dictionary<string, int>();
		public addFiles()
		{
			InitializeComponent();
			FBM.SetLocation($"{Environment.CurrentDirectory}\\FB");
		}
		public addFiles(string location)
		{
			InitializeComponent();
			FBM.SetLocation(location);
		}
		private void button1_Click(object sender, EventArgs e)
		{
			if (FileOpen.ShowDialog(this) == DialogResult.OK)
			{
				var reader = new StreamReader(FileOpen.OpenFile(), Encoding.UTF8);
				var pageText = reader.ReadToEnd();
				words = new HashSet<string>();
				choosedLemms = new HashSet<string>();
				sortedWords = new Dictionary<string, int>();
				foreach (var word in LM.ToWordsHTML(pageText))
				{
					if(!choosedLemms.Contains(LM.ToLemm(word)))
					{
						words.Add(word);
						choosedLemms.Add(LM.ToLemm(word));
						if (sortedWords.ContainsKey(word)) sortedWords[word]++;
						else sortedWords.Add(word, 1);
					}
				}
				flowLayoutPanel1.Controls.Clear();
				flowLayoutPanel1.Controls.Add(button3);
				button2.Enabled = true;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var reader = new StreamReader(FileOpen.OpenFile(), Encoding.Default);
			var pageText = reader.ReadToEnd();
			try
			{
				FBM.WriteToFile(FileOpen.SafeFileName, pageText);
			}
			catch (Exception) { }
			if (chooseWords)
			{
				List<string> choosedWords = new List<string>();
				foreach (var but in flowLayoutPanel1.Controls)
					if(but is CheckBox) if (((CheckBox)but).Checked) choosedWords.Add(((CheckBox)but).Text);
				string[] allWords = LM.ToWordsHTML(pageText);
				int wordsCount = (from a in allWords where choosedWords.Contains(a) select a).Count();
				FBM.AddToListOfFiles(FileOpen.SafeFileName, wordsCount);
				HashSet<string> choosedLemms = new HashSet<string>(from a in choosedWords select LM.ToLemm(a));
				var dic = FBM.GetDictionaryInDictionaryForm();
				var maxId = dic.Count != 0 ? (from a in dic select a.Value).Max() + 1 : 0;
				foreach (var a in choosedLemms)
				{
					if (!dic.ContainsKey(a))
						dic.Add(a, maxId++);
					var amount = (from b in allWords where a == LM.ToLemm(b) select b).Count();
					FBM.AddFileToWord(dic[a], FileOpen.SafeFileName.Replace(".htm", ""), amount);
				}
				FBM.SetDictionary(dic);
			}
			else
			{
				List<string> choosedWords = new List<string>(words);
				string[] allWords = LM.ToWordsHTML(pageText);
				int wordsCount = (from a in allWords where choosedWords.Contains(a) select a).Count();
				FBM.AddToListOfFiles(FileOpen.SafeFileName, wordsCount);
				HashSet<string> choosedLemms = new HashSet<string>(from a in choosedWords select LM.ToLemm(a));
				var dic = FBM.GetDictionaryInDictionaryForm();
				var maxId = dic.Count != 0 ? (from a in dic select a.Value).Max() + 1 : 0;
				foreach (var a in choosedLemms)
				{
					if (!dic.ContainsKey(a))
						dic.Add(a, maxId++);
					var amount = (from b in allWords where a == LM.ToLemm(b) select b).Count();
					FBM.AddFileToWord(dic[a], FileOpen.SafeFileName.Replace(".htm", ""), amount);
				}
				FBM.SetDictionary(dic);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			chooseWords = true;
			flowLayoutPanel1.Controls.Remove(button3);
			foreach (var word in sortedWords.OrderBy((x)=>x.Value+x.Key[0]/100))
			{
				var oneBut = new CheckBox();
				oneBut.Text = word.Key;
				if (word.Key.Count() > 2) oneBut.Checked = true;
				flowLayoutPanel1.Controls.Add(oneBut);
			}
		}
	}
}
