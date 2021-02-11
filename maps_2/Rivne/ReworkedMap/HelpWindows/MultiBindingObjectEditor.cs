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

namespace UserMap.HelpWindows
{
    public partial class MultiBindingObjectEditor : Form
    {
        private bool bingingsSet;
        private bool isAddingMode;
        private int editingElemIndex;

        private int objId;
        private Core.IDescribable describable;

        private ViewModel.ControllerVM<Emission> emissionsController;
        private DBManager dbManager;
        private Services.ILogger logger;

        public MultiBindingObjectEditor(int objId, Core.IDescribable describable)
        {
            InitializeComponent();

            bingingsSet = false;

            emissionsController = new ViewModel.ControllerVM<Emission>();
            dbManager = new DBManager();

            if (describable == null)
            {
                throw new ArgumentNullException("describable");
            }

            this.describable = describable;
            this.objId = objId;

            YearNumericUpDown.Maximum = DateTime.Now.Year;

            editingElemIndex = -1;
            logger = new Services.FileLogger();
        }
        private void SetBinings()
        {
            if (!bingingsSet && emissionsController.Elements.Count != 0)
            {
                var maxValueBinding = new Binding("Text", emissionsController, "CurrentElement.MaxValue",
                                                  true, DataSourceUpdateMode.OnPropertyChanged, 0);
                var avgValueBinding = new Binding("Text", emissionsController, "CurrentElement.AvgValue",
                                                  true, DataSourceUpdateMode.OnPropertyChanged, 0);
                var yearBinding = new Binding("Value", emissionsController, "CurrentElement.Year",
                                              true, DataSourceUpdateMode.OnPropertyChanged, DateTime.Now.Year);
                var monthBinding = new Binding("Value", emissionsController, "CurrentElement.Month",
                                               true, DataSourceUpdateMode.OnPropertyChanged, 1);
                var dayBinding = new Binding("Value", emissionsController, "CurrentElement.Day",
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

            if (emissionsController.Elements.Count != 0)
            {
                EnableEmissionEditAndDelete();
            }

            RestoreEmissionsButton.Enabled = true;
            AddEmissionButton.Enabled = true;

            this.Text += $"{describable.Name} ({describable.Type})";
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
                    );
            }

            if (tasks.Count != 0)
            {
                await Task.WhenAll(tasks);
            }

            await LoadEmissions()
                  .ContinueWith(FillDataGrid, TaskScheduler.FromCurrentSynchronizationContext());
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
                                foreach (var row in result.Result)
                                {
                                    var emission = Data.Helpers.Mapper.Map<Emission>(row);
                                    emission.Element = elements.First(elem => elem.Code == (int)row[6]);
                                    emission.Environment = environments.First(env => env.Id == (int)row[7]);

                                    emissionsController.AddElement(emission);
                                }
                            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        private void FillDataGrid(Task continueTask)
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.AllowNew = false;
            bindingSource.DataSource = emissionsController.Elements;

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

            if (emissionsController.Elements.Count == 0)
            {
                emissionsController.CurrentElementIndex = -1;
            }
            else
            {
                emissionsController.CurrentElementIndex = 0;
            }
        }

