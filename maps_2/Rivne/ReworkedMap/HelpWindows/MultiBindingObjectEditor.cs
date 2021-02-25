using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMap.Helpers;
using Data;
using Data.Entity;
using UserMap.ViewModel;

namespace UserMap.HelpWindows
{
    public partial class MultiBindingObjectEditor : Form
    {
        private bool isRestoring;
        private bool isReadOnly;
        private bool bingingsSet;
        private bool isAddingMode;
        private int editingElemIndex;
        private int oldIssueIndex;

        private int objId;
        private Core.IDescribable describable;

        private ControllerVM<Services.IController> controller;
        private Dictionary<int, List<CalculationSeries>> issuesSeries;
        private IDictionary<int, IEnumerable<int>> selectedSeries;

        private DBManager dbManager;
        private Services.ILogger logger;

        public MultiBindingObjectEditor(int objId, Core.IDescribable describable, bool isReadOnly)
        {
            InitializeComponent();

            bingingsSet = false;
            this.isReadOnly = isReadOnly;
            this.describable = describable;
            this.objId = objId;

            controller = new ControllerVM<Services.IController>();
            controller.AddElement(new ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>());
            controller.AddElement(new ControllerVM<Emission>());

            dbManager = new DBManager();

            if (describable == null)
            {
                throw new ArgumentNullException("describable");
            }

            issuesSeries = new Dictionary<int, List<CalculationSeries>>();
            selectedSeries = new Dictionary<int, IEnumerable<int>>();

            YearNumericUpDown.Maximum = DateTime.Now.Year;

            editingElemIndex = -1;
            logger = new Services.FileLogger();
        }

        public void AddNewPage(string pageName, Control content)
        {
            var tabPages = ContentContainerTabControl.TabPages;
            tabPages.Add(pageName);

            tabPages[tabPages.Count - 1].Controls.Add(content);
            content.Dock = DockStyle.Fill;
            content.Margin = new Padding(10);
        }

        private void SetBinings()
        {
            if (!bingingsSet && ((ControllerVM<Emission>)controller.CurrentElement).Elements.Count != 0)
            {
                var emissionController = (ControllerVM<Emission>)controller.CurrentElement;

                var maxValueBinding = new Binding("Text", emissionController, "CurrentElement.MaxValue",
                                                  true, DataSourceUpdateMode.OnPropertyChanged, 0);
                var avgValueBinding = new Binding("Text", emissionController, "CurrentElement.AvgValue",
                                                  true, DataSourceUpdateMode.OnPropertyChanged, 0);
                var yearBinding = new Binding("Value", emissionController, "CurrentElement.Year",
                                              true, DataSourceUpdateMode.OnPropertyChanged, DateTime.Now.Year);
                var monthBinding = new Binding("Value", emissionController, "CurrentElement.Month",
                                               true, DataSourceUpdateMode.OnPropertyChanged, 1);
                var dayBinding = new Binding("Value", emissionController, "CurrentElement.Day",
                                             true, DataSourceUpdateMode.OnPropertyChanged, 1);

                MaxValueTextBox.DataBindings.Add(maxValueBinding);
                AvgValueTextBox.DataBindings.Add(avgValueBinding);
                YearNumericUpDown.DataBindings.Add(yearBinding);
                MonthNumericUpDown.DataBindings.Add(monthBinding);
                DayNumericUpDown.DataBindings.Add(dayBinding);

                bingingsSet = true;
            }
        }
        private void InitializeAddionalComponent()
        {
            SetBinings();

            if (!isReadOnly)
            {

                var emissionController = (ControllerVM<Emission>)GetControllerByIndex(1);

                if (emissionController.Elements.Any())
                {
                    EnableEmissionEditAndDelete();
                }

                AddEmissionButton.Enabled = true;
            }

            this.Text += $"{describable.Name} ({describable.Type})";

            SetControllerIndex();
        }

        private async void MultiBindingObjectEditor_Load(object sender, EventArgs e)
        {
            var tasks = new List<Task>();

            if (MapCache.ContainsKey("environments"))
            {
                EnvironmentsComboBox.DataSource = MapCache.GetItem("environments");
            }
            else
            {
                tasks.Add(EnvironmentsComboBox.FillComboBoxFromBDAsync(dbManager, "environment", "*", "",
                              row => Data.Helpers.Mapper.Map<Data.Entity.Environment>(row),
                              displayComboBoxMember: "Name",
                              valueComboBoxMember: "Id",
                              falultAction: c =>
                              {
                                  c.Items.Clear();
                                  c.Items.Add(new Data.Entity.Environment { Id = -1, Name = "Не вдалось завантажити середовища." });
                              })
                    );
            }

            if (MapCache.ContainsKey("elements"))
            {
                ElementsComboBox.DataSource = MapCache.GetItem("elements");
            }
            else
            {
                tasks.Add(ElementsComboBox.FillComboBoxFromBDAsync(dbManager, "elements", "*", "",
                              row => Data.Helpers.Mapper.Map<Element>(row),
                              displayComboBoxMember: "Name",
                              valueComboBoxMember: "Code",
                              falultAction: c =>
                              {
                                  c.Items.Clear();
                                  c.Items.Add(new Element { Code = -1, Name = "Не вдалось завантажити елементи." });
                              })
                              .ContinueWith(result => MapCache.Add("elements", ElementsComboBox.DataSource),
                                            TaskScheduler.FromCurrentSynchronizationContext())
                          );


            }

            tasks.Add(dbManager.GetRowsAsync("issues", "*", "")
                               .ContinueWith(task => task.Result.Select(row => Data.Helpers.Mapper.Map<Issue>(row)).ToList())
                               .ContinueWith(task =>
                               {
                                   IssuesCheckedListBox.DataSource = task.Result;
                               }, System.Threading.CancellationToken.None,
                                  TaskContinuationOptions.OnlyOnRanToCompletion,
                                  TaskScheduler.FromCurrentSynchronizationContext()));

            if (tasks.Count != 0)
            {
                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
#if !DEBUG
                    logger.Log(ex);
#else
                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                }
            }

