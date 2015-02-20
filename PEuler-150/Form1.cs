using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace PEuler_150
{
    public partial class Form1 : Form
    {
        PE150Solver solver;
        bool calcCompleted = true;
        bool useUserDataSet = false;
        int[] inputs; // used in user mode only
        int depth;   // used in random mode only
        string results;
        Stopwatch sw = new Stopwatch();
        Thread calcThread;

        public Form1()
        {
            InitializeComponent();
            solver = new PE150Solver();
            //label1.Text = "00:00";
            //label2.Text = "0 %";
            timer1.Enabled = true;
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(','))
            {
                // user specified data set
                prepControls_start();

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
                calcThread = new Thread(Thread_Proc);
                calcThread.Start();
            }
            else
            {
                // randomly generated data set
                prepControls_start();

                useUserDataSet = false;
                textBox2.Text = "Calculating using randomly generated data set..." + Environment.NewLine;
                
                //textBox2.Text += PE150Solver.SolveInputs(int.Parse(textBox1.Text)).ToString();
                this.depth = int.Parse(textBox1.Text);
                calcThread = new Thread(Thread_Proc);
                calcThread.Start();
            }
        }

        void Thread_Proc()
        {
            calcCompleted = false;

            if (useUserDataSet)
            {
                results = solver.SolveInputs(inputs).ToString();
            }
            else
            {
                results = solver.SolveInputs(depth).ToString();
            }

            calcCompleted = true;
        }

        private void prepControls_start()
        {
            textBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            timer1.Start();
            sw.Reset();
            sw.Start();
        }

        private void prepControls_stop()
        {
            textBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            timer1.Stop();
            sw.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (calcCompleted)
            {
                // Enable the input controls and display results
                prepControls_stop();
                textBox2.Text += results + Environment.NewLine;
            }

            // Update progress bar
            progressBar1.Maximum = solver.NodeTotal;
            progressBar1.Value = solver.NodeProcessing;

            // Update time label
            label1.Text = sw.Elapsed.ToString(@"mm\:ss\.ff");

            // Update percent label
            //label2.Text = solver.NodeProcessing.ToString();
            label2.Text = (solver.NodeProcessing / (float)solver.NodeTotal).ToString(@"p");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (calcThread != null) calcThread.Abort();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (calcThread != null) calcThread.Abort();
            textBox2.Text += "Aborted!" + Environment.NewLine;
            prepControls_stop();
        }
    }
}
