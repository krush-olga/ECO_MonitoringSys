using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Data;
using HelpModule;

namespace Experts_Economist
{
    public partial class ExportDB : Form
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

        public ExportDB()
        {
            InitializeComponent();

            exportPathTB.Text = System.IO.Directory.GetCurrentDirectory();

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

        
        private void button2_Click(object sender, EventArgs e)
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
                startInfo.Arguments = $@"/c cd {LIB_PATH} && .\mysqldump -u{loginTB.Text} -p{passwordTB.Text} {schemaCB.Text} > {exportPathTB.Text}\{schemaCB.Text}{DateTime.Now.ToString("dd-MM-yyyy")}.sql";

                process.StartInfo = startInfo;
                process.Start();
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Деякі поля пусті!", "Увага!");
            }
             catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Помилка!");
            }
        }

        private bool isAnyFieldEmpty()
        {
            return string.IsNullOrEmpty(exportPathTB.Text) 
                || string.IsNullOrEmpty(loginTB.Text)
                || string.IsNullOrEmpty(passwordTB.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                exportPathTB.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void userDataChB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void неПриховуватиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = false;
            неПриховуватиToolStripMenuItem1.Visible = false;
            неПриховуватиToolStripMenuItem.Visible = true;
        }

        private void неПриховуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar = true;
            неПриховуватиToolStripMenuItem1.Visible = true;
            неПриховуватиToolStripMenuItem.Visible = false;
        }

		private void startTutorial_Click(object sender, EventArgs e)
		{
			new InteractiveToolTipCreator().CreateTips(new List<InteractiveToolTipModel>
			{
				new InteractiveToolTipModel
				{
					Control = button1,
					Text = "Оберіть папку для експорту файлу"
				},
				new InteractiveToolTipModel
				{
					Control = schemaCB,
					Text = "Оберіть схему з випадаючого списку"
				},
				new InteractiveToolTipModel
				{
					Control = loginTB,
					Text = "Введіть логін"
				},
				new InteractiveToolTipModel
				{
					Control = passwordTB,
					Text = "Введіть пароль"
				},
				new InteractiveToolTipModel
				{
					Control = exportBtn,
					Text = "Натисніть на кнопку \"Експортувати\""
                }
			});
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
