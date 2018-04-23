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
    public partial class LoginPage :Form
    {
        SqlConnection scon;
        SqlCommand cmd;

        public static String Username = "";

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd = scon.CreateCommand();
            scon.Open();
        }


        public LoginPage()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox6.PasswordChar = '*';

         
            pictureBox1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
            label10.BackColor = Color.Transparent;
            label11.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            //new WelcomeMessage().Show();
            this.ActiveControl = textBox1; // Auto Focus on username button
            WindowState = FormWindowState.Maximized;
        }

        private void login(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                errorProvider1.SetError(textBox1, "Please Enter Username");
            }
            else if(textBox2.Text=="") 
           {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Please Enter Password");
            }
            else
            {
                errorProvider1.Clear();
                conn();
                String s = "select Username,Password from Employee where Username= @uname AND  Password= @password";

                Username = textBox1.Text;
                cmd.CommandText = s;
                cmd.Parameters.AddWithValue("@uname", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read() == true)
                {
                    this.Hide();
                    new Customerpage().Show();
                }
                else
                {
                    UserInvalid ui = new UserInvalid();
                    ui.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                scon.Close();
              
            }
        }

        private void signup(object sender, EventArgs e)
        {
            if(textBox3.Text=="")
            {
                errorProvider1.SetError(textBox3, "Please Enter Firstname");
            }
            else if(textBox4.Text=="")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Please Enter Lastname");
            }
            else if(textBox5.Text=="")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Please Enter Username");
            }
            else if (textBox6.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox6, "Please Enter Password");
            }
            else if (comboBox1.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Please Select Your Position");
            }
            else if (textBox7.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox7, "Please Select Date");
            }
            else if (textBox8.Text == "")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox8, "Please Enter Commision Rate");
            }
            else
            {
                conn();
                string s = "insert into Employee(Username,Password,Firstname,Lastname,Position,Bdate,Commision) values(@uname,@password,@fname,@lname,@position,@bdate,@commision)";

                cmd.CommandText = s;

                cmd.Parameters.AddWithValue("@fname", textBox3.Text);
                cmd.Parameters.AddWithValue("@lname", textBox4.Text);
                cmd.Parameters.AddWithValue("@uname", textBox5.Text);
                cmd.Parameters.AddWithValue("@password", textBox6.Text);
                cmd.Parameters.AddWithValue("@position", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@bdate", textBox7.Text);
                cmd.Parameters.AddWithValue("@commision", textBox8.Text);

                cmd.ExecuteNonQuery();
                scon.Close();

                SignupDone sd = new SignupDone();
                sd.Show();

                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                comboBox1.Text = "";
            }
        }

        private void clear(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           SaveDone sd = new SaveDone();
            sd.Show();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox7.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

       
    }
}
