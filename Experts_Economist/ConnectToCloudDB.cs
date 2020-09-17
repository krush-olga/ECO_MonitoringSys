using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using oprForm;

namespace Experts_Economist
{
    public partial class ConnectToCloudDB : Form
    {
        //MySqlConnection conn_mysql;
        public ConnectToCloudDB()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //conn_mysql = new MySqlConnection("SERVER= "+textBox1.Text+";" + "DATABASE= "+textBox2.Text+";" + "UID= "+textBox3.Text+";" + "PASSWORD="+textBox4.Text+";" + "connection timeout = 180");
            //conn_mysql.Open();
            MySqlConnection conn_mysql = new MySqlConnection("SERVER=remotemysql.com;DATABASE= TuDREFK7z3;UID=TuDREFK7z3;PASSWORD=xHpEQcXUdZ;");
            try
            {
            conn_mysql.Open();
                MessageBox.Show("CONNECTED");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
