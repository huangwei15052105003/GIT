using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//download by http://down.liehuo.net
namespace 学生绩点管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //登录系统
        {
            String strName;
            String strPwd;
            strName = this.textBox1.Text;
            strPwd = this.textBox2.Text;

            if (strName != String.Empty && strPwd != String.Empty)
            {
                Form2 form = new Form2();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("用户名或密码不能为空！请确认！");
            }
        }

        private void button2_Click(object sender, EventArgs e)   //退出系统
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                e.Cancel = true;
        }


    }
}
