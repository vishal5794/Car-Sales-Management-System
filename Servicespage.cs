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
    public partial class Servicespage : Form
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

        public Servicespage()
        {
            InitializeComponent();
            menuStrip1.BackColor = Color.Transparent;

            uname.Visible = true;
            uname.Text = uname.Text + LoginPage.Username;
        }

        private void Servicespage_Load(object sender, EventArgs e)
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
            new Invoicepage().Show();
            this.Hide();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text+Environment.NewLine+textBox2.Text);
            }
            SaveDone sd = new SaveDone();
            sd.Show();
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

            double taxPercent =1.13;

            double partscost = double.Parse(textBox4.Text);
            double laborcost = double.Parse(textBox5.Text);
         
            double total = (partscost + laborcost)*taxPercent;

            double tax = (partscost + laborcost)*(taxPercent-1);

            string s = "insert into Service(Serdate,Cname,Serial,Partscost,Laborcost,Tax,Totalcost,Description)values(@sd,@cn,@serno,@pc,@lc,@tax,@total,@desc)";


            cmd.CommandText = s;

            
            cmd.Parameters.AddWithValue("@serno",textBox1.Text );
            cmd.Parameters.AddWithValue("@sd", textBox2.Text);
            cmd.Parameters.AddWithValue("@cn", textBox3.Text);
            cmd.Parameters.AddWithValue("@pc",textBox4.Text );
            cmd.Parameters.AddWithValue("@lc", textBox5.Text);
            cmd.Parameters.AddWithValue("@tax",tax );
            cmd.Parameters.AddWithValue("@total",total );
            cmd.Parameters.AddWithValue("@desc", richTextBox2.Text);

            cmd.ExecuteNonQuery();

            scon.Close();

            richTextBox1.Text = richTextBox1.Text + " Serial No:" + textBox1.Text +
                                "\n Service Date:" + textBox2.Text +
                                "\n Customer Name:" + textBox3.Text +
                                "\n Parts Cost:" + textBox4.Text +
                                "\n Labor Cost:" + textBox5.Text +
                                "\n Service Price:" + total +
                                "\n Service Description:" + richTextBox2.Text;


            MessageBox.Show("Service queued!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            
            richTextBox2.Clear();
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn();
            cmd.CommandText = "Delete from Service where Serial=@sn";

            cmd.Parameters.AddWithValue("@sn",textBox1.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Service Info deleted!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            richTextBox2.Clear();
            richTextBox1.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn();

            double taxPercent = 1.13;

            double partscost = double.Parse(textBox4.Text);
            double laborcost = double.Parse(textBox5.Text);

            double total = (partscost + laborcost) * taxPercent;

            double tax = (partscost + laborcost) * (taxPercent - 1);

            cmd.CommandText = " Update Service SET Serdate=@sd,Cname=@cn,Partscost=@pc,Laborcost=@lc,Tax=@tax,Totalcost=@total,Description=@desc where Serial=@serno";

            cmd.Parameters.AddWithValue("@serno",textBox1.Text );
            cmd.Parameters.AddWithValue("@sd", textBox2.Text);
            cmd.Parameters.AddWithValue("@cn", textBox3.Text);
            cmd.Parameters.AddWithValue("@pc",textBox4.Text );
            cmd.Parameters.AddWithValue("@lc", textBox5.Text);
            cmd.Parameters.AddWithValue("@tax",tax );
            cmd.Parameters.AddWithValue("@total",total );
            cmd.Parameters.AddWithValue("@desc", richTextBox2.Text);


            cmd.ExecuteNonQuery();
            MessageBox.Show("Service Info updated!!!!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            richTextBox2.Clear();
            richTextBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn();

            string s = "Select * from Service where Serial=@sn";

            cmd.CommandText = s;

            cmd.Parameters.AddWithValue("@sn",textBox1.Text);

            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                textBox1.Text = dr[5].ToString();
                textBox2.Text = dr[3].ToString();
                textBox3.Text = dr[4].ToString();
                textBox4.Text = dr[6].ToString();
                textBox5.Text = dr[7].ToString();
                textBox6.Text = dr[9].ToString();
                richTextBox2.Text = dr[10].ToString();
            }

            scon.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            monthCalendar1.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox2.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_MouseLeave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        

        
    }
}
