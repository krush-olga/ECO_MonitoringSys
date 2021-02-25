using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMap.UserControls
{
    public partial class MedStatUserControl : UserControl
    {
        private Data.DBManager dbManager;

        private Dictionary<int, object> medStatParams;

        public MedStatUserControl() : this(-1)
        { }
        public MedStatUserControl(int objId)
        {
            InitializeComponent();
            InitializeDBManager();

            FindObjectId = objId;

            medStatParams = new Dictionary<int, object>();
        }


        public int FindObjectId { get; set; }

        private void InitializeDBManager()
        {
            try
            {
                dbManager = new Data.DBManager();
            }
            catch { }
        }

        private Task LoadMedStat()
        {
            return dbManager.GetValueAsync("region", "id", "id_of_poligon = " + FindObjectId.ToString())
                            .ContinueWith(result =>
                            {
                                var res = result.Result;
                                if (res == null || res is DBNull)
                                {
                                    return null;
                                }

                                string tables = "med_stat, formulas, issues";
                                string columns = "formulas.name_of_formula, formulas.description_of_formula, formulas.measurement_of_formula," +
                                                 "med_stat.value, med_stat.year, issues.name, med_stat.nomer";
                                string joinCond = "med_stat.id_of_formula = formulas.id_of_formula, med_stat.issue_id = issues.issue_id";
                                string cond = "med_stat.region_id = " + res;

                                return dbManager.GetRowsUsingJoinAsync(tables, columns, joinCond, cond, Data.JoinType.LEFT)
                                                .ContinueWith(innerResult =>
                                                {
                                                    return innerResult.Result.Select(row =>
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
                                                    }).ToList();
                                                });
                            }, TaskContinuationOptions.OnlyOnRanToCompletion)
                            .ContinueWith(result =>
                            {
                                var res = result.Result;

                                if (res == null || !res.Result.Any())
                                {
                                    return;
                                }

                                string[] columnsName = { "Назва задачі", "Формула", "Значення",
                                                         "Одиниці виміру", "Рік розрахунку" };
                                int index = 0;

                                MedStatDataGridView.DataSource = res.Result;

                                MedStatDataGridView.Columns[3].Visible = false;
                                MedStatDataGridView.Columns[0].Visible = false;

                                foreach (DataGridViewColumn column in MedStatDataGridView.Columns)
                                {
                                    if (column.HeaderCell.Value.ToString() != "Id" &&
                                        column.HeaderCell.Value.ToString() != "FormulaDescription")
                                    {
                                        column.HeaderCell.Value = columnsName[index++];
                                    }
                                }

                            }, System.Threading.CancellationToken.None,
                               TaskContinuationOptions.OnlyOnRanToCompletion,
                               TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void MedStatUserControl_Load(object sender, EventArgs e)
        {
            await LoadMedStat();
        }

        private void MedStatDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
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

        public static Task<MedStatUserControl> CreateInstanceAsync()
        {
            return Task.Run(() =>
            {
                var newInstance = new MedStatUserControl();
                newInstance.OnLoad(EventArgs.Empty);

                return newInstance;
            });
        }
    }
}
