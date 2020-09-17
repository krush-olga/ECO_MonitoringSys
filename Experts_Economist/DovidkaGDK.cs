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
using System.Text.RegularExpressions;

namespace Experts_Economist
{
    public partial class DovidkaGDK : Form
    {
        private DBManager db = new DBManager();
        private List<RadioButton> radioButtons;
        private RadioButton oldValue = new RadioButton();
        private delegate void GDKAction();
        private delegate bool TextBoxErrorCondition(TextBox tb);
        GDKAction lambda;             //лямбда-функція, яка буде генеруватися для кожного RadioButton і виконеується при натисканні кнопки

        TextBox nameTB;
        ComboBox categoryCB;
        TextBox GDK_TB;
        TextBox measureTB;
        TextBox minTB;
        TextBox maxTB;

        ~DovidkaGDK()
        {
            nameTB.Dispose();
            nameTB = null;
            categoryCB.Dispose();
            categoryCB = null;
            GDK_TB.Dispose();
            GDK_TB = null;
            measureTB.Dispose();
            measureTB = null;
            minTB.Dispose();
            minTB = null;
            maxTB.Dispose();
            maxTB = null;

            db.Disconnect();
            db = null;
        }

        //____Спеціальні функції_____

        //Створення спеціального Label з автоматичним розміром.
        private Label createAutoSizedLabel(string txt)
        {
            Label label = new Label();
            label.Text = txt;
            label.AutoSize = true;

            return label;
        }

        //Створення спеціального поля з шириною 114 та вказаним ім'ям.
        private TextBox createTextBox(string txt)
        {
            TextBox text = new TextBox();
            text.Name = txt;
            text.Width = 114;

            return text;
        }