            try
            {
                await Task.WhenAll(LoadEmissions(), LoadIssuesAndSeries())
                          .ContinueWith(FillDataGrid, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
#if !DEBUG
                logger.Log(ex);
#else
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }

        private Task LoadEmissions()
        {
            string objColumn = describable.Type == "Маркер" ? "idPoi" : "idPoligon";
            var environments = (IEnumerable<Data.Entity.Environment>)EnvironmentsComboBox.DataSource;
            var elements = (IEnumerable<Element>)ElementsComboBox.DataSource;

            return dbManager.GetRowsAsync("emissions_on_map",
                                         "id, ValueAvg, ValueMax, Year, Month, day, idElement, idEnvironment",
                                         objColumn + " = " + objId.ToString())
                            .ContinueWith(result =>
                            {
                                var emissionController = (ControllerVM<Emission>)GetControllerByIndex(1);

                                foreach (var row in result.Result)
                                {
                                    var emission = Data.Helpers.Mapper.Map<Emission>(row);
                                    emission.Element = elements.First(elem => elem.Code == (int)row[6]);
                                    emission.Environment = environments.First(env => env.Id == (int)row[7]);

                                    emissionController.AddElement(emission);
                                }
                            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        private Task LoadIssuesAndSeries()
        {
            string objType = describable.Type == "Маркер" ? "0" : "1";
            string dependenciesTables = "map_object_dependencies, issues, calculations_description";
            string dependenciesColumns = "issues.issue_id, issues.name, calculations_description.calculation_number, " +
                                         "calculations_description.calculation_name, " +
                                         "calculations_description.issue_id";
            string dependenciesJoinCondition = "map_object_dependencies.type_rel = 0 AND " +
                                               "map_object_dependencies.id_of_ref = issues.issue_id, " +
                                               "map_object_dependencies.type_rel = 1 AND " +
                                               "map_object_dependencies.id_of_ref = calculations_description.calculation_number";
            string dependenciesCondition = "type_obj = " + (describable.Type == "Маркер" ? "0" : "1") +
                                           " AND id_of_object = " + objId.ToString();


            return dbManager.GetRowsUsingJoinAsync(dependenciesTables, dependenciesColumns,
                                                   dependenciesJoinCondition, dependenciesCondition, JoinType.LEFT)
                            .ContinueWith(result =>
                            {
                                var issueController = (ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)GetControllerByIndex(0);

                                var _result = result.Result;
                                var issues = _result.Where(row => !(row[0] is DBNull));

                                var res = issues.GroupJoin(_result, outer => outer[0], inner => inner[4], (row, inner) =>
                                           {
                                               var seriesController = new ControllerVM<CalculationSeries>();

                                               foreach (var innerItem in inner)
                                               {
                                                   var calculationSeries = new CalculationSeries { Id = (int)innerItem[2], Name = innerItem[3].ToString() };

                                                   seriesController.AddElement(calculationSeries);
                                               }

                                               var issue = new Issue { Id = (int)row[0], Name = row[1].ToString() };
                                               var currentIssue = new KeyValuePair<Issue, ControllerVM<CalculationSeries>>(issue, seriesController);

                                               issueController.AddElement(currentIssue);

                                               return currentIssue;
                                           });

                                return res;
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {
                                var res = result.Result;

                                var tasks = new List<Task>();
                                foreach (var item in res)
                                {
                                    tasks.Add(SetSeries(item.Key));
                                }

                                return Task.WhenAll();
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(CheckIssuesAndSeries, System.Threading.CancellationToken.None,
                                          TaskContinuationOptions.OnlyOnRanToCompletion,
                                          TaskScheduler.FromCurrentSynchronizationContext());
        }
        private async Task<List<CalculationSeries>> LoadSeriesByIssue(int issueId)
        {
            string condition = $"issue_id = {issueId}";

            try
            {
                return (await dbManager.GetRowsAsync("calculations_description", "calculation_number, calculation_name", condition))
                                       .Select(r =>
                                       {
                                           int id;
                                           int.TryParse(r[0].ToString(), out id);

                                           return new CalculationSeries
                                           {
                                               Id = id,
                                               Name = r[1].ToString()
                                           };
                                       })
                                       .ToList();
            }
            catch (Exception ex)
            {
#if !DEBUG
                MessageBox.Show("Сталась помилка при завантаженні серій. Спробуйте ще раз.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Log(ex);
#endif

            }

            return new List<Data.Entity.CalculationSeries>();
        }
        private async Task SetSeries(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException();
            }

            if (!issuesSeries.ContainsKey(issue.Id))
            {
                var loadedSeries = await LoadSeriesByIssue(issue.Id);

                if (loadedSeries.Count == 0)
                {
                    loadedSeries.Add(new CalculationSeries { Id = -1, Name = "Для даної задачі серії відсутні." });
                }

                issuesSeries[issue.Id] = loadedSeries;
            }
        }
        private void CheckIssuesAndSeries(Task result)
        {
            int index = -1;
            var currentController = (ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)GetControllerByIndex(0);

            var issueEnumerator = ((List<Issue>)IssuesCheckedListBox.DataSource).GetEnumerator();
            var findIssueEnumerator = currentController.Elements.OrderBy(pair => pair.Key.Id).GetEnumerator();

            if (!findIssueEnumerator.MoveNext())
            {
                return;
            }

            IssuesCheckedListBox.SelectedIndex = -1;

            while (issueEnumerator.MoveNext())
            {
                ++index;
                if (issueEnumerator.Current.Id == findIssueEnumerator.Current.Key.Id)
                {
                    IssuesCheckedListBox.SetItemChecked(index, true);
                    IssuesCheckedListBox.SelectedIndex = index;

                    if (!findIssueEnumerator.MoveNext())
                    {
                        break;
                    }
                }
            }
        }
        private void FillDataGrid(Task continueTask)
        {
            var emissionController = (ControllerVM<Emission>)GetControllerByIndex(1);

            BindingSource bindingSource = new BindingSource();
            bindingSource.AllowNew = false;
            bindingSource.DataSource = emissionController.Elements;

            EmissionsDataGridView.DataSource = bindingSource;

            string[] headers = { "Id", "Середнє значення", "Максимальне значення", "Рік",
                                 "Місяць", "День", "Елемент", "Середовище"};
            int index = 0;

            foreach (DataGridViewColumn column in EmissionsDataGridView.Columns)
            {
                column.HeaderText = headers[index++];

                if (column.Name == "Element" || column.Name == "Environment")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                if (column.Name == "Id")
                {
                    column.Visible = false;
                }
            }

            InitializeAddionalComponent();

            if (emissionController.Elements.Count == 0)
            {
                emissionController.CurrentElementIndex = -1;
            }
            else
            {
                emissionController.CurrentElementIndex = 0;
            }
        }

        private void ClearCheckedListBoxSelections(CheckedListBox checkedListBox)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SelectedIndex = i;
                checkedListBox.SetItemChecked(i, false);
            }
        }
        private void ResetDataGridView()
        {
            ((BindingSource)EmissionsDataGridView.DataSource).ResetBindings(false);
        }

        private void CheckElementsCheckedListBox<T>(CheckedListBox checkedListBox, IEnumerable<T> elements,
                                                    Func<T, T, bool> comparator)
        {
            if (comparator == null)
            {
                throw new ArgumentNullException("comparator");
            }

            var index = -1;
            var currentEnumerator = ((IEnumerable<T>)checkedListBox.DataSource).GetEnumerator();
            var elementsEnumerator = elements.GetEnumerator();

            if (!elementsEnumerator.MoveNext())
            {
                return;
            }

            while (currentEnumerator.MoveNext())
            {
                ++index;
                if (comparator(currentEnumerator.Current, elementsEnumerator.Current))
                {
                    checkedListBox.SelectedIndex = index;
                    checkedListBox.SetItemChecked(index, true);

                    if (!elementsEnumerator.MoveNext())
                    {
                        break;
                    }
                }
            }
        }

        private void StartEmissionAction()
        {
            CurrentEmisionGroupBox.Enabled = true;

            AddEmissionButton.Text = "Відміна";
            ChangeEmissionButton.Text = "Зберегти";
            DeleteEmissionButton.Enabled = false;
        }
        private void EndEmissionAction()
        {
            CurrentEmisionGroupBox.Enabled = false;

            AddEmissionButton.Text = "Додати";
            ChangeEmissionButton.Text = "Змінити";
            DeleteEmissionButton.Enabled = true;

            isAddingMode = false;
            editingElemIndex = -1;

            ChangeSaveToDBAndRestoreButton();
        }
        private void EnableEmissionEditAndDelete()
        {
            ChangeEmissionButton.Enabled = true;
            DeleteEmissionButton.Enabled = true;
        }
        private void DisableEmissionEditAndDelete()
        {
            ChangeEmissionButton.Enabled = false;
            DeleteEmissionButton.Enabled = false;
        }
        private void ChangeSaveToDBAndRestoreButton()
        {
            bool condition = false;
            var currentController = controller.CurrentElement;

            switch (ContentContainerTabControl.SelectedIndex)
            {
                case 0:
                    condition = currentController.ChangedELementsCount != 0;

                    if (!condition)
                    {
                        foreach (var changedElem in currentController.Elements)
                        {
                            condition = ((KeyValuePair<Issue, ControllerVM<CalculationSeries>>)changedElem).Value.ChangedELementsCount != 0 || condition;
                        }
                    }
                    break;
                case 1:
                    condition = currentController.ChangedELementsCount != 0;
                    break;
                default:
                    break;
            }

            if (condition)
            {
                EnableSaveToDBAndRestoreButton();
            }
            else
            {
                DisableSaveToDBAndRestoreButton();
            }
        }
        private void EnableSaveToDBAndRestoreButton()
        {
            RestoreButton.Enabled = true;
            SaveToBDButton.Enabled = true;
        }
        private void DisableSaveToDBAndRestoreButton()
        {
            SaveToBDButton.Enabled = false;
            RestoreButton.Enabled = false;
        }

        private Services.IController GetControllerByIndex(int index)
        {
            return controller.Elements.ElementAt(index);
        }
        private void SetControllerIndex(int index)
        {
            controller.CurrentElementIndex = index;
        }
        private void SetControllerIndex()
        {
            if (ContentContainerTabControl.SelectedIndex != -1 && ContentContainerTabControl.SelectedIndex < controller.Elements.Count)
            {
                controller.CurrentElementIndex = ContentContainerTabControl.SelectedIndex;
            }
        }

        private async Task SaveEmissionsToDB()
        {
            var changedEmission = ((ControllerVM<Emission>)controller.CurrentElement).ChangedElements;

            if (changedEmission.Count != 0)
            {
                string emissionTable = "emissions_on_map";
                string objColumn = describable.Type == "Маркер" ? "idPoi" : "idPoligon";
                var culture = System.Globalization.CultureInfo.InvariantCulture;

                var addedElements = changedEmission.Where(emission => emission.Value.Value == ChangeType.Added);
                var changedElements = changedEmission.Where(emission => emission.Value.Value == ChangeType.Changed);
                var deletedElements = changedEmission.Where(emission => emission.Value.Value == ChangeType.Deleted);

                dbManager.StartTransaction();

                if (addedElements.Any())
                {
                    try
                    {
                        string emissionColumns = "(" + objColumn + ", idElement, idEnvironment, ValueAvg, " +
                                                 "ValueMax, Year, Month, day, Measure) VALUES ";
                        var insertQuery = new StringBuilder($"INSERT INTO " + emissionTable + emissionColumns);

                        foreach (var addedElem in addedElements)
                        {
                            var currentEmission = (Emission)addedElem.Key;

                            insertQuery.Append('(');
                            insertQuery.Append(objId.ToString() + ", ");
                            insertQuery.Append(currentEmission.Element.Code.ToString() + ", ");
                            insertQuery.Append(currentEmission.Environment.Id.ToString() + ", ");
                            insertQuery.Append(currentEmission.AvgValue.ToString(culture) + ", ");
                            insertQuery.Append(currentEmission.MaxValue.ToString(culture) + ", ");
                            insertQuery.Append(currentEmission.Year.ToString() + ", ");
                            insertQuery.Append(currentEmission.Month.ToString() + ", ");
                            insertQuery.Append(currentEmission.Day.ToString());
                            insertQuery.AppendFormat(", \"{0}\"), ", currentEmission.Element.Measure);
                        }

                        insertQuery.Remove(insertQuery.Length - 2, 1);

                        await dbManager.InsertToBDAsync(insertQuery.ToString());
                    }
                    catch (Exception ex)
                    {
                        dbManager.RollbackTransaction();
#if DEBUG
                        MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                        return;
                    }
                }

                if (changedElements.Any())
                {
                    try
                    {
                        var updateQuery = new StringBuilder();

                        foreach (var changedElem in changedElements)
                        {
                            var currentEmission = changedElem.Key;
                            var originalEmission = changedElem.Value.Key;

                            updateQuery.Append("UPDATE ");
                            updateQuery.Append(emissionTable);
                            updateQuery.Append(" SET idElement = " + currentEmission.Element.Code.ToString());
                            updateQuery.Append(", idEnvironment = " + currentEmission.Environment.Id.ToString());
                            updateQuery.Append(", ValueAvg = " + currentEmission.AvgValue.ToString(culture));
                            updateQuery.Append(", ValueMax = " + currentEmission.MaxValue.ToString(culture));
                            updateQuery.Append(", Year = " + currentEmission.Year.ToString());
                            updateQuery.Append(", Month = " + currentEmission.Month.ToString());
                            updateQuery.Append(", day = " + currentEmission.Day.ToString());
                            updateQuery.AppendFormat(", Measure = \"{0}\" ", currentEmission.Element.Measure);
                            updateQuery.Append("WHERE");
                            updateQuery.AppendFormat(" {0} = {1} ", objColumn, objId.ToString());
                            updateQuery.Append(" AND idElement = " + originalEmission.Element.Code.ToString());
                            updateQuery.Append(" AND idEnvironment = " + originalEmission.Environment.Id.ToString());
                            updateQuery.Append(" AND ValueAvg = " + originalEmission.AvgValue.ToString(culture));
                            updateQuery.Append(" AND ValueMax = " + originalEmission.MaxValue.ToString(culture));
                            updateQuery.Append(" AND Year = " + originalEmission.Year.ToString());
                            updateQuery.Append(" AND Month = " + originalEmission.Month.ToString());
                            updateQuery.Append(" AND day = " + originalEmission.Day.ToString());
                            updateQuery.AppendFormat(" AND Measure = \"{0}\";\n", originalEmission.Element.Measure);
                        }

                        await dbManager.UpdateRecordAsync(updateQuery.ToString());
                    }
                    catch (Exception ex)
                    {
                        dbManager.RollbackTransaction();
#if DEBUG
                        MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                        return;
                    }
                }

                if (deletedElements.Any())
                {
                    try
                    {
                        var deleteQuery = new StringBuilder("DELETE FROM " + emissionTable + " WHERE ");

                        foreach (var deletedElem in deletedElements)
                        {
                            var originalEmission = (Emission)deletedElem.Value.Key;

                            deleteQuery.AppendFormat("({0} = {1} ", objColumn, objId.ToString());
                            deleteQuery.Append(" AND idElement = " + originalEmission.Element.Code.ToString());
                            deleteQuery.Append(" AND idEnvironment = " + originalEmission.Environment.Id.ToString());
                            deleteQuery.Append(" AND ValueAvg = " + originalEmission.AvgValue.ToString(culture));
                            deleteQuery.Append(" AND ValueMax = " + originalEmission.MaxValue.ToString(culture));
                            deleteQuery.Append(" AND Year = " + originalEmission.Year.ToString());
                            deleteQuery.Append(" AND Month = " + originalEmission.Month.ToString());
                            deleteQuery.Append(" AND day = " + originalEmission.Day.ToString());
                            deleteQuery.AppendFormat(" AND Measure = \"{0}\") OR ", originalEmission.Element.Measure);
                        }
                        deleteQuery.Remove(deleteQuery.Length - 3, 2);

                        await dbManager.DeleteFromDBAsync(deleteQuery.ToString());
                    }
                    catch (Exception ex)
                    {
                        dbManager.RollbackTransaction();
#if DEBUG
                        MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                        return;
                    }
                }

                dbManager.CommitTransaction();

                MessageBox.Show("Усі зміни збережені до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ((ControllerVM<Emission>)controller.CurrentElement).ClearChangedElems();
            }
        }
        private async Task SaveIssues()
        {
            var currController = ((ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)controller.CurrentElement);
            var changedIssues = currController.ChangedElements;

            var addedIssues = changedIssues.Where(elem => elem.Value.Value == ChangeType.Added);
            var deletedIssues = changedIssues.Where(elem => elem.Value.Value == ChangeType.Deleted);
            var issues = currController.Elements.Except(addedIssues.Select(added => added.Key));

            string objType = describable.Type == "Маркер" ? "0" : "1";
            string strObjId = objId.ToString();

            dbManager.StartTransaction();

            if (addedIssues.Any())
            {
                try
                {
                    string mapObjectDependenciesColumn = " (id_of_object, type_obj, id_of_ref, type_rel) VALUES ";
                    string startValueInsert = "(" + strObjId + ", 0, ";
                    var insertQuery = new StringBuilder($"INSERT INTO map_object_dependencies" + mapObjectDependenciesColumn);

                    foreach (var addedElem in addedIssues)
                    {
                        var currentIssue = addedElem.Key;

                        insertQuery.Append(startValueInsert);
                        insertQuery.Append(currentIssue.Key.Id);
                        insertQuery.Append(", 0), ");

                        var addedCalcSeries = currentIssue.Value.ChangedElements.Where(calcSeries => calcSeries.Value.Value == ChangeType.Added);

                        foreach (var _addedCalcSeries in addedCalcSeries)
                        {
                            insertQuery.Append(startValueInsert);
                            insertQuery.Append(_addedCalcSeries.Key.Id);
                            insertQuery.Append(", 1), ");
                        }
                    }

                    insertQuery.Remove(insertQuery.Length - 2, 1);

                    await dbManager.InsertToBDAsync(insertQuery.ToString());
                }
                catch (Exception ex)
                {
                    dbManager.RollbackTransaction();
#if DEBUG
                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    return;
                }
            }

            if (deletedIssues.Any())
            {
                try
                {
                    var deleteQuery = new StringBuilder($"DELETE FROM map_object_dependencies WHERE ");
                    var deleteStartQuery = " (id_of_object = " + strObjId + " AND type_obj = " + objType;

                    foreach (var deletedIssue in deletedIssues)
                    {
                        var currentEmission = deletedIssue.Key;

                        deleteQuery.Append(deleteStartQuery);
                        deleteQuery.Append(" AND id_of_ref = ");
                        deleteQuery.Append(currentEmission.Key.Id);
                        deleteQuery.Append(" AND type_rel = 0) OR");

                        var deletedCalcSeries = currentEmission.Value.ChangedElements.Where(calcSeries => calcSeries.Value.Value == ChangeType.Deleted);

                        foreach (var _deletedCalcSeries in deletedCalcSeries)
                        {
                            deleteQuery.Append(deleteStartQuery);
                            deleteQuery.Append(" AND id_of_ref = ");
                            deleteQuery.Append(_deletedCalcSeries.Key.Id);
                            deleteQuery.Append(" AND type_rel = 1) OR");
                        }

                        foreach (var _deletedCalcSeries in currentEmission.Value.Elements)
                        {
                            deleteQuery.Append(deleteStartQuery);
                            deleteQuery.Append(" AND id_of_ref = ");
                            deleteQuery.Append(_deletedCalcSeries.Id);
                            deleteQuery.Append(" AND type_rel = 1) OR");
                        }
                    }

                    deleteQuery.Remove(deleteQuery.Length - 2, 2);

                    await dbManager.DeleteFromDBAsync(deleteQuery.ToString());
                }
                catch (Exception ex)
                {
                    dbManager.RollbackTransaction();
#if DEBUG
                    MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    return;
                }
            }

            if (issues.Any())
            {
                foreach (var issue in issues)
                {
                    var addedCalcSeries = issue.Value.ChangedElements.Where(calcSeires => calcSeires.Value.Value == ChangeType.Added);
                    var deletedCalcSeries = issue.Value.ChangedElements.Where(calcSeires => calcSeires.Value.Value == ChangeType.Deleted);

                    if (addedCalcSeries.Any())
                    {
                        try
                        {
                            string mapObjectDependenciesColumn = " (id_of_object, type_obj, id_of_ref, type_rel) VALUES ";
                            string startValueInsert = "(" + strObjId + ", 0, ";
                            var insertQuery = new StringBuilder($"INSERT INTO map_object_dependencies" + mapObjectDependenciesColumn);

                            foreach (var _addedCalcSeries in addedCalcSeries)
                            {
                                var currentSeries = _addedCalcSeries.Key;

                                insertQuery.Append(startValueInsert);
                                insertQuery.Append(currentSeries.Id);
                                insertQuery.Append(", 1), ");
                            }

                            insertQuery.Remove(insertQuery.Length - 2, 1);

                            await dbManager.InsertToBDAsync(insertQuery.ToString());
                        }
                        catch (Exception ex)
                        {
                            dbManager.RollbackTransaction();
#if DEBUG
                            MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                            return;
                        }
                    }

                    if (deletedCalcSeries.Any())
                    {
                        try
                        {
                            var deleteQuery = new StringBuilder($"DELETE FROM map_object_dependencies WHERE ");
                            var deleteStartQuery = " (id_of_object = " + strObjId + " AND type_obj = " + objType;

                            foreach (var _deletedCalcSeries in deletedCalcSeries)
                            {
                                var currentSeries = _deletedCalcSeries.Key;

                                deleteQuery.Append(deleteStartQuery);
                                deleteQuery.Append(" AND id_of_ref = ");
                                deleteQuery.Append(currentSeries.Id);
                                deleteQuery.Append(" AND type_rel = 1) OR");
                            }

                            deleteQuery.Remove(deleteQuery.Length - 2, 2);

                            await dbManager.DeleteFromDBAsync(deleteQuery.ToString());
                        }
                        catch (Exception ex)
                        {
                            dbManager.RollbackTransaction();
#if DEBUG
                            MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                        logger.Log(ex);
                        MessageBox.Show("Сталась помилка під час збереження змін.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                            return;
                        }
                    }
                }
            }

            dbManager.CommitTransaction();

            MessageBox.Show("Усі зміни збережені до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);

            currController.ClearChangedElems();

            foreach (var elem in currController.Elements)
            {
                elem.Value.ClearChangedElems();
            }
        }

        private void EmissionsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }

            var dataGird = sender as DataGridView;

            if (dataGird != null)
            {
                switch (dataGird.Columns[e.ColumnIndex].Name)
                {
                    case "Element":
                        var elem = e.Value as Element;
                        e.Value = elem.Name + " (" + elem.Measure + ")";
                        break;
                    default:
                        break;
                }
            }
        }
        private void EmissionsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dataGrid = sender as DataGridView;

            if (dataGrid.CurrentRow == null)
            {
                return;
            }

            var currentElemIndex = dataGrid.CurrentRow.Index;

            var currentController = (ControllerVM<Emission>)GetControllerByIndex(1);

            currentController.CurrentElementIndex = currentElemIndex;

            var currentElem = currentController.CurrentElement;

            if (isAddingMode && currentElemIndex == currentController.Elements.Count - 1 ||
                editingElemIndex != -1 && editingElemIndex == currentElemIndex)
            {
                CurrentEmisionGroupBox.Enabled = true;
            }
            else
            {
                CurrentEmisionGroupBox.Enabled = false;
            }

            if (currentElem.Environment != null)
                EnvironmentsComboBox.SelectedIndex = EnvironmentsComboBox.Items.IndexOf<Data.Entity.Environment>(env => env.Name == currentElem.Environment.Name);
            else
                EnvironmentsComboBox.SelectedIndex = 0;

            if (currentElem.Element != null)
                ElementsComboBox.SelectedIndex = ElementsComboBox.Items.IndexOf<Element>(elem => elem.Name == currentElem.Element.Name);
            else
                ElementsComboBox.SelectedIndex = 0;
        }

        private void AddEmissionButton_Click(object sender, EventArgs e)
        {
            var currentController = (ControllerVM<Emission>)controller.CurrentElement;

            if (AddEmissionButton.Text == "Додати")
            {
                currentController.StartAddingNewElement(new Emission());
                ElementsComboBox.SelectedIndex = -1;
                ElementsComboBox.SelectedIndex = 0;
                EnvironmentsComboBox.SelectedIndex = -1;
                EnvironmentsComboBox.SelectedIndex = 0;
                DisableSaveToDBAndRestoreButton();

                ChangeEmissionButton.Enabled = true;
                isAddingMode = true;

                SetBinings();
                StartEmissionAction();
            }
            else
            {
                currentController.CancelEditElement();
                currentController.CancelAddingNewElement();

                EndEmissionAction();

                if (currentController.Elements.Count == 0)
                {
                    DisableEmissionEditAndDelete();
                }
            }

            ResetDataGridView();
        }
        private void ChangeEmissionButton_Click(object sender, EventArgs e)
        {
            var currentController = (ControllerVM<Emission>)controller.CurrentElement;

            if (ChangeEmissionButton.Text == "Змінити")
            {
                currentController.StartEditElement();
                editingElemIndex = EmissionsDataGridView.CurrentRow.Index;
                DisableSaveToDBAndRestoreButton();

                StartEmissionAction();
            }
            else if (ChangeEmissionButton.Text == "Зберегти")
            {
                currentController.EndEditElement();
                currentController.EndAddingNewElement();

                if (currentController.Elements.Count > 0)
                {
                    EnableEmissionEditAndDelete();
                }

                EndEmissionAction();
            }

            ResetDataGridView();
        }
        private void DeleteEmissionButton_Click(object sender, EventArgs e)
        {
            ((ControllerVM<Emission>)controller.CurrentElement).RemoveElementAt(((ControllerVM<Emission>)controller.CurrentElement).CurrentElementIndex);

            if (((ControllerVM<Emission>)controller.CurrentElement).Elements.Count == 0)
            {
                DisableEmissionEditAndDelete();
            }

            ChangeSaveToDBAndRestoreButton();
            ResetDataGridView();
        }

        private void EnvironmentsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentEmission = ((ControllerVM<Emission>)GetControllerByIndex(1)).CurrentElement;
            if (currentEmission != null)
            {
                currentEmission.Environment = (Data.Entity.Environment)EnvironmentsComboBox.SelectedItem;
            }
        }
        private void ElementsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentEmission = ((ControllerVM<Emission>)GetControllerByIndex(1)).CurrentElement;
            if (currentEmission != null)
            {
                currentEmission.Element = (Element)ElementsComboBox.SelectedItem;
            }
        }

        private void EmissionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void RestoreEmissionsButton_Click(object sender, EventArgs e)
        {
            isRestoring = true;

            controller.CurrentElement.RestoreElements();

            switch (ContentContainerTabControl.SelectedIndex)
            {
                case 0:
                    foreach (KeyValuePair<Issue, ControllerVM<CalculationSeries>> element in controller.CurrentElement.Elements)
                    {
                        element.Value.RestoreElements();
                        element.Value.Sort(series => series.Id);
                    }

                    ClearCheckedListBoxSelections(IssuesCheckedListBox);

                    if (IssuesCheckedListBox.Items.Count != 0)
                    {
                        IssuesCheckedListBox.SelectedIndex = 0;
                    }

                    var issuesCollection = controller.CurrentElement.Elements
                                                                    .OfType<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>()
                                                                    .OrderBy(obj => obj.Key.Id)
                                                                    .Select(obj => obj.Key);

                    CheckElementsCheckedListBox(IssuesCheckedListBox, issuesCollection, (left, right) => left.Id == right.Id);
                    break;
                case 1:
                    if (((ControllerVM<Emission>)controller.CurrentElement).Elements.Count != 0)
                    {
                        EnableEmissionEditAndDelete();
                    }
                    else
                    {
                        DisableEmissionEditAndDelete();
                    }
                    break;
                default:
                    break;
            }

            ResetDataGridView();
            ChangeSaveToDBAndRestoreButton();

            isRestoring = false;
        }
        private async void SaveToBDButton_Click(object sender, EventArgs e)
        {
            switch (ContentContainerTabControl.SelectedIndex)
            {
                case 0:
                    await SaveIssues();
                    break;
                case 1:
                    await SaveEmissionsToDB();
                    break;
                default:
                    break;
            }

            ChangeSaveToDBAndRestoreButton();
        }

        private void ContentContainerTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            SetControllerIndex();

            if (ContentContainerTabControl.SelectedIndex >= controller.Elements.Count)
            {
                SaveToBDButton.Visible = false;
                RestoreButton.Visible = false;
            }
            else
            {
                SaveToBDButton.Visible = true;
                RestoreButton.Visible = true;
            }
        }

        private async void IssuesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IssuesCheckedListBox.SelectedIndex != -1)
            {
                var issue = IssuesCheckedListBox.SelectedItem as Issue;

                if (issue != null)
                {
                    await SetSeries(issue);

                    ClearCheckedListBoxSelections(SeriesCheckedListBox);
                    SeriesCheckedListBox.DataSource = issuesSeries[issue.Id];
                }
            }

            if (((CalculationSeries)SeriesCheckedListBox.Items[0]).Id == -1)
            {
                SeriesCheckedListBox.Enabled = false;
            }
            else
            {
                SeriesCheckedListBox.Enabled = true;
            }

            if (IssuesCheckedListBox.SelectedIndex == -1)
            {
                return;
            }

            var currentController = (ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)GetControllerByIndex(0);
            var issueIndex = currentController.IndexOf(elem => elem.Key.Id == ((Issue)IssuesCheckedListBox.SelectedItem).Id);

            if (issueIndex != -1)
            {
                CheckElementsCheckedListBox(SeriesCheckedListBox, currentController.Elements[issueIndex].Value.Elements,
                                            (left, right) => left.Id == right.Id);
            }

            if (SeriesCheckedListBox.Items.Count != 0)
            {
                SeriesCheckedListBox.SelectedIndex = 0;
            }
        }

