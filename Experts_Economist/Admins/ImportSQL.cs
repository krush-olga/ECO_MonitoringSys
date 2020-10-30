using System;
using System.Windows.Forms;
using Data;

namespace Experts_Economist
{
    public partial class ImportSQL : Form
    {
        private enum UserData
        {
            SERVER = 0,
            DATABASE,
            LOGIN,
            PASSWORD
        }

        private string LIB_PATH = Application.StartupPath.Substring(0, Application.StartupPath.IndexOf(@"Experts_Economist\")) + @"Experts_Economist\Resources\libs";

        private DBManager db = new DBManager();


        public ImportSQL()
        {
            InitializeComponent();
            importPathTB.Text = System.IO.Directory.GetCurrentDirectory();

            loginTB.Text = getUserData(UserData.LOGIN);
            passwordTB.Text = getUserData(UserData.PASSWORD);

            //select schema_name
            //from information_schema.schemata
            Dovidka.populateComboBox(ref schemaCB, db.connectionString, "information_schema.schemata", "schema_name");

        }

        private string getUserData(UserData dataType)
        {
            //Розбиваємо строку з'єднання на масив даних
            string[] userData = db.connectionString.Split(';');

            //Шукаємо початковий індекс для зчитування
            int startIndex = userData[(int)dataType].IndexOf('=') + 1;

            return userData[(int)dataType].Substring(startIndex);
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAnyFieldEmpty())
                    throw new ArgumentException();

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                startInfo.ErrorDialog = true;

                //Запускаємо командну строку та виконуємо команду для експорту
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $@"/c cd {LIB_PATH} && .\mysql -u{loginTB.Text} -p{passwordTB.Text} {schemaCB.Text} < {importPathTB.Text}";

                process.StartInfo = startInfo;
                process.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Деякі поля пусті!", "Увага!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private bool isAnyFieldEmpty()
        {
            return string.IsNullOrEmpty(importPathTB.Text)
                || string.IsNullOrEmpty(loginTB.Text)
                || string.IsNullOrEmpty(passwordTB.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                importPathTB.Text = openFileDialog.FileName;
                MessageBox.Show(openFileDialog.FileName);
            }
        }

        private void неПриховуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = true;
            приховатиToolStripMenuItem.Visible = true;
            неПриховуватиToolStripMenuItem.Visible = false;
        }

        private void приховатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = false;
            приховатиToolStripMenuItem.Visible = false;
            неПриховуватиToolStripMenuItem.Visible = true;
        }
    }
}
