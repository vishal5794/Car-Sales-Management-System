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
    public partial class Customerpage : Form
    {
        SqlConnection scon;
        SqlCommand cmd1, cmd2,cmd3;
        SqlDataReader dr1,dr2;

        public void conn()
        {
            String con = Properties.Settings.Default.connection;
            scon = new SqlConnection(con);
            cmd1 = scon.CreateCommand();
            cmd2 = scon.CreateCommand();
            cmd3 = scon.CreateCommand();
            scon.Open();
        }

        public Customerpage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            linkLabel1.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;

        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            conn();
            cmd1.CommandText = "Select Serial from Car where Sold=@sold";
            cmd1.Parameters.AddWithValue("sold","NO");
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox5.Items.Add(dr1[0]);
            }
            dr1.Close();
            scon.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }

        private void foreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                customerToolStripMenuItem.ForeColor = dg.Color;
                carToolStripMenuItem.ForeColor = dg.Color;
                employeeToolStripMenuItem.ForeColor = dg.Color;
                invoiceToolStripMenuItem.ForeColor = dg.Color;
                serviceToolStripMenuItem1.ForeColor = dg.Color;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            conn();

            cmd1.CommandText = "insert into Customer(Name,Street,City,Prov,Pcode,Phone)values(@name,@street,@city,@prov,@pcode,@pno)";
           
            cmd1.Parameters.AddWithValue("@name", textBox1.Text);
            cmd1.Parameters.AddWithValue("@street", textBox2.Text);
            cmd1.Parameters.AddWithValue("@city", textBox3.Text);
            cmd1.Parameters.AddWithValue("@prov", textBox4.Text);
            cmd1.Parameters.AddWithValue("@pcode", textBox5.Text);
            cmd1.Parameters.AddWithValue("@pno", textBox6.Text);

            cmd2.CommandText = "select ID from customer where Name=@cname";
            cmd2.Parameters.AddWithValue("@cname",textBox1.Text);

            dr1 = cmd2.ExecuteReader();
            string Cid="";
            while (dr1.Read())
            {
               Cid  = dr1[0].ToString();
            }
            dr1.Close();

            cmd3.CommandText = "Update Car SET Cid=@cid,Purchinv=@pinv,Purchdate=@pdate,Sold=@sold where Serial=@serial";
            cmd3.Parameters.AddWithValue("@cid", Cid);
            cmd3.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
            cmd3.Parameters.AddWithValue("@pinv", textBox10.Text);
            cmd3.Parameters.AddWithValue("@pdate", textBox9.Text);
            cmd3.Parameters.AddWithValue("@sold", "YES");

            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();

            scon.Close();
            MessageBox.Show("Customer Added Successfully!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox5.Text = "";
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

            scon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn();

            cmd1.CommandText = " Update Car SET Model=@mod,Trim=@trim,Color=@color,Year=@year,Price=@price,Purchinv=@piv,Purchdate=@pdate where Serial=@serial";

            cmd1.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
            cmd1.Parameters.AddWithValue("@mod", comboBox1.SelectedItem.ToString());
            cmd1.Parameters.AddWithValue("@trim", comboBox2.SelectedItem.ToString());
            cmd1.Parameters.AddWithValue("@color", comboBox3.SelectedItem.ToString());
            cmd1.Parameters.AddWithValue("@year", comboBox4.SelectedItem.ToString());
            cmd1.Parameters.AddWithValue("@price", textBox8.Text);
            cmd1.Parameters.AddWithValue("@piv", textBox10.Text);
            cmd1.Parameters.AddWithValue("@pdate", textBox9.Text);


            cmd1.ExecuteNonQuery();
            MessageBox.Show("Car sale Info updated!!!!");
            scon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn();

            if (textBox1.Text != "" && comboBox5.SelectedItem.ToString() == "")
            {
                string s_name = "Select c.Name,c.Street,c.City,c.Prov,c.Pcode,c.Phone,cr.Serial,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price,cr.Purchinv,cr.Purchdate from Customer c,Car cr WHERE c.ID=cr.Carid AND c.Name=@cname";
                cmd1.CommandText = s_name;
                cmd1.Parameters.AddWithValue("@cname", textBox1.Text);

                dr2 = cmd1.ExecuteReader();
                while (dr2.Read())
                {
                    textBox1.Text = dr2[0].ToString();
                    textBox2.Text = dr2[1].ToString();
                    textBox3.Text = dr2[2].ToString();
                    textBox4.Text = dr2[3].ToString();
                    textBox5.Text = dr2[4].ToString();
                    textBox6.Text = dr2[5].ToString();
                    comboBox5.Text = dr2[6].ToString();
                    comboBox1.Text = dr2[7].ToString();
                    comboBox2.Text = dr2[8].ToString();
                    comboBox3.Text = dr2[9].ToString();
                    comboBox4.Text = dr2[10].ToString();
                    textBox8.Text = dr2[11].ToString();
                    textBox9.Text = dr2[13].ToString();
                    textBox10.Text = dr2[12].ToString();


                }
            }
            else if (textBox1.Text == "" && comboBox5.SelectedItem.ToString() != "")
            {
                string s_cid = "Select DISTINCT c.Name,c.Street,c.City,c.Prov,c.Pcode,c.Phone,cr.Serial,cr.Model,cr.Trim,cr.Color,cr.Year,cr.Price,cr.Purchinv,cr.Purchdate from Customer c,Car cr where c.ID=cr.Carid AND cr.Serial=@serial";

                cmd2.CommandText = s_cid;
                cmd2.Parameters.AddWithValue("@serial", comboBox5.SelectedItem.ToString());
                SqlDataReader dr = cmd2.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
                    textBox4.Text = dr[3].ToString();
                    textBox5.Text = dr[4].ToString();
                    textBox6.Text = dr[5].ToString();
                    comboBox5.Text = dr[6].ToString();
                    comboBox1.Text = dr[7].ToString();
                    comboBox2.Text = dr[8].ToString();
                    comboBox3.Text = dr[9].ToString();
                    comboBox4.Text = dr[10].ToString();
                    textBox8.Text = dr[11].ToString();
                    textBox9.Text = dr[13].ToString();
                    textBox10.Text = dr[12].ToString();
                    count++;
                }
            }

            scon.Close();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //do nothing
            Customerpage hp = new Customerpage();
            hp.Show();
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
            new Invoicepage().Show();
            this.Hide();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Servicespage().Show();
            this.Hide();
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


        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Logout");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox9.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn();
            cmd1.CommandText = "Select Model,Trim,Color,Year,Price from Car where Serial=@serial";
            cmd1.Parameters.AddWithValue("@serial",comboBox5.SelectedItem.ToString());
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox1.Text = dr1[0].ToString();
                comboBox2.Text = dr1[1].ToString();
                comboBox3.Text = dr1[2].ToString();
                comboBox4.Text = dr1[3].ToString();
                textBox8.Text = dr1[4].ToString();
            }
            dr1.Close();
            scon.Close();
        }
    }
}
