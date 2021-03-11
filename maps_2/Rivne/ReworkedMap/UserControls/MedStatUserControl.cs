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

namespace UserMap.UserControls
{
    public partial class MedStatUserControl : UserControl
    {
        private int regionId;
        private int findObjectId;

        private Data.DBManager dbManager;

        private System.Threading.Timer refreshTimer;

        public MedStatUserControl() : this(-1)
        { }
        public MedStatUserControl(int objId)
        {
            InitializeComponent();
            InitializeDBManager();

            FindObjectId = objId;

            refreshTimer = new System.Threading.Timer(RefreshButtonClickTimeOut, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        /// <summary>
        /// Представляет ИД объекта, по котором будет выводиться мед статистика.
        /// </summary>
        public int FindObjectId
        {
            get => findObjectId;
            set
            {
                findObjectId = value;
                SetRegionId();
            }
        }

        private void InitializeDBManager()
        {
            try
            {
                dbManager = new Data.DBManager();
            }
            catch { }
        }

        private void CheckedInnerMostTreeNodesAction(TreeView treeView, Action<TreeNode> action)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Checked)
                {
                    TreeNodes(node);
                }
            }

            //Локальная функция для поиска вложенных ветвей
            void TreeNodes(TreeNode treeNode)
            {
                if (treeNode.Nodes.Count != 0)
                {
                    foreach (TreeNode innerNode in treeNode.Nodes)
                    {
                        if (innerNode.Checked)
                        {
                            TreeNodes(innerNode);
                        }
                    }
                }
                else
                {
                    action(treeNode);
                }
            }
        }

