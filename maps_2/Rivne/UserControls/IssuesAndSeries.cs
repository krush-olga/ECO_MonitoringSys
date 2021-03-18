using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UserMap.UserControls
{
    public partial class IssuesAndSeries : UserControl
    {
        private int oldIssueIndex;
        private IDictionary<int, IEnumerable<int>> selectedSeries;
        private IDictionary<int, IList<Data.Entity.CalculationSeries>> series;

        private Action<Data.Entity.Issue, IDictionary<int, IList<Data.Entity.CalculationSeries>>> issuesCheckListBoxIndexChanged;

        public IssuesAndSeries()
        {
            InitializeComponent();

            selectedSeries = new Dictionary<int, IEnumerable<int>>();
            series = new Dictionary<int, IList<Data.Entity.CalculationSeries>>();
        }

        public void SubscribeIssuesCheckedListBoxIndexChanged(Action<Data.Entity.Issue, IDictionary<int, IList<Data.Entity.CalculationSeries>>> newAction)
        {
            issuesCheckListBoxIndexChanged += newAction;
        }
        public void UnsubscribeIssuesCheckedListBoxIndexChanged(Action<Data.Entity.Issue, IDictionary<int, IList<Data.Entity.CalculationSeries>>> existAction)
        {
            issuesCheckListBoxIndexChanged -= existAction;
        }

        public void SetIssues(IList<Data.Entity.Issue> issues)
        {
            IssuesCheckedListBox.DataSource = issues;
        }
        public void SetSeries(IDictionary<int, IList<Data.Entity.CalculationSeries>> series)
        {
            if (series == null || series.Count == 0)
            {
                throw new ArgumentException("Серии не могу быть пустыми.");
            }

            this.series = series;
        }

        public IEnumerable<Data.Entity.Issue> GetSelectedIssues()
        {
            return IssuesCheckedListBox.SelectedItems.OfType<Data.Entity.Issue>();
        }
        public IDictionary<int, IEnumerable<int>> GetSelectedSeriesIndices()
        {
            return selectedSeries;
        }

        private void IssuesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
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
                    if (issuesCheckListBoxIndexChanged != null)
                    {
                        issuesCheckListBoxIndexChanged(issue, series);
                    }

                    SeriesCheckedListBox.Items.Clear();

                    foreach (var series in series[issue.Id])
                    {
                        SeriesCheckedListBox.Items.Add(series);
                    }
                }
            }

            if (((Data.Entity.CalculationSeries)SeriesCheckedListBox.Items[0]).Id == -1)
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
    }
}
