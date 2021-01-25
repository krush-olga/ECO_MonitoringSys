using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maps.HelpWindows
{
    public partial class ItemConfigurationWindow : Form
    {
        private static readonly Data.Entity.CalculationSeries missingSeries;

        private int oldIssueIndex;
        private int oldElementIndex;
        private string oldObjName;
        private string oldObjDescription;

        private IEnumerable<int> oldSelectedIssues;
        private IEnumerable<int> oldSelectedElements;
        private IDictionary<int, IEnumerable<int>> oldSelectedSeries;
        private IDictionary<int, IEnumerable<int>> oldSelectedEnvironments;

        private Role expert;

        private ViewModel.EmissionsControllerVM emissionsController;
        private IDictionary<int, IList<Data.Entity.CalculationSeries>> series;
        private IDictionary<int, IEnumerable<int>> selectedSeries;
        private IDictionary<int, IEnumerable<int>> selectedEnvironments;
        private DBManager dbManager;

        static ItemConfigurationWindow()
        {
            missingSeries = new Data.Entity.CalculationSeries
            {
                Id = -1,
                Name = "Серії для даного експерта відстуні"
            };
        }

        public ItemConfigurationWindow(Role expert)
        {
            InitializeComponent();

            this.expert = expert;

            dbManager = new DBManager();
            emissionsController = new ViewModel.EmissionsControllerVM();
            series = new Dictionary<int, IList<Data.Entity.CalculationSeries>>();
            selectedSeries = new Dictionary<int, IEnumerable<int>>();
            selectedEnvironments = new Dictionary<int, IEnumerable<int>>();

            oldIssueIndex = -1;
            oldElementIndex = -1;
            oldObjName = string.Empty;
            oldObjDescription = string.Empty;
            oldSelectedIssues = null;
            oldSelectedElements = null;
            oldSelectedSeries = null;
            oldSelectedEnvironments = null;

            InitializeAdditionalComponent();
        }

        private void InitializeAdditionalComponent()
        {
            Binding issuesCheckBinding = new Binding("Enabled", IssueCheckBox, "Checked");
            Binding emissionCheckBinding = new Binding("Enabled", EmissionCheckBox, "Checked");

            IssueGroupBox.DataBindings.Add(issuesCheckBinding);
            EmissionGroupBox.DataBindings.Add(emissionCheckBinding);

            DateTime dateTime = DateTime.Now;

            YearNumericUpDown.Maximum = dateTime.Year;
            DayNumericUpDown.Maximum = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            if (Helpers.MapCache.ContainsKey("environments"))
            {
                var environments = Helpers.MapCache.GetItem("environments") as IList<Data.Entity.Environment>;

                foreach (var env in environments)
                {
                    EnvironmentsCheckedListBox.Items.Add(env);
                }
            }

        }

        private void SelectCheckedListBoxItems(CheckedListBox checkedListBox, IEnumerable<int> indices)
        {
            foreach (var index in indices)
            {
                checkedListBox.SetItemChecked(index, true);
            }
        }
        private void ClearSelectedItems(CheckedListBox checkedListBox)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, false);
            }
        }

        public string GetObjName()
        {
            return ObjectNameTextBox.Text;
        }
        public string GetObjDescrition()
        {
            return ObjectDescriptionTextBox.Text;
        }
        public IEnumerable<Data.Entity.Issue> GetIssues()
        {
            return IssuesCheckedListBox.CheckedItems.Count != 0 ?
                   IssuesCheckedListBox.CheckedItems.OfType<Data.Entity.Issue>() :
                   new List<Data.Entity.Issue>();
        }
        public IDictionary<Data.Entity.Issue, List<Data.Entity.CalculationSeries>> GetSeries()
        {
            var _series = new Dictionary<Data.Entity.Issue, List<Data.Entity.CalculationSeries>>();

            if (IssueCheckBox.Checked)
            {
                IssuesCheckedListBox.SelectedIndex = -1;

                foreach (int index in IssuesCheckedListBox.CheckedIndices)
                {
                    if (selectedSeries.ContainsKey(index) && selectedSeries[index].Any())
                    {
                        var seriesColl = new List<Data.Entity.CalculationSeries>();
                        var currentSeriesIndices = selectedSeries[index];

                        foreach (var seriesIndex in currentSeriesIndices)
                        {
                            var issue = (Data.Entity.Issue)IssuesCheckedListBox.Items[index];
                            seriesColl.Add(series[issue.Id][seriesIndex]);
                        }

                        var currentIssue = (Data.Entity.Issue)IssuesCheckedListBox.Items[index];

                        _series.Add(currentIssue, seriesColl);
                    }
                }
            }

            return _series;
        }
        public IEnumerable<Data.Entity.Emission> GetEmissions()
        {
            var checkedEmissions = new List<Data.Entity.Emission>();
            var environmentCount = EnvironmentsCheckedListBox.Items.Count;

            ElementsCheckedListBox.SelectedIndex = -1;

            if (EnvironmentsCheckedListBox.CheckedItems.Count != 0)
            {
                foreach (int index in ElementsCheckedListBox.CheckedIndices)
                {
                    foreach (var envIndex in selectedEnvironments[index])
                    {
                        var selectedEmission = emissionsController.Emissions.FirstOrDefault(emission => emission.Id == index * environmentCount + envIndex);

                        if (selectedEmission != null)
                        {
                            checkedEmissions.Add(selectedEmission);
                        }
                    }
                }
            }

            return checkedEmissions;
        }

        //public void SetIssueIndex(int index)
        //{
        //    if (index == -1)
        //    {
        //        return;
        //    }

        //    IssueCheckBox.Checked = true;

        //    SetComboBoxIndex<Data.Entity.Issue>(IssuesComboBox, index, ref oldIssueIndex);
        //}
        //public void SetEnvironmentIndex(int index)
        //{
        //    if (index == -1)
        //    {
        //        return;
        //    }

        //    EnvironmentCheckBox.Checked = true;

        //    //SetComboBoxIndex<Data.Entity.Environment>(EnvironmentsComboBox, index, ref oldEnvironmentIndex);
        //}
        //public void SetSeriesIndex(int index)
        //{
        //    if (index == -1)
        //    {
        //        return;
        //    }

        //    if (IssuesComboBox.SelectedIndex != -1)
        //    {
        //        SetSeries();
        //        SetComboBoxIndex<Data.Entity.CalculationSeries>(SeriesComboBox, index, ref oldSeriesIndex);
        //    }
        //}
        //public void SetIssue(Data.Entity.Issue issue)
        //{
        //    if (issue != null)
        //    {
        //        IssueCheckBox.Checked = true;

        //        SetIssueIndex(IndexOf(issues, _issue => _issue.Id == issue.Id));
        //    }
        //    else
        //    {
        //        SetIssueIndex(-1);
        //    }
        //}
        //public void SetEnvironment(Data.Entity.Environment environment)
        //{
        //    if (environment != null)
        //    {
        //        EnvironmentCheckBox.Checked = true;

        //        SetEnvironmentIndex(IndexOf(environments, _env => _env.Id == environment.Id));
        //    }
        //    else
        //    {
        //        SetEnvironmentIndex(-1);
        //    }
        //}
        //public void SetSeries(Data.Entity.CalculationSeries series)
        //{
        //    if (series != null && IssuesComboBox.SelectedIndex != -1)
        //    {
        //        IssueCheckBox.Checked = true;

        //        SetSeries();

        //        var issue = IssuesComboBox.SelectedItem as Data.Entity.Issue;

        //        if (issue == null)
        //        {
        //            SetSeriesIndex(-1);
        //        }

        //        var seriesCollection = this.series[issue.Id];

        //        SetSeriesIndex(IndexOf(seriesCollection, _series => _series.Id == series.Id));
        //    }
        //    else
        //    {
        //        SetSeriesIndex(-1);
        //    }
        //}
        //public void SetEmission(Data.Entity.Emission emission)
        //{
        //    if (emission != null)
        //    {
        //        EmissionCheckBox.Checked = true;

        //        if (emission.Element != null)
        //        {
        //            SetComboBoxIndex<Data.Entity.Element>(ElementsComboBox,
        //                                                  IndexOf(elements, 
        //                                                          _element => _element.Code == emission.Element.Code),
        //                                                  ref oldElementIndex);
        //        }   

        //        if (emission.Environment != null)
        //        {
        //            SetEnvironment(emission.Environment);
        //        }

        //        YearNumericUpDown.Value = emission.Year;
        //        MonthNumericUpDown.Value = emission.Month;
        //        DayNumericUpDown.Value = emission.Day;

        //        MaxEmissionValueTextBox.Text = emission.MaxValue.ToString();
        //        AvgEmissionValueTextBox.Text = emission.AvgValue.ToString();
        //    }
        //}

        public void SetObjName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Название не может быть пустым.", "name");
            }

            oldObjName = name;
            ObjectNameTextBox.Text = name;
        }
        public void SetObjDescritpion(string description)
        {
            oldObjDescription = description;
            ObjectDescriptionTextBox.Text = description;
        }

        private int FindEmissionById(int id)
        {
            for (int i = 0; i < emissionsController.Emissions.Count; i++)
            {
                if (emissionsController.Emissions[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        private async void SetSeries(Data.Entity.Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException();
            }

            if (!series.ContainsKey(issue.Id))
            {
                var loadedSeries = await LoadSeriesByIssue(issue.Id);

                if (loadedSeries.Count == 0)
                {
                    loadedSeries.Add(missingSeries);
                }

                series[issue.Id] = loadedSeries;
            }

            SeriesCheckedListBox.Items.Clear();

            foreach (var series in series[issue.Id])
            {
                SeriesCheckedListBox.Items.Add(series);
            }
        }

        private async Task FillAndReturnCheckedListBoxFromDBAsync<TResult>(CheckedListBox checkedListBox, string table, string columns,
                                                          string condition, Func<List<object>, TResult> func,
                                                          Action<CheckedListBox> falultAction = null)
        {
            if (checkedListBox == null || string.IsNullOrEmpty(table) ||
                columns == null || condition == null || func == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                var results = (await dbManager.GetRowsAsync(table, columns, condition))
                                                   .Select(func)
                                                   .ToList();

                foreach (var item in results)
                {
                    checkedListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                if (falultAction != null)
                {
                    checkedListBox.Invoke(falultAction, checkedListBox);
                }

#if DEBUG
                MessageBox.Show($"Message:\n{ex.Message}\n\nStack trace: \n{ex.StackTrace}",
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else 
                MessageBox.Show("Сталась помилка при завантажені об'єктів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
        private async Task<List<Data.Entity.CalculationSeries>> LoadSeriesByIssue(int issueId)
        {
            string condition = $"issue_id = {issueId}";

            if (expert != 0)
            {
                condition += $" AND id_of_expert = {((int)expert)}";
            }

            return (await dbManager.GetRowsAsync("calculations_description", "calculation_number, calculation_name", condition))
                                   .Select(r =>
                                   {
                                       int id;
                                       int.TryParse(r[0].ToString(), out id);

                                       return new Data.Entity.CalculationSeries
                                       {
                                           Id = id,
                                           Name = r[1].ToString()
                                       };
                                   })
                                   .ToList();
        }

        private void ItemConfigurationWindow_Load(object sender, EventArgs e)
        {
            ObjectNameTextBox.Text = oldObjName;
            ObjectDescriptionTextBox.Text = oldObjDescription;
        }

        private async void IssueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IssuesCheckedListBox.Items.Count == 0)
            {
                await FillAndReturnCheckedListBoxFromDBAsync(IssuesCheckedListBox, "issues", "issue_id, name", "",
                                               r =>
                                               {
                                                   int id;
                                                   int.TryParse(r[0].ToString(), out id);

                                                   return new Data.Entity.Issue(id)
                                                   {
                                                       Name = r[1].ToString()
                                                   };
                                               });
            }
        }
        private async void EmissionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ElementsCheckedListBox.Items.Count == 0)
            {
                await FillAndReturnCheckedListBoxFromDBAsync(ElementsCheckedListBox, "elements", "*", "",
                                                             r => Data.Helpers.Mapper<Data.Entity.Element>.Map(r));

                if (EnvironmentsCheckedListBox.Items.Count == 0)
                {
                    await FillAndReturnCheckedListBoxFromDBAsync(EnvironmentsCheckedListBox, "environment", "*", "",
                                                                 r => Data.Entity.EnvironmentMapper.Map(r));
                }

                ElementsCheckedListBox.SelectedIndex = 0;
                EnvironmentsCheckedListBox.SelectedIndex = 0;

                MaxEmissionValueTextBox.DataBindings.Add("Text", emissionsController, "CurrentEmission.MaxValue");
                AvgEmissionValueTextBox.DataBindings.Add("Text", emissionsController, "CurrentEmission.AvgValue");
                YearNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentEmission.Year");
                MonthNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentEmission.Month");
                DayNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentEmission.Day");
            }
        }

        private void ElementsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            Data.Entity.Element element = e.ListItem as Data.Entity.Element;

            if (element != null)
            {
                e.Value = element.ToString("NM");
            }
        }

        private void EmissionValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-') && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }
        private void EmissionValueTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = "0";
                }
            }
        }

        private void IssuesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeriesCheckedListBox.Enabled && SeriesCheckedListBox.CheckedIndices.Count != 0 && oldIssueIndex != -1)
            {
                selectedSeries[oldIssueIndex] = SeriesCheckedListBox.CheckedIndices.OfType<int>().ToArray();
            }

            if (IssuesCheckedListBox.SelectedIndex != -1)
            {
                var issue = IssuesCheckedListBox.Items[IssuesCheckedListBox.SelectedIndex] as Data.Entity.Issue;

                if (issue != null)
                {
                    SetSeries(issue);
                }
            }

            if (SeriesCheckedListBox.Items[0] == missingSeries)
            {
                SeriesCheckedListBox.Enabled = false;
            }
            else
            {
                SeriesCheckedListBox.Enabled = true;
            }

            oldIssueIndex = IssuesCheckedListBox.SelectedIndex;

            if (selectedSeries.ContainsKey(IssuesCheckedListBox.SelectedIndex))
            {
                foreach (var index in selectedSeries[IssuesCheckedListBox.SelectedIndex])
                {
                    SeriesCheckedListBox.SetItemChecked(index, true);
                }
            }
        }
        private void ElementsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnvironmentsCheckedListBox.SelectedIndex = -1;

            if (ElementsCheckedListBox.SelectedIndex != -1)
            {
                CurrentElementLable.Text = ElementsCheckedListBox.Items[ElementsCheckedListBox.SelectedIndex].ToString();
            }

            if (EnvironmentsCheckedListBox.CheckedIndices.Count != 0 && oldElementIndex != -1)
            {
                selectedEnvironments[oldElementIndex] = EnvironmentsCheckedListBox.CheckedIndices.OfType<int>().ToArray();
            }

            oldElementIndex = ElementsCheckedListBox.SelectedIndex;

            ClearSelectedItems(EnvironmentsCheckedListBox);

            if (selectedEnvironments.ContainsKey(ElementsCheckedListBox.SelectedIndex))
            {
                foreach (var index in selectedEnvironments[ElementsCheckedListBox.SelectedIndex])
                {
                    EnvironmentsCheckedListBox.SetItemChecked(index, true);
                }
            }

            EnvironmentsCheckedListBox.SelectedIndex = 0;
        }
        private void EnvironmentsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EnvironmentsCheckedListBox.SelectedIndex == -1 ||
                ElementsCheckedListBox.SelectedIndex == -1)
            {
                return;
            }

            int findEmissionIndex = (ElementsCheckedListBox.SelectedIndex * EnvironmentsCheckedListBox.Items.Count) + EnvironmentsCheckedListBox.SelectedIndex;
            int emissionIndex = FindEmissionById(findEmissionIndex);

            if (emissionIndex == -1)
            {
                var emission = new Data.Entity.Emission
                {
                    Id = findEmissionIndex,
                    Element = ElementsCheckedListBox.Items[ElementsCheckedListBox.SelectedIndex] as Data.Entity.Element,
                    Environment = EnvironmentsCheckedListBox.Items[EnvironmentsCheckedListBox.SelectedIndex] as Data.Entity.Environment,
                };

                emissionsController.AddEmission(emission);
                emissionsController.CurrentEmissionIndex = emissionsController.Emissions.Count - 1;
            }
            else
            {
                emissionsController.CurrentEmissionIndex = emissionIndex;
            }
        }

        private void CurrentElementLable_TextChanged(object sender, EventArgs e)
        {
            string text = CurrentElementLable.Text;

            if (text.Length > 25)
            {
                CurrentElementLable.Text = text.Remove(25) + "...";
            }
        }

        private void m_AcceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ObjectNameTextBox.Text))
            {
                MessageBox.Show("Ви не заповнили усі необхідні поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IssuesCheckedListBox.CheckedItems.Count == 0 && EnvironmentsCheckedListBox.CheckedItems.Count == 0 &&
                MessageBox.Show("Ви не вибрали жодної прив'язки для об'єкта. Якщо ви продовжите, " +
                                "то об'єкт не можливо буде фільтрувати на карті.",
                                "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            IssuesCheckedListBox.SelectedIndex = -1;
            ElementsCheckedListBox.SelectedIndex = -1;

            oldObjName = ObjectNameTextBox.Text;
            oldObjDescription = ObjectDescriptionTextBox.Text;
            oldSelectedElements = ElementsCheckedListBox.CheckedIndices.OfType<int>().ToList();
            oldSelectedIssues = IssuesCheckedListBox.CheckedIndices.OfType<int>().ToList();
            oldSelectedSeries = CopySelectedIndiciesDictionary(selectedSeries);
            oldSelectedEnvironments = CopySelectedIndiciesDictionary(selectedEnvironments);

            this.DialogResult = DialogResult.OK;
        }

        private IDictionary<int, IEnumerable<int>> CopySelectedIndiciesDictionary(IDictionary<int, IEnumerable<int>> oldDictionary)
        {
            var newDictionary = new Dictionary<int, IEnumerable<int>>();

            foreach (var keyValuePair in oldDictionary)
            {
                var selectedIndices = new int[keyValuePair.Value.Count()];
                int index = 0;
                foreach (var _index in keyValuePair.Value)
                {
                    selectedIndices[index++] = _index;
                }

                newDictionary.Add(keyValuePair.Key, selectedIndices);
            }

            return newDictionary;
        }

        public void Deconstruct(out string name, out string description, 
                                out IDictionary<Data.Entity.Issue, List<Data.Entity.CalculationSeries>> series,
                                out IEnumerable<Data.Entity.Emission> emissions)
        {
            series = GetSeries();
            name = ObjectNameTextBox.Text;
            description = ObjectDescriptionTextBox.Text;
            emissions = GetEmissions();
        }

        private void m_CancelButton_Click(object sender, EventArgs e)
        {
            if (oldSelectedIssues != null && oldSelectedIssues.Any())
            {
                ClearSelectedItems(IssuesCheckedListBox);
                SelectCheckedListBoxItems(IssuesCheckedListBox, oldSelectedIssues);
            }

            if (oldSelectedElements != null && oldSelectedElements.Any())
            {
                ClearSelectedItems(ElementsCheckedListBox);
                SelectCheckedListBoxItems(ElementsCheckedListBox, oldSelectedElements);
            }

            if (oldSelectedEnvironments != null && oldSelectedEnvironments.Any())
            {
                selectedEnvironments = CopySelectedIndiciesDictionary(oldSelectedEnvironments);
                ClearSelectedItems(EnvironmentsCheckedListBox);
                ElementsCheckedListBox.SelectedIndex = 0;
            }

            if (oldSelectedSeries != null && oldSelectedSeries.Any())
            {
                ClearSelectedItems(SeriesCheckedListBox);
                selectedSeries = CopySelectedIndiciesDictionary(oldSelectedSeries);
                SeriesCheckedListBox.SelectedIndex = 0;
            }
        }
    }
}
