using Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using experts_jurist;

namespace Experts_Economist
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
            Initialize();
        }

        public int userId;
        private DBManager db;

        private void Initialize()
        {
            userId = 0;
        }
        private void InitializeDBManager()
        {
            try
            {
                db = new DBManager();
            }
            catch (Exception)
            {
                MessageBox.Show("Сталась помилка при підключенні до бази даних.",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void InputCheck(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Oem5)
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (db == null)
            {
                InitializeDBManager();
            }

            if (string.IsNullOrEmpty(loginTB.Text) || string.IsNullOrEmpty(passTB.Text))
            {
                MessageBox.Show("Поля з даними не можуть бути пустими.", "Помилка", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var user = db.GetRows("user", "*", $"user_name = '{loginTB.Text}' AND password = '{passTB.Text}'");

                if (user.Count == 0)
                {
                    MessageBox.Show("Помилка, перевірте правильність вводу", "Помилка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }

                if (user[0][0].ToString() == "4")
                {
                    mainWin gol = new mainWin(Int32.Parse(user[0][3].ToString()));
                    gol.ShowDialog();
                    gol = null;
                }
                else if (user[0][0].ToString() != "4")
                {
                    Hide();
                    Golovna gol = new Golovna(Int32.Parse(user[0][3].ToString()));
                    gol.ShowDialog();
                    gol = null;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"{ex.Message}\n\n----------------------\n\n{ex.StackTrace}",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else 
                MessageBox.Show("Сталась не передбачена помилка.", "Помилка", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
            finally
            {
                Show();
            }
        }

        private void UserLogin_Leave(object sender, EventArgs e)
        {
            loginTB.Text = "";
            passTB.Text = "";
        }

        private void UserLogin_KeyDown(object sender, KeyEventArgs e)
        {
            InputCheck(e);
        }
        private void passTB_KeyDown(object sender, KeyEventArgs e)
        {
            InputCheck(e);
        }

        private void UserLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void loginTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\\' || e.KeyChar == '/')
            {
                e.Handled = true;
            }
        }
    }
}