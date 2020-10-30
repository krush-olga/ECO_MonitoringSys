using Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UserLoginForm
{
    public partial class User_editor : Form
    {
        private DBManager db = new DBManager();
        private string userName;
        private bool isAddingMode;
        private bool isEditingMode;

        public User_editor()
        {
            InitializeComponent();
            RefreshDVG();

            isAddingMode = false;
        }

        private void RefreshDVG()
        {
            UsersDGV.Rows.Clear();
            UsersDGV.Columns.Clear();

            db.Connect();
            DataGridViewTextBoxColumn userName = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn password = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn expertType = new DataGridViewTextBoxColumn();

            userName.Name = "Логін";
            password.Name = "Пароль";
            expertType.Name = "Тип експерта";

            UsersDGV.Columns.AddRange(new DataGridViewColumn[] { userName, password, expertType });

            var users = new List<List<Object>>();
            users = db.GetRows("user", "user_name,password,id_of_expert", "");

            int usersLength = users.Count;
            //int propCount = users[0].Count;

            for (int i = 0; i < usersLength; i++)
            {
                this.UsersDGV.Rows.Add(users[i][0].ToString(), users[i][1].ToString(), users[i][2].ToString());
            }

            db.Disconnect();
        }

        private void OnAddingMode()
        {
            UsernameTextBox.Text = "";
            PasswordTextBox.Text = "";
            ExperTypeTextBox.Text = "";

            UsernameTextBox.ReadOnly = false;
            PasswordTextBox.ReadOnly = false;
            ExperTypeTextBox.ReadOnly = false;

            button1.Enabled = false;

            isAddingMode = true;
            AddUserButton.Text = "Зберегти";
            UpdateUserButton.Text = "Відмінити";
        }
        private void OffAddingMode()
        {
            button1.Enabled = true;

            UsernameTextBox.ReadOnly = true;
            PasswordTextBox.ReadOnly = true;
            ExperTypeTextBox.ReadOnly = true;

            isAddingMode = false;
            AddUserButton.Text = "Додати";
            UpdateUserButton.Text = "Змінити";
        }

        private void OnEditingMode()
        {
            UsernameTextBox.ReadOnly = false;
            PasswordTextBox.ReadOnly = false;
            ExperTypeTextBox.ReadOnly = false;

            button1.Enabled = false;

            isEditingMode = true;
            AddUserButton.Text = "Зберегти";
            UpdateUserButton.Text = "Відмінити";
        }
        private void OffEditingMode()
        {
            button1.Enabled = true;

            UsernameTextBox.ReadOnly = true;
            PasswordTextBox.ReadOnly = true;
            ExperTypeTextBox.ReadOnly = true;

            isEditingMode = false;
            AddUserButton.Text = "Додати";
            UpdateUserButton.Text = "Змінити";
        }

        private bool UpdateUser()
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text) ||
                string.IsNullOrEmpty(ExperTypeTextBox.Text))
            {
                MessageBox.Show("Заповніть усі поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string userName = DBUtil.AddQuotes(this.UsernameTextBox.Text.Replace('\'', '`'));
            string userPassword = DBUtil.AddQuotes(this.PasswordTextBox.Text.Replace('\'', '`'));
            string userType = DBUtil.AddQuotes(this.ExperTypeTextBox.Text.Replace('\'', '`'));

            string[] updateCols = new string[] { "user_name", "user_name", "password", "id_of_expert" };
            string[] updateVals = new string[] { this.userName, DBUtil.AddQuotes(userName), DBUtil.AddQuotes(userPassword), DBUtil.AddQuotes(userType) };

            db.Connect();
            db.UpdateRecord("user", updateCols, updateVals);
            db.Disconnect();

            RefreshDVG();

            MessageBox.Show("Дані користувача успішно змінені.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private bool AddUser()
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text) ||
                string.IsNullOrEmpty(ExperTypeTextBox.Text))
            {
                MessageBox.Show("Заповніть усі поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            db.Connect();

            string userName = DBUtil.AddQuotes(this.UsernameTextBox.Text.Replace('\'', '`'));
            string userPassword = DBUtil.AddQuotes(this.PasswordTextBox.Text.Replace('\'', '`'));
            string userType = DBUtil.AddQuotes(this.ExperTypeTextBox.Text.Replace('\'', '`'));
            string id_of_user = Convert.ToString(Convert.ToInt32(db.GetValue("user", "max(id_of_user)", "")) + 1);

            string[] fields = new string[] { "user_name", "password", "id_of_expert", "id_of_user" };
            string[] values = new string[] { userName, userPassword, userType, id_of_user };

            db.InsertToBD("user", fields, values);

            db.Disconnect();

            RefreshDVG();

            MessageBox.Show("Користувач успішно додан.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        

        private void EditCurrentUser(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (isAddingMode)
            {
                OffAddingMode();
            }

            int currentRow = this.UsersDGV.CurrentCell.RowIndex;
            string userName = this.UsersDGV.Rows[currentRow].Cells[0].Value.ToString();
            string userPassword = this.UsersDGV.Rows[currentRow].Cells[1].Value.ToString();
            string userType = this.UsersDGV.Rows[currentRow].Cells[2].Value.ToString();

            this.userName = DBUtil.AddQuotes(userName);

            this.UsernameTextBox.Text = userName;
            this.PasswordTextBox.Text = userPassword;
            this.ExperTypeTextBox.Text = userType;
        }

        private void UpdateCurrentUser(object sender, EventArgs e)
        {
            if (isAddingMode)
            {
                OffAddingMode();
            }
            else if (!isEditingMode)
            {
                if (UsersDGV.CurrentRow.Index == -1 || string.IsNullOrEmpty(UsernameTextBox.Text) || 
                   string.IsNullOrEmpty(PasswordTextBox.Text) || string.IsNullOrEmpty(ExperTypeTextBox.Text))
                {
                    MessageBox.Show("Ви не вибрали користувача для зміни його даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OnEditingMode();
                }
            }
            else if (isEditingMode)
            {
                OffEditingMode();
            }
        }

        private void AddNewUser(object sender, EventArgs e)
        {
            if (!isAddingMode && !isEditingMode)
            {
                OnAddingMode();
            }
            else if (isEditingMode && UpdateUser())
            {
                OffEditingMode();
            }
            else if (isAddingMode && AddUser())
            {
                OffAddingMode();
            }
        }

        private void deleteCurrentUser(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text) || 
                string.IsNullOrEmpty(ExperTypeTextBox.Text))
            {
                MessageBox.Show("Ви не вибрали користувача для його видалення.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var promptRes = MessageBox.Show("Ви впевнені, що хочете видалити користувача?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (promptRes == DialogResult.No)
            {
                return;
            }

            string userName = DBUtil.AddQuotes(this.UsernameTextBox.Text.Replace('\'', '`'));
            string userPassword = DBUtil.AddQuotes(this.PasswordTextBox.Text.Replace('\'', '`'));
            string userType = DBUtil.AddQuotes(this.ExperTypeTextBox.Text.Replace('\'', '`'));

            db.Connect();

            db.DeleteFromDB("user", "user_name", userName);

            db.Disconnect();

            RefreshDVG();

            UsernameTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
            ExperTypeTextBox.Text = string.Empty;

            MessageBox.Show("Користувач успішно видален.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}