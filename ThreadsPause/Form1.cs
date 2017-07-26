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

namespace ThreadsPause
{
    public partial class Form1 : Form
    {
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = tokenSource.Token;
        AutoResetEvent are = new AutoResetEvent(true);
        private bool isCancle = false;
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;//关闭自动重置事件默认值为非终止状态
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tokenSource=new CancellationTokenSource();
            token = tokenSource.Token;
            Thread t = new Thread(fun);
            isCancle = false;
            t.IsBackground = true;
            t.Start();
            button1.Enabled = false;
            button2.Enabled = true;
            textBox1.Text = "true";
            button4.Enabled = true;
        }

        private void fun()
        {
            int i = 0;
            try
            {
                if (isCancle)
                {
                    ;
                    isCancle = false;
                }
                else
                {
                    while (i < 100000)
                    {
                        token.ThrowIfCancellationRequested();
                        i += 1;
                        textBox2.Text = i.ToString();
                        if (textBox1.Text == "false")
                        {
                            are.WaitOne();
                        }
                    }
                    button1.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "false";
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "true";
            are.Set();//释放所有被阻塞的线程  
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            isCancle = true;
            tokenSource.Cancel(); 
            button1.Enabled = true;
            button2.Enabled =false;
            button3.Enabled = false;
            button4.Enabled = false;
        }
    }
}
