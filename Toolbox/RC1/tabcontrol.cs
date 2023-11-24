using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication5;

namespace RC1
{
    public partial class tabcontrol : Form
    {
        public tabcontrol()
        {
            InitializeComponent();
        }
        q1 tabpageform1 = new q1();
        rebar tabpageform2 = new rebar();
        q3 tabpageform3 = new q3();
        List<Form> formList = new List <Form>();
        private void tabcontrol_Load(object sender, EventArgs e)
        {
            formList.Add(tabpageform1);
            formList.Add(tabpageform2);
            formList.Add(tabpageform3);
            for (int i=0; i<tabControl1.TabPages.Count ;i++)
            {
                if(i>=formList.Count)
                    break;
                Form f = formList[i];
                f.Dock=DockStyle.Fill;
                f.TopLevel=false;
                f.FormBorderStyle=FormBorderStyle.None;
                f.Show();
                tabControl1.TabPages[i].Controls.Clear();
                tabControl1.TabPages[i].Controls.Add(f);
            
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            menu fr = new menu();
            fr.ShowDialog();
        }

    }
}
