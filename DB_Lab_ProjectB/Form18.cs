﻿using System;
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
    public partial class Form18 : Form
    {
        public Form18()
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
            Form19 mf = new Form19();
            mf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form22 mf = new Form22();
            mf.Show();
            this.Hide();

        }
    }
}
