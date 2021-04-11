using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HelpModule.Models;

namespace HelpModule.Forms
{
	public partial class ListScenarioForm : Form
	{
		private string _path = "file:///D:/Project/ECO_MonitoringSys_new/HelpModule/Scenarios";

		private string _pathPages = "pages";

		private int _currentNumber;

		// Словарь который содержит информацию про сценарии
		private static Dictionary<int, Scenario> GetPathByScenarios
			=> new Dictionary<int, Scenario>
			{
				[0] = new Scenario { Name = "Полигон", Pages =10 },
				[1] = new Scenario { Name = "Маркер", Pages = 14 },
				[2] = new Scenario { Name = "Область", Pages = 14 },
				[3] = new Scenario { Name = "Система", Pages = 18 }
			};
		public ListScenarioForm()
		{
			InitializeComponent();
			scenarios.SelectedIndex = 0;
			searchButton.Enabled = false;
		}

		// Событие при нажатии на кнопку Пошук
		private void searchButton_Click(object sender, EventArgs e) => Search();
		
		// Изменение поля для поиска
		private void searchQuery_TextChanged(object sender, EventArgs e)
		{
			searchButton.Enabled = !string.IsNullOrEmpty(searchQuery.Text);
		}

		// Показывать определенный сценарий
		private void scenarios_SelectedIndexChanged(object sender, EventArgs e)
		{
			var scenario = GetPathByScenarios[scenarios.SelectedIndex];
			_currentNumber = 1;
			prev.Enabled = false;
			next.Enabled = true;
			var path = $"{_path}/{scenario.Name}/{_pathPages}/{_currentNumber}.html";
			webBrowser1.Navigate(path);
			prev.Enabled = false;
		}

		// Поиск по имени сценария
		private void Search()
		{
			if (string.IsNullOrEmpty(searchQuery.Text))
			{
				return;
			}
			var found = false;
			for (var i = 0; i <= scenarios.Items.Count - 1; i++)
			{
				if (scenarios.Items[i].ToString().ToLower().Contains(searchQuery.Text.ToLower()))
				{
					scenarios.SetSelected(i, true);
					found = true;
					break;
				}
			}
			if (!found)
			{
				MessageBox.Show("Сценарія не знайдено!");
			}
		}

		// Событие при вводе текста в поле для поиска
		private void searchQuery_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				Search();
		}

		// Событие при нажатии на кнопку Далі
		private void next_Click(object sender, EventArgs e)
		{
			++_currentNumber;
			var scenario = GetPathByScenarios[scenarios.SelectedIndex];
			var path = $"{_path}/{scenario.Name}/pages/{_currentNumber}.html";
			if (_currentNumber == scenario.Pages)
			{
				next.Enabled = false;
			}

			if (_currentNumber > 1)
			{
				prev.Enabled = true;
			}
			webBrowser1.Navigate(path);
		}

		// Событие при нажатии на кнопку Назад
		private void prev_Click(object sender, EventArgs e)
		{
			--_currentNumber;
			var scenario = GetPathByScenarios[scenarios.SelectedIndex];
			var path = $"{_path}/{scenario.Name}/pages/{_currentNumber}.html";
			if (_currentNumber == 1)
			{
				prev.Enabled = false;
			}

			if (_currentNumber < scenario.Pages)
			{
				next.Enabled = true;
			}
			webBrowser1.Navigate(path);
		}
	}
}
