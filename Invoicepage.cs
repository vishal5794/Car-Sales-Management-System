using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Car_Sales_Management_System
{
    public partial class Invoicepage : Form
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

        public Invoicepage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Invoicepage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            monthCalendar1.Visible = false;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Customerpage().Show();
            this.Hide();
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Carpage().Show();
            this.Hide();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Employeepage().Show();
            new Employeepage().getEmployee();
            this.Hide();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox2.Text + Environment.NewLine + textBox3.Text);
            }
            SaveDone sd = new SaveDone();
            sd.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn();

            string insurance = "";
            if(radioButton1.Checked){
                insurance = "YES";
            }else{
                insurance="NO";
            }

            cmd.CommandText = "Insert into Saleinv(Cname,Salesman,Saledate,Price,Insurance)values(@cname,@sname,@sdate,@price,@insurance)";
            cmd.Parameters.AddWithValue("@cname",textBox1.Text);
            cmd.Parameters.AddWithValue("@sname",textBox3.Text);
            cmd.Parameters.AddWithValue("@sdate",textBox5.Text);
            cmd.Parameters.AddWithValue("@price",textBox6.Text);
            cmd.Parameters.AddWithValue("@insurance",insurance);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Sale invoice inserted");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn();
            cmd.CommandText = "Delete from Saleinv where Serial=@cname";

            cmd.Parameters.AddWithValue("@cname", textBox2.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Sale Invoice deleted!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn();

            string insurance = "";
            if (radioButton1.Checked)
            {
                insurance = "YES";
            }
            else
            {
                insurance = "NO";
            }

            cmd.CommandText = " Update Saleinv SET Salesman=@sman,Saledate=@sdate,Price=@price,Insurance=@ins where Cname=@cname";

            cmd.Parameters.AddWithValue("@cname", textBox2.Text);
            cmd.Parameters.AddWithValue("@sman", textBox3.Text);
            cmd.Parameters.AddWithValue("@sdate", textBox5.Text);
            cmd.Parameters.AddWithValue("@price", textBox6.Text);
            cmd.Parameters.AddWithValue("@ins", insurance );
            cmd.ExecuteNonQuery();
            MessageBox.Show("Car sale Info updated!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn();

            if (textBox2.Text != "" && textBox1.Text == "")
            {
                string s_name = "Select cr.Serial,c.Name,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Purchdate,cr.Price,s.Salesman,s.Insurance from Car cr,Customer c,Saleinv s where cr.Cid=c.ID AND cr.Cid=s.Cid AND c.Name=@cname";
                cmd.CommandText = s_name;
                cmd.Parameters.AddWithValue("@cname", textBox2.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[8].ToString();
                    comboBox1.Text = dr[2].ToString();
                    comboBox2.Text = dr[3].ToString();
                    comboBox3.Text = dr[4].ToString();
                    comboBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    textBox6.Text = dr[7].ToString();
                    if (dr[9].ToString().Equals("YES"))
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
            }
            else if (textBox2.Text == "" && textBox1.Text != "")
            {
                string s_name = "Select cr.Serial,c.Name,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Purchdate,cr.Price,s.Salesman,s.Insurance from Car cr,Customer c,Saleinv s where cr.Cid=c.ID AND cr.Cid=s.Cid AND cr.Serial=@serial";
            cmd.CommandText = s_name;
            cmd.Parameters.AddWithValue("@serial", textBox1.Text);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[8].ToString();
                comboBox1.Text = dr[2].ToString();
                comboBox2.Text = dr[3].ToString();
                comboBox3.Text = dr[4].ToString();
                comboBox4.Text = dr[5].ToString();
                textBox5.Text = dr[6].ToString();
                textBox6.Text = dr[7].ToString();
                if (dr[9].ToString().Equals("YES"))
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }    
            }
            }
            

            
            scon.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox5.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            conn();
            cmd.CommandText = "Select cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price from Car cr,Customer c where cr.Cid=c.ID AND cr.Serial=@serial";
            cmd.Parameters.AddWithValue("@serial", textBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Text = dr[0].ToString();
                comboBox2.Text = dr[1].ToString();
                comboBox3.Text = dr[2].ToString();
                comboBox4.Text = dr[3].ToString();
                textBox6.Text = dr[4].ToString();
            }
            scon.Close();
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
    }
}
