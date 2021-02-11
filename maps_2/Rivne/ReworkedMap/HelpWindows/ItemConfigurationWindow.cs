using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMap.Helpers;

namespace UserMap.HelpWindows
{
    public partial class ItemConfigurationWindow : Form
    {
        private static readonly Data.Entity.CalculationSeries missingSeries;
        private readonly Services.ILogger logger;

        private int oldIssueIndex;
        private int oldElementIndex;
        private string oldObjName;
        private string oldObjDescription;

        private IEnumerable<int> oldSelectedIssues;
        private IEnumerable<int> oldSelectedElements;
        private IDictionary<int, IEnumerable<int>> oldSelectedSeries;
        private IDictionary<int, IEnumerable<int>> oldSelectedEnvironments;

        private Role expert;

        private ViewModel.ControllerVM<Data.Entity.Emission> emissionsController;
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
            emissionsController = new ViewModel.ControllerVM<Data.Entity.Emission>();
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
            logger = new Services.FileLogger();

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
        public IDictionary<Data.Entity.Issue, IEnumerable<Data.Entity.CalculationSeries>> GetSeries()
        {
            var _series = new Dictionary<Data.Entity.Issue, IEnumerable<Data.Entity.CalculationSeries>>();

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

            if (ElementsCheckedListBox.CheckedItems.Count != 0)
            {
                foreach (int index in ElementsCheckedListBox.CheckedIndices)
                {
                    foreach (var envIndex in selectedEnvironments[index])
                    {
                        var selectedEmission = emissionsController.Elements.FirstOrDefault(emission => emission.Id == index * environmentCount + envIndex);

                        if (selectedEmission != null)
                        {
                            checkedEmissions.Add(selectedEmission);
                        }
                    }
                }
            }

            return checkedEmissions;
        }

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
        public void SetSelectedIssues(IEnumerable<Data.Entity.Issue> issues)
        {
            if (issues == null)
            {
                throw new ArgumentNullException("issues");
            }
            if (IssuesCheckedListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("Не возможно установить выбранные задачи. Для начала загрузите их явно.");
            }

            var innerIssues = IssuesCheckedListBox.Items.OfType<Data.Entity.Issue>();

            foreach (Data.Entity.Issue issue in issues)
            {
                int index = innerIssues.IndexOf(_issue => _issue.Id == issue.Id || _issue.Name == issue.Name);
                if (index != -1)
                {
                    IssuesCheckedListBox.SetItemChecked(index, true);
                }
            }

            IssueCheckBox.Checked = true;
            IssuesCheckedListBox.SelectedIndex = 0;
        }
        public void SetSelectedElements(IEnumerable<Data.Entity.Element> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }
            if (ElementsCheckedListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("Не возможно установить выбранные елементы. Для начала загрузите их явно.");
            }

            var innerElement = ElementsCheckedListBox.Items.OfType<Data.Entity.Element>();

            foreach (Data.Entity.Element element in elements)
            {
                int index = innerElement.IndexOf(_element => _element.Code == element.Code || _element.Name == element.Name);
                if (index != -1)
                {
                    ElementsCheckedListBox.SetItemChecked(index, true);
                }
            }

