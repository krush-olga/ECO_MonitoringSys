using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Data;
using Data.Entity;
using System.Linq;
using System.Reflection;
using Calculations;
using HelpModule;

namespace Experts_Economist
{
    public partial class Rozrah : Form
    {
        //используем библиотеки работы с БД и формулами
        private DBManager db = new DBManager();

        //объект для подсчёта формул для експертов
        Case_Of_Calc calculation = new Case_Of_Calc();

        private bool normWasPrevious = false;
        private IEnumerable<Issue> Issues;
        private IEnumerable<Formula> formulas;

        public int id_of_exp;
        public bool help = true;//если true - ещё не посчитали, false - посчитали

        public Rozrah()
        {
            InitializeComponent();
        }

        private void Rozrah_Load(object sender, EventArgs e)
        {

            logTb.Text = "";
            experts_CB.Items.Clear();
            var obj3 = db.GetRows("expert", "*", "id_of_expert > 0 AND id_of_expert != 4 AND id_of_expert != 5");
            var Experts = new List<Expert>();
            foreach (var row in obj3)
            {
                Experts.Add(ExpertMapper.Map(row));
            }
            experts_CB.Items.AddRange(Experts.ToArray());
            formulasDGV.AllowUserToAddRows = false;

            //вызываем при первом открытии формы функцию refresh, которая обновляет элементы со списками, таблицу, номер расчета
            Get_values();

            formulas_idLB.SelectedIndexChanged += formulas_idLB_SelectedIndexChanged;
        }

        //функция для забивания данных в список формул, и номера расчета
        private void Get_values()
        {
            if (id_of_exp == 0)
            {
                label10.Visible = true;
                experts_CB.Visible = true;
                experts_CB.SelectedIndex = 0;
            }
            //очищаем списки формул и список id формул
            formulasLB.Items.Clear();
            formulas_idLB.Items.Clear();
            formulasDGV.Rows.Clear();

            //создаем переменную для записи в неё имён формулы, в цикле записываем имена формулы в список
            formulas = db.GetRows("formulas", "*",
                                  $"formulas.id_of_expert = {id_of_exp} and formulas.id_of_formula IN " +
                                  $"(select id_of_formula from formula_compound)")
                         .Select(row => FormulaMapper.Map(row));

            foreach (var formula in formulas)
            {
                formulasLB.Items.Add(formula);
                formulas_idLB.Items.Add(formula.Id);
            }

            name_of_seriesCB.Items.Clear();
            //функция заполнения таблицы формулами и их параметрами

            issueTB.Items.Clear();
            Issues = db.GetRows("issues", "*", "").Select(row => IssueMapper.Map(row))
                                                  .GroupBy(i => i.name)
                                                  .Select(g => g.FirstOrDefault());

            issueTB.Items.AddRange(Issues.ToArray());

            if (issueTB.Items.Count == 0)
            {
                issueTB.Text = "Не знайдено жодної задачі.";
                addNewSeriesButton.Enabled = false;
            }
            else
            {
                issueTB.SelectedIndex = 0;
            }

            if (formulasLB.Items != null && formulasLB.Items.Count != 0)
            {
                formulasLB.SelectedIndex = 0;
            }

            AllowPatametrTextCB.Visible = formulasDGV.Visible;
        }

        private void EconomistCalculation(int formulaId)
        {
            #region formulas_ekonomist
            formulasDGV.Rows.Add("Result",
                calculation.case_of_formulas(id_of_exp, formulaId, formulasDGV, 0, (int)Iterations.Value),"");
            #endregion formulas_ekonomist
        }

        private void MedicCalculation(int formulaId)
        {
            #region formulas_medic
            calculation.case_of_formulas(id_of_exp, formulaId, formulasDGV, 0, (int)Iterations.Value);
            #endregion formulas_medic
        }

        private void EcologCalculation(int formulaId)
        {
            #region formulas_ecolog
            calculation.case_of_formulas(id_of_exp, formulaId, formulasDGV, 0, (int)Iterations.Value);
            #endregion
        }

        private void EnergeticCalculation(int formulaId)
        {
            #region formulas_energo
            calculation.case_of_formulas(id_of_exp, formulaId, formulasDGV, 0, (int)Iterations.Value);
            #endregion
        }

        // при событии Изменение индекса в списке формулы, то есть при выборе формулы чисти таблицу справа и заносим в неё список параметров для дальнейшего подсчета
        private void FormulasLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (normWasPrevious)
            {
                formulasDGV.Visible = true;
                normDGV.Visible = false;
                normWasPrevious = false;
                taxesDataGridView.Visible = false;
            }
            this.formulasDGV.Rows.Clear();//очищаем таблицу
            formulas_idLB.SelectedIndex = formulasLB.SelectedIndex;//ставим выбранное id в соответствии с выбранной формулой
            string idf = formulas_idLB.SelectedItem.ToString();//переменная для хранения id выбранной формулы

            var formulaDescription = db.GetRows("formulas", "description_of_formula, measurement_of_formula", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);

