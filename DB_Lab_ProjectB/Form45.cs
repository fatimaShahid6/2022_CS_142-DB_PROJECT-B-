using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Lab_ProjectB
{
    public partial class Form45 : Form
    {
        public Form45()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form44 f44 = new Form44();
            f44.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form46 f46 = new Form46();
            f46.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form47 f47 = new Form47();
            f47.Show();
            this.Hide();
        }
    }
}
