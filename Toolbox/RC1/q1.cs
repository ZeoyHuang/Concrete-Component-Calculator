using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RC1
{
    public partial class q1 : Form
    {
        public q1()
        {
            InitializeComponent();
        }
        SqlConnection myconn = new SqlConnection(@"Data Source=LAPTOP-91SDKBLS\SHUJUKU;Initial Catalog=reinforce_concrete2;Integrated Security=True");
        static double a;
        static double b;
        static double c;


        private void q1_Load(object sender, EventArgs e)
        {
                
                SqlDataAdapter myadapter = new SqlDataAdapter("select strength from Concrete", myconn);
                DataSet mydataset = new DataSet();
                myadapter.Fill(mydataset, "hunningtu");
                comboBox1.DataSource = mydataset.Tables["hunningtu"];
                comboBox1.DisplayMember = "strength";
                myconn.Close();
            
                int j=0,m=0;
                SqlCommand mycmd = new SqlCommand("select max(ct) from Concrete" , myconn);
                SqlDataReader mydatareader;
                myconn.Open();
                {
                    mydatareader = mycmd.ExecuteReader();
                    mydatareader.Read();
                    j = int.Parse(mydatareader.GetValue(0).ToString());
                    mydatareader.Close();
                }
                myconn.Close();

                mycmd = new SqlCommand("select rownum from Concrete where ct=" + j, myconn);
                myconn.Open();
                {
                    mydatareader = mycmd.ExecuteReader();
                    mydatareader.Read();
                    j = int.Parse(mydatareader.GetValue(0).ToString());
                    mydatareader.Close();
                }
                myconn.Close();
                comboBox1.SelectedIndex = j;

                mycmd = new SqlCommand("select ct from Concrete where rownum=0", myconn);
                myconn.Open();
                {
                    mydatareader = mycmd.ExecuteReader();
                    mydatareader.Read();
                    m = int.Parse(mydatareader.GetValue(0).ToString());
                    mydatareader.Close();
                }
                myconn.Close();

                m = m - 1;
                string cmd = "update Concrete set ct=" + m + "where rownum=0";
                mycmd = new SqlCommand(cmd, myconn);
                myconn.Open();
                {
                    try
                    {
                        mycmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                myconn.Close();

                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                myadapter = new SqlDataAdapter("select Strength,Ec,fc,ft from Concrete", myconn);
                myadapter.Fill(mydataset, "hunning");
                dataGridView1.DataSource = mydataset.Tables["hunning"];
                myconn.Close();
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countt=0;
            SqlCommand mycmd = new SqlCommand("select Ec,fc,ft,ct from Concrete where strength ='" + comboBox1.Text +"'", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                if (mydatareader.HasRows)
                {
                    textBox1.Text = mydatareader.GetValue(0).ToString();
                    textBox2.Text = mydatareader.GetValue(1).ToString();
                    textBox3.Text = mydatareader.GetValue(2).ToString();
                    countt = int.Parse(mydatareader.GetValue(3).ToString());
                    countt++;
                }
                mydatareader.Close();
            }
            myconn.Close();

            a = double.Parse(textBox1.Text);
            b = double.Parse(textBox2.Text);
            c = double.Parse(textBox3.Text);
            
            string cmd = "update Concrete set ct=" + countt + "where strength='" + comboBox1.Text + "'";
            mycmd = new SqlCommand(cmd, myconn);
            myconn.Open();
            {
                try
                {
                    mycmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }    
            }
            myconn.Close();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            switch (comboBox2.Text)
            {
                case "MPa": textBox1.Text = a.ToString(); break;
                case "KPa": textBox1.Text = (a * 1000).ToString(); break;
                case "HPa": textBox1.Text = (a * 10000).ToString(); break;
                case "Pa": textBox1.Text = (a * 1000000).ToString(); break;
                case "atm": textBox1.Text = (a * 9.8692327).ToString(); break;
                case "mmHg": textBox1.Text = (a * 7500.616827).ToString(); break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "MPa": textBox2.Text = b.ToString(); break;
                case "KPa": textBox2.Text = (b * 1000).ToString(); break;
                case "HPa": textBox2.Text = (b * 10000).ToString(); break;
                case "Pa": textBox2.Text = (b * 1000000).ToString(); break;
                case "atm": textBox2.Text = (b * 9.8692327).ToString(); break;
                case "mmHg": textBox2.Text = (b * 7500.616827).ToString(); break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             switch (comboBox4.Text)
            {
                case "MPa": textBox3.Text = c.ToString(); break;
                case "KPa": textBox3.Text = (c * 1000).ToString(); break;
                case "HPa": textBox3.Text = (c * 10000).ToString(); break;
                case "Pa": textBox3.Text = (c * 1000000).ToString(); break;
                case "atm": textBox3.Text = (c * 9.8692327).ToString(); break;
                case "mmHg": textBox3.Text = (c * 7500.616827).ToString(); break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < 14)
            {
                if (dataGridView1[0, i].Value.ToString() == comboBox1.Text)
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    dataGridView1.Rows[i].Selected = true;
                   
                    break;
                }
                i++;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
