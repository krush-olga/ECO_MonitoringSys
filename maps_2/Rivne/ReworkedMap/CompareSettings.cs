using Data;
using Maps.Core;
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

        private IList<IDescribable> describableItems;

        public CompareSettings(IList<IDescribable> elements)
        {
            InitializeComponent();

            Elements = elements;
            LoadContext();

            UpdateListBox();
        }

        public IList<IDescribable> Elements
        {
            get { return describableItems; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                describableItems = value;

                UpdateListBox();
            }
        }

        private async void LoadContext()
        {
            try
            {
                db = await Task.Run(() => { return new DBManager(); });

                StringBuilder elementsCondition = new StringBuilder();
                string emissionIdColumn = string.Empty;
                string objectNameColumn = string.Empty;
                string objectTableName = string.Empty;
                string objectIdColumn = string.Empty;
                string joinCondition = "emissions_on_map.idElement = elements.code, emissions_on_map.idPoi IS NOT NULL" +
                                       " AND emissions_on_map.idPoi = poi.Id, emissions_on_map.idPoligon IS NOT NULL" +
                                       " AND emissions_on_map.idPoligon = poligon.Id_of_poligon";
                string format = "({3}.{1} = '{0}' AND emissions_on_map.{2} = {3}.{4}) OR ";

                foreach (var element in Elements)
                {
                    if (element.Type == "Маркер")
                    {
                        emissionIdColumn = "idPoi";
                        objectNameColumn = "Name_Object";
                        objectTableName = "poi";
                        objectIdColumn = "Id";
                    }
                    else if (element.Type == "Полігон")
                    {
                        emissionIdColumn = "idPoligon";
                        objectNameColumn = "name";
                        objectTableName = "poligon";
                        objectIdColumn = "Id_of_poligon";
                    }

                    elementsCondition.AppendFormat(format, element.Name, objectNameColumn,
                                                   emissionIdColumn, objectTableName, objectIdColumn);
                }

                elementsCondition.Remove(elementsCondition.Length - 3, 3);

                ElementsСomboBox.DataSource = (await db.GetRowsUsingJoinAsync($"elements, emissions_on_map, poi, poligon",
                                                                       "DISTINCT elements.code, elements.short_name",
                                                                       joinCondition, elementsCondition.ToString(), JoinType.LEFT))
                                                        .Select(row => new
                                                        {
                                                            Id = row[0].ToString(),
                                                            Name = row[1].ToString()
                                                        })
                                                        .ToList();

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
            CompareObjectsListBox.DataSource = Elements;
        }

        private void DeleteObjectButton_Click(object sender, EventArgs e)
        {
            var listElements = Elements;

            if (Elements != null && (listElements.Count < 3 || listElements.Count - CompareObjectsListBox.SelectedIndices.Count < 2))
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
                    listElements.RemoveAt(CompareObjectsListBox.SelectedIndices[i]);
                }
            }
            else if (CompareObjectsListBox.SelectedIndex != -1)
            {
                listElements.RemoveAt(CompareObjectsListBox.SelectedIndex);
            }

            UpdateListBox();
        }

        private async void CompareButton_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder nameObjectIdCondition = new StringBuilder();
                string emissionIdColumn = string.Empty;
                string objectNameColumn = string.Empty;
                string objectTableName = string.Empty;
                string objectIdColumn = string.Empty;
                string format = "({3}.{1} = '{0}' AND emissions_on_map.{2} = {3}.{4} AND emissions_on_map.idElement = {5} AND " +
                                "STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d') >= " +
                                "'{6}' AND STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d') <=" +
                                "'{7}') OR ";
                string joinCondition = "emissions_on_map.idPoi IS NOT NULL AND emissions_on_map.idPoi = poi.Id, " +
                                       "emissions_on_map.idPoligon IS NOT NULL AND emissions_on_map.idPoligon = poligon.Id_of_poligon";

                foreach (var element in Elements)
                {
                    if (element.Type == "Маркер")
                    {
                        emissionIdColumn = "idPoi";
                        objectNameColumn = "Name_Object";
                        objectTableName = "poi";
                        objectIdColumn = "Id";
                    }
                    else if (element.Type == "Полігон")
                    {
                        emissionIdColumn = "idPoligon";
                        objectNameColumn = "name";
                        objectTableName = "poligon";
                        objectIdColumn = "Id_of_poligon";
                    }

                    nameObjectIdCondition.AppendFormat(format, element.Name, objectNameColumn,
                                                       emissionIdColumn, objectTableName, objectIdColumn,
                                                       ElementsСomboBox.SelectedValue,
                                                       StartDateDTPicker.Value.ToString("yyyy-MM-dd"),
                                                       EndDateDTPicker.Value.ToString("yyyy-MM-dd"));
                }

                nameObjectIdCondition.Remove(nameObjectIdCondition.Length - 3, 3);

                //for (int i = 0; i < Elements.Count; i++)
                //{
                //    nameObjectIdCondition.Append("(Name_Object = ");
                //    nameObjectIdCondition.Append(DBUtil.AddQuotes(describableItems[i]));
                //    nameObjectIdCondition.Append(" AND ");
                //    nameObjectIdCondition.Append("emissions_on_map.idPoi = poi.Id");
                //    nameObjectIdCondition.Append(" AND ");
                //    nameObjectIdCondition.Append("emissions_on_map.idElement = ");
                //    nameObjectIdCondition.Append(ElementsСomboBox.SelectedValue);
                //    nameObjectIdCondition.Append(" AND ");
                //    nameObjectIdCondition.Append("STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d')");
                //    nameObjectIdCondition.Append(" >= '");
                //    nameObjectIdCondition.Append(StartDateDTPicker.Value.ToString("yyyy-MM-dd"));
                //    nameObjectIdCondition.Append("'");
                //    nameObjectIdCondition.Append(" AND ");
                //    nameObjectIdCondition.Append("STR_TO_DATE(CONCAT(Year,'-',LPAD(Month,2,'00'),'-',LPAD(day,2,'00')), '%Y-%m-%d')");
                //    nameObjectIdCondition.Append(" <= '");
                //    nameObjectIdCondition.Append(EndDateDTPicker.Value.ToString("yyyy-MM-dd"));
                //    nameObjectIdCondition.Append("')");

                //    if (i != Elements.Count - 1)
                //    {
                //        nameObjectIdCondition.Append(" OR ");
                //    }
                //}



                var result = await db.GetRowsUsingJoinAsync("emissions_on_map, poi, poligon",
                                                            "poi.Name_Object, poligon.name, emissions_on_map.ValueAvg, emissions_on_map.ValueMax, " +
                                                            "CONCAT(" +
                                                            "LPAD(emissions_on_map.day, 2, 0), '-', " +
                                                            "LPAD(emissions_on_map.Month, 2, 0), '-', " +
                                                            "emissions_on_map.Year ), " +
                                                            "emissions_on_map.Measure",
                                                            joinCondition, nameObjectIdCondition.ToString(), JoinType.LEFT);

                var borderLimits = await db.GetRowsAsync("gdk", "mpc_m_ot, mpc_avrg_d", 
                                                         "code = " + ElementsСomboBox.SelectedValue.ToString());

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
                //Первые две колонки в результате являются именами, так 
                //Первая итерация должна быть названиями маркеров (можно изменить, если есть желание)
                //Т.к. в переменной result первой строкой есть poi.Name_Object
                for (int i = 0; i < result[0].Count; i++)
                {
                    if (i == 0 || i == 1)
                    {
                        for (int j = 0; j < result.Count; j++)
                        {
                            if (!(result[j][i] is DBNull))
                            {
                                names.Add(result[j][i].ToString());
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < result.Count; j++)
                        {
                            rowsNameAndvalues.Values.ElementAt(i - 2).Add(result[j][i]);
                        }
                    }
                }

                var distinctNames = names.Distinct();
                int currentRowNameIndex = 0;

                if (distinctNames.Count() < Elements.Count)
                {
                    names.AddRange(Elements.Select(elem => elem.Name).Except(distinctNames));
                }

                names.Insert(0, "");



                IDictionary<string, double> rowsWithBorderLimits = null;
                if (borderLimits.Count != 0)
                {
                    rowsWithBorderLimits = borderLimits.First()
                                                       .OfType<double>()
                                                       .ToDictionary(limit =>
                                                       {
                                                           return rowsNameAndvalues.Keys.ElementAt(currentRowNameIndex++);
                                                       });
                }
                var chartRows = new List<string> { "Середнє значення", "Максимальне значення" };

                CompareForm compareForm = null;

                if (borderLimits.Count == 0)
                {
                    compareForm = new CompareForm(rowsNameAndvalues, names, legend, null, chartRows, "Дата, за яку вибрано");
                }
                else
                {
                    compareForm = new CompareForm(rowsNameAndvalues, names, legend, rowsWithBorderLimits, chartRows,  "Дата, за яку вибрано");
                }

                compareForm.ShowDialog();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"{ex.Message}\n\n{ex.StackTrace}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                MessageBox.Show("Помилка при порівнянні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
    }
}
