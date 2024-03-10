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
    public partial class Form33 : Form
    {
        public Form33()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form38 f38 = new Form38();
            f38.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form39 f39 = new Form39();
            f39.Show();
            this.Hide();
        }
    }
}