            EmissionCheckBox.Checked = true;
            ElementsCheckedListBox.SelectedIndex = 0;
        }
        public void SetSelectedSeries(IDictionary<Data.Entity.Issue, IEnumerable<Data.Entity.CalculationSeries>> series)
        {
            if (series == null)
            {
                throw new ArgumentNullException("series");
            }

            var issues = IssuesCheckedListBox.Items.OfType<Data.Entity.Issue>();

            foreach (var keyValuePairs in series)
            {
                Task task = SetSeries(keyValuePairs.Key);
                task.Wait(10000);

                if (task.IsCanceled)
                {
                    throw new InvalidOperationException("Не возможно загрузить серии для установки. Попробуйте ещё раз.");
                }

                var innerSeries = this.series[keyValuePairs.Key.Id];
                var indices = new List<int>();

                foreach (var value in keyValuePairs.Value)
                {
                    indices.Add(innerSeries.IndexOf(_series => _series.Id == value.Id ||
                                                     _series.Name == value.Name));
                }

                int issueIndex = issues.IndexOf(issue => issue.Id == keyValuePairs.Key.Id ||
                                                issue.Name == keyValuePairs.Key.Name);

                IssuesCheckedListBox.SetItemChecked(issueIndex, true);
                selectedSeries[issueIndex] = indices;
            }

            IssuesCheckedListBox.SelectedIndex = 0;
            IssueCheckBox.Checked = true;
        }
        public void SetSelectedEmissions(IEnumerable<Data.Entity.Emission> emissions)
        {
            if (emissions == null)
            {
                throw new ArgumentNullException("emissions");
            }
            if (ElementsCheckedListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("Не возможно установить выбранные выбросы. Для начала загрузите явно елементы.");
            }
            if (EnvironmentsCheckedListBox.Items.Count == 0)
            {
                throw new InvalidOperationException("Не возможно установить выбранные выбросы. Для начала загрузите явно среды.");
            }

            var environments = EnvironmentsCheckedListBox.Items.OfType<Data.Entity.Environment>();
            var groupedEmissions = emissions.GroupBy(_emission => _emission.Element.Name);
            var elements = ElementsCheckedListBox.Items.OfType<Data.Entity.Element>();

            foreach (var emissionGroup in groupedEmissions)
            {
                var currentElement = emissionGroup.FirstOrDefault();
                var indices = new List<int>();
                foreach (var emission in emissionGroup)
                {
                    indices.Add(environments.IndexOf(environment => environment.Id == emission.Environment.Id ||
                                                     environment.Name == emission.Environment.Name));
                }

                if (currentElement != null && currentElement.Element != null)
                {
                    int elemIndex = elements.IndexOf(element => element.Code == currentElement.Element.Code ||
                                                     element.Name == currentElement.Element.Name);
                    ElementsCheckedListBox.SetItemChecked(elemIndex, true);
                    selectedEnvironments[elemIndex] = indices;
                }
            }

            ElementsCheckedListBox.SelectedIndex = 0;
            EmissionCheckBox.Checked = true;
        }

        private int FindEmissionById(int id)
        {
            for (int i = 0; i < emissionsController.Elements.Count; i++)
            {
                if (emissionsController.Elements[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        private async Task SetSeries(Data.Entity.Issue issue)
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
        }

        public Task LoadIssuesAsync()
        {
            IssuesCheckedListBox.Items.Clear();
            selectedSeries.Clear();
            oldSelectedSeries = null;
            oldSelectedIssues = null;

            return FillAndReturnCheckedListBoxFromDBAsync(IssuesCheckedListBox, "issues", "issue_id, name", "",
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
        public Task LoadElementsAsync()
        {
            ElementsCheckedListBox.Items.Clear();
            selectedEnvironments.Clear();
            oldSelectedEnvironments = null;
            oldSelectedElements = null;
            emissionsController.Clear();

            return FillAndReturnCheckedListBoxFromDBAsync(ElementsCheckedListBox, "elements", "*", "",
                                                         r => Data.Helpers.Mapper.Map<Data.Entity.Element>(r));
        }
        public Task LoadEnvironments()
        {
            selectedEnvironments.Clear();
            oldSelectedEnvironments = null;
            emissionsController.Clear();

            return FillAndReturnCheckedListBoxFromDBAsync(EnvironmentsCheckedListBox, "environment", "*", "",
                                                          r => Data.Entity.EnvironmentMapper.Map(r));
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
                logger.Log(ex);
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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Сталась помилка при завантаженні серій. Спробуйте ще раз.", 
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
            }

            return new List<Data.Entity.CalculationSeries>();
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
                await LoadIssuesAsync();
            }
        }
        private async void EmissionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ElementsCheckedListBox.Items.Count == 0)
            {
                await LoadElementsAsync();

                if (EnvironmentsCheckedListBox.Items.Count == 0)
                {
                    await LoadEnvironments();
                }

                ElementsCheckedListBox.SelectedIndex = 0;
                EnvironmentsCheckedListBox.SelectedIndex = 0;

                MaxEmissionValueTextBox.DataBindings.Add("Text", emissionsController, "CurrentElement.MaxValue",
                                                         true, DataSourceUpdateMode.OnPropertyChanged, 0);
                AvgEmissionValueTextBox.DataBindings.Add("Text", emissionsController, "CurrentElement.AvgValue",
                                                         true, DataSourceUpdateMode.OnPropertyChanged, 0);
                YearNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentElement.Year",
                                                   true, DataSourceUpdateMode.OnPropertyChanged, DateTime.Now.Year);
                MonthNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentElement.Month",
                                                    true, DataSourceUpdateMode.OnPropertyChanged, 1);
                DayNumericUpDown.DataBindings.Add("Value", emissionsController, "CurrentElement.Day",
                                                  true, DataSourceUpdateMode.OnPropertyChanged, 1);
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

        private async void IssuesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeriesCheckedListBox.Enabled && oldIssueIndex != -1)
            {
                selectedSeries[oldIssueIndex] = SeriesCheckedListBox.CheckedIndices.OfType<int>().ToArray();
            }

