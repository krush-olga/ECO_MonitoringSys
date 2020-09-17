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

namespace UserLoginForm
{
	public partial class UserLogin : Form
	{
		DBManager db = new DBManager();

		public UserLogin()
		{
			InitializeComponent();
		}

        public int userId;
		private void loginBtn_Click(object sender, EventArgs e)
		{
			db.Connect();

			string login = DBUtil.ValidateForSQL(loginTB.Text);
			string pass = DBUtil.ValidateForSQL(passTB.Text);
            
			var user = db.GetValue("user", "id_of_user", "user_name=" + login +
				" AND password=" + pass);
            var userExpertId = db.GetValue("user", "id_of_expert", "user_name=" + login +
                " AND password=" + pass);
            if (user != null){
                if((int)userExpertId == 4)
                {
                    userId = (Int32.Parse(user.ToString()));
                    DialogResult = DialogResult.OK;
                } else
                {
                    MessageBox.Show("You are not a jurist. Be jurist.");
                    DialogResult = DialogResult.None;
                }
            }
            else
            {
                MessageBox.Show("No user found");
            }
				

			db.Disconnect();
		}

        private void UserLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
