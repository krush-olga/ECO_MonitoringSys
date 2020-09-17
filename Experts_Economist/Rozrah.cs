using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Data;



namespace Experts_Economist
{
    public partial class Rozrah : Form
    {
        private DBManager db = new DBManager();
        private EconomistCalculator calc = new EconomistCalculator();
        private MedicalCalculator medCalc = new MedicalCalculator();
        private EcologCalculator ecoCalc = new EcologCalculator();
        private bool normWasPrevious = false;

        public int id_of_exp;

        public Rozrah()
        {
            InitializeComponent();
            
        }

        private void Rozrah_Load(object sender, EventArgs e)
        {
            
            logTb.Text = "";
            experts_CB.Items.Clear();
            var obj3 = db.GetRows("expert", "*", "id_of_expert > 0 AND id_of_expert < 4");
            var Experts = new List<Expert>();
            foreach (var row in obj3)
            {
                Experts.Add(ExpertMapper.Map(row));
            }
            experts_CB.Items.AddRange(Experts.ToArray());
            formulasDGV.AllowUserToAddRows = false;

            //вызываем при первом открытии формы функцию refresh, которая обновляет элементы со списками, таблицу, номер расчета
            Get_values();
        }

        public bool help = true;//если true - ещё не посчитали, false - посчитали

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
            var obj = db.GetRows("formulas, formula_compound", "distinct name_of_formula",
                $"formulas.id_of_expert = {id_of_exp} and formulas.id_of_formula IN (select id_of_formula from formula_compound)");
            string item = "";//вспомогательная переменная для хранения имён
            for (int i = 0; i < obj.Count; i++)
            {
                for (int j = 0; j < obj[i].Count; j++)
                {
                    item += obj[i][j];
                }
                formulasLB.Items.Add(item);
                item = "";
            }

            //создаем переменную для записи в неё id формулы, в цикле записываем id формулы в список
            var obj1 = db.GetRows("formulas", "id_of_formula",
                $"formulas.id_of_expert = {id_of_exp} and formulas.id_of_formula IN (select id_of_formula from formula_compound)");
            item = "";//вспомогательная переменная для хранения id
            for (int i = 0; i < obj1.Count; i++)
            {
                for (int j = 0; j < obj1[i].Count; j++)
                {
                    item += obj1[i][j];
                }
                formulas_idLB.Items.Add(item);
                item = "";
            }
            calc_numbCB.Items.Clear();//очищаем список расчётов
            name_of_seriesCB.Items.Clear();
            //создаём переменную для хранения списка номеров расчётов и забиваем её в список расчётов
            var obj01 = db.GetRows("calculations_description", "calculation_number, calculation_name", "id_of_expert = " + id_of_exp);
            for (int i = 0; i < obj01.Count; i++)
            {
                calc_numbCB.Items.Add(Convert.ToInt32(obj01[i][0]).ToString());
                name_of_seriesCB.Items.Add(obj01[i][1].ToString());
            }
            //функция заполнения таблицы формулами и их параметрами

            //находим максимальный номер расчётов и записываем в ячейку с номером расчетов это число + 1, если расчетов нет, ставим номер расчетов 1
            var obj02 = db.GetRows("calculations_description", "Max(calculation_number)", "id_of_expert = " + id_of_exp);
            try
            {
                calc_numbCB.Text = (Convert.ToInt32(obj02[0][0]) + 1).ToString();
            }
            catch { calc_numbCB.Text = "1"; }

            issueTB.Items.Clear();
            var obj3 = db.GetRows("issues", "*", "");
            var Issues = new List<Issue>();
            foreach (var row in obj3)
            {
                Issues.Add(IssueMapper.Map(row));
            }

