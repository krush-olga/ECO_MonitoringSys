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

namespace Maps
{
    public partial class CompareSettings : Form
    {
        private DBManager db;

        private IList<string> markers;
        private IList<string> elements;

        private object locker;

        public CompareSettings(IList<string> markers)
        {
            locker = new object();

            InitializeComponent();

            Markers = markers;
            LoadContext();

            UpdateListBox();
        }

        public IList<string> Markers
        {
            get { return markers; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Markers");
                }

                markers = value;

                UpdateListBox();
            }
        }

        private async void LoadContext()
        {
            try
            {
                db = await Task.Run(() => { return new DBManager(); });

                ElementsСomboBox.DataSource = await Task.Run(() => 
                {
                    StringBuilder elementsCondition = new StringBuilder();
                    string startCondition = "(Name_Object = ";
                    string or = " OR ";
                    string idPoiEquality = " AND emissions_on_map.idPoi = poi.Id AND emissions_on_map.idElement = elements.code)";

                    for (int i = 0; i < Markers.Count; i++)
                    {
                        elementsCondition.Append(startCondition);
                        elementsCondition.Append(DBUtil.AddQuotes(Markers[i]));
                        elementsCondition.Append(idPoiEquality);

                        if (i != Markers.Count - 1)
                        {
                            elementsCondition.Append(or);
                        }
                    }

                    return db.GetRows("elements, emissions_on_map, poi", 
                                      "DISTINCT elements.code, elements.short_name", 
                                      elementsCondition.ToString())
                             .Select(e => new { Id = e[0], Name = e[1] }).ToList(); 
                });

                ElementsСomboBox.Text = string.Empty;
                ElementsСomboBox.DisplayMember = "Name";
                ElementsСomboBox.ValueMember = "Id";

                EndDateDTPicker.MaxDate = DateTime.Now;

                Binding maxEndDateBinding = new Binding("MaxDate", EndDateDTPicker, "Value");

                StartDateDTPicker.DataBindings.Add(maxEndDateBinding);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                MessageBox.Show("Виникла помилка при підключенні до бази даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }

        private void UpdateListBox()
        {
            CompareObjectsListBox.DataSource = null;
            CompareObjectsListBox.DataSource = Markers;
        }

        private void DeleteObjectButton_Click(object sender, EventArgs e)
        {
            if (Markers != null && (Markers.Count < 3 || Markers.Count - CompareObjectsListBox.SelectedIndices.Count < 2))
            {
                MessageBox.Show("Не можливо видалити елемент. " +
                                "Для порівняння необхідно хоча б два елементи",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (CompareObjectsListBox.SelectedIndices.Count != 0)
            {
                for (int i = 0; i < CompareObjectsListBox.SelectedIndices.Count; i++)
                {
                    Markers.RemoveAt(CompareObjectsListBox.SelectedIndices[i]);
                }
            }
            else if (CompareObjectsListBox.SelectedIndex != -1)
            {
                Markers.RemoveAt(CompareObjectsListBox.SelectedIndex);
            }

            UpdateListBox();
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder nameObjectIdCondition = new StringBuilder();

                for (int i = 0; i < Markers.Count; i++)
                {
                    nameObjectIdCondition.Append("(Name_Object = ");
                    nameObjectIdCondition.Append(DBUtil.AddQuotes(markers[i]));
                    nameObjectIdCondition.Append(" AND ");
                    nameObjectIdCondition.Append("emissions_on_map.idPoi = poi.Id");
                    nameObjectIdCondition.Append(" AND ");
                    nameObjectIdCondition.Append("emissions_on_map.idElement = ");
                    nameObjectIdCondition.Append(ElementsСomboBox.SelectedValue);
                    nameObjectIdCondition.Append(" AND ");
                    nameObjectIdCondition.Append("STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d')");
                    nameObjectIdCondition.Append(" >= '");
                    nameObjectIdCondition.Append(StartDateDTPicker.Value.ToString("yyyy-MM-dd"));
                    nameObjectIdCondition.Append("'");
                    nameObjectIdCondition.Append(" AND ");
                    nameObjectIdCondition.Append("STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d')");
                    nameObjectIdCondition.Append(" <= '");
                    nameObjectIdCondition.Append(EndDateDTPicker.Value.ToString("yyyy-MM-dd"));
                    nameObjectIdCondition.Append("')");

                    if (i != Markers.Count - 1)
                    {
                        nameObjectIdCondition.Append(" OR ");
                    }
                }

                var result = await Task.Run<List<List<object>>>(() =>
                {
                    return db.GetRows("emissions_on_map, poi",
                                      "poi.Name_Object, emissions_on_map.ValueAvg, emissions_on_map.ValueMax, " +
                                      "CONCAT(" +
                                      "LPAD(emissions_on_map.day, 2, 0), '-', " +
                                      "LPAD(emissions_on_map.Month, 2, 0), '-', " +
                                      "emissions_on_map.Year" +
                                      "), " +
                                      "emissions_on_map.Measure", nameObjectIdCondition.ToString());
                });

                var borderLimits = await Task.Run<List<List<object>>>(() => 
                {
                    object value = null;
                    Action assignComponentValue = () =>
                    {
                        value = ElementsСomboBox.SelectedValue;
                    };

                    ElementsСomboBox.Invoke(assignComponentValue);

                    return db.GetRows("gdk", "mpc_m_ot, mpc_avrg_d", "code = " + value);
                });

                if (result == null || result.Count == 0)
                {
                    MessageBox.Show("Результати для порівння відсутні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string legend = $"Дане порівняння актуальне для {ElementsСomboBox.Text}.";

                List<string> names = new List<string>();
                Dictionary<string, List<object>> rowsNameAndvalues = new Dictionary<string, List<object>>();

                rowsNameAndvalues.Add("Середнє значення", new List<object>());
                rowsNameAndvalues.Add("Максимальне значення", new List<object>());
                rowsNameAndvalues.Add("Дата, за яку вибрано", new List<object>());
                rowsNameAndvalues.Add("Одиниці виміру значення", new List<object>());

                //Записываем данные в  массив горизонтально (таблица растёт в право, а не в низ)
                //Берём result[0].Count с учётом того, что все последующие массивы одинаковые
                //Первая итерация должна быть названиями маркеров (можно изменить, если есть желание)
                //Т.к. в переменной result первой строкой есть poi.Name_Object
                for (int i = 0; i < result[0].Count; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < result.Count; j++)
                        {
                            names.Add(result[j][i].ToString());
                        }
                    }
                    else
                    {
                        for (int j = 0; j < result.Count; j++)
                        {
                            rowsNameAndvalues.Values.ElementAt(i - 1).Add(result[j][i]);
                        }
                    }
                }

                var distinctNames = names.Distinct();
                int currentRowNameIndex = 0;

                if (distinctNames.Count() < Markers.Count)
                {
                    names.AddRange(Markers.Except(distinctNames));
                }

                names.Insert(0, "");

                var rowsWithBorderLimits = borderLimits.First()
                                                       .OfType<double>()
                                                       .ToDictionary(limit => 
                                                       { 
                                                           return rowsNameAndvalues.Keys.ElementAt(currentRowNameIndex++); 
                                                       });
                var chartRows = new List<string> { "Середнє значення", "Максимальне значення" };

                CompareForm compareForm = null;

                if (borderLimits.Count == 0)
                {
                    compareForm = new CompareForm(rowsNameAndvalues, names, legend);
                }
                else
                {
                    compareForm = new CompareForm(rowsNameAndvalues, names, legend, rowsWithBorderLimits, chartRows,  "Дата, за яку вибрано");
                }

                compareForm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка при порівнянні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
