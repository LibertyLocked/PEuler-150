using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PEuler_150
{
    public partial class Form1 : Form
    {
        bool calcCompleted = true;
        bool useUserDataSet = false;
        int[] inputs; // used in user mode only
        int depth;   // used in random mode only
        string results;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(','))
            {
                // user specified data set
                textBox1.Enabled = false;
                button1.Enabled = false;

                useUserDataSet = true;
                textBox2.Text = "Calculating using user specified data set..." + Environment.NewLine;

                string[] inputStrings = textBox1.Text.Split(',');
                int[] inputs = new int[inputStrings.Length];

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = int.Parse(inputStrings[i].Trim());
                }

                //textBox2.Text += PE150Solver.SolveInputs(inputs).ToString();
                this.inputs = inputs;
                Thread calcThread = new Thread(Thread_Proc);
                calcThread.Start();
            }
            else
            {
                // randomly generated data set
                textBox1.Enabled = false;
                button1.Enabled = false;

                useUserDataSet = false;
                textBox2.Text = "Calculating using randomly generated data set..." + Environment.NewLine;
                
                //textBox2.Text += PE150Solver.SolveInputs(int.Parse(textBox1.Text)).ToString();
                this.depth = int.Parse(textBox1.Text);
                Thread calcThread = new Thread(Thread_Proc);
                calcThread.Start();
            }
        }

        void Thread_Proc()
        {
            calcCompleted = false;

            if (useUserDataSet)
            {
                results = PE150Solver.SolveInputs(inputs).ToString();
            }
            else
            {
                results = PE150Solver.SolveInputs(depth).ToString();
            }

            calcCompleted = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (calcCompleted && !textBox1.Enabled)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox2.Text += results + Environment.NewLine;
            }
        }
    }
}
