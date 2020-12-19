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
        private int oldEnvironmentIndex;
        private string oldObjName;
        private string oldObjDescription;

        private bool isLoading;
        private Role expert;

        private IList<Data.Entity.Issue> issues;
        private IList<Data.Entity.Environment> environments;
        private IDictionary<int, IList<Data.Entity.CalculationSeries>> series;
        private DBManager dbManager;

        private object _lock;

        static ItemConfigurationWindow()
        {
            missingSeries = new Data.Entity.CalculationSeries
            {
                id = -1,
                name = "Серії для даного експерта відстуні"
            };
        }

        public ItemConfigurationWindow(Role expert) : this(expert, null, null)
        { }
        public ItemConfigurationWindow(Role expert, IList<Data.Entity.Issue> issues, IList<Data.Entity.Environment> environments)
        {
            _lock = new object();

            InitializeComponent();
            InitializeAdditionalComponent();

            this.expert = expert;
            this.issues = issues;
            this.environments = environments;

            isLoading = false;

            dbManager = new DBManager();
            series = new Dictionary<int, IList<Data.Entity.CalculationSeries>>();

            oldIssueIndex = 0;
            oldEnvironmentIndex = 0;
            oldObjName = string.Empty;
            oldObjDescription = string.Empty;
        }

        public string GetObjName()
        {
            return ObjectNameTextBox.Text;
        }
        public string GetObjDescrition()
        {
            return ObjectDescriptionTextBox.Text;
        }
        public Data.Entity.Issue GetIssue()
        {
            return IssuesComboBox.SelectedItem as Data.Entity.Issue;
        }
        public Data.Entity.Environment GetEnvironment()
        {
            return EnvironmentsComboBox.SelectedItem as Data.Entity.Environment;
        }
        public Data.Entity.CalculationSeries GetSeries()
        {
            return SeriesComboBox.SelectedItem as Data.Entity.CalculationSeries;
        }

        private void InitializeAdditionalComponent()
        {
            Binding envCheckBinding = new Binding("Enabled", EnvironmentCheckBox, "Checked");
            Binding issuesCheckBinding = new Binding("Enabled", IssueCheckBox, "Checked");

            EnvironmentGroupBox.DataBindings.Add(envCheckBinding);
            IssueGroupBox.DataBindings.Add(issuesCheckBinding);

            IssuesComboBox.DataSource = issues;
            EnvironmentsComboBox.DataSource = environments;
        }

        private async void LoadIssues()
        {
            IssuesComboBox.Items.Add("Йде завантаження...");
            IssuesComboBox.SelectedIndex = 0;

            try
            {
                issues = await Task.Run(() =>
                {
                    while (isLoading)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    lock (_lock)
                    {
                        isLoading = true;
                    }

                    return dbManager.GetRows("issues", "issue_id, name", "")
                                    .Select(r =>
                                    {
                                        int id;
                                        int.TryParse(r[0].ToString(), out id);

                                        return new Data.Entity.Issue(id)
                                        {
                                            name = r[1].ToString()
                                        };
                                    })
                                    .ToList();
                });

                IssuesComboBox.Items.Clear();

                if (issues.Count != 0)
                {
                    IssuesComboBox.DataSource = issues;
                    IssuesComboBox.DisplayMember = "name";
                }
                else
                {
                    IssuesComboBox.Items.Add("Задачі для даного експерта відсутні");
                    IssuesComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                IssuesComboBox.Items.Clear();
                IssuesComboBox.DataSource = null;

                IssuesComboBox.Items.Add("Не вдалось завантажити задачі");
                IssuesComboBox.SelectedIndex = 0;

#if DEBUG
                MessageBox.Show($"Message:\n{ex.Message}\n\nStack trace: \n{ex.StackTrace}",
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            finally
            {
                isLoading = false;
            }

        }
        private async void LoadEnvironments()
        {
            EnvironmentsComboBox.Items.Add("Йде завантаження...");
            EnvironmentsComboBox.SelectedIndex = 0;

            try
            {
                environments = await Task.Run(() =>
                {
                    while (isLoading)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    lock (_lock)
                    {
                        isLoading = true;
                    }

                    return dbManager.GetRows("environment", "*", "")
                                    .Select(r => Data.Entity.EnvironmentMapper.Map(r))
                                    .ToList();
                });

                EnvironmentsComboBox.Items.Clear();

                if (environments.Count != 0)
                {
                    EnvironmentsComboBox.DataSource = environments;
                    EnvironmentsComboBox.DisplayMember = "name";
                }
                else
                {
                    EnvironmentsComboBox.Items.Add("Помилка при завантаженні середовищ");
                    EnvironmentsComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                EnvironmentsComboBox.Items.Clear();
                EnvironmentsComboBox.DataSource = null;

                EnvironmentsComboBox.Items.Add("Помилка при завантаженні середовищ");
                EnvironmentsComboBox.SelectedIndex = 0;

#if DEBUG
                MessageBox.Show($"Message:\n{ex.Message}\n\nStack trace: \n{ex.StackTrace}",
                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            finally
            {
                isLoading = false;
            }
        }

        private Task<List<Data.Entity.CalculationSeries>> LoadSeriesByIssue(int issueId)
        {
            return Task.Run(() =>
            {
                string condition = $"issue_id = {issueId}";

                if (expert != 0)
                {
                    condition += $" AND id_of_expert = {((int)expert)}";
                }

                return dbManager.GetRows("calculations_description",
                                         "calculation_number, calculation_name",
                                         condition)
                                .Select(r =>
                                {
                                    int id;
                                    int.TryParse(r[0].ToString(), out id);

                                    return new Data.Entity.CalculationSeries
                                    {
                                        id = id,
                                        name = r[1].ToString()
                                    };
                                })
                                .ToList();
            });
        }

        private void ItemConfigurationWindow_Load(object sender, EventArgs e)
        {
            if (IssuesComboBox.DataSource != null)
            {
                IssuesComboBox.SelectedIndex = oldIssueIndex;
            }

            if (EnvironmentsComboBox.DataSource != null)
            {
                EnvironmentsComboBox.SelectedIndex = oldEnvironmentIndex;
            }

            ObjectNameTextBox.Text = oldObjName;
            ObjectDescriptionTextBox.Text = oldObjDescription;
        }

        private async void IssuesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IssuesComboBox.DataSource != null && IssuesComboBox.SelectedItem != null &&
                IssuesComboBox.SelectedItem is Data.Entity.Issue)
            {
                Data.Entity.Issue issue = (Data.Entity.Issue)IssuesComboBox.SelectedValue;

                if (!series.ContainsKey(issue.id))
                {
                    var loadSeries = await LoadSeriesByIssue(issue.id);

                    if (loadSeries.Count == 0)
                    {
                        loadSeries.Add(missingSeries);
                    }

                    series.Add(issue.id, loadSeries);
                }

                SeriesComboBox.DataSource = null;
                SeriesComboBox.DataSource = series[issue.id];
            }
        }

        private void IssueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (issues == null || issues.Count == 0)
            {
                LoadIssues();
            }
        }
        private void EnvironmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (environments == null || environments.Count == 0)
            {
                LoadEnvironments();
            }
        }

        private void m_AcceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ObjectNameTextBox.Text))
            {
                MessageBox.Show("Ви не заповнили усі необхідні поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IssueCheckBox.Checked && !EnvironmentCheckBox.Checked &&
                MessageBox.Show("Ви не вибрали жодної прив'язки для об'єкта. Якщо ви продовжите, " +
                                "то об'єкт не можливо буде фільтрувати на карті.",
                                "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            oldIssueIndex = IssuesComboBox.SelectedIndex != -1 ? IssuesComboBox.SelectedIndex : 0;
            oldEnvironmentIndex = EnvironmentsComboBox.SelectedIndex != -1 ? EnvironmentsComboBox.SelectedIndex : 0;

            oldObjName = ObjectNameTextBox.Text;
            oldObjDescription = ObjectDescriptionTextBox.Text;

            this.DialogResult = DialogResult.OK;
        }

        public void Deconstruct(out Data.Entity.Issue issue, out Data.Entity.Environment environment,
                                out Data.Entity.CalculationSeries series,
                                out string name, out string description)
        {
            issue = GetIssue();
            environment = GetEnvironment();
            series = GetSeries();
            name = ObjectNameTextBox.Text;
            description = ObjectDescriptionTextBox.Text;
        }
    }
}
