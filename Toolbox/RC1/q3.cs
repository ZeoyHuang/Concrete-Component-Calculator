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
    public partial class q3 : Form
    {
        public q3()
        {
            InitializeComponent();
        }

        SqlConnection myconn = new SqlConnection(@"Data Source=LAPTOP-91SDKBLS\SHUJUKU;Initial Catalog=reinforce_concrete2;Integrated Security=True");
        static double a;
        static double b;

        private void q3_Load(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select distinct cat from pc1", myconn);
            DataSet mydataset = new DataSet();
            myadapter.Fill(mydataset, "yuying1");
            comboBox1.DataSource = mydataset.Tables["yuying1"];
            comboBox1.DisplayMember = "cat";
            myconn.Close();

            int j = 0,j1 = 0;
            SqlCommand mycmd = new SqlCommand("select max(ct) from pc1", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                j = int.Parse(mydatareader.GetValue(0).ToString());
                mydatareader.Close();
            }
            myconn.Close();

            mycmd = new SqlCommand("select rownum1,rownum2 from pc1 where ct=" + j, myconn);
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                j = int.Parse(mydatareader.GetValue(0).ToString());
                j1 = int.Parse(mydatareader.GetValue(1).ToString());
                mydatareader.Close();
            }
            myconn.Close();
            comboBox1.SelectedIndex = j;
            comboBox2.SelectedIndex = j1;

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            myadapter = new SqlDataAdapter("select cat,fptk,fpy,fpyy from pc1", myconn);
            myadapter.Fill(mydataset, "yuyingg");
            dataGridView1.DataSource = mydataset.Tables["yuyingg"];
            myconn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 0; 
            while (i < 13)
            {
                if ((dataGridView1[0, i].Value.ToString() == comboBox1.Text) && (dataGridView1[1, i].Value.ToString() == comboBox2.Text))
                {
                    dataGridView1.CurrentCell = dataGridView1[0, i];
                    dataGridView1.Rows[i].Selected = true;
                    break;
                }
                i++;
            }

            int countt = 0;
            SqlCommand mycmd = new SqlCommand("select ct from pc1 where cat='" + comboBox1.Text + "'and fptk = '"+ comboBox2.Text + "'", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                if (mydatareader.HasRows)
                {
                    countt = int.Parse(mydatareader.GetValue(0).ToString());
                    countt++;
                }
                mydatareader.Close();
            }   
            myconn.Close();

            string cmd = "update pc1 set ct=" + countt + "where cat='" + comboBox1.Text + "'and fptk = '" + comboBox2.Text + "'";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select fptk from pc1 where cat='" + comboBox1.Text + "'", myconn);
            DataSet mydataset = new DataSet();
            myadapter.Fill(mydataset, "yuying2");
            comboBox2.DataSource = mydataset.Tables["yuying2"];
            comboBox2.DisplayMember = "fptk";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand mycmd = new SqlCommand("select fpy,fpyy from pc1 where cat='" + comboBox1.Text + "'and fptk = '"+ comboBox2.Text + "'", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                if (mydatareader.HasRows)
                {
                    textBox2.Text = mydatareader.GetValue(0).ToString();
                    textBox3.Text = mydatareader.GetValue(1).ToString();
                }
                mydatareader.Close();
            }   
            myconn.Close();

            a = double.Parse(textBox2.Text);
            b = double.Parse(textBox3.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "MPa": textBox2.Text = a.ToString(); break;
                case "KPa": textBox2.Text = (a * 1000).ToString(); break;
                case "HPa": textBox2.Text = (a * 10000).ToString(); break;
                case "Pa": textBox2.Text = (a * 1000000).ToString(); break;
                case "atm": textBox2.Text = (a * 9.8692327).ToString(); break;
                case "mmHg": textBox2.Text = (a * 7500.616827).ToString(); break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
                case "MPa": textBox3.Text = a.ToString(); break;
                case "KPa": textBox3.Text = (a * 1000).ToString(); break;
                case "HPa": textBox3.Text = (a * 10000).ToString(); break;
                case "Pa": textBox3.Text = (a * 1000000).ToString(); break;
                case "atm": textBox3.Text = (a * 9.8692327).ToString(); break;
                case "mmHg": textBox3.Text = (a * 7500.616827).ToString(); break;
            }
        }
    }
}
