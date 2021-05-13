using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using UserMap.Services;
using UserMap.Helpers;
using UserMap.ViewModel;
using Data.Entity;
using System.Collections;

namespace UserMap.UserControls
{
    public partial class EmissionsUC : UserControl, ISavable, IReadOnlyable
    {
        private bool isReadOnly;
        private bool bingingsSet;
        private bool isAddingMode;
        private int editingElemIndex;

        private int objId;
        private Core.IDescribable describable;

        private ControllerVM<Emission> emissionsController;

        private Dictionary<Data.Entity.Environment, List<Element>> environmentsWithElements;

        private DBManager dbManager;
        private ILogger logger;

        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                SetReadOnlyChanges();
            }
        }

        public EmissionsUC(int objId, Core.IDescribable describable)
        {
            InitializeComponent();

            if (describable == null)
                throw new ArgumentNullException("describable");

            this.objId = objId;
            this.describable = describable;

            dbManager = new DBManager();
            logger = new FileLogger();

            emissionsController = new ControllerVM<Emission>();

            editingElemIndex = -1;

            environmentsWithElements = new Dictionary<Data.Entity.Environment, List<Element>>();

            SetYearConstraints();
        }

        public event EventHandler ElementChanged;

        public bool HasChangedElements()
        {
            return emissionsController.ChangedELementsCount != 0;
        }

        public void RestoreChanges()
        {
            emissionsController.RestoreElements();
            ResetDataGridView();
            EndEmissionAction();
            ToggleChangeAndRemoveButton();
        }

        private void SetYearConstraints()
        {
            var yearMin = System.Configuration.ConfigurationManager.AppSettings.Get("YearMinValue");
            var yearMax = System.Configuration.ConfigurationManager.AppSettings.Get("YearMaxValue");

            if (int.TryParse(yearMin, out int min))
                YearNumericUpDown.Minimum = min;
            else
                YearNumericUpDown.Minimum = 1991;

            if (int.TryParse(yearMax, out int max))
                YearNumericUpDown.Maximum = max;
            else
                YearNumericUpDown.Maximum = DateTime.Now.Year + 1;
        }

        public Task SaveChangesAsync()
        {
            return SaveEmissionsToDB();
        }

        private void SetReadOnlyChanges()
        {
            if (!isReadOnly)
            {
                if (emissionsController.Elements.Any())
                {
                    EnableEmissionEditAndDelete();
                }

                AddEmissionButton.Enabled = true;
            }
            else
            {
                DisableEmissionEditAndDelete();
                AddEmissionButton.Enabled = false;
            }
        }
        private void ToggleChangeAndRemoveButton()
        {
            if (emissionsController.Elements.Any())
            {
                EnableEmissionEditAndDelete();
            }
            else
            {
                DisableEmissionEditAndDelete();
            }
        }

        private async void EmissionUC_Load(object sender, EventArgs e)
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
                                               .ContinueWith(result =>
                                                            {
                                                                MapCache.Add("environment", ElementsComboBox.DataSource);

                                                                //Необходимо для начальной фильтрации
                                                                EnvironmentsComboBox.SelectedIndex = -1;
                                                                EnvironmentsComboBox.SelectedIndex = 0;
                                                            }, TaskScheduler.FromCurrentSynchronizationContext())
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

            if (tasks.Count != 0)
            {
                try
                {
                    await Task.WhenAll(tasks).CatchErrorOrCancel(ex => System.Diagnostics.Debug.WriteLine(ex.Message));
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

            await LoadEmissions().ContinueWith(FillDataGrid, TaskScheduler.FromCurrentSynchronizationContext())
                                 .CatchErrorOrCancel(ex =>
                                 {
#if !DEBUG
                                    logger.Log(ex);
#else
                                     MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                                 });
        }
        private void InitializeAddionalComponent()
        {
            SetBinings();

            if (!isReadOnly)
            {
                if (emissionsController.Elements.Any() &&
                    EmissionsDataGridView.CurrentRow != null)
                {
                    EnableEmissionEditAndDelete();
                }

                AddEmissionButton.Enabled = true;
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
                                foreach (var row in result.Result)
                                {
                                    var emission = Data.Helpers.Mapper.Map<Emission>(row);
                                    emission.Element = elements.First(elem => elem.Code == (int)row[6]);
                                    emission.Environment = environments.First(env => env.Id == (int)row[7]);

                                    emissionsController.AddElement(emission);
                                }
                            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        private async Task SaveEmissionsToDB()
        {
            var changedEmission = emissionsController.ChangedElements;

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

            if (EmissionsDataGridView.CurrentRow == null)
            {
                DisableEmissionEditAndDelete();
            }
            else
            {
                EnableEmissionEditAndDelete();
            }
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

        private void ResetDataGridView()
        {
            ((BindingSource)EmissionsDataGridView.DataSource)?.ResetBindings(false);

            if (EmissionsDataGridView.Rows.Count != 0)
            {
                EmissionsDataGridView.ClearSelection();
                EmissionsDataGridView.Rows[EmissionsDataGridView.Rows.Count - 1].Selected = true;

                if (editingElemIndex == EmissionsDataGridView.Rows.Count - 1)
                {
                    var vScrollBar = EmissionsDataGridView.Controls.OfType<ScrollBar>().ElementAt(1);
                    if (vScrollBar.Visible)
                    {
                        vScrollBar.Value = vScrollBar.Maximum;
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

        private void OnElementChanged()
        {
            ElementChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessEmptyComboBox(ComboBox comboBox)
        {
            if (((IList)comboBox.DataSource).Count == 0)
            {
                comboBox.Text = string.Empty;
                ChangeEmissionButton.Enabled = false;
            }
            else
                ChangeEmissionButton.Enabled = true;
        }

        private void SetToEmissionComboBoxItem<T>(ComboBox fromComboBox, Action<T, Emission> setAction)
        {
            var currentEmission = emissionsController.CurrentElement;
            if (currentEmission != null)
            {
                setAction((T)fromComboBox.SelectedItem, currentEmission);
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
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

            if (e.Handled)
                System.Media.SystemSounds.Beep.Play();
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
                DisableEmissionEditAndDelete();
                return;
            }
            else
            {
                EnableEmissionEditAndDelete();
            }

            var currentElemIndex = dataGrid.CurrentRow.Index;

            if (editingElemIndex != -1 && currentElemIndex != editingElemIndex && 
                dataGrid.Rows.Count > editingElemIndex)
            {
                foreach (DataGridViewCell cell in dataGrid.Rows[currentElemIndex].Cells)
                {
                    cell.Selected = false;
                }

                dataGrid.Rows[editingElemIndex].Selected = true;
                dataGrid.Rows[editingElemIndex].Cells[0].Selected = true;
                return;
            }    

            emissionsController.CurrentElementIndex = currentElemIndex;

            var currentElem = emissionsController.CurrentElement;

            if (isAddingMode && currentElemIndex == emissionsController.Elements.Count - 1 ||
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
            if (AddEmissionButton.Text == "Додати")
            {
                emissionsController.StartAddingNewElement(new Emission());
                SetToEmissionComboBoxItem<Data.Entity.Environment>(EnvironmentsComboBox,
                                                   (env, emission) => emission.Environment = env);

                editingElemIndex = emissionsController.Elements.Count - 1;

                var _emission = emissionsController.CurrentElement;
                _emission.Element = (Element)ElementsComboBox?.Items[0];
                _emission.Year = (int)YearNumericUpDown.Value;
                _emission.Month = (int)MonthNumericUpDown.Value;
                _emission.Day = (int)DayNumericUpDown.Value;

                ChangeEmissionButton.Enabled = true;
                isAddingMode = true;

                SetBinings();
                StartEmissionAction();

                ProcessEmptyComboBox(EnvironmentsComboBox);
            }
            else
            {
                emissionsController.CancelEditElement();
                emissionsController.CancelAddingNewElement();

                EndEmissionAction();

                if (emissionsController.Elements.Count == 0)
                {
                    DisableEmissionEditAndDelete();
                }

                OnElementChanged();
            }

            ResetDataGridView();
        }
        private void ChangeEmissionButton_Click(object sender, EventArgs e)
        {
            if (ChangeEmissionButton.Text == "Змінити")
            {
                editingElemIndex = EmissionsDataGridView.CurrentRow.Index;
                emissionsController.StartEditElement(editingElemIndex);

                StartEmissionAction();
                ProcessEmptyComboBox(EnvironmentsComboBox);
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
                OnElementChanged();

                ResetDataGridView();
            }
        }
        private void DeleteEmissionButton_Click(object sender, EventArgs e)
        {
            emissionsController.RemoveElementAt(emissionsController.CurrentElementIndex);

            if (emissionsController.Elements.Count == 0)
            {
                DisableEmissionEditAndDelete();
            }

            ResetDataGridView();
            OnElementChanged();
        }

        private void EnvironmentsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentEnvironement = (Data.Entity.Environment)EnvironmentsComboBox.SelectedItem;

            if (!environmentsWithElements.ContainsKey(currentEnvironement))
            {
                //Ожидаем загрузку элементов
                Task.Run(() => { while (ElementsComboBox.DataSource == null) { } })
                    .ContinueWith(result =>
                    {
                        return dbManager.GetRowsAsync("gdk", "code", "environment = " + currentEnvironement.Id);
                    })
                    .Unwrap()
                    .ContinueWith(result =>
                    {
                        var res = result.Result;
                        var elements = (IList<Element>)MapCache.GetItem("elements");

                        var joinedElements = elements.Join(res, outer => outer.Code, inner => (int)inner[0],
                                                                    (outer, inner) => outer)
                                                     .ToList();

                        environmentsWithElements.Add(currentEnvironement, joinedElements);

                        //Нужно для сброса размера выпадающего елемента
                        ElementsComboBox.DataSource = null;
                        ElementsComboBox.DataSource = joinedElements;

                        ProcessEmptyComboBox(ElementsComboBox);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                //Нужно для сброса размера выпадающего елемента
                ElementsComboBox.DataSource = null;
                ElementsComboBox.DataSource = environmentsWithElements[currentEnvironement];
                ProcessEmptyComboBox(ElementsComboBox);
            }

            SetToEmissionComboBoxItem<Data.Entity.Environment>(EnvironmentsComboBox,
                                                               (env, emission) => emission.Environment = env);
        }
        private void ElementsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetToEmissionComboBoxItem<Element>(ElementsComboBox, (elem, emission) => emission.Element = elem);
        }

        private void EmissionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
    }
}
