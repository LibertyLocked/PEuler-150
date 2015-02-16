using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PEuler_150
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rows;

            if (int.TryParse(textBox1.Text, out rows))
            {
                textBox2.Text = PE150Solver.Solve(rows).ToString();
            }
            else
            {
                MessageBox.Show("Please enter an integer!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.button1_Click(this, e);
            }
        }
    }
}
