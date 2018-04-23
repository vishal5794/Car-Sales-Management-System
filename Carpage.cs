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
    public partial class Carpage : Form
    {
        SqlConnection scon;
        SqlCommand cmd;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd = scon.CreateCommand();
            scon.Open();
        }


        public Carpage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Carpage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Customerpage().Show();
            this.Hide();
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Employeepage().Show();
            new Employeepage().getEmployee();
            this.Hide();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Invoicepage().Show();
            this.Hide();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Servicespage().Show();
            this.Hide();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Logout");
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn();

            string s = "insert into Car(Serial,Model,Trim,Color,Year,Price,Sold)values(@sn,@model,@trim,@color,@year,@price,@sold)";


            cmd.CommandText = s;

            cmd.Parameters.AddWithValue("@sn", textBox1.Text);
            cmd.Parameters.AddWithValue("@model", comboBox1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@year", int.Parse(comboBox4.SelectedItem.ToString()));
            cmd.Parameters.AddWithValue("@price", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@sold","NO");

            cmd.ExecuteNonQuery();
         
            scon.Close();
            MessageBox.Show("Inventory updated!!!!");

            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string model = comboBox1.SelectedItem.ToString();

            if (model.Equals("HURACAN"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("PERFORMANTE");
                comboBox2.Items.Add("COUPE");
                comboBox2.Items.Add("SPYDER");
                comboBox2.Items.Add("AVIO");
            }
            else if (model.Equals("AVENTADOR"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
                comboBox2.Items.Add("PIRELLI");
                comboBox2.Items.Add("COUPE");
                comboBox2.Items.Add("MIURA HOMAGE");
            }
            else if (model.Equals("VENENO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
            }
            else if (model.Equals("GALLARDO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("POLIZIA");
                comboBox2.Items.Add("BALBONI");
                comboBox2.Items.Add("SUPER TROFOE");
                comboBox2.Items.Add("AVIO");
            }
            else if (model.Equals("MURCEILAGO"))
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("ROADSTER");
                comboBox2.Items.Add("SUPERVELOCE");
                comboBox2.Items.Add("VERSACE");
                comboBox2.Items.Add("ROADSTER VERSACE");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn();

            cmd.CommandText = " Update Car SET Model=@mod,Trim=@trim,Color=@color,Year=@year,Price=@price where Serial=@serial";

            cmd.Parameters.AddWithValue("@serial", textBox1.Text);
            cmd.Parameters.AddWithValue("@mod", comboBox1.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@year", comboBox4.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@price", textBox2.Text);
            

            cmd.ExecuteNonQuery();
            MessageBox.Show("Car Info updated!!!!");
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn();
            string s = "Select * from Car where Serial=@sn";

            cmd.CommandText = s;

            cmd.Parameters.AddWithValue("@sn",textBox1.Text);

            SqlDataReader rd = cmd.ExecuteReader();
            int count = 0;
            while (rd.Read())
                {
                    textBox1.Text = rd[2].ToString();
                    comboBox1.Text = rd[3].ToString();
                    comboBox2.Text = rd[4].ToString();
                    comboBox3.Text = rd[5].ToString();
                    comboBox4.Text = rd[6].ToString();
                    textBox2.Text = rd[7].ToString();
                    count++;
                }

            scon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
