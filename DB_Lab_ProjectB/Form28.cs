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
    public partial class Form28 : Form
    {
        public Form28()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form32 f32 = new Form32();
            f32.Show();
            this.Hide();
        }

        private void Form28_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainForm mf = new mainForm();
            mf.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form29 f29 = new Form29();
            f29.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form30 f30 = new Form30();
            f30.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form31 f31 = new Form31();
            f31.Show();
            this.Hide();
        }
    }
}
