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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2();
            fr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabcontrol fr = new tabcontrol();
            fr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu fr = new menu();
            fr.ShowDialog();
        }
    }
}
