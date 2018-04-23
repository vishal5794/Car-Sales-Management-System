using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Sales_Management_System
{
    public partial class ChangePassword : Form
    {

        SqlConnection scon;
        SqlCommand cmd1, cmd2;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd1 = scon.CreateCommand();
            cmd2 = scon.CreateCommand();
            scon.Open();
        }


        public ChangePassword()
        {
            InitializeComponent();
            label2.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new Customerpage().Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String oldPwd = "";
            conn();

            string getPwd = "select Password from Employee where Username=@un";
            cmd1.CommandText = getPwd;
            cmd1.Parameters.AddWithValue("@un", LoginPage.Username);

            SqlDataReader rd = cmd1.ExecuteReader();

            while (rd.Read())
            {
                oldPwd = rd[0].ToString();
            }

            if (oldPwd.Equals(textBox1.Text))
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    string s = "Update Employee SET Password=@newpwd where Username=@uname";

                    cmd2.CommandText = s;

                    cmd2.Parameters.AddWithValue("@uname", LoginPage.Username);
                    cmd2.Parameters.AddWithValue("@newpwd", textBox3.Text);

                    cmd2.ExecuteNonQuery();
                    scon.Close();

                    new ChangePasswordDone().Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Please confirm the new password!");
                }
            }
            else
            {
                MessageBox.Show("Please enter old password correctly!");
            }
        }
    }
}