        private void ResetDataGridView()
        {
            ((BindingSource)EmissionsDataGridView.DataSource).ResetBindings(false);
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

            ChangeSaveToDBButton();
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
        private void ChangeSaveToDBButton()
        {
            if (emissionsController.ChangedELementsCount == 0)
            {
                SaveToBDButton.Enabled = false;
            }
            else
            {
                SaveToBDButton.Enabled = true;
            }
        }

        private async Task SaveEmissionsToDB()
        {
            var changedEmission = emissionsController.ChangedElements;

            if (changedEmission.Count != 0)
            {
                string emissionTable = "emissions_on_map";
                string objColumn = describable.Type == "Маркер" ? "idPoi" : "idPoligon";
                var culture = System.Globalization.CultureInfo.InvariantCulture;

                var addedElements = changedEmission.Where(emission => emission.Value.Value == ViewModel.ChangeType.Added);
                var changedElements = changedEmission.Where(emission => emission.Value.Value == ViewModel.ChangeType.Changed);
                var deletedElements = changedEmission.Where(emission => emission.Value.Value == ViewModel.ChangeType.Deleted);

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
                            var currentEmission = addedElem.Key;

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
                            var originalEmission = deletedElem.Value.Key;

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

                emissionsController.ClearChangedElems();
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

            emissionsController.CurrentElementIndex = currentElemIndex;

            if (isAddingMode && currentElemIndex == emissionsController.Elements.Count - 1 || 
                editingElemIndex != -1 && editingElemIndex == currentElemIndex)
            {
                CurrentEmisionGroupBox.Enabled = true;
            }
            else
            {
                CurrentEmisionGroupBox.Enabled = false;
            }

            if (emissionsController.CurrentElement.Environment != null)
                EnvironmentsComboBox.SelectedIndex = EnvironmentsComboBox.Items.IndexOf<Data.Entity.Environment>(env => env.Name ==  emissionsController.CurrentElement.Environment.Name);
            else
                EnvironmentsComboBox.SelectedIndex = 0;

            if (emissionsController.CurrentElement.Element != null)
                ElementsComboBox.SelectedIndex = ElementsComboBox.Items.IndexOf<Element>(elem => elem.Name == emissionsController.CurrentElement.Element.Name);
            else
                ElementsComboBox.SelectedIndex = 0;
        }

        private void AddEmissionButton_Click(object sender, EventArgs e)
        {
            if (AddEmissionButton.Text == "Додати")
            {
                emissionsController.StartAddingNewElement();
                ChangeEmissionButton.Enabled = true;
                isAddingMode = true;

                SetBinings();
                StartEmissionAction();
            }
            else
            {
                emissionsController.CancelEditElement();
                emissionsController.CancelAddingNewElement();

                if (emissionsController.Elements.Count == 0)
                {
                    ChangeEmissionButton.Enabled = false;
                }

                EndEmissionAction();
            }

            ResetDataGridView();
        }
        private void ChangeEmissionButton_Click(object sender, EventArgs e)
        {
            if (ChangeEmissionButton.Text == "Змінити")
            {
                emissionsController.StartEditElement();
                editingElemIndex = EmissionsDataGridView.CurrentRow.Index;

                StartEmissionAction();
            }
            else if (ChangeEmissionButton.Text == "Зберегти")
            {
                emissionsController.EndEditElement();
                emissionsController.EndAddingNewElement();

                if (emissionsController.Elements.Count > 0)
                {
                    EnableEmissionEditAndDelete();
                }

                EndEmissionAction();
            }

            ResetDataGridView();
        }
        private void DeleteEmissionButton_Click(object sender, EventArgs e)
        {
            emissionsController.RemoveEmissionAt(emissionsController.CurrentElementIndex);
            EmissionsDataGridView.Refresh();

            if (emissionsController.Elements.Count == 0)
            {
                DisableEmissionEditAndDelete();
            }

            ChangeSaveToDBButton();
            ResetDataGridView();
        }
        private void RestoreEmissionsButton_Click(object sender, EventArgs e)
        {
            emissionsController.RestoreElements();
            ResetDataGridView();

            if (emissionsController.Elements.Count != 0)
            {
                EnableEmissionEditAndDelete();
            }
            else
            {
                DisableEmissionEditAndDelete();
            }

            ChangeSaveToDBButton();
        }

        private void EnvironmentsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (emissionsController.CurrentElement != null)
            {
                emissionsController.CurrentElement.Environment =
                    (Data.Entity.Environment)EnvironmentsComboBox.SelectedItem;
            }
        }
        private void ElementsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (emissionsController.CurrentElement != null)
            {
                emissionsController.CurrentElement.Element = (Element)ElementsComboBox.SelectedItem;
            }
        }

        private void EmissionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private async void SaveToBDButton_Click(object sender, EventArgs e)
        {
            await SaveEmissionsToDB();

            ChangeSaveToDBButton();
        }

    }
}
