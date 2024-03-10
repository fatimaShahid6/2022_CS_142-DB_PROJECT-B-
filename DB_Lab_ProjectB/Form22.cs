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
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            Form23 f23 = new Form23();
            f23.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form24 f24 = new Form24();
            f24.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form25 f25 = new Form25();
            f25.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form26 f26 = new Form26();
            f26.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form27 f27 = new Form27();
            f27.Show();
            this.Hide();
        }
    }
}
