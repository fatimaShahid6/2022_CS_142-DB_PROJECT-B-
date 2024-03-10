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
    public partial class Form39 : Form
    {
        public Form39()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form41 f41 = new Form41();
            f41.Show();
            this.Hide();
        }

        private void Form39_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form42 f42 = new Form42();
            f42.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form33 f33 = new Form33();
            f33.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form40 f40 = new Form40();
            f40.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form43 f43 = new Form43();
            f43.Show();
            this.Hide();
        }
    }
}