            issueTB.Items.AddRange(Issues.ToArray());
            try
            {
                issueTB.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
            try
            {
                formulasLB.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
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
            form_desc_L.Text = "Опис формули : " + db.GetValue("formulas", "description_of_formula", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp).ToString();
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
                
                for_i.Visible = true;
                Iterations.Visible = true;
                //return;
            }else if( ( (Convert.ToInt32(idf) == 181) || (Convert.ToInt32(idf) == 213) ) && id_of_exp == 2 ){       //блок кода для выпадающего меню еклога
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
            var LetsGetFormulaId = db.GetRows("formulas", "id_of_formula","");
            var obj1 = new List<List<Object>>();//переменная для хранения имени параметра и единиц измерения
            if(((Convert.ToInt32(idf) == 181) ) && id_of_exp == 2)
            {
                for (int i = 0; i < obj.Count; i++)
                {
                    obj1 = db.GetRows("formulas", "name_of_formula, measurement_of_formula,description_of_formula", "id_of_formula = " + obj[i][0].ToString() + " AND id_of_expert = " + id_of_exp);
                    this.formulasDGV.Rows.Add(obj1[0][0].ToString(), "0", obj1[0][1].ToString(), obj1[0][2].ToString());
                }
            }
            else if(((Convert.ToInt32(idf)) == 171) && id_of_exp == 2)
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
            else if(((Convert.ToInt32(idf)) == 172) && id_of_exp == 1)
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
                    object calculatedFormula = db.GetValue("calculations_result","result", $"id_of_formula = {obj[i][0].ToString()} and id_of_expert = {id_of_exp}");
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
            if (help == false && normWasPrevious == false)// проверяем есть ли строка с результатом, если есть - удаляем и сбрасываем переменную
            {
                formulasDGV.Rows.RemoveAt(formulasDGV.Rows.Count - 1);
                help = true;
            }

            int idf = Convert.ToInt32(formulas_idLB.SelectedItem);//переменная для хранения id формулы
            double[] h = new double[90];
            try
            {
                for (int i = 0; i < formulasDGV.Rows.Count; i++)
                {
                    h[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                    if (formulasDGV.Rows[i].Cells[1].Value.Equals(""))
                    {
                        MessageBox.Show("Один чи декілька параметрів було введено неправильно");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Один чи декілька параметрів було введено неправильно");
                return;
            }
            if (id_of_exp == 1)
            {
                #region formulas_ekonomist

                switch (idf)//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом
                {
                    case 31:
                        {
                            double Гоз = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Пд = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Кз = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double Кег = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рш(Гоз, Пд, Кз, Кег), "");
                            break;
                        }
                    case 37:
                        {
                            double Аф = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Вф = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Зф = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Сіф(Аф, Вф, Зф), "");
                            break;
                        }
                    case 41:
                        {
                            double Нр = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Мр = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Рсг = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double Ррг = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double Рлг = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double Ррек = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                            double Рпзф = Convert.ToDouble(formulasDGV.Rows[6].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Сіп(Нр, Мр, Рсг, Ррг, Рлг, Ррек, Рпзф), "");
                            break;
                        }
                    case 43:
                        {
                            double Фв = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Фг = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Пр = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double Пр_с = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double Сн = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double Мдг = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Мр(Фв, Фг, Пр, Пр_с, Сн, Мдг), "");
                            break;
                        }
                    case 44:
                        {
                            double Рсг1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Рсг2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рсг(Рсг1, Рсг2), "");
                            break;
                        }
                    case 46:
                        {
                            double Рлг1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Рлг2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Рлг3 = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рлг(Рлг1, Рлг2, Рлг3), "");
                            break;
                        }
                    case 47:
                        {
                            //int iterations = Convert.ToInt32(Iterations.Text);
                            //double[] Зр = { };
                            //double[] Рп = { };
                            //double[] Рс = { };
                            //for (int i = 0; i < iterations-3; i++)
                            //{
                            //    Зр[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            //    Рп[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                            //    Рс[i] = Convert.ToDouble(formulasDGV.Rows[i+2].Cells[1].Value);
                            //}

                            //formulasDGV.Rows.Add("Result",calc.Ррек(iterations, Зр, Рп, Рс), "");
                            double Т = Convert.ToInt32(formulasDGV.Rows[0].Cells[1].Value);
                            double П = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);


                            formulasDGV.Rows.Add("Result",calc.Ррек_2(Т, П), "");

                            break;
                        }
                    case 48: {
                            double Пз = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Рз = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рпзф(Пз, Рз), "");
                            break;
                        }
                    case 49: {
                            double Втрр = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Вдп = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Ввтг = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Нр(Втрр, Вдп, Ввтг), "");
                            break;
                        }
                    case 50: {
                            double N = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Мл = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Мт = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double Мі = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double Мз = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Втрр(N, Мл, Мт, Мі, Мз), "");
                            break;
                        }
                    case 51: {
                            double Мдп = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Nз = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Вдп(Мдп, Nз), "");
                            break;
                        }
                    case 52: {
                            double Мвтг = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Вд = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Втг(Мвтг, Вд), "");
                            break;
                        }
                    case 63: {
                            int iterations = Convert.ToInt32(Iterations.Text);
                            double[] p = new double[iterations];
                            double[] k = new double[iterations];
                            double лв = Convert.ToDouble(formulasDGV.Rows[formulasDGV.Rows.Count-1].Cells[1].Value);
                            for (int i = 0; i <= iterations - 1; i++)
                            {
                                p[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                k[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Фг(iterations, p, k, лв), "");
                            break;
                        }
                    case 64: {
                            double Прп = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Прс = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Пр(Прп, Прс), "");
                            break;
                        }
                    case 65: {
                            double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] S = new double[iterations];
                            double[] k = new double[iterations];
                            double[] У = new double[iterations];
                            double[] Ц = new double[iterations];
                            double[] Здод = new double[iterations];
                            for (int i = 1; i < iterations-4; i++)
                            {
                                S[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                k[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                                У[i] = Convert.ToDouble(formulasDGV.Rows[i+2].Cells[1].Value);
                                Ц[i] = Convert.ToDouble(formulasDGV.Rows[i+3].Cells[1].Value);
                                Здод[i] = Convert.ToDouble(formulasDGV.Rows[i+4].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Пр_с(m, S, k, У, Ц, Здод), "");
                            break;
                        }
                    case 66: {
                            double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] Ц = new double[iterations];
                            double[] q = new double[iterations];
                            for (int i = 1; i < iterations-1; i++)
                            {
                                Ц[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                q[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Сн(m, Ц, q), "");
                            break;
                        }
                    case 67: {
                            double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] P = new double[iterations];
                            double[] Ka = new double[iterations];
                            double[] k = new double[iterations];
                            double[] qорг = new double[iterations];
                            double[] Цср = new double[iterations];
                            double[] qгр = new double[iterations];
                            for (int i = 1; i < iterations-5; i++)
                            {
                                P[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                Ka[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                                k[i] = Convert.ToDouble(formulasDGV.Rows[i+2].Cells[1].Value);
                                qорг[i] = Convert.ToDouble(formulasDGV.Rows[i+3].Cells[1].Value);
                                Цср[i] = Convert.ToDouble(formulasDGV.Rows[i+4].Cells[1].Value);
                                qгр[i] = Convert.ToDouble(formulasDGV.Rows[i+5].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Мдг(m, P, Ka, k, qорг, Цср, qгр), "");
                            break;
                        }
                    case 71: {
                            double n = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] C = new double[iterations];
                            double[] q = new double[iterations];
                            for (int i = 1; i <= iterations-1; i++)
                            {
                                C[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                q[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Прп(n, C, q), "");
                            break;
                        }
                    case 75: {
                            double n = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] Ц = new double[iterations];
                            double[] q = new double[iterations];
                            for (int i = 0; i < iterations-1; i++)
                            {
                                Ц[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                                q[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                            }

                            this.formulasDGV.Rows.Add("Result", calc.Прс(n, Ц, q), "");
                            break;
                        }
                    case 92:
                        {
                            double H = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рсг1(H, П), "");
                            break;
                        }
                    case 93:
                        {
                            double k = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double H = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рсг2(k, H, П), "");
                            break;
                        }
                    case 98:
                        {
                            double B = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double N = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Мтв(B, N), "");
                            break;
                        }
                    case 102:
                        {
                            double Н = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double k = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рлг1(Н, k, П), "");
                            break;
                        }
                    case 106:
                        {
                            double Н = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double k = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рлг2(Н, k, П), "");
                            break;
                        }
                    case 110:
                        {
                            double Н1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Н2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double k = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Рлг3(Н1, Н2, k, П), "");
                            break;
                        }
                    case 115: {
                            double П = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double S = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double M = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double П1 = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double К1 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double П2 = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                            double К2 = Convert.ToDouble(formulasDGV.Rows[6].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N(П, S, M, П1, К1, П2, К2), "");
                            break;
                        }
                    case 123:
                        {
                            double П = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Z = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Q = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double C = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double K = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double M = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N1(П, Z, Q, C, K, M), "");
                            break;
                        }
                    case 130:
                        {
                            double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double H = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double P = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double K1 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double K2 = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N2(S, H, П, P, K1, K2), "");
                            break;
                        }
                    case 131:
                        {
                            double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double P = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double K1 = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double K2 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N3(S, П, P, K1, K2), "");
                            break;
                        }
                    case 138:
                        {
                            double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double P = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N4(S, P), "");
                            break;
                        }
                    case 141:
                        {
                            double Z = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Q = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double C = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double M = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            double S = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                            double П = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                            double K = Convert.ToDouble(formulasDGV.Rows[6].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.N5(S, П, Z, Q, C, K, M), "");
                            break;
                        }
                    case 145:
                        {
                            double Ап = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Анс = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] Ii = new double[iterations];

                            for (int i = 2; i < iterations; i++)
                            {
                                Ii[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            }
                            
                            this.formulasDGV.Rows.Add("Result", calc.Пз(Ап, Анс, Ii), "");
                            break;
                        }
                    case 146:
                        {
                            int iterations = Convert.ToInt32(Iterations.Value);
                            double[] Q1 = new double[iterations];
                            double[] Q2 = new double[iterations];
                            for (int i = 0; i < iterations; i++)
                            {
                                Q1[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value); 
                                Q2[i] = Convert.ToDouble(formulasDGV.Rows[i+1].Cells[1].Value);
                            }
                            this.formulasDGV.Rows.Add("Result", calc.Рз(iterations, Q1, Q2), "");
                            break;
                        }
                    case 153:
                        {
                            double Ut = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Ct = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Zt = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.Qt(Ut, Ct, Zt), "");
                            break;
                        }
                    case 157:
                        {
                            double ЧГПо = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double ЧГПі = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double ЧГПф = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", calc.ЧГПп(ЧГПо, ЧГПі, ЧГПф), "");
                            break;
                        }
                    case 161:
                        {
                            double Ut = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double Ct = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double Zt = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);

                            //this.formulasDGV.Rows.Add("Result", calc.(Ut, Ct, Zt), "");
                            break;
                        }

                    case 172: {///////
                            double result = 0;
                            foreach (DataGridViewRow row in taxesDataGridView.Rows)
                            {
                                //Если достигнута строка с пустыми значениями то выходим с цикла
                                if (row.Cells[0].Value == null) break;
                                if (row.Cells[1].Value == null) row.Cells[1].Value = 0;
                                try
                                {
                                    foreach (var ch in row.Cells[1].Value.ToString())
                                    {
                                        if (!Char.IsDigit(ch) && ch != ',' && ch != '.')
                                        {
                                            throw new ArgumentException("Значення повинні бути числові");
                                        }
                                    }
                                    //Получаем идентификатор формулы по короткому имени так как оно уникальное 
                                    var element_ID = db.GetValue("elements", "code", $"short_name = '{row.Cells[0].Value.ToString()}'");
                                    //Ищем такой же идентификатор в таблице с записями про ГДЗ(гранично допустимые значения) и получаем ОБУВ(уровень опасности)
                                    var element_gdk = db.GetValue("gdk", "tsel", $"code = {element_ID}");
                                    //Считаем налог в зависимости от уровня опасности
                                    if(Convert.ToDouble(element_gdk) >= 0.1)
                                    {
                                        result = 74.17 * Convert.ToDouble(row.Cells[1].Value);
                                    }
                                    else if(Convert.ToDouble(element_gdk) < 0.1 && Convert.ToDouble(element_gdk) >= 0.01)
                                    {
                                        result = 1968.65 * Convert.ToDouble(row.Cells[1].Value);
                                    }
                                    else if(Convert.ToDouble(element_gdk) < 0.01 && Convert.ToDouble(element_gdk) >= 0.001)
                                    {
                                        result = 7015.25 * Convert.ToDouble(row.Cells[1].Value);
                                    }
                                    else if(Convert.ToDouble(element_gdk) < 0.001 && Convert.ToDouble(element_gdk) >= 0.0001)
                                    {
                                        result = 50783.62 * Convert.ToDouble(row.Cells[1].Value);
                                    }
                                    else if(Convert.ToDouble(element_gdk) < 0.0001)
                                    {
                                        result = 592712.5 * Convert.ToDouble(row.Cells[1].Value);
                                    }
                                    //Пишем результат в следующей колонке рядка
                                    row.Cells[2].Value = result;
                                }
                                catch (ArgumentException argEx)
                                {
                                    MessageBox.Show(argEx.Message);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Помилка.\nМожливо в таблиці відсутнє значення ставки для цього елемента");
                                }
                            }
                            break;
                        }
                    default:
                        { 
                            MessageBox.Show("Формулу не знайдено");
                            break;
                        }
                        #region old
                        //    case 43:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 2:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        //            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        //            if (d <= 0)
                        //            {
                        //                MessageBox.Show("Сума розрахунків та пасивів не може мати таке значення");
                        //                return;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Kp(a, b, c, d), "");
                        //            break;
                        //        }
                        //    case 3:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 4:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Mi = new double[it];
                        //            double[] Npi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Mi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Npi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Mi, Npi), "");
                        //            break;
                        //        }
                        //    case 5:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Mi = new double[it];
                        //            double[] Npi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Mi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Npi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pc(it, Mi, Npi), "");
                        //            break;
                        //        }
                        //    case 6:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Mi = new double[it];
                        //            double[] Npi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Mi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Npi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Prv(it, Mi, Npi), "");
                        //            break;
                        //        }
                        //    case 161:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            int c = Convert.ToInt32(formulasDGV.Rows[2].Cells[1].Value);
                        //            int d = Convert.ToInt32(formulasDGV.Rows[3].Cells[1].Value);
                        //            double f = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        //            if (c <= 0 || d <= 0)
                        //            {
                        //                MessageBox.Show("Неправильні значення Tр чи Тт");
                        //                return;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.E(a, b, c, d, f), "");
                        //            break;
                        //        }
                        //    case 8:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 9:
                        //        {
                        //            double[] Mr = new double[7];
                        //            for (int i = 0; i < 7; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 7), "");
                        //            break;
                        //        }
                        //    case 10:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 11:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Ml = new double[it];
                        //            double[] Nl = new double[it];
                        //            double[] Mt = new double[it];
                        //            double[] Nt = new double[it];
                        //            double[] Mi = new double[it];
                        //            double[] Ni = new double[it];
                        //            double[] Mz = new double[it];
                        //            double[] Nz = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Ml[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Nl[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Mt[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Nt[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Mi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Ni[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Mz[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Nz[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Vtrr(it, Ml, Nl, Mt, Nt, Mi, Ni, Mz, Nz), "");
                        //            break;
                        //        }
                        //    case 12:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Mdp = new double[it];
                        //            double[] Nz = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Mdp[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Nz[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Mdp, Nz), "");
                        //            break;
                        //        }
                        //    case 13:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Mdp = new double[it];
                        //            double[] Nz = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Mdp[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Nz[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Vtg(it, Mdp, Nz), "");
                        //            break;
                        //        }
                        //    case 14:
                        //        {
                        //            double[] Mr = new double[6];
                        //            for (int i = 0; i < 6; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 6), "");
                        //            break;
                        //        }
                        //    case 15:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Pi = new double[it];
                        //            double[] Li = new double[it];
                        //            double Lv = Convert.ToDouble(formulasDGV.Rows[it * 2].Cells[1].Value);
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Pi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Li[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Fg(it, Pi, Li, Lv), "");
                        //            break;
                        //        }
                        //    case 16:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Pi = new double[it];
                        //            double[] Li = new double[it];
                        //            double Lv = Convert.ToDouble(formulasDGV.Rows[it * 2].Cells[1].Value);
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Pi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Li[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Fg(it, Pi, Li, Lv), "");
                        //            break;
                        //        }
                        //    case 17:
                        //        {
                        //            double[] Mr = new double[2];
                        //            for (int i = 0; i < 2; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 2), "");
                        //            break;
                        //        }
                        //    case 18:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Ci = new double[it];
                        //            double[] qi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Ci[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Ci, qi), "");
                        //            break;
                        //        }
                        //    case 19:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Tci = new double[it];
                        //            double[] qi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Tci[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Tci, qi), "");
                        //            break;
                        //        }
                        //    case 20:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Si = new double[it];
                        //            double[] Ki = new double[it];
                        //            double[] Ui = new double[it];
                        //            double[] Tci = new double[it];
                        //            double[] Zi_dod = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Si[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Ki[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Ui[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Tci[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Zi_dod[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Prc(it, Si, Ki, Ui, Tci, Zi_dod), "");
                        //            break;
                        //        }
                        //    case 21:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Tci_ser = new double[it];
                        //            double[] qi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Tci_ser[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Tci_ser, qi), "");
                        //            break;
                        //        }
                        //    case 22:
                        //        {
                        //            double[] Mr = new double[2];
                        //            for (int i = 0; i < 2; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 2), "");
                        //            break;
                        //        }
                        //    case 23:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Tci = new double[it];
                        //            double[] qi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Tci[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pvc(it, Tci, qi), "");
                        //            break;
                        //        }
                        //    case 24:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Pi = new double[it];
                        //            double[] Ki = new double[it];
                        //            double[] ki = new double[it];
                        //            double[] qi = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Pi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Ki[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                ki[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mdg_o(it, Pi, Ki, ki, qi), "");
                        //            break;
                        //        }
                        //    case 25:
                        //        {
                        //            double[] Mr = new double[2];
                        //            for (int i = 0; i < 2; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 2), "");
                        //            break;
                        //        }
                        //    case 26:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rsg1(a, b), "");
                        //            break;
                        //        }
                        //    case 27:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rsg2(a, b, c), "");
                        //            break;
                        //        }
                        //    case 28:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rsg1(a, b), "");
                        //            break;
                        //        }
                        //    case 29:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 101:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rlg1(a, b, c), "");
                        //            break;
                        //        }
                        //    case 106:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rlg2(a, b, c), "");
                        //            break;
                        //        }
                        //    case 110:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        //            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rlg3(a, b, c, d), "");
                        //            break;
                        //        }
                        //    case 33:
                        //        {
                        //            double[] Mr = new double[6];
                        //            for (int i = 0; i < 6; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 6), "");
                        //            break;
                        //        }
                        //    case 115:
                        //        {
                        //            double[] Mr = new double[7];
                        //            for (int i = 0; i < 7; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.N(Mr), "");
                        //            break;
                        //        }
                        //    case 123:
                        //        {
                        //            double[] Mr = new double[6];
                        //            for (int i = 0; i < 6; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.N1(Mr), "");
                        //            break;
                        //        }
                        //    case 36:
                        //        {
                        //            double[] Mr = new double[6];
                        //            for (int i = 0; i < 6; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.N2(Mr), "");
                        //            break;
                        //        }
                        //    case 37:
                        //        {
                        //            double[] Mr = new double[5];
                        //            for (int i = 0; i < 5; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.N3(Mr), "");
                        //            break;
                        //        }
                        //    case 92:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rsg1(a, b), "");
                        //            break;
                        //        }
                        //    case 141:
                        //        {
                        //            double[] Mr = new double[7];
                        //            for (int i = 0; i < 7; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.N5(Mr), "");
                        //            break;
                        //        }
                        //    case 40:
                        //        {
                        //            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //            this.formulasDGV.Rows.Add("Result", calc.Rsg1(a, b), "");
                        //            break;
                        //        }
                        //    case 41:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 42:
                        //        {
                        //            double[] Mr = new double[2];
                        //            for (int i = 0; i < 2; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 2), "");
                        //            break;
                        //        }
                        //    case 47:
                        //        {
                        //            double[] Mr = new double[3];
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                Mr[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Mr(Mr, 3), "");
                        //            break;
                        //        }
                        //    case 145:
                        //        {
                        //            int it = Convert.ToInt32(Iterations.Text);
                        //            int j = 0;
                        //            double[] Qi = new double[it];
                        //            double[] Qi_p = new double[it];
                        //            for (int i = 0; i < it; i++)
                        //            {
                        //                Qi[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //                Qi_p[i] = Convert.ToDouble(formulasDGV.Rows[j].Cells[1].Value);
                        //                j++;
                        //            }
                        //            this.formulasDGV.Rows.Add("Result", calc.Pz(it, Qi, Qi_p), "");
                        //            break;
                        //        }
                        #endregion old
                }

                #endregion formulas_ekonomist
            }
            else if (id_of_exp == 3)
            {
                #region formulas_medic
                switch (idf)//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом
                {
                    case 1:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getOSTG(a, b), "");
                            break;
                        }
                    case 167:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getOSTG_2(a, b, c), "");
                            break;
                        }
                    case 20:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getPM_GIM(a, b, c, d), "");
                            break;
                        }
                    case 29:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getPM_MI(a, b), "");
                            break;
                        }
                    case 25:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getPM_HCVP(a, b), "");
                            break;
                        }
                    case 169:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getBVForMen(a, b, c, d), "");
                            break;
                        }
                    case 7:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getBVForWomen(a, b, c, d), "");
                            break;
                        }
                    case 18:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getNBVForMen(a), "");
                            break;
                        }
                    case 170:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getNBVForWomen(a), "");
                            break;
                        }
                    case 13:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                            double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getFV_SSSForMen(a, b, c, d), "");
                            break;
                        }
                    case 28:
                        {
                            double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                            double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                            this.formulasDGV.Rows.Add("Result", medCalc.getFV_SSSForWomen(a, b), "");
                            break;
                        }
                }
                #endregion formulas_medic
            }
            else if(id_of_exp==2)
            {
                #region formulas_ecolog
                switch (idf)
                {
                    case 186:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.PERELOGI),"");
                            break;
                        }
                    /*case 172:
                        {
                            double area = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double volume = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double index = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",ecoCalc.Кз(area,volume,index),"");
                            break;
                        }*/
                    case 175:
                        {
                            double mass = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double density = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Озр(mass, density),"куб.м");
                            break;
                        }
                    case 178:
                        {
                            double concentration = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double groundThickness = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double index = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Кз_2(concentration, groundThickness, index), "куб.м");
                            break;
                        }
                    case 181:
                        {
                            double max = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            int length = Convert.ToInt32(Iterations.Text);
                            double[] arr = {0};
                            Array.Resize(ref arr, length);
                            for (int i = 1; i < length; i++)
                            {
                                arr[i-1] = Convert.ToDouble(this.formulasDGV.Rows[i].Cells[1].Value);
                            }
                            this.formulasDGV.Rows.Add("Result",ecoCalc.Рш_заг(max, arr),"");
                            break;
                        }
                    case 50:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double wasteDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",
                                ecoCalc.Ршз(
                                    EcologCalculator.RivenZasmichenia.NIZKUYI,
                                    money,
                                    polutionArea,
                                    polutionSubstanceDangerCoefficient,
                                    wasteDangerCoefficient,
                                    ecoAndEconomValue
                                ), 
                           "");

                            break;
                        }
                    case 51:
                        {

                            break;
                        }
                    case 52:
                        {
                            double avgValueOfMassConcentration = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double valueOfAllowableWaste = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double gasDustFlowCost = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",
                                ecoCalc.Formula7(
                                    avgValueOfMassConcentration,
                                    valueOfAllowableWaste,
                                    gasDustFlowCost
                                ),
                           "");

                            break;
                        }
                    case 203:
                        {
                            double population = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double cityMeaning = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Кт(population, cityMeaning), "");

                            break;
                        }
                    case 199:
                        {
                            double polutionSubstanceMass = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double relativeDangerOfPolutionSubstance = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double atmospherePolutionLevelCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double minSalary = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",
                                ecoCalc.З(
                                   polutionSubstanceMass,
                                   minSalary,
                                   relativeDangerOfPolutionSubstance,
                                   ecoAndEconomValue,
                                   atmospherePolutionLevelCoefficient
                                ),
                           "");

                            break;
                        }
                    case 55:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double wasteDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",
                                ecoCalc.Ршз(
                                    EcologCalculator.RivenZasmichenia.VELUKYUI,
                                    money,
                                    polutionArea,
                                    polutionSubstanceDangerCoefficient,
                                    wasteDangerCoefficient,
                                    ecoAndEconomValue
                                ),
                           "");

                            break;
                        }
                    case 56:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result",
                                ecoCalc.Рш(
                                    money,
                                    polutionArea,
                                    groundPolutionCoefficient,
                                    polutionSubstanceDangerCoefficient,
                                    ecoAndEconomValue,
                                    EcologCalculator.PonuzuvalnuyKoef.LISOSMUGI_NASADJENYA
                                ),
                            "");

                            break;
                        }
                    case 57:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.LISOVI_ZEMLI), "");
                            break;
                        }
                    case 58:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.CHAGARNYKI), "");
                            break;
                        }
                    case 59:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.ZABUDOVANI_ZEMLI), "");
                            break;
                        }
                    case 60:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.ZABOLOXHENI_ZEMLI), "");
                            break;
                        }
                    case 61:
                        {
                            double money = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double polutionArea = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double groundPolutionCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);
                            double polutionSubstanceDangerCoefficient = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value);
                            double ecoAndEconomValue = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.VIDKRYTI_ZEMLI), "");
                            break;
                        }
                    case 62:
                        {
                            double atmosphereTemperatureCoefficient = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value); 
                            double massOfHarmfulSubstance = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value); 
                            double speedOfSubstanceSedimentationCoefficient = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value); 
                            double firstGasPollutionCondition = Convert.ToDouble(this.formulasDGV.Rows[3].Cells[1].Value); 
                            double secondGasPollutionCondition = Convert.ToDouble(this.formulasDGV.Rows[4].Cells[1].Value); 
                            double terrainTopographyCoefficient = Convert.ToDouble(this.formulasDGV.Rows[5].Cells[1].Value); 
                            double sourceHeight = Convert.ToDouble(this.formulasDGV.Rows[6].Cells[1].Value); 
                            double fixedCostsOfGasEmission = Convert.ToDouble(this.formulasDGV.Rows[7].Cells[1].Value); 
                            double temperatureDifference = Convert.ToDouble(this.formulasDGV.Rows[8].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula20(
                                atmosphereTemperatureCoefficient,
                                massOfHarmfulSubstance,
                                speedOfSubstanceSedimentationCoefficient,
                                firstGasPollutionCondition,
                                secondGasPollutionCondition,
                                terrainTopographyCoefficient,
                                sourceHeight,
                                fixedCostsOfGasEmission,
                                temperatureDifference
                                ),
                            "");

                            break;
                        }
                    case 63:
                        {
                            double fuelAmount = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double amountOfFuelWaste = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula15(
                                fuelAmount,
                                amountOfFuelWaste
                                ),
                            "");

                            break;
                        }
                    case 64:
                        {
                            double burnedMassOfTPV = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double specificEmission = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula21(
                                burnedMassOfTPV,
                                specificEmission
                                ),
                            "");

                            break;
                        }
                    case 65:
                        {
                            double maxPermisibbleValue = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double maxSewageWaste = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.ГДС(
                                maxPermisibbleValue,
                                maxSewageWaste
                                ),
                            "");

                            break;
                        }
                    case 66:
                        {
                            double flowCoefficient = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double areaOfGround = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double precipitations = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula17(
                                flowCoefficient,
                                areaOfGround,
                                precipitations
                                ),
                            "");

                            break;
                        }
                    case 67:
                        {
                            double waterConsumption = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double wateringAmount = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double areaOfGround = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula18(
                                waterConsumption,
                                wateringAmount,
                                areaOfGround
                                ),
                            "");

                            break;
                        }
                    case 213:
                        {
                            // List<double> concentrations = new List<double>();
                            // List<double> maxPermisibbleValues = new List<double>();
                            int length = Convert.ToInt32(Iterations.Text);

                            double[] concentrations = new double[length];
                            double[] maxPermisibbleValue= new double[length];

                            int i = 0;
                            for (i = 0; i <= length-1; i++)
                            {
                                maxPermisibbleValue[i] = Convert.ToDouble(this.formulasDGV.Rows[i].Cells[1].Value);
                                concentrations[i] = Convert.ToDouble(this.formulasDGV.Rows[i+1].Cells[1].Value);

                                //maxPermisibbleValues.Add(Convert.ToDouble(this.formulasDGV.Rows[i].Cells[1].Value));
                                //concentrations.Add(Convert.ToDouble(this.formulasDGV.Rows[i+1].Cells[1].Value));
                            }

                            string waterQuality = "";

                            this.formulasDGV.Rows.Add("Result", ecoCalc.ІЗВ(
                                //concentrations.ToArray(),
                               // maxPermisibbleValues.ToArray(),
                               concentrations,
                               maxPermisibbleValue,
                                length,
                                ref waterQuality
                                ),
                            "");

                            this.formulasDGV.Rows[i+1].Cells[2].Value = waterQuality;

                            break;
                        }
                    case 171:
                        {
                            foreach (DataGridViewRow row in normDGV.Rows)
                            {
                                //Если достигнута строка с пустыми значениями то выходим с цикла
                                if (row.Cells[0].Value == null) break;
                                if (row.Cells[1].Value == null) row.Cells[1].Value = 0;
                                try
                                {
                                    foreach (var ch in row.Cells[1].Value.ToString())
                                    {
                                        if (!Char.IsDigit(ch) && ch != ',' && ch != '.')
                                        {
                                            throw new ArgumentException("Значення повинні бути числові");
                                        }
                                    }
                                    //Получаем идентификатор формулы по короткому имени так как оно уникальное 
                                    var element_ID = db.GetValue("elements", "code", $"short_name = '{row.Cells[0].Value.ToString()}'");
                                    //Ищем такой же идентификатор в таблице с записями про ГДЗ(гранично допустимые значения)
                                    var element_gdk = db.GetValue("gdk", "mpc_m_ot", $"code = {element_ID}");
                                    //Получаем разницу между введёнными пользователем значениями и табличными если таковые есть
                                    double result = (double)element_gdk - Convert.ToDouble(row.Cells[1].Value);
                                    //Пишем результат в следующей колонке рядка
                                    row.Cells[2].Value = result;
                                }
                                catch (ArgumentException argEx)
                                {
                                    MessageBox.Show(argEx.Message);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Помилка.\nМожливо в таблиці відсутнє значення гдк для цього елемента");
                                }
                            }
                            break;
                        }
                    case 191:
                        {
                            double avrg = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double norm = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);
                            double objem = Convert.ToDouble(this.formulasDGV.Rows[2].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Formula7(
                                avrg,
                                norm,
                                objem
                                ),
                            "");
                            break;
                        }
                    case 195:
                        {
                            break;
                        }
                    /*case 172:
                        {
                            double Taxation = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                            double Pollution = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                            this.formulasDGV.Rows.Add("Result", ecoCalc.Под(Taxation, Pollution),"");
                            break;
                        }*/
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Для цього експерта немає розрахунків");
                help = false;
                return;
            }
            //проверка введён ли корректный номер расчётной серии
            if (calc_numbCB.Text == "" || calc_numbCB.Text == "0")
            {
                var obj2 = db.GetRows("calculations_description", "Max(calculation_number)", "id_of_expert = " + id_of_exp);
                if (obj2.Count == 1)
                    calc_numbCB.Text = "1";
                else
                    calc_numbCB.Text = (Convert.ToInt32(obj2[0][0]) + 1).ToString();
            }

            if (name_of_seriesCB.Text == "")
            {
                MessageBox.Show("Введіть ім'я для серії");
                help = false;
                return;
            }

            //создаём переменные для хранения номера расчётной серии, времени расчёта, id формулы и результата и записываем это всё в БД
            DateTime localDate = DateTime.Now;
            string[] fields5 = { };
            string[] values5 = { };
            int issueid = (issueTB.SelectedItem as Issue).id;
            string[] fields4_1 = { "calculation_number", "calculation_name", "description_of_calculation", "issue_id", "id_of_expert" };
            string[] values4_1 = { calc_numbCB.Text, "'" + name_of_seriesCB.Text.Replace('\'', '`') + "'", "'" + desc_of_seriesTB.Text.Replace('\'', '`') + "'", issueid.ToString(), id_of_exp.ToString() };



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
                        values5 = new[] { 0.ToString(), calc_numbCB.Text, "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), $"'{row.Cells[0].Value.ToString()}'", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString().Replace(",", "."), 2.ToString() };
                    }
                    else
                    {
                        fields5 = new[] { "Id", "calculation_number", "date_of_calculation", "id_of_formula", "element_name", "value", "result", "id_of_expert" };
                        values5 = new[] { (Convert.ToInt32(tmp[tmp.Count - 1][0]) + 1).ToString(), calc_numbCB.Text, "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), $"'{row.Cells[0].Value.ToString()}'", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString().Replace(",", "."), 2.ToString() };
                    }
                    db.InsertToBD("calculations_norm", fields5, values5);
                }
                return;
                //normDGV.Rows.
                //values5 = new[]{ calc_numbCB.Text, "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), normDGV.Rows[normDGV.Rows.Count - 1].Cells[2].Value.ToString().Replace(",", "."), id_of_exp.ToString() };
            }
            else
            {
                
                fields5 = new[]{ "calculation_number", "date_of_calculation", "id_of_formula", "result", "id_of_expert" };
                values5 = new[]{ calc_numbCB.Text, "'" + localDate.ToString("yyyy-MM-dd HH:mm:ss") + "'", idf.ToString(), formulasDGV.Rows[formulasDGV.Rows.Count - 1].Cells[1].Value.ToString().Replace(",", "."), id_of_exp.ToString() };
            }
            try
            {
                db.InsertToBD("calculations_description", fields4_1, values4_1);
            }
            catch (Exception)
            {
            }
            try
            {
                db.InsertToBD("calculations_result", fields5, values5);
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
                string[] values5_1 = { calc_numbCB.Text, obj[0][0].ToString(), it.ToString(), "0", id_of_exp.ToString(), idf.ToString() };
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
                string[] values6 = { calc_numbCB.Text, "", "", "0", id_of_exp.ToString(), idf.ToString() };
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
                string idf = formulas_idLB.Items[nIdx].ToString();
                var obj2 = db.GetRows("formulas", "description_of_formula", "id_of_formula = " + idf + " AND id_of_expert = " + id_of_exp);
                strTip = obj2[0][0].ToString();
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
                string[] values4_1 = { calc_numbCB.Text, "'" + name_of_seriesCB.Text.Replace('\'', '`') + "'", "'" + desc_of_seriesTB.Text.Replace('\'', '`') + "'", issueid.ToString(), id_of_exp.ToString() };
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

                calc_numbCB.Text = (Convert.ToInt32(calc_numbCB.Text) + 1).ToString();
                if (formulasLB.Items.Count > 0)
                {
                    formulasLB.SelectedIndex = 1;
                    formulasLB.SelectedIndex = 0;
                }
            }
            name_of_seriesCB.Text = "";
            desc_of_seriesTB.Clear();

        }

        private void calc_numbCB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                desc_of_seriesTB.Clear();// очищаем поле описания расчёта
                string idc = calc_numbCB.Text;//переменная для хранения id расчёта
                //переменная для хранения имени и описания расчёта
                var calc = db.GetRows("calculations_description", "calculation_name,description_of_calculation,issue_id", "calculation_number = " + idc + " AND id_of_expert = " + id_of_exp);
                //заполняем поля соответственно
                if (calc.Count > 0)
                {
                    name_of_seriesCB.SelectedIndex = name_of_seriesCB.FindString(calc[0][0].ToString());
                }
                else
                {
                    name_of_seriesCB.Text = "";
                }
                desc_of_seriesTB.Text = calc[0][1].ToString();
                for (int i = 0; i < issueTB.Items.Count; i++)
                {
                    if ((issueTB.Items[i] as Issue).id == Convert.ToInt32(calc[0][2]))
                        issueTB.SelectedIndex = i;
                }
            }
            catch (Exception)
            {
            }
            if (calc_numbCB.Text == "")
                calc_numbCB.Text = prev_calc_numb;
        }

        public string prev_calc_numb = "";

        //проверка ввода номера серии расчётов
        private void calc_numbCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            prev_calc_numb = calc_numbCB.Text;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void experts_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_of_exp = (experts_CB.Items[experts_CB.SelectedIndex] as Expert).id;
            //вызываем при первом открытии формы функцию refresh, которая обновляет элементы со списками, таблицу, номер расчета
            Get_values();
        }

        private void name_of_seriesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var calc = db.GetRows("calculations_description", "calculation_number", "calculation_name = '" + name_of_seriesCB.Text.Replace('\'', '`') + "' AND id_of_expert = " + id_of_exp);
                if (calc_numbCB.Text == calc[0][0].ToString())
                {
                    return;
                }
                calc_numbCB.SelectedIndex = calc_numbCB.FindString(calc[0][0].ToString());
            }
            catch (Exception)
            {
            }
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
                MessageBox.Show("Error"+ex.Message,"Error");
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

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void розрахованіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> experts = new Dictionary<int, string>() {
                {0, "Admin"},
                {1, "\"Економіст\"" },
                {2, "\"Еколог\"" },
                {3, "\"Медик\"" }
            };
            int index = name_of_seriesCB.SelectedIndex+1;
            SugestionsForm sugestions = new SugestionsForm("Edit", experts[id_of_exp], series: index, expert_ID: id_of_exp);
            sugestions.showDialog();
            
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
                    foreach(var row in calc_rows)
                    {
                        dataGrid.Rows.Add(row[0],db.GetValue("formulas", "name_of_formula", $"id_of_formula = {row[1]} AND id_of_expert = {expert_ID}"), row[2]);
                    }
                };

                contentTitle = new Label()
                {
                    Left = 137,
                    Top = 26,
                    AutoSize = true,
                    Text = "Останні розраховані формули",
                    Font = new System.Drawing.Font("Arial",14)
                };

                content = new Label()
                {
                    Left = 140,
                    Top = 64,
                    AutoSize = true,
                    Text = "Формули для розрахунків експерта "+contentText,
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
                    var row_id = db.GetValue("formulas", "id_of_formula",$"name_of_formula = '{dataGrid.Rows[rowIndex].Cells[1].Value.ToString()}' AND id_of_expert = {expert_ID}");
                    var row_name = db.GetValue("calculations_result", "result", $"id_of_formula = {row_id} AND id_of_expert = {expert_ID} AND calculation_number = {series}");
                    Clipboard.SetText(row_name.ToString());
                    MessageBox.Show("Скопійовано в буфер обміну\n"+ row_name.ToString(),"Виконано!");
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
    }
}