        private Task LoadFilteredMedStat()
        {
            string tables = "med_stat, med_stat_param, formulas, issues";
            string columns = "formulas.name_of_formula, formulas.description_of_formula, formulas.measurement_of_formula," +
                             "med_stat.value, med_stat.year, issues.name, med_stat.nomer";
            string joinCond = "med_stat.nomer = med_stat_param.number_of_formula, " +
                              "med_stat.id_of_formula = formulas.id_of_formula, " +
                              "med_stat.issue_id = issues.issue_id";
            StringBuilder cond = new StringBuilder("med_stat.region_id = " + regionId.ToString());

            int lastPointedYear = -1;

            var checkedYearNodes = MedStatYearsTreeView.Nodes.OfType<TreeNode>()
                                                             .Where(treeNode => treeNode.Checked);
            if (checkedYearNodes.Any())
            {
                cond.Append(" AND (");
                foreach (TreeNode node in checkedYearNodes)
                {
                    if (int.TryParse(node.Text, out int year))
                    {
                        if (lastPointedYear < year)
                        {
                            lastPointedYear = year;
                        }

                        cond.Append("med_stat.year = ");
                        cond.Append(node.Text);
                    }
                    else
                    {
                        if (lastPointedYear == -1)
                        {
                            lastPointedYear = DateTime.Now.Year - 1;
                        }

                        cond.Append(" (med_stat.year > ");
                        cond.Append(lastPointedYear);
                        cond.Append(" AND med_stat.year <= ");
                        cond.Append(DateTime.Now.Year);
                        cond.Append(')');
                    }

                    cond.Append(" OR ");
                }
                cond.Remove(cond.Length - 3, 3).Append(')');
            }

            cond.Append(" AND (");
            CheckedInnerMostTreeNodesAction(MedStatIndicatorsTreeView, node => cond.Append("med_stat_param.value = ").Append(Data.DBUtil.AddQuotes(node.Text)).Append(" OR "));

            var isSNC = ConditionAmount(MedStatIndicatorsTreeView);

            if (cond[cond.Length - 1] != '(')
            {
                cond.Remove(cond.Length - 3, 3);
                cond.Append(')');
            }
            else
            {
                cond.Remove(cond.Length - 5, 5);
            }

            return dbManager.GetRowsUsingJoinAsync(tables, columns, joinCond, cond.ToString(), Data.JoinType.LEFT)
                            .ContinueWith(result =>
                            {
                                return result.Result.Select(row =>
                                {
                                    return new
                                    {
                                        Id = (int)row[6],
                                        IssueName = row[5].ToString(),
                                        FormulaName = row[0].ToString(),
                                        FormulaDescription = row[1].ToString(),
                                        Value = row[3].ToString(),
                                        FormulaMeasurment = row[2].ToString(),
                                        Year = row[4].ToString(),
                                    };
                                });
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {

                                return result.Result.GroupBy(key => key.Id)
                                                    .Where(item => isSNC == item.Count())
                                                    .Select(item => item.First())
                                                    .ToList();
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {
                                var res = result.Result;

                                if (res == null || !res.Any())
                                {
                                    return;
                                }

                                string[] columnsName = { "Назва задачі", "Показник", "Рік розрахунку" };
                                int index = 0;

                                MedStatDataGridView.DataSource = res;

                                MedStatDataGridView.Columns[3].Visible = false;
                                MedStatDataGridView.Columns[0].Visible = false;
                                MedStatDataGridView.Columns[4].Visible = false;
                                MedStatDataGridView.Columns[5].Visible = false;

                                foreach (DataGridViewColumn column in MedStatDataGridView.Columns)
                                {
                                    var columnHeader = column.HeaderCell.Value.ToString();

                                    if (columnHeader != "Id" && columnHeader != "FormulaDescription" &&
                                        columnHeader != "Value" && columnHeader != "FormulaMeasurment")
                                    {
                                        column.HeaderCell.Value = columnsName[index++];
                                    }
                                }

                            }, System.Threading.CancellationToken.None,
                               TaskContinuationOptions.OnlyOnRanToCompletion,
                               TaskScheduler.FromCurrentSynchronizationContext())
                            .CatchErrorOrCancel(ex => System.Diagnostics.Debug.WriteLine(ex.Message));
        }
        private Task SetRegionId()
        {
            return dbManager.GetValueAsync("region", "id", "id_of_poligon = " + FindObjectId.ToString())
                            .ContinueWith(result =>
                            {
                                var resultId = result.Result;
                                if (resultId == null || resultId is DBNull)
                                    regionId = -1;
                                else
                                    regionId = (int)resultId;
                            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        private Task LoadFiltrationYear()
        {
            return dbManager.GetRowsAsync("med_stat", "DISTINCT year", "")
                            .ContinueWith(result =>
                            {
                                return result.Result.Select(row => new TreeNode(row[0].ToString()))
                                                    .ToArray();
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {
                                MedStatYearsTreeView.Nodes.AddRange(result.Result);
                            }, System.Threading.CancellationToken.None,
                               TaskContinuationOptions.OnlyOnRanToCompletion,
                               TaskScheduler.FromCurrentSynchronizationContext());
        }
        private async Task LoadFiltrationIndicators()
        {
            string medStatTables = "med_stat, med_stat_param";
            string medStatColumns = "DISTINCT med_stat_param.id_of_param, med_stat.id_of_formula," +
                                    "med_stat_param.value";
            string medStatJoinCond = "med_stat.nomer = med_stat_param.number_of_formula";
            string medStatCond = "med_stat.region_id = " + regionId.ToString();

            var medStatResult = await dbManager.GetRowsUsingJoinAsync(medStatTables, medStatColumns, medStatJoinCond,
                                                                      medStatCond, Data.JoinType.INNER)
                                               .ContinueWith(result =>
                                               {
                                                   return result.Result.GroupBy(key => (int)key[1], value => new
                                                   {
                                                       Id = (int)value[0],
                                                       Value = value[2].ToString()
                                                   })
                                                   .ToDictionary(key => key.Key, value =>
                                                   {
                                                       var innerResult = value.GroupBy(innerKey => innerKey.Id,
                                                                                       innerValue => innerValue.Value)
                                                                              .ToDictionary(innerKey => innerKey.Key,
                                                                                            innerValue => innerValue.ToList());

                                                       return innerResult;
                                                   });
                                               })
                                               .CatchErrorOrCancel(exception =>
                                               {
                                                   if (exception is AggregateException aggregateException)
                                                   {
                                                       foreach (var ex in aggregateException.InnerExceptions)
                                                       {
                                                           System.Diagnostics.Debug.WriteLine(ex);
                                                       }
                                                   }
                                                   else
                                                   {
                                                       System.Diagnostics.Debug.WriteLine(exception.Message);
                                                   }
                                               }, res => res);

            StringBuilder formulasCond = new StringBuilder();

            foreach (var item in medStatResult)
            {
                formulasCond.Append("id_of_formula = ");
                formulasCond.Append(item.Key);
                formulasCond.Append(" OR ");

                foreach (var innerItem in item.Value)
                {
                    formulasCond.Append("id_of_formula = ");
                    formulasCond.Append(innerItem.Key);
                    formulasCond.Append(" OR ");
                }
            }
            formulasCond.Remove(formulasCond.Length - 3, 3);

            await dbManager.GetRowsAsync("formulas", "DISTINCT id_of_formula, name_of_formula, " +
                                         "measurement_of_formula", formulasCond.ToString())
                            .ContinueWith(result =>
                            {
                                var typedResult = result.Result.Select(row => new
                                {
                                    Id = (int)row[0],
                                    Name = row[1].ToString(),
                                    Measurment = row[2].ToString(),
                                    Values = (List<string>)null
                                });

                                var res = typedResult.Join(medStatResult, outer => outer.Id, inner => inner.Key,
                                                      (row, inner) =>
                                                      {
                                                          var paramNames = inner.Value.Join(typedResult, outer => outer.Key,
                                                                                            _inner => _inner.Id, (param, tResult) =>
                                                                                            {
                                                                                                return new
                                                                                                {
                                                                                                    Name = tResult.Name,
                                                                                                    Measurment = tResult.Measurment,
                                                                                                    Values = param.Value
                                                                                                };
                                                                                            });

                                                          return new
                                                          {
                                                              FormulaName = row.Name,
                                                              FormulaMeasurment = row.Measurment,
                                                              Parameters = paramNames
                                                          };
                                                      });

                                return res;
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {
                                var nodes = new List<TreeNode>();

                                foreach (var formula in result.Result)
                                {
                                    var paramNode = new TreeNode($"{formula.FormulaName} ({formula.FormulaMeasurment})");
                                    foreach (var parameter in formula.Parameters)
                                    {
                                        var parameterNode = new TreeNode($"{parameter.Name} ({parameter.Measurment})");
                                        foreach (var value in parameter.Values)
                                        {
                                            parameterNode.Nodes.Add(value);
                                        }
                                        paramNode.Nodes.Add(parameterNode);
                                    }

                                    nodes.Add(paramNode);
                                }

                                MedStatIndicatorsTreeView.Nodes.AddRange(nodes.ToArray());
                            }, System.Threading.CancellationToken.None,
                                TaskContinuationOptions.OnlyOnRanToCompletion,
                                TaskScheduler.FromCurrentSynchronizationContext())
                            .ConfigureAwait(false);
        }

        private int GetTimerDelay()
        {
            var dbCallDelayStr = System.Configuration.ConfigurationManager.AppSettings.Get("DBCallDelay");
            int dbCallDelay = 0;

            if (!int.TryParse(dbCallDelayStr, out dbCallDelay))
            {
                dbCallDelay = 5000;
            }

            return dbCallDelay;
        }
        private int ConditionAmount(TreeView treeView)
        {
            int index = 0;

            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Nodes.Count != 0)
                {
                    var checkedNodes = CheckedNodes(node);

                    if (checkedNodes != 0)
                    {
                        return checkedNodes;
                    }
                }
                else if (node.Checked)
                {
                    index++;
                }
            }

            int CheckedNodes(TreeNode node)
            {
                int innderIndex = 0;

                if (!node.Checked)
                {
                    return innderIndex;
                }

                foreach (TreeNode _node in node.Nodes)
                {
                    if (_node.Nodes.Count != 0 && _node.Nodes[0].Nodes.Count != 0)
                    {
                        var checkedNodes = CheckedNodes(_node);

                        if (checkedNodes != 0)
                        {
                            return checkedNodes;
                        }
                    }
                    else if (_node.Checked)
                    {
                        innderIndex++;
                    }
                }

                return innderIndex;
            }

            return index;
        }

        private async void MedStatUserControl_Load(object sender, EventArgs e)
        {
            await SetRegionId();
            await LoadFiltrationYear();
            await LoadFiltrationIndicators();

            MedStatYearsTreeView.Nodes.Add("Поточний рік");
        }
        private void MedStatUserControlTabControl_Resize(object sender, EventArgs e)
        {
            if (MedStatDataGridView.ScrollBars == ScrollBars.Both ||
                MedStatDataGridView.ScrollBars == ScrollBars.Horizontal)
            {
                var hScrollBar = MedStatDataGridView.Controls.OfType<ScrollBar>().First();

                if (hScrollBar.Visible)
                {
                    MedStatDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                    MedStatParamDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
                    MedStatParamLable.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                }
                else
                {
                    MedStatDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
                    MedStatParamDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                    MedStatParamLable.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }

                if (!hScrollBar.Visible && MedStatDataGridView.Width > 400)
                {
                    this.Resize -= MedStatUserControlTabControl_Resize;
                }
            }
        }

        private void MedStatDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            if (sender is DataGridView dataGridView)
            {
                var currentRow = dataGridView.Rows[e.RowIndex];

                switch (dataGridView.Tag.ToString())
                {
                    case "1":
                        currentRow.Cells[e.ColumnIndex].ToolTipText = currentRow.Cells[3].Value.ToString();
                        break;
                    case "2":
                        currentRow.Cells[e.ColumnIndex].ToolTipText = currentRow.Cells[1].Value.ToString();
                        break;
                    default:
                        break;
                }

            }


        }

        private async void MedStatDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (MedStatDataGridView.SelectedRows.Count == 0)
            {
                return;
            }

            string tables = "med_stat_param, formulas";
            string columns = "formulas.name_of_formula, formulas.description_of_formula, " +
                             "formulas.measurement_of_formula, med_stat_param.value";
            string joinCond = "med_stat_param.id_of_param = formulas.id_of_formula";
            string cond = "med_stat_param.number_of_formula = " + MedStatDataGridView.SelectedRows[0].Cells[0].Value;

            await dbManager.GetRowsUsingJoinAsync(tables, columns, joinCond, cond, Data.JoinType.INNER)
                           .ContinueWith(result =>
                           {
                               var _result = result.Result;

                               return _result.Select(row =>
                               {
                                   return new
                                   {
                                       FormulaName = row[0].ToString(),
                                       FormulaDescription = row[1].ToString(),
                                       Value = row[3].ToString(),
                                       Measurement = row[2].ToString()
                                   };
                               }).ToList();
                           }, TaskContinuationOptions.OnlyOnRanToCompletion)
                           .ContinueWith(result =>
                           {
                               var res = result.Result;
                               var selectedMedStatRow = MedStatDataGridView.SelectedRows[0];

                               res.Add(new
                               {
                                   FormulaName = "Значення",
                                   FormulaDescription = selectedMedStatRow.Cells[3].Value.ToString(),
                                   Value = selectedMedStatRow.Cells[4].Value.ToString(),
                                   Measurement = selectedMedStatRow.Cells[5].Value.ToString()
                               });

                               MedStatParamDataGridView.DataSource = res;

                               string[] columnsName = { "Назва параметру", "Опис", "Значення", "Одиниці виміру" };
                               int index = 0;

                               MedStatParamDataGridView.Columns[1].Visible = false;

                               foreach (DataGridViewColumn column in MedStatParamDataGridView.Columns)
                               {
                                   column.HeaderCell.Value = columnsName[index++];
                               }
                           }, System.Threading.CancellationToken.None,
                              TaskContinuationOptions.OnlyOnRanToCompletion,
                              TaskScheduler.FromCurrentSynchronizationContext())
                           .ContinueWith(result =>
                            {
                                if (result.IsCanceled)
                                {
                                    MessageBox.Show("Не вдалось завантажити дані.", "Помилка",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }, TaskContinuationOptions.NotOnFaulted);
        }

        private async void AcceptFiltrarionButton_Click(object sender, EventArgs e)
        {
            MedStatDataGridView.DataSource = null;
            MedStatParamDataGridView.DataSource = null;
            AcceptFiltrarionButton.Text = "Завантаження...";
            AcceptFiltrarionButton.Enabled = false;

            await LoadFilteredMedStat();

            AcceptFiltrarionButton.Text = "Фільтрувати";
            refreshTimer.Change(GetTimerDelay(), System.Threading.Timeout.Infinite);
        }

        private void RefreshButtonClickTimeOut(object sender)
        {
            if (IsHandleCreated)
            {
                AcceptFiltrarionButton.Invoke((Action)(() => AcceptFiltrarionButton.Enabled = true));
            }
            refreshTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
        private void ResetFilterButtonDelay_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var currentNode = e.Node;

            if (currentNode.Checked)
            {
                var checkedNode = currentNode.Nodes.OfType<TreeNode>()
                                                   .Where(node => node.Checked);

                if (!checkedNode.Any())
                {
                    foreach (TreeNode node in currentNode.Nodes)
                    {
                        node.Checked = currentNode.Checked;
                    }
                }
                if (currentNode.Parent != null && !currentNode.Parent.Checked)
                {
                    currentNode.Parent.Checked = true;
                }
            }
            else
            {
                foreach (TreeNode node in currentNode.Nodes)
                {
                    if (node.Checked)
                    {
                        node.Checked = currentNode.Checked;
                    }
                }

                if (currentNode.Parent != null && currentNode.Parent.Checked && 
                    !currentNode.Parent.Nodes.OfType<TreeNode>().Where(node => node.Checked).Any())
                {
                    currentNode.Parent.Checked = false;
                }
            }

            if (!AcceptFiltrarionButton.Enabled)
            {
                refreshTimer.Change(0, System.Threading.Timeout.Infinite);
            }
        }

        private void MedStatDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        public static Task<MedStatUserControl> CreateInstanceAsync(int objId)
        {
            return Task.Run(() =>
            {
                var newInstance = new MedStatUserControl(objId);
                newInstance.OnLoad(EventArgs.Empty);

                return newInstance;
            });
        }
    }
}