        //Завантаження даних з SQL-таблиці experts.elements у таблицю gdkDataView.
        private void loadDataToTable()
        {
            try
            {
                gdkDataGrid.Rows.Clear();
                var obj3 = db.GetRows("elements", "*", "");
                foreach (var row in obj3)
                {
                    gdkDataGrid.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка при завантажені даних у таблицю!", "Увага!");
            }
        }

        //Функція для зміни положення(Alignment) вмісту колонок.
        //УВАГА!: перший параметр порібно вказувати у вигляді звичайної строки,
        //елементи яких розділені комами.
        private void changeTableColumnsAlignment(string columnNames, DataGridViewContentAlignment alignment)
        {
            List<string> str = columnNames.Split(',').ToList<string>();
            List<DataGridViewColumn> columnsToAlign = new List<DataGridViewColumn>();

            str.ForEach(
                (columnName) =>
                {
                    columnsToAlign.Add(gdkDataGrid.Columns[columnName.Trim()]);
                }
            );

            columnsToAlign.ForEach(
                    (column) =>
                    {
                        try
                        {
                            column.DefaultCellStyle.Alignment = alignment;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Неможливо знайти вказану колонку!", "Увага!");
                        }
                    }
                );
        }

        private bool checkTextBoxesForAnErrorWhere(IEnumerable<TextBox> textBoxes,TextBoxErrorCondition doesHaveAnError)
        {
            return textBoxes.Any(textBox => doesHaveAnError(textBox));
        }

        private string formAnErrorStringWhere(IEnumerable<TextBox> textBoxes, TextBoxErrorCondition doesHaveAnError)
        {

            string errorStr = String.Empty;
            var errorInTextBoxes = textBoxes.Where(textBox => doesHaveAnError(textBox));
            foreach(var textBox in errorInTextBoxes)
            {
                errorStr += $"{textBox.Name}, ";
            }
            return Regex.Replace(errorStr, @", $", "");
        }

        private bool checkTextBoxesForAnError()
        {
            TextBoxErrorCondition textBoxesIsEmpty = textBox => textBox.Text.Trim() == "";
            TextBoxErrorCondition textBoxesIsNotInt = textBox => !double.TryParse(textBox.Text.Trim(), out double result);
            var textBoxes = flowLayoutPanel1.Controls.OfType<TextBox>();
            var numericTextBoxes = textBoxes.Where(textBox => { return textBox.Name == "GDK" || textBox.Name == "Min" || textBox.Name == "Max"; });
            string errorStr = String.Empty;

            numericTextBoxes.ToList().ForEach((textBox) => textBox.Text = textBox.Text.Trim().Replace(',', '.'));

            if (checkTextBoxesForAnErrorWhere(textBoxes, textBoxesIsEmpty))                    //Якщо хоча б один TextBox пустий, то формуємо строку з іменем пустих полів 
            {
                errorStr = $"Декілька або одне поле має невірний формат введених даних\nПоля: {formAnErrorStringWhere(textBoxes,textBoxesIsEmpty)}";
            }
            else if(checkTextBoxesForAnErrorWhere(numericTextBoxes, textBoxesIsNotInt))
            {
                errorStr = $"Декілька або одне поле має невірний формат введених даних\nПоля: {formAnErrorStringWhere(numericTextBoxes, textBoxesIsNotInt)}";
            }
            else if (categoryCB.SelectedIndex == -1 || categoryCB.Text.Trim() == "")
            {
                errorStr = "Оберіть значення категорії!";
            }
            else { return false; }
            MessageBox.Show(errorStr, "УВАГА!");
            return true;
        }

        //Функція для відстежування натискань на RadioButtons на панелі "Режим роботи".
        //В залежності від обраного RadioButton додаються спеціальний набір елементів
        //для взаємодії з таблицейю.
        private void handleRadioButtonChange(object sender, EventArgs e)
        {
            RadioButton selected = (RadioButton)sender;
            List<Control> controls = new List<Control>();
            lambda = () => { return; };

            if (oldValue.Equals(selected)) return;    //при зміні комбобоксу код може виконатися 2 рази (так як дія зміни виконується 2 рази "стареОбране->нове обране" замість потрібного "нове обране")
            else if (oldValue == searchRadio) loadDataToTable();       //кнопка пошуку своїми діями змінює таблицю і тому є необхідність перезаповнити її 

            nameTB = createTextBox("Name");
            categoryCB = new ComboBox();
            categoryCB.Name = "Category";
            categoryCB.Width = 114;

            var rows = db.GetRows("elements", "distinct category", "");

            foreach (var row in rows)
            {
                row.ForEach(record => categoryCB.Items.Add(record));
            }

            GDK_TB = createTextBox("GDK");
            measureTB = createTextBox("Measure");
            minTB = createTextBox("Min");
            maxTB = createTextBox("Max");

            flowLayoutPanel1.Controls.Clear();
            switch (selected.Name)
            {
                case "searchRadio":
                    {
                        doneBtn.Text = "Виконати";
                        var categorys = db.GetRows("elements", "distinct category", "");     //отримуємо унікальні категорії з БД
                        var names = db.GetRows("elements","name","");
                        ComboBox categoryFilter = new ComboBox();
                        ComboBox nameFilter = new ComboBox();

                        foreach (var subList in categorys)                                      //заповнюємо сам комбобокс значеннями поля "category" з таблиці "elements"
                        {
                            subList.ForEach(record => categoryFilter.Items.Add(record));
                        }
                        foreach (var subList in names)                                      //заповнюємо сам комбобокс значеннями поля "name" з таблиці "elements"
                        {
                            subList.ForEach(record => nameFilter.Items.Add(record));
                        }

                        //додаємо надписи та комбобокси
                        controls = new List<Control> {
                            createAutoSizedLabel("Фільтрація за ім'ям"), nameFilter,
                            createAutoSizedLabel("Фільтрація за категорією: "), categoryFilter
                        };

                        lambda = () =>
                        {
                            gdkDataGrid.Rows.Clear();
                            List<List<object>> records = new List<List<object>>();

                            string categoryText = categoryFilter.Text.Trim();
                            string nameText = nameFilter.Text.Trim();

                            if (categoryText != "" && nameText != "")
                            {
                                records = db.GetRows("elements", "*", $"category = '{categoryText}' AND  name = '{nameText}'");
                            }
                            else if (categoryText != "" && nameText == "")
                            {
                                records = db.GetRows("elements", "*", $"category ='{categoryText }'");
                            }
                            else if (categoryText == "" && nameText != "")
                            {
                                records = db.GetRows("elements", "*", $"name ='{ nameText }'");
                            }
                            else
                            {
                                loadDataToTable();
                                return;
                            }

                            foreach (var record in records)
                            {
                                gdkDataGrid.Rows.Add(record[0], record[1], record[2], record[3], record[4], record[5], record[6]);
                            }

                        };

                        break;
                    }
                case "addRadio":
                    {
                        doneBtn.Text = "Додати";

                        controls = new List<Control> {
                            createAutoSizedLabel("Name:"), nameTB, createAutoSizedLabel("Category:"),
                            categoryCB, createAutoSizedLabel("GDK:"), GDK_TB, createAutoSizedLabel("Measure:"),
                            measureTB,createAutoSizedLabel("Min:"), minTB, createAutoSizedLabel("Max:"), maxTB
                        };

                        lambda = () => {
                           
                            string[] fieldNames = {"name", "category", "gdk", "measure", "minVal", "maxVal" };
                            string[] fieldValues = {
                               "'"+ nameTB.Text.Trim()+"'", "'"+categoryCB.Text.Trim()+"'", GDK_TB.Text.Trim().Replace(',','.'),
                                "'"+measureTB.Text.Trim()+"'", minTB.Text.Trim().Replace(',','.'), maxTB.Text.Trim().Replace(',','.')
                            };

                            try
                            {
                                db.InsertToBDWithoutId("elements", fieldNames, fieldValues);
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show($"Неможливо створити запис\n{exception.Message}", "Помилка!");
                            }
                        };
                        break;
                    }
                case "editRadio":
                    {
                        doneBtn.Text = "Редагувати";

                        controls = new List<Control> {
                            createAutoSizedLabel("Name:"), nameTB, createAutoSizedLabel("Category:"),
                            categoryCB, createAutoSizedLabel("GDK:"), GDK_TB, createAutoSizedLabel("Measure:"),
                            measureTB,createAutoSizedLabel("Min:"), minTB, createAutoSizedLabel("Max:"), maxTB
                        };

                        lambda = () =>
                        {
                            try
                            {
                                int rowIndex = gdkDataGrid.CurrentCell.RowIndex;
                                var record_ID = db.GetValue("elements", "id", $"name = '{gdkDataGrid.Rows[rowIndex].Cells[1].Value.ToString()}'");

                                //MessageBox.Show(record_ID.ToString());
                                string[] colNames = { "id", "name", "category", "gdk", "measure", "minVal", "maxVal" };
                                string[] colValues = { record_ID.ToString(), $"'{nameTB.Text.Trim()}'", $"'{categoryCB.Text.Trim()}'", GDK_TB.Text.Trim(), $"'{measureTB.Text.Trim() }'", minTB.Text.Trim(), maxTB.Text.Trim() };
                                db.UpdateRecord("elements", colNames, colValues);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show($"Помилка оновлення даних!\n{ex.Message}","Помилка");
                            }
                        };
                        break;
                    }
                case "deleteRadio":
                    {
                        doneBtn.Text = "Видалити";

                        //controls = new List<Control> {
                        //    createAutoSizedLabel("Name:"), nameTB, createAutoSizedLabel("Category:"),
                        //    categoryCB, createAutoSizedLabel("GDK:"), GDK_TB, createAutoSizedLabel("Measure:"),
                        //    measureTB,createAutoSizedLabel("Min:"), minTB, createAutoSizedLabel("Max:"), maxTB
                        //};

                        lambda = () =>
                        {
                            if (MessageBox.Show("Видалити рядок?","Видалення",MessageBoxButtons.OKCancel)==DialogResult.Cancel)
                            {
                                return;
                            }

                            try {
                                int rowIndex = gdkDataGrid.CurrentCell.RowIndex;
                                var record_ID = db.GetValue("elements", "id", $"name = '{gdkDataGrid.Rows[rowIndex].Cells[1].Value.ToString()}'");

                                db.DeleteFromDB("elements", "id", record_ID.ToString());
                            } catch(Exception exception)
                            {
                                MessageBox.Show($"Не вдалося видалити рядок\n{exception.Message}","Помилка!");
                            }
                        };

                        break;
                    }
                default:
                    break;
            }
            flowLayoutPanel1.Controls.AddRange(controls.ToArray());
            controls = null;
            oldValue = selected;
        }



        //_____Сгенеровані функції______

        public DovidkaGDK()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DovidkaGDK_Load(object sender, EventArgs e)
        {
            loadDataToTable();
            changeTableColumnsAlignment("Category, GDK, Measure", DataGridViewContentAlignment.MiddleCenter);
            radioButtons = new List<RadioButton> { searchRadio, addRadio, editRadio, deleteRadio };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchRadio_CheckedChanged(object sender, EventArgs e)
        {
           //RadioButton selected = radioButtons.Single((elem)=> elem.Checked);
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if (lambda != null)
            {
                if (searchRadio.Checked && deleteRadio.Checked)
                {
                    checkTextBoxesForAnError();
                }
                else if (searchRadio.Checked)
                {
                    lambda();
                }
                else
                {
                    lambda();
                    loadDataToTable();
                }
            }
            else
                MessageBox.Show("Оберіть режим роботи!", "Увага!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gdkDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (editRadio.Checked)
            {
                try
                {
                    int index = gdkDataGrid.CurrentCell.RowIndex;
                    var cells = gdkDataGrid.Rows[index].Cells;

                   // MessageBox.Show(cells.Count.ToString());

                    nameTB.Text = cells[1].Value.ToString();
                    categoryCB.Text = cells[2].Value.ToString();
                    GDK_TB.Text = cells[3].Value.ToString();
                    measureTB.Text = cells[4].Value.ToString();
                    minTB.Text = cells[5].Value.ToString();
                    maxTB.Text = cells[6].Value.ToString();

                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show($"Не вдалося завантажити дані!\n{ex.Message} ","Увага!");
                }
            }
        }
    }

}
