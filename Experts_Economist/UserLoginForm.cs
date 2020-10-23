using Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using experts_jurist;

namespace Experts_Economist
{
    public partial class UserLogin : Form
       
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        public int userId = 0;
        private DBManager db = new DBManager();

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string login = "'" + loginTB.Text + "'";
            string pass = "'" + passTB.Text + "'";

            List<List<Object>> user = new List<List<Object>>();
            try
            {
                user = db.GetRows("user", "*", "user_name=" + login +
                    " AND password=" + pass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Помилка, перевірте правильність вводу");
                return;
            }

            if(user.Count > 0 && user[0][0].ToString() == "4")
            {
                mainWin gol = new mainWin(Int32.Parse(user[0][3].ToString()));
                gol.ShowDialog();
                gol = null;
            }
            else if (user.Count > 0 && user[0][0].ToString() != "4")
            {
                try
                {
                    Hide();
                    Golovna gol = new Golovna(Int32.Parse(user[0][3].ToString()));
                    gol.ShowDialog();
                    gol = null;
                    Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Помилка, перевірте правильність вводу");
                DialogResult = DialogResult.None;
            }
        }

        private void UserLogin_Leave(object sender, EventArgs e)
        {
            loginTB.Text = "";
            passTB.Text = "";
        }

        private void UserLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }

        private void passTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }

        private void UserLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}