        private async void RefreshCalcResButton_Click(object sender, EventArgs e)
        {
            var selectedSeries = SeriesCheckedListBox.CheckedItems.OfType<CalculationSeries>();

            CalculationsResultDataGridView.Rows.Clear();
            if (!selectedSeries.Any())
            {
                return;
            }

            string tables = "calculations_result, formulas";
            string columns = "calculations_result.calculation_number, formulas.name_of_formula, " +
                             "calculations_result.result, formulas.description_of_formula, " +
                             "calculations_result.date_of_calculation";
            string joinCondition = "calculations_result.id_of_formula = formulas.id_of_formula";
            StringBuilder condition = new StringBuilder();

            foreach (var _selectedSeries in selectedSeries)
            {
                condition.Append("calculations_result.calculation_number = ");
                condition.Append(_selectedSeries.Id);
                condition.Append(" OR ");
            }
            condition.Remove(condition.Length - 3, 2);

            await dbManager.GetRowsUsingJoinAsync(tables, columns, joinCondition, condition.ToString(), JoinType.INNER)
                           .ContinueWith(result =>
                           {
                               return result.Result.Select(row => new
                               {
                                   SeriesName = (int)row[0],
                                   Formula = row[1].ToString(),
                                   Result = row[2].ToString(),
                                   FormulaDescription = row[3].ToString(),
                                   Date = row[4]
                               }).ToList();
                           }, TaskContinuationOptions.OnlyOnRanToCompletion)
                           .ContinueWith(result =>
                           {
                               foreach (var item in result.Result)
                               {
                                   var row = new DataGridViewRow();
                                   row.Cells.Add(new DataGridViewTextBoxCell { Value = selectedSeries.First(_series => _series.Id == item.SeriesName).Name });
                                   row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Formula });
                                   row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Result });
                                   row.Cells.Add(new DataGridViewTextBoxCell { Value = item.FormulaDescription });
                                   row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Date });

                                   CalculationsResultDataGridView.Rows.Add(row);
                               }
                           }, System.Threading.CancellationToken.None,
                              TaskContinuationOptions.OnlyOnRanToCompletion,
                              TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SeriesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isRestoring)
            {
                return;
            }

            var currentController = (ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)GetControllerByIndex(0);
            var currentIssue = (Issue)IssuesCheckedListBox.SelectedItem;

            if (currentIssue == null)
            {
                return;
            }

            var controllerIssueIndex = currentController.IndexOf(obj => obj.Key.Id == currentIssue.Id);

            if (controllerIssueIndex == -1)
            {
                return;
            }

            var currentSeriesController = currentController.Elements[controllerIssueIndex].Value;

            switch (e.NewValue)
            {
                case CheckState.Unchecked:
                    currentSeriesController.RemoveElementAt(currentSeriesController.IndexOf(calSerires => calSerires.Id == ((CalculationSeries)SeriesCheckedListBox.SelectedItem).Id));
                    break;
                case CheckState.Checked:
                    if (SeriesCheckedListBox.SelectedItem == null ||
                        currentSeriesController.IndexOf(obj => obj.Id == ((CalculationSeries)SeriesCheckedListBox.SelectedItem).Id) != -1)
                    {
                        return;
                    }

                    currentSeriesController.StartAddingNewElement((CalculationSeries)SeriesCheckedListBox.Items[e.Index]);
                    currentSeriesController.EndAddingNewElement();
                    break;
                default:
                    break;
            }

            ChangeSaveToDBAndRestoreButton();
        }
        private void IssuesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isRestoring)
            {
                return;
            }

            var currentController = (ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>)GetControllerByIndex(0);
            var currentIssue = (Issue)IssuesCheckedListBox.SelectedItem;

            if (currentIssue == null)
            {
                return;
            }

            var controllerIssueIndex = currentController.IndexOf(obj => obj.Key.Id == currentIssue.Id);

            switch (e.NewValue)
            {
                case CheckState.Unchecked:
                    if (controllerIssueIndex == -1)
                    {
                        return;
                    }

                    ClearCheckedListBoxSelections(SeriesCheckedListBox);

                    currentController.RemoveElementAt(controllerIssueIndex);
                    break;
                case CheckState.Checked:
                    if (controllerIssueIndex != -1)
                    {
                        return;
                    }

                    currentController.StartAddingNewElement(new KeyValuePair<Issue, ControllerVM<CalculationSeries>>(currentIssue, new ControllerVM<CalculationSeries>()));
                    currentController.EndAddingNewElement();
                    break;
                default:
                    break;
            }

            ChangeSaveToDBAndRestoreButton();
        }
    }
}