            if (formulaDescription != null && formulaDescription.Count != 0)
            {
                form_desc_L.Text = $"Опис формули: {formulaDescription[0][0]}.";

                if (string.IsNullOrEmpty(formulaDescription[0][1].ToString()))
                {
                    form_desc_L.Text += "\nОдиниці виміру відсутні.";
                }
                else
                {
                    form_desc_L.Text += "\nОдиниці виміру: " + formulaDescription[0][1] + ".";
                }
            }
            else
            {
                form_desc_L.Text = "Опис формули відсутній.";
            }

            if (((Convert.ToInt32(idf) == 4) || (Convert.ToInt32(idf) == 5) || (Convert.ToInt32(idf) == 6) || (Convert.ToInt32(idf) == 11)
                || (Convert.ToInt32(idf) == 12) || (Convert.ToInt32(idf) == 13) || (Convert.ToInt32(idf) == 15) || (Convert.ToInt32(idf) == 63)
                || (Convert.ToInt32(idf) == 71) || (Convert.ToInt32(idf) == 65) || (Convert.ToInt32(idf) == 75) || (Convert.ToInt32(idf) == 66)
                || (Convert.ToInt32(idf) == 67) || (Convert.ToInt32(idf) == 24) || (Convert.ToInt32(idf) == 44)) & id_of_exp == 1)
            {
                if ((Convert.ToInt32(idf) == 4) || (Convert.ToInt32(idf) == 5) || (Convert.ToInt32(idf) == 6))
                    for_i.Text = "Кількість забрудників";
                if ((Convert.ToInt32(idf) == 15) || (Convert.ToInt32(idf) == 63))
                    for_i.Text = "Кількість видів основних фондів";
                if ((Convert.ToInt32(idf) == 71) || (Convert.ToInt32(idf) == 65) || (Convert.ToInt32(idf) == 75) || (Convert.ToInt32(idf) == 66) || (Convert.ToInt32(idf) == 67) || (Convert.ToInt32(idf) == 24))
                    for_i.Text = "Кількість втраченої продукції";
                if ((Convert.ToInt32(idf) == 11) || (Convert.ToInt32(idf) == 12) || (Convert.ToInt32(idf) == 13))
                    for_i.Text = "Кількість постраждалих";
                if (Convert.ToInt32(idf) == 44)
                    for_i.Text = "Кількість установ \n природно-заповідного фонду ";
                if ((Convert.ToInt32(idf) == 249))
                    for_i.Text = "Кількість відібраних проб";

                for_i.Visible = true;
                Iterations.Visible = true;
                //return;
            }
            else if (((Convert.ToInt32(idf) == 181) || (Convert.ToInt32(idf) == 213)) && id_of_exp == 2)
            {       //блок кода для выпадающего меню еклога
                if (Convert.ToInt32(idf) == 181)
                    for_i.Text = "Кількість забруднюючих речовин";
                for_i.Visible = true;
                Iterations.Visible = true;
                return;
            }
            else
            {
                for_i.Visible = false;
                Iterations.Visible = false;
                this.formulasDGV.Rows.Clear();
            }
            var obj = db.GetRows("formula_compound", "id_of_parameter", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);//переменная для хранения списка параментров привязанных к данной формуле
            var LetsGetFormulaId = db.GetRows("formulas", "id_of_formula", "");
            var obj1 = new List<List<Object>>();//переменная для хранения имени параметра и единиц измерения
            if (((Convert.ToInt32(idf) == 181)) && id_of_exp == 2)
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula,description_of_formula", "id_of_formula = " + obj[i][0].ToString() + " AND id_of_expert = " + id_of_exp);
                    this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                }
            }
            else if (((Convert.ToInt32(idf)) == 171) && id_of_exp == 2)
            {
                normWasPrevious = true;
                formulasDGV.Visible = false;
                normDGV.Visible = true;

                MySqlConnection connection = new MySqlConnection(db.connectionString);

                try
                {
                    connection.Open();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter($"select short_name from elements", connection);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);
                    element.DataSource = DS.Tables[0];
                    element.DisplayMember = "short_name";

                    //close connection
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else if (((Convert.ToInt32(idf)) == 172) && id_of_exp == 1)
            {
                normWasPrevious = true;
                formulasDGV.Visible = false;
                taxesDataGridView.Visible = true;

                MySqlConnection connection = new MySqlConnection(db.connectionString);

                try
                {
                    connection.Open();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter($"select short_name from elements", connection);
                    DataSet DS = new DataSet();
                    mySqlDataAdapter.Fill(DS);
                    elements_taxes.DataSource = DS.Tables[0];
                    elements_taxes.DisplayMember = "short_name";

                    //close connection
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {////
                for (int i = 0; i < obj.Count; i++)//в цикле записываем в таблицу имена параметров,пустое поле для ввода значения параметра и единици измерения
                {
                    obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula,description_of_formula", "id_of_formula = " + obj[i][0].ToString() + " AND id_of_expert = " + id_of_exp);
                    object calculatedFormula = db.GetValue("calculations_result", "result", $"id_of_formula = {obj[i][0].ToString()} and id_of_expert = {id_of_exp}");
                    this.formulasDGV.Rows.Add(obj1[0][0].ToString(), calculatedFormula ?? "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                }
            }
            help = true;//вспомогательная переменная для проверки наличия строки Result с результатом исчисления
        }

        private void Iterations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formulas_idLB.SelectedItem != null)
            {
                this.formulasDGV.Rows.Clear();//очищаем таблицу
                string idf = formulas_idLB.SelectedItem.ToString();//переменная для хранения id выбранной формулы
                int it = (int)Iterations.Value;
                int obj_items = 0;
                int help1 = 0;
                for (int j = 0; j < it; j++)
                {
                    var obj = db.GetRows("formula_compound", "id_of_parameter", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);//переменная для хранения списка параметров привязанных к данной формуле
                    var obj1 = new List<List<Object>>();//переменная для хранения имени параметра и единиц измерения
                    if (Convert.ToInt32(idf) == 15 || Convert.ToInt32(idf) == 16)
                        help1 = 1;
                    obj_items = obj.Count - help1;

                    //Проверяем если индекс формулы равняется 181 и эксперт - эколог
                    if (Convert.ToInt32(idf) == 181 && id_of_exp == 2)
                    {
                        if (j == 0) //Формируем первый параметр только один раз
                        {
                            obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula, description_of_formula",
                                  "id_of_formula = " + obj[0][0].ToString() + " AND id_of_expert = " + id_of_exp);
                            this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                        }
                        if (Convert.ToInt32(obj[1][0]) < 1) // Формируем второй параметр столько раз сколько указано пользователем в выпадающем списке
                            continue;
                        obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula,description_of_formula",
                            "id_of_formula = " + obj[1][0].ToString() + " AND id_of_expert = " + id_of_exp);
                        this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                    }
                    else
                    {
                        for (int i = 0; i < obj_items; i++)//в цикле записываем в таблицу имена параметров,пустое поле для ввода значения параметра и единици измерения
                        {
                            if (Convert.ToInt32(obj[i][0]) < 1)
                                continue;
                            obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula, description_of_formula",
                                "id_of_formula = " + obj[i][0].ToString() + " AND id_of_expert = " + id_of_exp);
                            this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                        }
                    }
                }
                if (help1 == 1)
                {
                    var obj = db.GetRows("formula_compound", "id_of_parameter", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);//переменная для хранения списка параметров привязанных к данной формуле
                    var obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula,description_of_formula", "id_of_formula = " + obj[obj_items][0].ToString() + " AND id_of_expert = " + id_of_exp);
                    this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                }
                help = true;//вспомогательная переменная для проверки наличия строки Result с результатом исчисления
            }
        }

        //событие нажатия на кнопку Зберегти значення та порахувати, смотрит id  формулы и считает по определенным образом, потом записывает в БД значения формулы и параметров если формула ещё не была расчитана в данной серии расчётов
        private void Save_values_Click(object sender, EventArgs e)
        {
            if (name_of_seriesCB.SelectedItem == null || !(name_of_seriesCB.SelectedItem is CalculationSeries))
            {
                MessageBox.Show("Виберіть серію розрахунків.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AllowPatametrTextCB.Checked = false;

            object objId = db.GetValue("calculations_description", "Max(calculation_number) + 1", "id_of_expert = " + id_of_exp);
            string newId = null;

            if (objId == null)
            {
                return;
            }

            newId = objId.ToString();

            if (help == false && normWasPrevious == false)// проверяем есть ли строка с результатом, если есть - удаляем и сбрасываем переменную
            {
                formulasDGV.Rows.RemoveAt(formulasDGV.Rows.Count - 1);
                help = true;
            }

            int idf = Convert.ToInt32(formulas_idLB.SelectedItem);//переменная для хранения id формулы
            try
            {
                for (int i = 0; i < formulasDGV.Rows.Count; i++)
                {
                    if (formulasDGV.Rows[i].Cells[1].Value == null ||
                        formulasDGV.Rows[i].Cells[1].Value.Equals(string.Empty))
                    {
                        MessageBox.Show("Один чи декілька параметрів відстутні.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (formulasDGV.Rows[i].Cells[1].Value.ToString().Length > 100)
                    {
                        MessageBox.Show("Значення параметру не може перевищувати 100 символів.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Один чи декілька параметрів було введено неправильно", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (id_of_exp == 1)
                {
                    EconomistCalculation(idf);
                }
                else if (id_of_exp == 3)
                {
                    MedicCalculation(idf);
                }
                else if (id_of_exp == 2)
                {
                    EcologCalculation(idf);
                }
                else if (id_of_exp == 6)
                {
                    EnergeticCalculation(idf);
                }
                else
                {
                    MessageBox.Show("Для цього експерта немає розрахунків");
                    help = false;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Один чи декілька параметрів було введено неправильно.\n" +
                                "Можливо, ви ввели текст у параметри, де його не має бути.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (name_of_seriesCB.Text == "")
            {
                MessageBox.Show("Виберіть ім'я для серії");
                help = false;
                return;
            }

            //создаём переменные для хранения номера расчётной серии, времени расчёта, id формулы и результата и записываем это всё в БД
            DateTime localDate = DateTime.Now;
            CalculationSeries currentSeries = (CalculationSeries)name_of_seriesCB.SelectedItem;
            string[] fields5 = { };
            string[] values5 = { };
            int issueid = (issueTB.SelectedItem as Issue).id;

            if (idf == 171 && id_of_exp == 2)
            {
                //return;
                //TODO: Save norm results

                foreach (DataGridViewRow row in normDGV.Rows)
                {
                    if (row.Cells[0].Value == null) break;
                    if (row.Cells[1].Value == null) row.Cells[1].Value = 0;
                    var tmp = db.GetRows("calculations_norm", "Id", "");
                    if (tmp.Count == 0)
                    {
                        fields5 = new[] { "Id", "calculation_number", "date_of_calculation", "id_of_formula", "element_name", "value", "result", "id_of_expert" };
                        values5 = new[] { "0", currentSeries.Id.ToString(), "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), $"'{row.Cells[0].Value.ToString()}'", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString().Replace(",", "."), 2.ToString() };
                    }
                    else
                    {
                        fields5 = new[] { "Id", "calculation_number", "date_of_calculation", "id_of_formula", "element_name", "value", "result", "id_of_expert" };
                        values5 = new[] { (Convert.ToInt32(tmp[tmp.Count - 1][0]) + 1).ToString(), currentSeries.Id.ToString(), "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), $"'{row.Cells[0].Value.ToString()}'", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString().Replace(",", "."), 2.ToString() };
                    }
                    db.InsertToBD("calculations_norm", fields5, values5);
                }
                return;
            }
            else
            {
                fields5 = new[] { "calculation_number", "date_of_calculation", "id_of_formula", "result", "id_of_expert" };
                //  if (idf==175 &&id_of_exp==6)//for energetic ОСЕрік
                values5 = new[] { currentSeries.Id.ToString(), "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), formulasDGV.Rows[formulasDGV.Rows.Count - 1].Cells[1].Value.ToString().Replace(",", "."), id_of_exp.ToString() };
            }

            try
            {
                db.InsertToBD("calculations_result", fields5, values5);
                MessageBox.Show("Результат обрахунків додано до бази даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)// ловим эксепшн mysql если идёт дупликация ключа
            {
                //MYSQL_DUPLICATE_PK = 1062;
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Ця формула вже була розрахована у данній серії \nЗмінити ці значення ви можете у вкладці 'Перегляд результатів' ");
                    help = false;
                    return;
                }
                else if (ex.Number == 1452)
                {
                    MessageBox.Show("Серія з таким ім'ям вже існує, виберіть її з списку серій");
                    help = false;
                    return;
                }
                else
                    throw ex;
            }
            // по аналогии с функцие записи результатов формулы записываем значения параметров в БД
            // для формул с сумой записываем по другому алгоритму
            if (((Convert.ToInt32(idf) == 4) || (Convert.ToInt32(idf) == 5) || (Convert.ToInt32(idf) == 6) || (Convert.ToInt32(idf) == 11) || (Convert.ToInt32(idf) == 12) || (Convert.ToInt32(idf) == 13) || (Convert.ToInt32(idf) == 15) || (Convert.ToInt32(idf) == 16) || (Convert.ToInt32(idf) == 18) || (Convert.ToInt32(idf) == 19) || (Convert.ToInt32(idf) == 20) || (Convert.ToInt32(idf) == 21) || (Convert.ToInt32(idf) == 23) || (Convert.ToInt32(idf) == 24) || (Convert.ToInt32(idf) == 44)) & id_of_exp == 1)
            {
                //переменная для хранения значения i - количества итераций
                int it = Convert.ToInt32(Iterations.Text);
                //список id параметров данной формулы
                var obj = db.GetRows("formula_compound", "id_of_parameter", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);
                //первый параметр i - количество итераций
                string[] fields5_1 = { "calculation_number", "id_of_parameter", "parameter_value", "index_of_parameter", "id_of_expert", "id_of_formula" };
                string[] values5_1 = { currentSeries.Id.ToString(), obj[0][0].ToString(), it.ToString(), "0", id_of_exp.ToString(), idf.ToString() };
                db.InsertToBD("parameters_value", fields5_1, values5_1);
                //переменная для хранения количества параметров у формулы
                int obj_items = obj.Count;
                //вспомогательная  переменная, 1 для обычных сумм, 2 для сумм с дополнительными параметрами
                int s = 1;
                //если 4 параметра( i, два для сумы и 1 вне сумы) ставим s=2
                if (obj_items == 4)
                    s = 2;
                //в цикле начиная с 1 посколько i первый параметр и до obj_items-s количества параметров записываем в БД значения параметров
                for (int i = 1; i < (obj_items - s + 1); i++)
                {
                    //переменная для хранения индекса параметров
                    int iter = 1;
                    //id параметра
                    values5_1[1] = obj[i][0].ToString();
                    //в цикле начиная с первого (i-1) - места i-го параметра и до it * (obj_items - s) - последннего места i-го параметра
                    //шаг - j + obj_items (количество переменных) - s
                    for (int j = (i - 1); j < (it * (obj_items - s)); j = j + obj_items - s)
                    {
                        values5_1[2] = formulasDGV.Rows[j].Cells[1].Value.ToString().Replace(",", ".");
                        values5_1[3] = iter.ToString();
                        db.InsertToBD("parameters_value", fields5_1, values5_1);
                        iter++;
                    }
                }
                if (s == 2)
                {
                    values5_1[1] = obj[obj_items - 1][0].ToString();
                    values5_1[2] = formulasDGV.Rows[it * 2].Cells[1].Value.ToString().Replace(",", ".");
                    values5_1[3] = "0";
                    db.InsertToBD("parameters_value", fields5_1, values5_1);
                }
            }
            else
            {
                var obj = db.GetRows("formula_compound", "id_of_parameter", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);
                string[] fields6 = { "calculation_number", "id_of_parameter", "parameter_value", "index_of_parameter", "id_of_expert", "id_of_formula" };
                string[] values6 = { currentSeries.Id.ToString(), "", "", "0", id_of_exp.ToString(), idf.ToString() };
                for (int i = 0; i < obj.Count; i++)
                {
                    values6[1] = obj[i][0].ToString();
                    values6[2] = formulasDGV.Rows[i].Cells[1].Value.ToString().Replace(",", ".");
                    db.InsertToBD("parameters_value", fields6, values6);
                    //  MessageBox.Show();
                }
            }
            help = false;

            logTb.Text += formulasLB.SelectedItem.ToString() + " = " + formulasDGV.Rows[formulasDGV.Rows.Count - 1].Cells[1].Value.ToString() + "\n";
        }

        //событие ведения мышки по списку формул, при наведении на формулу показываем подсказку из БД по формуле
        private void formulasLB_MouseMove(object sender, MouseEventArgs e)
        {
            string strTip = "";//переменная для хранения сообщения подсказки

            //смотрим на каком месте указатель, считываем id формулы и делаем запрос в БД для изъятия подсказки
            int nIdx = formulasLB.IndexFromPoint(e.Location);
            if ((nIdx >= 0) && (nIdx < formulasLB.Items.Count))
            {
                strTip = ((Formula)formulasLB.Items[nIdx]).Description;
            }

            toolTip1.SetToolTip(formulasLB, strTip);
        }

        //событие нажатия клавиши в окне серии расчётов, проверяем на правильность, разрешаем вводить только цифры и только 1 точку
        private void rozrah_numbTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //та же самая проверка только для второй колонке таблицы - колонки значений компонентов
        private void Column2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-') && (e.KeyChar != ' '))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        //событие редактирования ячейки, через него вызываем событие нажатия кнопки для 2 колонки(проверка)
        private void formulasDGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column2_KeyPress);

            if (AllowPatametrTextCB.Checked == true)
            {
                return;
            }


            if (formulasDGV.CurrentCell.ColumnIndex == 1) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column2_KeyPress);
                }
            }
        }

        private void Iterations_VisibleChanged(object sender, EventArgs e)
        {
            Iterations.Value = 1;
            Iterations_SelectedIndexChanged(sender, e);
        }

        private void series_over_Click(object sender, EventArgs e)
        {
            if (issueTB.SelectedItem == null || !(issueTB.SelectedItem is Issue) ||
                name_of_seriesCB.SelectedItem == null || !(name_of_seriesCB.SelectedItem is CalculationSeries))
            {
                return;
            }

            CalculationSeries currentSeries = (CalculationSeries)name_of_seriesCB.SelectedItem;

            //Сохраняем название и описание серии расчётов
            int issueid = (issueTB.SelectedItem as Issue).id;
            if (id_of_exp == 2)
            {
                var tmp = db.GetRows("calculations_description", "calculation_number", "id_of_expert = '2'");
                if (db.GetValue("calculations_description", "calculation_number", $"issue_id = '{issueid}' AND id_of_expert = '2'") == null)
                {
                    if (tmp.Count != 0)
                        db.InsertToBD("calculations_description", $"'{(Convert.ToInt32(tmp[tmp.Count - 1][0]) + 1)}', '{name_of_seriesCB.Text}', '{desc_of_seriesTB.Text}', '{issueid.ToString()}', '2'");
                    else
                        db.InsertToBD("calculations_description", $"'0', {name_of_seriesCB.Text}, {desc_of_seriesTB.Text}, {issueid.ToString()}, '2'");
                }
            }
            else
            {
                string[] fields4_1 = { "calculation_number", "calculation_name", "description_of_calculation", "issue_id", "id_of_expert" };
                string[] values4_1 = { currentSeries.Id.ToString(), "'" + name_of_seriesCB.Text.Replace('\'', '`') + "'", "'" + desc_of_seriesTB.Text.Replace('\'', '`') + "'", issueid.ToString(), id_of_exp.ToString() };
                try
                {
                    db.UpdateRecord("calculations_description", fields4_1, values4_1);
                }
                catch (MySqlException)// ловим эксепшн mysql если идёт дупликация ключа
                {
                    MessageBox.Show("Ця серія вже була описана \nЗмінити ці значення ви можете у вкладці 'Перегляд результатів' ");
                    // MessageBox.Show(eboi.ToString());
                    help = false;
                    return;
                }

                if (formulasLB.Items.Count > 0)
                {
                    formulasLB.SelectedIndex = 1;
                    formulasLB.SelectedIndex = 0;
                }
            }
            name_of_seriesCB.Text = "";
            desc_of_seriesTB.Clear();

        }

        private void experts_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_of_exp = (experts_CB.Items[experts_CB.SelectedIndex] as Expert).id;
            //вызываем при первом открытии формы функцию refresh, которая обновляет элементы со списками, таблицу, номер расчета
            Get_values();
        }

        private void name_of_seriesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (name_of_seriesCB.SelectedItem == null)
            {
                return;
            }

            if (help == false && normWasPrevious == false)// проверяем есть ли строка с результатом, если есть - удаляем и сбрасываем переменную
            {
                formulasDGV.Rows.RemoveAt(formulasDGV.Rows.Count - 1);
                help = true;
            }

            desc_of_seriesTB.Text = ((CalculationSeries)name_of_seriesCB.SelectedItem).Description;
        }

        private void formulasDGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //if ((e.RowIndex >= 0) && (e.RowIndex < formulasDGV.Rows.Count) )
                //{
                //    var cell = formulasDGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //    cell.ToolTipText = formulasDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error");
            }
        }

        private void оновитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logTb.Text = "";
            experts_CB.Items.Clear();
            var obj3 = db.GetRows("expert", "*", "id_of_expert>0 AND id_of_expert<4");
            var Experts = new List<Expert>();
            foreach (var row in obj3)
            {
                Experts.Add(ExpertMapper.Map(row));
            }
            experts_CB.Items.AddRange(Experts.ToArray());
            formulasDGV.AllowUserToAddRows = false;

            Get_values();
        }

        private void showLog_Click(object sender, EventArgs e)
        {
            logL.Visible = !logL.Visible;
            logTb.Visible = !logTb.Visible;
        }

        private void розрахованіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> experts = new Dictionary<int, string>() {
                {0, "Admin"},
                {1, "\"Економіст\"" },
                {2, "\"Еколог\"" },
                {3, "\"Медик\"" }
            };
            int index = name_of_seriesCB.SelectedIndex + 1;
            SugestionsForm sugestions = new SugestionsForm("Edit", experts[id_of_exp], series: index, expert_ID: id_of_exp);
            sugestions.showDialog();

        }

        private void issueTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            name_of_seriesCB.Items.Clear();

            //создаём переменную для хранения списка номеров расчётов и забиваем её в список расчётов
            var calcDescription = db.GetRows("calculations_description", "*", "id_of_expert = " +
                                    id_of_exp + " and issue_id=" + Issues.ElementAt(issueTB.SelectedIndex).id.ToString());

            IEnumerable<CalculationSeries> calculationSeries = calcDescription.Select(c => CalculatoinSeriesMapper.Map(c))
                                                                              .GroupBy(calcSer => calcSer.Name)
                                                                              .Select(g => g.FirstOrDefault());

            if (calcDescription.Count != 0)
            {
                foreach (var calculation in calculationSeries)
                {
                    name_of_seriesCB.Items.Add(calculation);
                }

                name_of_seriesCB.SelectedIndex = 0;
            }
            else
            {
                name_of_seriesCB.Text = "Не знайдено жодної серії розрахунків.";
            }

        }

        private void addNewSeriesButton_Click(object sender, EventArgs e)
        {

            AddNewSeriesWindow addNewSeriesWindow = new AddNewSeriesWindow();

            if (addNewSeriesWindow.ShowDialog() == DialogResult.OK)
            {

                foreach (CalculationSeries item in name_of_seriesCB.Items)
                {
                    if (item.Name == addNewSeriesWindow.SeriesTextBox.Text)
                    {
                        MessageBox.Show("Така серія вже існує.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                Issue currentIssue = (Issue)issueTB.SelectedItem;

                object objId = db.GetValue("calculations_description", "Max(calculation_number) + 1", "");
                string newId = null;

                if (objId == null)
                {
                    return;
                }

                newId = objId.ToString();

                if (newId == "")
                {
                    newId = "1";
                }

                CalculationSeries newCalculationSeries = new CalculationSeries(int.Parse(newId), addNewSeriesWindow.SeriesTextBox.Text, addNewSeriesWindow.DescriptionTextBox.Text);

                string[] fields = { "calculation_number", "calculation_name", "description_of_calculation", "issue_id", "id_of_expert" };
                string[] values = { newId, DBUtil.AddQuotes(newCalculationSeries.Name), DBUtil.AddQuotes(newCalculationSeries.Description), currentIssue.id.ToString(), id_of_exp.ToString() };

                try
                {
                    db.InsertToBD("calculations_description", fields, values);

                    name_of_seriesCB.Items.Add(newCalculationSeries);
                    name_of_seriesCB.SelectedIndex = name_of_seriesCB.Items.Count - 1;

                    desc_of_seriesTB.Text = newCalculationSeries.Description;

                    MessageBox.Show("Нова серій додана.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        class SugestionsForm
        {
            private string title = string.Empty;
            private string contentText = string.Empty;
            private Color primaryColor;
            private Color secondaryColor;
            private Point lastLocation = new Point();
            private int series;
            private int expert_ID;
            private bool mouseDown = false;

            private Form mainForm;
            private Panel sidePanel;
            private Label contentTitle;
            private Label content;
            private Button copy_btn;
            private Button exit_btn;
            private DataGridView dataGrid;

            public SugestionsForm(string title, string content, Color? primaryColor = null, Color? secondaryColor = null, int series = 1, int expert_ID = 0)
            {
                this.title = title;
                this.contentText = content;
                this.primaryColor = primaryColor ?? Color.Black;
                this.secondaryColor = secondaryColor ?? Color.FromArgb(84, 110, 122);
                this.series = series;
                this.expert_ID = expert_ID;
            }
            ~SugestionsForm()
            {
                dataGrid.Dispose();
                exit_btn.Dispose();
                copy_btn.Dispose();
                sidePanel.Dispose();
                mainForm.Dispose();

                dataGrid = null;
                exit_btn = null;
                copy_btn = null;
                sidePanel = null;
                mainForm = null;
            }

            public DialogResult showDialog()
            {
                initComponents();
                initMouseEvenets();
                initDragEvent();
                addAllComponents();

                return mainForm.ShowDialog();
            }

            private void initComponents()
            {
                mainForm = new Form()
                {
                    Left = 0,
                    Top = 0,
                    Width = 600,
                    Height = 400,
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = Color.White
                };

                sidePanel = new Panel()
                {
                    Left = 0,
                    Top = 0,
                    Width = 125,
                    Height = 400,
                    BorderStyle = BorderStyle.None,
                    BackColor = primaryColor
                };

                dataGrid = new DataGridView()
                {
                    Left = 137,
                    Top = 110,
                    Width = 447,
                    Height = 280,
                    BorderStyle = BorderStyle.None

                };

                DataGridViewTextBoxColumn formulaName = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn result = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn date = new DataGridViewTextBoxColumn();

                formulaName.HeaderText = "Назва";
                result.HeaderText = "Результат";
                date.HeaderText = "Дата";

                dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGrid.Columns.Add(date);
                dataGrid.Columns.Add(formulaName);
                dataGrid.Columns.Add(result);


                mainForm.Load += (s, ev) =>
                {
                    DBManager db = new DBManager();
                    var calc_rows = db.GetRows("calculations_result", "date_of_calculation, id_of_formula, result", $"calculation_number = {series} AND id_of_expert = {expert_ID}");
                    foreach (var row in calc_rows)
                    {
                        dataGrid.Rows.Add(row[0], db.GetValue("formulas", "name_of_formula", $"id_of_formula = {row[1]} AND id_of_expert = {expert_ID}"), row[2]);
                    }
                };

                contentTitle = new Label()
                {
                    Left = 137,
                    Top = 26,
                    AutoSize = true,
                    Text = "Останні розраховані формули",
                    Font = new System.Drawing.Font("Arial", 14)
                };

                content = new Label()
                {
                    Left = 140,
                    Top = 64,
                    AutoSize = true,
                    Text = "Формули для розрахунків експерта " + contentText,
                    Font = new System.Drawing.Font("Century Gothic", 10)
                };

                copy_btn = new Button()
                {
                    Left = 20,
                    Top = 37,
                    Width = 85,
                    Height = 39,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = secondaryColor,
                    Text = "КОПІЮВАТИ",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.White
                };

                exit_btn = new Button()
                {
                    Left = 20,
                    Top = 91,
                    Width = 85,
                    Height = 39,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = secondaryColor,
                    Text = "ЗАКРИТИ",
                    DialogResult = DialogResult.Cancel,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                };

                exit_btn.FlatAppearance.BorderSize = 0;
                copy_btn.FlatAppearance.BorderSize = 0;
            }
            private void initMouseEvenets()
            {
                exit_btn.MouseClick += (s, ev) => mainForm.Close();
                copy_btn.MouseClick += (s, ev) =>
                {
                    DBManager db = new DBManager();
                    int rowIndex = dataGrid.CurrentCell.RowIndex;
                    var row_id = db.GetValue("formulas", "id_of_formula", $"name_of_formula = '{dataGrid.Rows[rowIndex].Cells[1].Value.ToString()}' AND id_of_expert = {expert_ID}");
                    var row_name = db.GetValue("calculations_result", "result", $"id_of_formula = {row_id} AND id_of_expert = {expert_ID} AND calculation_number = {series}");
                    Clipboard.SetText(row_name.ToString());
                    MessageBox.Show("Скопійовано в буфер обміну\n" + row_name.ToString(), "Виконано!");
                };
            }
            private void initDragEvent()
            {
                mainForm.MouseDown += (s, ev) =>
                {
                    lastLocation = ev.Location;
                    mouseDown = true;
                };

                mainForm.MouseMove += (s, ev) =>
                {
                    if (ev.Button == MouseButtons.Left && mouseDown)
                    {
                        mainForm.Location = new System.Drawing.Point(
                    (mainForm.Location.X - lastLocation.X) + ev.X, (mainForm.Location.Y - lastLocation.Y) + ev.Y);

                        mainForm.Update();
                    }
                };

                mainForm.MouseUp += (s, ev) =>
                {
                    mouseDown = false;
                };
            }
            private void addAllComponents()
            {
                sidePanel.Controls.Add(copy_btn);
                sidePanel.Controls.Add(exit_btn);

                mainForm.Controls.Add(contentTitle);
                mainForm.Controls.Add(content);

                mainForm.Controls.Add(dataGrid);
                mainForm.Controls.Add(sidePanel);

                mainForm.CancelButton = exit_btn;
            }
        }

        private void formulas_idLB_SelectedIndexChanged(object sender, EventArgs e)
        {
	        Dictionary<int, string> formulas = GetFormulasHelp();
	        var index = int.Parse(formulas_idLB.SelectedItem.ToString());
	        if (formulas.ContainsKey(index))
	        {
		        Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, $"{formulas[index]}#f{formulas_idLB.SelectedItem}");
	        }
        }

        private Dictionary<int, string> GetFormulasHelp()
			=> new Dictionary<int, string>
			{
				[31] = "formulaspage4.html",
				[34] = "formulaspage4.html",
				[49] = "formulaspage4.html",
				[50] = "formulaspage4.html",
				[51] = "formulaspage4.html",
				[52] = "formulaspage4.html",
				[63] = "formulaspage4.html",
				[64] = "formulaspage4.html",
				[65] = "formulaspage4.html",
				[66] = "formulaspage4.html",
				[67] = "formulaspage4.html",
				[71] = "formulaspage4.html",
				[75] = "formulaspage4.html",
				[92] = "formulaspage4.html",
				[93] = "formulaspage4.html",
				[98] = "formulaspage4.html",
				[102] = "formulaspage4.html",
				[106] = "formulaspage4.html",
				[110] = "formulaspage4.html",
				[115] = "formulaspage4.html",
				[123] = "formulaspage4.html",
				[130] = "formulaspage4.html",
				[131] = "formulaspage4.html",
				[138] = "formulaspage4.html",
				[141] = "formulaspage4.html",
				[145] = "formulaspage4.html",
				[146] = "formulaspage4.html",
				[20] = "formulaspage7.html",
				[25] = "formulaspage7.html",
				[29] = "formulaspage7.html",
				[248] = "formulaspage2.html",
				[249] = "formulaspage2.html",
				[253] = "formulaspage2.html",
				[256] = "formulaspage2.html",
				[258] = "formulaspage2.html",
				[1] = "formulaspage6.html",
				[167] = "formulaspage6.html",
            };

        private void startTutorial_Click(object sender, EventArgs e)
        {
	        var frm = new HelpToolTipForm(delegate
	        {
		        new InteractiveToolTipCreator().CreateTips(new List<InteractiveToolTipModel>
		        {
			        new InteractiveToolTipModel
			        {
				        Control = formulasLB,
				        Text = "В блоці \"Список формул\" оберіть формулу"
			        },
			        new InteractiveToolTipModel
			        {
				        Control = name_of_seriesCB,
				        Text = "Оберіть серію із випадаючого списку"
			        },
			        new InteractiveToolTipModel
			        {
				        Control = addNewSeriesButton,
				        Text = "Кнопка \"+\" призначена для додання нової серії розрахунків"
			        },
			        new InteractiveToolTipModel
			        {
				        Control = issueTB,
				        Text = "Необхідно обрати задачу, до якої буде прикріплена серія розрахунку"
			        },
			        new InteractiveToolTipModel
			        {
				        Control = normDGV,
				        Text = "Необхідно заповнити стовпчик \"Значення\" в блоці \"Список параметрів даної формули\""
			        },
			        new InteractiveToolTipModel
			        {
				        Control = AllowPatametrTextCB,
				        Text =
					        "Для того, щоб дозволити введення строкових значень у таблицю, необхідно заповнити дане поле\""
			        },
			        new InteractiveToolTipModel
			        {
				        Control = Save_values,
				        Text = "Після заповнення натисніть на кнопку \"Порахувати та зберегти\""
			        },
			        new InteractiveToolTipModel
			        {
				        Control = normDGV,
				        Text =
					        "Після цього у серію буде додано необхідні розрахунки та в таблиці з’явиться запис \"Результат\" - це значення, що було розраховано за формулою"
			        },
			        new InteractiveToolTipModel
			        {
				        Control = showLog,
				        Text = "Щоб переглянути останні розрахунки необхідно натиснути на кнопку \">\""
			        }
		        });
	        }, delegate { Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, "p7.html"); });
	        frm.ShowDialog();
        }

        private void startTutorial_MouseEnter(object sender, EventArgs e)
		{
			startTutorial.Font = new Font(startTutorial.Font, FontStyle.Bold);
		}

		private void startTutorial_MouseLeave(object sender, EventArgs e)
		{
			startTutorial.Font = new Font(startTutorial.Font, FontStyle.Regular);
		}
    }
}