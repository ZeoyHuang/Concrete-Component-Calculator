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
    public partial class rebar : Form
    {
        public rebar()
        {
            InitializeComponent();
        }

        SqlConnection myconn = new SqlConnection(@"Data Source=LAPTOP-91SDKBLS\SHUJUKU;Initial Catalog=reinforce_concrete2;Integrated Security=True");
        static double d;
        static double ee;

        private void rebar_Load(object sender, EventArgs e)
        {
            SqlDataAdapter myadapter = new SqlDataAdapter("select brand from Strength", myconn);
            DataSet mydataset = new DataSet();
            myadapter.Fill(mydataset, "gangjin");
            comboBox5.DataSource = mydataset.Tables["gangjin"];
            comboBox5.DisplayMember = "brand";
            myconn.Close();

            int j = 0, m = 0;
            SqlCommand mycmd = new SqlCommand("select max(ct) from Strength", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                j = int.Parse(mydatareader.GetValue(0).ToString());
                mydatareader.Close();
            }
            myconn.Close();

            mycmd = new SqlCommand("select rownum from Strength where ct=" + j, myconn);
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                j = int.Parse(mydatareader.GetValue(0).ToString());
                mydatareader.Close();
            }
            myconn.Close();
            comboBox5.SelectedIndex = j;

            mycmd = new SqlCommand("select ct from Strength where rownum=0", myconn);
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                m = int.Parse(mydatareader.GetValue(0).ToString());
                mydatareader.Close();
            }
            myconn.Close();

            m = m - 1;
            string cmd = "update Strength set ct=" + m + "where rownum=0";
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

            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            myadapter = new SqlDataAdapter("select brand,fy,Es from Strength", myconn);
            myadapter.Fill(mydataset, "gang");
            dataGridView2.DataSource = mydataset.Tables["gang"];
            myconn.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countt = 0;
            SqlCommand mycmd = new SqlCommand("select fy,Es,ct from Strength where brand ='" + comboBox5.Text + "'", myconn);
            SqlDataReader mydatareader;
            myconn.Open();
            {
                mydatareader = mycmd.ExecuteReader();
                mydatareader.Read();
                if (mydatareader.HasRows)
                {
                    textBox4.Text = mydatareader.GetValue(0).ToString();
                    textBox5.Text = mydatareader.GetValue(1).ToString();
                    countt = int.Parse(mydatareader.GetValue(2).ToString());
                    countt++;
                }
                mydatareader.Close();
            }
            myconn.Close();

            d = double.Parse(textBox4.Text);
            ee = double.Parse(textBox5.Text);

            string cmd = "update Strength set ct=" + countt + "where brand ='" + comboBox5.Text + "'";
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

        private void button6_Click(object sender, EventArgs e)
        {
            switch (comboBox6.Text)
            {
                case "MPa": textBox4.Text = d.ToString(); break;
                case "KPa": textBox4.Text = (d * 1000).ToString(); break;
                case "HPa": textBox4.Text = (d * 10000).ToString(); break;
                case "Pa": textBox4.Text = (d * 1000000).ToString(); break;
                case "atm": textBox4.Text = (d * 9.8692327).ToString(); break;
                case "mmHg": textBox4.Text = (d * 7500.616827).ToString(); break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            switch (comboBox7.Text)
            {
                case "MPa": textBox5.Text = ee.ToString(); break;
                case "KPa": textBox5.Text = (ee * 1000).ToString(); break;
                case "HPa": textBox5.Text = (ee * 10000).ToString(); break;
                case "Pa": textBox5.Text = (ee * 1000000).ToString(); break;
                case "atm": textBox5.Text = (ee * 9.8692327).ToString(); break;
                case "mmHg": textBox5.Text = (ee * 7500.616827).ToString(); break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (i < 8)
            {
                if (dataGridView2[0, i].Value.ToString() == comboBox5.Text)
                {
                    dataGridView2.CurrentCell = dataGridView2[0, i];
                    dataGridView2.Rows[i].Selected = true;

                    break;
                }
                i++;
            }
        }
    }
}