            if (IssuesCheckedListBox.SelectedIndex != -1)
            {
                var issue = IssuesCheckedListBox.Items[IssuesCheckedListBox.SelectedIndex] as Data.Entity.Issue;

                if (issue != null)
                {
                    await SetSeries(issue);

                    SeriesCheckedListBox.Items.Clear();

                    foreach (var series in series[issue.Id])
                    {
                        SeriesCheckedListBox.Items.Add(series);
                    }
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

            if (oldElementIndex != -1)
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

                emissionsController.AddElement(emission);
                emissionsController.CurrentElementIndex = emissionsController.Elements.Count - 1;
            }
            else
            {
                emissionsController.CurrentElementIndex = emissionIndex;
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
            IssuesCheckedListBox.SelectedIndex = -1;
            ElementsCheckedListBox.SelectedIndex = -1;

            if (string.IsNullOrEmpty(ObjectNameTextBox.Text))
            {
                MessageBox.Show("Ви не заповнили усі необхідні поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CheckSelectedElementsMatchedSelectedEnvironments())
            {
                MessageBox.Show("Для кожного вибраного елемента " +
                                "необхідно вибрати мінімум одне середовище.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IssuesCheckedListBox.CheckedItems.Count == 0 && EnvironmentsCheckedListBox.CheckedItems.Count == 0 &&
                MessageBox.Show("Ви не вибрали жодної прив'язки для об'єкта. Якщо ви продовжите, " +
                                "то об'єкт не можливо буде фільтрувати на карті.",
                                "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            oldObjName = ObjectNameTextBox.Text;
            oldObjDescription = ObjectDescriptionTextBox.Text;
            oldSelectedElements = ElementsCheckedListBox.CheckedIndices.OfType<int>().ToList();
            oldSelectedIssues = IssuesCheckedListBox.CheckedIndices.OfType<int>().ToList();
            oldSelectedSeries = CopySelectedIndiciesDictionary(selectedSeries);
            oldSelectedEnvironments = CopySelectedIndiciesDictionary(selectedEnvironments);

            this.DialogResult = DialogResult.OK;
        }

        private bool CheckSelectedElementsMatchedSelectedEnvironments()
        {
            foreach (int index in ElementsCheckedListBox.CheckedIndices)
            {
                if (selectedEnvironments.ContainsKey(index))
                {
                    if (!selectedEnvironments[index].Any())
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
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
                                out IDictionary<Data.Entity.Issue, IEnumerable<Data.Entity.CalculationSeries>> series,
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
