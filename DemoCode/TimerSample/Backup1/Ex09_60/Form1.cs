using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Download by http://www.codefans.net
namespace Ex09_60
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (this.progressBar1.Value == this.progressBar1.Maximum)
            {
                this.progressBar1.Value = this.progressBar1.Minimum;
            }
            else
            {
                this.progressBar1.PerformStep();
            }
            int intPercent;
            intPercent = 100 * (this.progressBar1.Value - this.progressBar1.Minimum)
                            / (this.progressBar1.Maximum - this.progressBar1.Minimum);
            label2.Text = Convert.ToInt16(intPercent).ToString() + "%";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                button1.Text = "¿ªÊ¼";
            }
            else
            {
                timer1.Enabled = true;
                button1.Text = "Í£Ö¹";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}