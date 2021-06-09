using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculations
{
    public class Case_Of_Calc
    {
        private EconomistCalculator calc = new EconomistCalculator();
        private MedicalCalculator medCalc = new MedicalCalculator();
        private EcologCalculator ecoCalc = new EcologCalculator();
        private EnergoCalculator enegroCalc = new EnergoCalculator();
        public Case_Of_Calc()
        {

        }
        //
        public string case_of_formulas(int id_of_exp, int idf, DataGridView formulasDGV, int cur_row, int iterations)
        {
            if (id_of_exp == 1)//ecomonist
            {
                return formulas_economist(idf, formulasDGV, cur_row, iterations);
            }
            else if (id_of_exp == 3)//medic
            {
                return formulas_medic(idf, formulasDGV, cur_row, iterations);
            }
            else if (id_of_exp == 2)//ecolog
            {
                return formulas_ecolog(idf, formulasDGV, cur_row, iterations);
            }
            return " ";
        }
        
        public string formulas_economist(int idf, DataGridView formulasDGV, int cur_row, int iterations)
        {
            #region formulas_ekonomist

            switch (idf)//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом
            {
                case 31:
                    {
                        double Гоз = Convert.ToDouble(formulasDGV.Rows[cur_row + 0].Cells[1].Value);
                        double Пд = Convert.ToDouble(formulasDGV.Rows[cur_row + 1].Cells[1].Value);
                        double Кз = Convert.ToDouble(formulasDGV.Rows[cur_row + 2].Cells[1].Value);
                        double Кег = Convert.ToDouble(formulasDGV.Rows[cur_row + 3].Cells[1].Value);
                        return calc.Рш(Гоз, Пд, Кз, Кег).ToString();             
                    }
                case 37:
                    {
                        double Аф = Convert.ToDouble(formulasDGV.Rows[cur_row + 0].Cells[1].Value);
                        double Вф = Convert.ToDouble(formulasDGV.Rows[cur_row + 1].Cells[1].Value);
                        double Зф = Convert.ToDouble(formulasDGV.Rows[cur_row + 2].Cells[1].Value);
                        return calc.Сіф(Аф, Вф, Зф).ToString();                        
                    }
                case 41:
                    {
                        double Нр = Convert.ToDouble(formulasDGV.Rows[cur_row + 0].Cells[1].Value);
                        double Мр = Convert.ToDouble(formulasDGV.Rows[cur_row + 1].Cells[1].Value);
                        double Рсг = Convert.ToDouble(formulasDGV.Rows[cur_row + 2].Cells[1].Value);
                        double Ррг = Convert.ToDouble(formulasDGV.Rows[cur_row + 3].Cells[1].Value);
                        double Рлг = Convert.ToDouble(formulasDGV.Rows[cur_row + 4].Cells[1].Value);
                        double Ррек = Convert.ToDouble(formulasDGV.Rows[cur_row + 5].Cells[1].Value);
                        double Рпзф = Convert.ToDouble(formulasDGV.Rows[cur_row + 6].Cells[1].Value);
                        return calc.Сіп(Нр, Мр, Рсг, Ррг, Рлг, Ррек, Рпзф).ToString();                       
                    }
                case 43:
                    {
                        double Фв = Convert.ToDouble(formulasDGV.Rows[cur_row + 0].Cells[1].Value);
                        double Фг = Convert.ToDouble(formulasDGV.Rows[cur_row + 1].Cells[1].Value);
                        double Пр = Convert.ToDouble(formulasDGV.Rows[cur_row + 2].Cells[1].Value);
                        double Пр_с = Convert.ToDouble(formulasDGV.Rows[cur_row + 3].Cells[1].Value);
                        double Сн = Convert.ToDouble(formulasDGV.Rows[cur_row + 4].Cells[1].Value);
                        double Мдг = Convert.ToDouble(formulasDGV.Rows[cur_row + 5].Cells[1].Value);
                        return calc.Мр(Фв, Фг, Пр, Пр_с, Сн, Мдг).ToString();                        
                    }
                case 44:
                    {
                        double Рсг1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Рсг2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Рсг(Рсг1, Рсг2).ToString();                       
                    }
                case 46:
                    {
                        double Рлг1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Рлг2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double Рлг3 = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Рлг(Рлг1, Рлг2, Рлг3).ToString(); 
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
                        return calc.Ррек_2(Т, П).ToString();
                    }
                case 48:
                    {
                        double Пз = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Рз = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Рпзф(Пз, Рз).ToString();
                    }
                case 49:
                    {
                        double Втрр = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Вдп = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double Ввтг = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Нр(Втрр, Вдп, Ввтг).ToString();
                    }
                case 50:
                    {
                        double N = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Мл = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double Мт = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double Мі = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double Мз = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return calc.Втрр(N, Мл, Мт, Мі, Мз).ToString();
                    }
                case 51:
                    {
                        double Мдп = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Nз = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Вдп(Мдп, Nз).ToString();
                    }
                case 52:
                    {
                        double Мвтг = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Вд = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Втг(Мвтг, Вд).ToString();
                    }
                case 63:
                    {
                        // int iterations = Convert.ToInt32(Iterations.Text);
                        double[] p = new double[iterations];
                        double[] k = new double[iterations];
                        double лв = Convert.ToDouble(formulasDGV.Rows[formulasDGV.Rows.Count - 1].Cells[1].Value);
                        for (int i = 0; i <= iterations - 1; i++)
                        {
                            p[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            k[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                        }
                        return calc.Фг(iterations, p, k, лв).ToString();
                    }
                case 64:
                    {
                        double Прп = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Прс = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Пр(Прп, Прс).ToString();
                    }
                case 65:
                    {
                        double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] S = new double[iterations];
                        double[] k = new double[iterations];
                        double[] У = new double[iterations];
                        double[] Ц = new double[iterations];
                        double[] Здод = new double[iterations];
                        for (int i = 1; i < iterations - 4; i++)
                        {
                            S[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            k[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                            У[i] = Convert.ToDouble(formulasDGV.Rows[i + 2].Cells[1].Value);
                            Ц[i] = Convert.ToDouble(formulasDGV.Rows[i + 3].Cells[1].Value);
                            Здод[i] = Convert.ToDouble(formulasDGV.Rows[i + 4].Cells[1].Value);
                        }
                        return calc.Пр_с(m, S, k, У, Ц, Здод).ToString();
                    }
                case 66:
                    {
                        double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] Ц = new double[iterations];
                        double[] q = new double[iterations];
                        for (int i = 1; i < iterations - 1; i++)
                        {
                            Ц[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            q[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                        }
                        return calc.Сн(m, Ц, q).ToString();
                    }
                case 67:
                    {
                        double m = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] P = new double[iterations];
                        double[] Ka = new double[iterations];
                        double[] k = new double[iterations];
                        double[] qорг = new double[iterations];
                        double[] Цср = new double[iterations];
                        double[] qгр = new double[iterations];
                        for (int i = 1; i < iterations - 5; i++)
                        {
                            P[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            Ka[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                            k[i] = Convert.ToDouble(formulasDGV.Rows[i + 2].Cells[1].Value);
                            qорг[i] = Convert.ToDouble(formulasDGV.Rows[i + 3].Cells[1].Value);
                            Цср[i] = Convert.ToDouble(formulasDGV.Rows[i + 4].Cells[1].Value);
                            qгр[i] = Convert.ToDouble(formulasDGV.Rows[i + 5].Cells[1].Value);
                        }
                        return calc.Мдг(m, P, Ka, k, qорг, Цср, qгр).ToString();
                    }
                case 71:
                    {
                        double n = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] C = new double[iterations];
                        double[] q = new double[iterations];
                        for (int i = 1; i <= iterations - 1; i++)
                        {
                            C[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            q[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                        }
                        return calc.Прп(n, C, q).ToString();
                    }
                case 75:
                    {
                        double n = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] Ц = new double[iterations];
                        double[] q = new double[iterations];
                        for (int i = 0; i < iterations - 1; i++)
                        {
                            Ц[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            q[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                        }
                        return calc.Прс(n, Ц, q).ToString();
                    }
                case 92:
                    {
                        double H = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Рсг1(H, П).ToString();
                    }
                case 93:
                    {
                        double k = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double H = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Рсг2(k, H, П).ToString();
                    }
                case 98:
                    {
                        double B = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double N = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.Мтв(B, N).ToString();
                    }
                case 102:
                    {
                        double Н = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double k = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Рлг1(Н, k, П).ToString();
                    }
                case 106:
                    {
                        double Н = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double k = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Рлг2(Н, k, П).ToString();
                    }
                case 110:
                    {
                        double Н1 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Н2 = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double k = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        return calc.Рлг3(Н1, Н2, k, П).ToString();
                    }
                case 115:
                    {
                        double П = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double S = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double M = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double П1 = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double К1 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        double П2 = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                        double К2 = Convert.ToDouble(formulasDGV.Rows[6].Cells[1].Value);
                        return calc.N(П, S, M, П1, К1, П2, К2).ToString();
                    }
                case 123:
                    {
                        double П = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Z = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double Q = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double C = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double K = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        double M = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                        return calc.N1(П, Z, Q, C, K, M).ToString();
                    }
                case 130:
                    {
                        double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double H = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double P = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double K1 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        double K2 = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                        return calc.N2(S, H, П, P, K1, K2).ToString();
                    }
                case 131:
                    {
                        double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double П = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double P = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double K1 = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double K2 = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return calc.N3(S, П, P, K1, K2).ToString();
                    }
                case 138:
                    {
                        double S = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double P = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return calc.N4(S, P).ToString();
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
                        return calc.N5(S, П, Z, Q, C, K, M).ToString();
                    }
                case 145:
                    {
                        double Ап = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Анс = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        //int iterations = Convert.ToInt32(Iterations.Value);
                        double[] Ii = new double[iterations];
                        for (int i = 2; i < iterations; i++)
                        {
                            Ii[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return calc.Пз(Ап, Анс, Ii).ToString();
                    }
                case 146:
                    {
                       // int iterations = Convert.ToInt32(Iterations.Value);
                        double[] Q1 = new double[iterations];
                        double[] Q2 = new double[iterations];
                        for (int i = 0; i < iterations; i++)
                        {
                            Q1[i] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                            Q2[i] = Convert.ToDouble(formulasDGV.Rows[i + 1].Cells[1].Value);
                        }
                        return calc.Рз(iterations, Q1, Q2).ToString();
                    }
                case 153:
                    {
                        double Ut = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Ct = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double Zt = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.Qt(Ut, Ct, Zt).ToString();
                    }
                case 157:
                    {
                        double ЧГПо = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double ЧГПі = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double ЧГПф = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return calc.ЧГПп(ЧГПо, ЧГПі, ЧГПф).ToString();
                    }
                case 161:
                    {
                        double B0 = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double Ef = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        int Tt = Convert.ToInt32(formulasDGV.Rows[2].Cells[1].Value);
                        int Tr = Convert.ToInt32(formulasDGV.Rows[3].Cells[1].Value);
                        double vvp = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return calc.E(vvp, B0, Tt, Tr, Ef).ToString();
                    }

               /* case 172:
                    {///////                     
                        double result = 0; ///////НАЛОГИ ПЕРЕДЕЛАТЬ!!!!
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
                                if (Convert.ToDouble(element_gdk) >= 0.1)
                                {
                                    result = 74.17 * Convert.ToDouble(row.Cells[1].Value);
                                }
                                else if (Convert.ToDouble(element_gdk) < 0.1 && Convert.ToDouble(element_gdk) >= 0.01)
                                {
                                    result = 1968.65 * Convert.ToDouble(row.Cells[1].Value);
                                }
                                else if (Convert.ToDouble(element_gdk) < 0.01 && Convert.ToDouble(element_gdk) >= 0.001)
                                {
                                    result = 7015.25 * Convert.ToDouble(row.Cells[1].Value);
                                }
                                else if (Convert.ToDouble(element_gdk) < 0.001 && Convert.ToDouble(element_gdk) >= 0.0001)
                                {
                                    result = 50783.62 * Convert.ToDouble(row.Cells[1].Value);
                                }
                                else if (Convert.ToDouble(element_gdk) < 0.0001)
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
                    }*/
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
            return " ";
            #endregion formulas_ekonomist
        }
        string formulas_ecolog(int idf, DataGridView formulasDGV, int cur_row, int iterations)
        {
            #region formulas_ecolog
            switch (idf)
            {//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом            {
                case 186:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);

                        return ecoCalc.Рш( money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.PERELOGI).ToString();
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
                        double mass = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double density = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.Озр(mass, density).ToString();
                    }
                case 178:
                    {
                        double concentration = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double groundThickness = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double index = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return ecoCalc.Кз_2(concentration, groundThickness, index).ToString();
                    }
                case 181:
                    {
                        double max = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        int length = Convert.ToInt32(iterations);
                        double[] arr = { 0 };
                        Array.Resize(ref arr, length);
                        for (int i = 1; i < length; i++)
                        {
                            arr[i - 1] = Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return ecoCalc.Рш_заг(max, arr).ToString();
                    }
                case 50:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double wasteDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);

                        return ecoCalc.Ршз(
                                EcologCalculator.RivenZasmichenia.NIZKUYI,
                                money,
                                polutionArea,
                                polutionSubstanceDangerCoefficient,
                                wasteDangerCoefficient,
                                ecoAndEconomValue
                            ).ToString();
                    }
                //case 51: { break; }
                case 52:
                    {
                        double avgValueOfMassConcentration = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double valueOfAllowableWaste = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double gasDustFlowCost = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);

                        return ecoCalc.Formula7(
                                avgValueOfMassConcentration,
                                valueOfAllowableWaste,
                                gasDustFlowCost
                            ).ToString();
                    }
                case 203:
                    {
                        double population = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double cityMeaning = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.Кт(population, cityMeaning).ToString();
                    }
                case 199:
                    {
                        double polutionSubstanceMass = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double relativeDangerOfPolutionSubstance = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double atmospherePolutionLevelCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double minSalary = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.З(
                               polutionSubstanceMass,
                               minSalary,
                               relativeDangerOfPolutionSubstance,
                               ecoAndEconomValue,
                               atmospherePolutionLevelCoefficient
                            ).ToString();
                    }
                case 55:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double wasteDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Ршз(
                                EcologCalculator.RivenZasmichenia.VELUKYUI,
                                money,
                                polutionArea,
                                polutionSubstanceDangerCoefficient,
                                wasteDangerCoefficient,
                                ecoAndEconomValue
                            ).ToString();
                    }
                case 56:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                                money,
                                polutionArea,
                                groundPolutionCoefficient,
                                polutionSubstanceDangerCoefficient,
                                ecoAndEconomValue,
                                EcologCalculator.PonuzuvalnuyKoef.LISOSMUGI_NASADJENYA
                            ).ToString();
                    }
                case 57:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                            money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.LISOVI_ZEMLI).ToString();
                    }
                case 58:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                            money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.CHAGARNYKI).ToString();
                    }
                case 59:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                            money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.ZABUDOVANI_ZEMLI).ToString();
                    }
                case 60:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                            money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.ZABOLOXHENI_ZEMLI).ToString();
                    }
                case 61:
                    {
                        double money = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double polutionArea = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double groundPolutionCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double polutionSubstanceDangerCoefficient = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double ecoAndEconomValue = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        return ecoCalc.Рш(
                            money,
                            polutionArea,
                            groundPolutionCoefficient,
                            polutionSubstanceDangerCoefficient,
                            ecoAndEconomValue,
                            EcologCalculator.PonuzuvalnuyKoef.VIDKRYTI_ZEMLI).ToString();
                    }
                case 62:
                    {
                        double atmosphereTemperatureCoefficient = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double massOfHarmfulSubstance = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double speedOfSubstanceSedimentationCoefficient = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double firstGasPollutionCondition = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        double secondGasPollutionCondition = Convert.ToDouble(formulasDGV.Rows[4].Cells[1].Value);
                        double terrainTopographyCoefficient = Convert.ToDouble(formulasDGV.Rows[5].Cells[1].Value);
                        double sourceHeight = Convert.ToDouble(formulasDGV.Rows[6].Cells[1].Value);
                        double fixedCostsOfGasEmission = Convert.ToDouble(formulasDGV.Rows[7].Cells[1].Value);
                        double temperatureDifference = Convert.ToDouble(formulasDGV.Rows[8].Cells[1].Value);
                        return ecoCalc.Formula20(
                            atmosphereTemperatureCoefficient,
                            massOfHarmfulSubstance,
                            speedOfSubstanceSedimentationCoefficient,
                            firstGasPollutionCondition,
                            secondGasPollutionCondition,
                            terrainTopographyCoefficient,
                            sourceHeight,
                            fixedCostsOfGasEmission,
                            temperatureDifference
                            ).ToString();
                    }
                case 63:
                    {
                        double fuelAmount = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double amountOfFuelWaste = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.Formula15(
                            fuelAmount,
                            amountOfFuelWaste
                            ).ToString();
                    }
                case 64:
                    {
                        double burnedMassOfTPV = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double specificEmission = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.Formula21(
                            burnedMassOfTPV,
                            specificEmission
                            ).ToString();
                    }
                case 65:
                    {
                        double maxPermisibbleValue = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double maxSewageWaste = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.ГДС(
                            maxPermisibbleValue,
                            maxSewageWaste
                            ).ToString();
                    }
                case 66:
                    {
                        double flowCoefficient = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double areaOfGround = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double precipitations = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return ecoCalc.Formula17(
                            flowCoefficient,
                            areaOfGround,
                            precipitations
                            ).ToString();
                    }
                case 67:
                    {
                        double waterConsumption = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double wateringAmount = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double areaOfGround = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return ecoCalc.Formula18(
                            waterConsumption,
                            wateringAmount,
                            areaOfGround
                            ).ToString();
                    }
                case 261://213
                    {
                        // List<double> concentrations = new List<double>();
                        // List<double> maxPermisibbleValues = new List<double>();
                        int length = Convert.ToInt32(iterations);

                        double[] concentrations = new double[length];
                        double[] maxPermisibbleValue = new double[length];

                        for (int i = 0; i <= length - 1; i++)
                        {
                            maxPermisibbleValue[i] = Convert.ToDouble(formulasDGV.Rows[i*2].Cells[1].Value);
                            concentrations[i] = Convert.ToDouble(formulasDGV.Rows[2*i + 1].Cells[1].Value);

                            //maxPermisibbleValues.Add(Convert.ToDouble(this.formulasDGV.Rows[i].Cells[1].Value));
                            //concentrations.Add(Convert.ToDouble(this.formulasDGV.Rows[i+1].Cells[1].Value));
                        }

                        string waterQuality = "";
                       /*return ecoCalc.ІЗВ(
                           //concentrations.ToArray(),
                           // maxPermisibbleValues.ToArray(),
                           concentrations,
                           maxPermisibbleValue,
                            length,
                            ref waterQuality
                            ).ToString();*/
                        double result = ecoCalc.ІЗВ(
                            //concentrations.ToArray(),
                            // maxPermisibbleValues.ToArray(),
                            concentrations,
                            maxPermisibbleValue,
                             length,
                             ref waterQuality);
                        switch (result)
                        {
                            case object res when (Convert.ToDouble(res) < 0.2):
                                {
                                    waterQuality = "I";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) >= 0.2) && (Convert.ToDouble(res) <= 1.0)):
                                {
                                    waterQuality = "II";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) > 1.0) && (Convert.ToDouble(res) <= 2.0)):
                                {
                                    waterQuality = "III";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) > 2.0) && (Convert.ToDouble(res) <= 4.0)):
                                {
                                    waterQuality = "IV";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) > 4.0) && (Convert.ToDouble(res) <= 6.0)):
                                {
                                    waterQuality = "V";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) > 6.0) && (Convert.ToDouble(res) <= 10.0)):
                                {
                                    waterQuality = "VI";
                                    break;
                                }
                            case object res when ((Convert.ToDouble(res) > 10.0)):
                                {
                                    waterQuality = "VII";
                                    break;
                                }
                            default:
                                break;
                        }

                        formulasDGV.Rows.Add("waterQuality", waterQuality, "");
                        return result.ToString();

                    }
               /* case 171:
                    {//NORMIROVANIE
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
                    }*/
                case 191:
                    {
                        double avrg = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double norm = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double objem = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return ecoCalc.Formula7(
                            avrg,
                            norm,
                            objem
                            ).ToString();
                    }
                // case 195: { break; }
                /*case 172:
                    {
                        double Taxation = Convert.ToDouble(this.formulasDGV.Rows[0].Cells[1].Value);
                        double Pollution = Convert.ToDouble(this.formulasDGV.Rows[1].Cells[1].Value);

                        this.formulasDGV.Rows.Add("Result", ecoCalc.Под(Taxation, Pollution),"");
                        break;
                    }*/
                case 248: //розрахунок маси забруднюючої речовини у водний об'єкт внаслідок перевищенняя ГДС
                    {
                        double cif = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double cid = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);
                        double qif = Convert.ToInt32(formulasDGV.Rows[2].Cells[1].Value);
                        double t = Convert.ToInt32(formulasDGV.Rows[3].Cells[1].Value);
                        return ecoCalc.mi1(cif, cid, qif, t).ToString();
                    }
                case 253: //розрахунок маси забруднюючої речовини у водний об'єкт на основі форми 2-ТП(водгосп)
                    {
                        double mif = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double mil = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.mi2(mif, mil).ToString();
                    }
                case 256: //розрахунок маси наднормативного скиду забруднюючої речовини у водний об'єкт, що підлягають нормуванню 
                    {
                        double cif = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double cik = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);
                        double qif = Convert.ToInt32(formulasDGV.Rows[2].Cells[1].Value);
                        double t = Convert.ToInt32(formulasDGV.Rows[3].Cells[1].Value);
                        return ecoCalc.mi3(cif, cik, qif, t).ToString();
                    }
                case 258: //розрахунок маси наднормативного скиду забруднюючої речовини у водний об'єкт, що не підлягають нормуванню
                    {
                        double cif = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double qif = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);
                        double t = Convert.ToInt32(formulasDGV.Rows[2].Cells[1].Value);
                        return ecoCalc.mi4(cif, qif, t).ToString();
                    }
               /* case 249: //середня фактична концентрація забруднюючої речовини 
                    {
                        double cin[1] = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        int n = Convert.ToInt32(formulasDGV.Rows[1].Cells[1].Value);
                        return ecoCalc.cif(cin[], n).ToString();
                    }*/
            } return " ";
            #endregion
        }
        string formulas_medic(int idf, DataGridView formulasDGV, int cur_row, int iterations)
        {
            #region formulas_medic
            switch (idf)//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом
            {
                case 1:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return medCalc.getOSTG(a, b).ToString();
                    }
                case 167:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        return medCalc.getOSTG_2(a, b, c).ToString();
                    }
                case 20:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        return medCalc.getPM_GIM(a, b, c, d).ToString();
                    }
                case 29:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return medCalc.getPM_MI(a, b).ToString();
                    }
                case 25:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return medCalc.getPM_HCVP(a, b).ToString();
                    }
                case 169:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        return medCalc.getBVForMen(a, b, c, d).ToString();
                    }
                case 7:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        return medCalc.getBVForWomen(a, b, c, d).ToString();
                    }
                case 18:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        return medCalc.getNBVForMen(a).ToString();
                    }
                case 170:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        return medCalc.getNBVForWomen(a).ToString();
                    }
                case 13:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        double c = Convert.ToDouble(formulasDGV.Rows[2].Cells[1].Value);
                        double d = Convert.ToDouble(formulasDGV.Rows[3].Cells[1].Value);
                        return medCalc.getFV_SSSForMen(a, b, c, d).ToString();
                    }
                case 28:
                    {
                        double a = Convert.ToDouble(formulasDGV.Rows[0].Cells[1].Value);
                        double b = Convert.ToDouble(formulasDGV.Rows[1].Cells[1].Value);
                        return medCalc.getFV_SSSForWomen(a, b).ToString();
                    }
            }return " ";
            #endregion formulas_medic
        }
        string formulas_enegro(int id_of_exp, int idf, DataGridView formulasDGV, int cur_row, int iterations)
        {
            #region formulas_energo
            switch (idf)//свитч для подсчета формул, общий вид - несколько параметров беруться из ячеек таблицы и потом передаются в функцию подсчета класс Calculation, потом добавляем в таблицу строку с результатом
            {
                //підрахунок річного значення із введенням значень за кожен місяць
                case 175://ОСЕрік - обсяги споживання електроенергії за рік, вводячи цифри за 12 місяців
                    {
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return enegroCalc.sum_months(values_month).ToString();
                    }
                case 177://ОВЕрік - обсяги виробництва електроенергії за рік, вводячи цифри за 12 місяців
                    {
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return enegroCalc.sum_months(values_month).ToString();
                    }
                case 179://ОВТЕНПрік - обсяги виробництва теплової енергії на продаж за рік, вводячи цифри за 12 місяців
                    {
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return enegroCalc.sum_months(values_month).ToString();
                    }
                case 182://ОСГрік - обсяги споживання газу за рік, вводячи цифри за 12 місяців
                    {
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return enegroCalc.sum_months(values_month).ToString();
                    }
                case 184://ОСВрік - обсяги споживання води за рік, вводячи цифри за 12 місяців
                    {
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        return enegroCalc.sum_months(values_month).ToString();
                    }

                //підрахунок середньодобового річного значення із введенням значень за кожен місяць
                case 215://ОСЕср - обсяги споживання електроенергії річне середньодобове значення
                    {
                        int year, work_type;
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        year = Convert.ToInt32(formulasDGV.Rows[12].Cells[1].Value);
                        work_type = Convert.ToInt32(formulasDGV.Rows[13].Cells[1].Value);
                        return enegroCalc.costs_mid_year(values_month, year, work_type).ToString();
                    }
                case 216://ОВТЕНПср - обсяги виробництва теплової енергії річне середньодобове значення
                    {
                        int year, work_type;
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        year = Convert.ToInt32(formulasDGV.Rows[12].Cells[1].Value);
                        work_type = Convert.ToInt32(formulasDGV.Rows[13].Cells[1].Value);
                        return enegroCalc.costs_mid_year(values_month, year, work_type).ToString();
                    }
                case 217://ОСГср - обсяги споживання газу річне середньодобове значення
                    {
                        int year, work_type;
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        year = Convert.ToInt32(formulasDGV.Rows[12].Cells[1].Value);
                        work_type = Convert.ToInt32(formulasDGV.Rows[13].Cells[1].Value);
                        return enegroCalc.costs_mid_year(values_month, year, work_type).ToString();
                    }
                case 218://ОСВср - обсяги споживання води річне середньодобове значення
                    {
                        int year, work_type;
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        year = Convert.ToInt32(formulasDGV.Rows[12].Cells[1].Value);
                        work_type = Convert.ToInt32(formulasDGV.Rows[13].Cells[1].Value);
                        return enegroCalc.costs_mid_year(values_month, year, work_type).ToString();
                    }
                case 222://ОВЕср - обсяги виробництва електроенергії річне середньодобове значення
                    {
                        int year, work_type;
                        float[] values_month = new float[12];
                        for (int i = 0; i < 12; i++)
                        {
                            values_month[i] = (float)Convert.ToDouble(formulasDGV.Rows[i].Cells[1].Value);
                        }
                        year = Convert.ToInt32(formulasDGV.Rows[12].Cells[1].Value);
                        work_type = Convert.ToInt32(formulasDGV.Rows[13].Cells[1].Value);
                        float[] mid_res = enegroCalc.mid_months(values_month, year, work_type);
                        return enegroCalc.costs_mid_year(values_month, year, work_type).ToString();
                    }
            }
            return " ";
            #endregion
        }
    }
}
