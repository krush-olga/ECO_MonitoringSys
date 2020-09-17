using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Data;
using System.IO;

namespace Experts_Economist
{
    public partial class ConnectDB : Form
    {
        private enum UserData
        {
            SERVER = 0,
            DATABASE,
            LOGIN,
            PASSWORD
        }

        private DBManager db = new DBManager();

        public ConnectDB()
        {
            InitializeComponent();
            loginTB.Text = getUserData(UserData.LOGIN);
            passwordTB.Text = getUserData(UserData.PASSWORD);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (checkDB_Conn())
            {
                List<string> fileData = new List<string>();
                using (StreamReader reader = new StreamReader("init.ini"))
                {
                    while (!reader.EndOfStream)
                    {
                        fileData.Add(reader.ReadLine());
                    }

                }
                fileData.Insert(0, $"Server={serverTB.Text};Database={databaseTB.Text};Uid={loginTB.Text};Pwd={passwordTB.Text};");
                using (StreamWriter writer = new StreamWriter("init.ini"))
                {
                    foreach (string line in fileData)
                    {
                        writer.WriteLine(line);
                    }
                }
                MessageBox.Show("Успішно підключено","Повідомлення");
            }
            else
            {
                MessageBox.Show("Неможливо підключитися до бази даних\nПеревірте введені дані!","Помилка");
            }
        }


        private string getUserData(UserData dataType)
        {
            //Розбиваємо строку з'єднання на масив даних
            string[] userData = db.connectionString.Split(';');

            //Шукаємо початковий індекс для зчитування
            int startIndex = userData[(int)dataType].IndexOf('=') + 1;

            return userData[(int)dataType].Substring(startIndex);
        }

        public bool checkDB_Conn()
        {
            var conn_info = $"Server={serverTB.Text};Database={databaseTB.Text};Uid={loginTB.Text};Pwd={passwordTB.Text};";
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(conn_info);
                conn.Open();
                isConn = true;
            }
            catch (ArgumentException a_ex)
            {
                MessageBox.Show(a_ex.ToString());
                /*
                Console.WriteLine("Check the Connection String.");
                Console.WriteLine(a_ex.Message);
                Console.WriteLine(a_ex.ToString());
                */
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.ToString());
                /*string sqlErrorMessage = "Message: " + ex.Message + "\n" +
                "Source: " + ex.Source + "\n" +
                "Number: " + ex.Number;
                Console.WriteLine(sqlErrorMessage);
                */
                isConn = false;
                switch (ex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        MessageBox.Show($"Неможливо підключитися до заданого хоста. Перевірте сервер");
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        MessageBox.Show($"Неправильні дані. Перевірте назву бази даних, імя користувача та пароль");
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }
    }
}
