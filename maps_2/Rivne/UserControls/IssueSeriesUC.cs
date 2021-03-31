using Data;
using Data.Entity;
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
using UserMap.Services;
using UserMap.ViewModel;

namespace UserMap.UserControls
{
    public partial class IssueSeriesUC : UserControl, ISavable, IReadOnlyable
    {
        private bool isReadOnly;
        private bool isRestoring;
        private bool isLoading;

        private int objId;
        private Core.IDescribable describable;

        private DBManager dbManager;
        private ILogger logger;

        private ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>> issuesController;
        private Dictionary<int, List<CalculationSeries>> issuesSeries;
        private Dictionary<int, List<int>> selectedSeries;

        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
            }
        }

        public IssueSeriesUC(int objId, Core.IDescribable describable)
        {
            InitializeComponent();

            if (describable == null)
                throw new ArgumentNullException("describable");

            this.objId = objId;

            dbManager = new DBManager();
            logger = new FileLogger();

            this.describable = describable;

            issuesController = new ControllerVM<KeyValuePair<Issue, ControllerVM<CalculationSeries>>>();
            issuesSeries = new Dictionary<int, List<CalculationSeries>>();
            selectedSeries = new Dictionary<int, List<int>>();
        }

        public event EventHandler ElementChanged;

        public bool HasChangedElements()
        {
            bool condition = false;
            condition = issuesController.ChangedELementsCount != 0;

            if (!condition)
            {
                foreach (var changedElem in issuesController.Elements)
                {
                    condition = changedElem.Value.ChangedELementsCount != 0 || condition;
                }
            }

            return condition;
        }

        public void RestoreChanges()
        {
            isRestoring = true;

            issuesController.RestoreElements();

            foreach (var element in issuesController.Elements)
            {
                element.Value.RestoreElements();
                element.Value.Sort(series => series.Id);
            }

            ClearCheckedListBoxSelections(IssuesCheckedListBox);

            if (IssuesCheckedListBox.Items.Count != 0)
            {
                IssuesCheckedListBox.SelectedIndex = 0;
            }

            var issuesCollection = issuesController.Elements.OrderBy(obj => obj.Key.Id)
                                                            .Select(obj => obj.Key);

            CheckElementsCheckedListBox(IssuesCheckedListBox, issuesCollection, (left, right) => left.Id == right.Id);

            isRestoring = false;
        }

        public async Task SaveChangesAsync()
        {
            var changedIssues = issuesController.ChangedElements;

            var addedIssues = changedIssues.Where(elem => elem.Value.Value == ChangeType.Added);
            var deletedIssues = changedIssues.Where(elem => elem.Value.Value == ChangeType.Deleted);
            var issues = issuesController.Elements.Except(addedIssues.Select(added => added.Key));

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

            issuesController.ClearChangedElems();

            foreach (var elem in issuesController.Elements)
            {
                elem.Value.ClearChangedElems();
            }
        }

        public Dictionary<Issue, IList<CalculationSeries>> GetIssuesAndSeries()
        {
            var result = new Dictionary<Issue, IList<CalculationSeries>>();

            foreach (var element in issuesController.Elements)
            {
                result.Add(element.Key, new System.Collections.ObjectModel.ReadOnlyCollection<CalculationSeries>(element.Value.Elements));
            }

            return result;
        }

        private void OnElementChanged()
        {
            ElementChanged?.Invoke(this, EventArgs.Empty);
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

                                    issuesController.AddElement(currentIssue);

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

            var issueEnumerator = ((List<Issue>)IssuesCheckedListBox.DataSource).GetEnumerator();
            var findIssueEnumerator = issuesController.Elements.OrderBy(pair => pair.Key.Id).GetEnumerator();

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

        private void ClearCheckedListBoxSelections(CheckedListBox checkedListBox)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SelectedIndex = i;
                checkedListBox.SetItemChecked(i, false);
            }
        }

        private async void IssueSeriesUC_Load(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                await dbManager.GetRowsAsync("issues", "*", "")
                           .ContinueWith(task => task.Result.Select(row => Data.Helpers.Mapper.Map<Issue>(row)).ToList())
                           .ContinueWith(task =>
                           {
                               IssuesCheckedListBox.DataSource = task.Result;
                           }, TaskScheduler.FromCurrentSynchronizationContext())
                           .CatchErrorOrCancel(ex => System.Diagnostics.Debug.WriteLine(ex.Message));
            }
            catch (Exception ex)
            {
#if !DEBUG
                logger.Log(ex);
#else
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }

            try
            {
                await LoadIssuesAndSeries();
            }
            catch (Exception ex)
            {
#if !DEBUG
                logger.Log(ex);
#else
                MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }

            isLoading = false;
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

            var issueIndex = issuesController.IndexOf(elem => elem.Key.Id == ((Issue)IssuesCheckedListBox.SelectedItem).Id);

            if (issueIndex != -1)
            {
                CheckElementsCheckedListBox(SeriesCheckedListBox, issuesController.Elements[issueIndex].Value.Elements,
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
            if (isRestoring || isReadOnly)
            {
                return;
            }

            var currentIssue = (Issue)IssuesCheckedListBox.SelectedItem;

            if (currentIssue == null)
            {
                return;
            }

            var controllerIssueIndex = issuesController.IndexOf(obj => obj.Key.Id == currentIssue.Id);

            if (controllerIssueIndex == -1)
            {
                return;
            }

            var currentSeriesController = issuesController.Elements[controllerIssueIndex].Value;

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

            OnElementChanged();
        }
        private void IssuesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!isLoading && (isRestoring || isReadOnly))
            {
                if (isReadOnly)
                {
                    switch (e.NewValue)
                    {
                        case CheckState.Unchecked:
                            e.NewValue = CheckState.Checked;
                            break;
                        case CheckState.Checked:
                            e.NewValue = CheckState.Unchecked;
                            break;
                        default:
                            break;
                    }
                }

                return;
            }

            var currentIssue = (Issue)IssuesCheckedListBox.SelectedItem;

            if (currentIssue == null)
            {
                return;
            }

            var controllerIssueIndex = issuesController.IndexOf(obj => obj.Key.Id == currentIssue.Id);

            switch (e.NewValue)
            {
                case CheckState.Unchecked:
                    if (controllerIssueIndex == -1)
                    {
                        return;
                    }

                    ClearCheckedListBoxSelections(SeriesCheckedListBox);

                    issuesController.RemoveElementAt(controllerIssueIndex);
                    break;
                case CheckState.Checked:
                    if (controllerIssueIndex != -1)
                    {
                        return;
                    }

                    issuesController.StartAddingNewElement(new KeyValuePair<Issue, ControllerVM<CalculationSeries>>(currentIssue, new ControllerVM<CalculationSeries>()));
                    issuesController.EndAddingNewElement();
                    break;
                default:
                    break;
            }

            OnElementChanged();
        }
    }
}
