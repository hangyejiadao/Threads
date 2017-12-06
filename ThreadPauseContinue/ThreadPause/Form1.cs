using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadPause
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnCon.Enabled = false;
            btnPause.Enabled = false;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            run = true;
            button1.Enabled = false;
            btnCon.Enabled = false;
            btnPause.Enabled = true;
            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        private AutoResetEvent are = new AutoResetEvent(false);
        bool run = true;
        int i = 0;
        void Run()
        {
            
            while (true)
            {
                if (!run)
                {
                    are.WaitOne();
                }
                i++; 
                label1.Invoke(new Action(() =>
                {
                    label1.Text = i.ToString();
                }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnCon.Enabled = false;
            btnPause.Enabled = true;
            run = true;
            are.Set();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            btnCon.Enabled = true;
            btnPause.Enabled = false;
            run = false;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            button2.Enabled = false;
            btnCon.Enabled = false;
            label1.Text = "0";
            i = 0;
            button1.Enabled = true;
            btnPause.Enabled = false;
            run = false;
        }
    }
}
