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
    public partial class Form38 : Form
    {
        public Form38()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form36 f36 = new Form36();
            f36.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form35 f35 = new Form35();
            f35.Show();
            this.Hide();
        }

        private void Form38_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form34 f34 = new Form34();
            f34.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form37 f37 = new Form37();
            f37.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form33 f33 = new Form33();
            f33.Show();
            this.Hide();
        }
    }
}
