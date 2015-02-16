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
            if (textBox1.Text.Contains(','))
            {
                // user specified data set
                string[] inputStrings = textBox1.Text.Split(',');
                int[] inputs = new int[inputStrings.Length];

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = int.Parse(inputStrings[i].Trim());
                }

                textBox2.Text = "Using user specified data set!" + Environment.NewLine;
                textBox2.Text += PE150Solver.SolveInputs(inputs).ToString();
            }
            else
            {
                // randomly generated data set
                textBox2.Text = "Using randomly generated data set!" + Environment.NewLine;
                textBox2.Text += PE150Solver.SolveInputs(int.Parse(textBox1.Text)).ToString();
            }
        }
    }
}
