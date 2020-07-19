using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//download by http://down.liehuo.net
namespace Case08_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Environment.GetEnvironmentVariable("UserName");        //获取用户名
            this.textBox2.Text = Environment.SystemDirectory;                           //获取系统目录
            Microsoft.VisualBasic.Devices.Computer myComputer = new Microsoft.VisualBasic.Devices.Computer();
            textBox3.Text = myComputer.Name;                                           //获取计算机名
            string mode = SystemInformation.BootMode.ToString();
            switch (mode)                                                           //利用switch多条件语句获取系统启动模式
            {
                case "FailSafe":
                    textBox4.Text = "不带网络支持的安全模式";
                    break;
                case "FailSafeWithNetwork":
                    textBox4.Text = "具有网络支持的安全模式";
                    break;
                case "Normal":
                    textBox4.Text = "标准模式";
                    break;
            }

            OperatingSystem myos = Environment.OSVersion;
            if (myos.Version.Major == 5)                                      //利用条件语句获取系统版本
            {
                switch (myos.Version.Minor)
                {
                    case 0:
                        textBox5.Text = "Windows 2000 " + myos.ServicePack;
                        break;
                    case 1:
                        textBox5.Text = "Windows XP " + myos.ServicePack;
                        break;
                    case 2:
                        textBox5.Text = "Windows Server 2003 " + myos.ServicePack;
                        break;
                    default:
                        textBox5.Text = myos.ToString() + " " + myos.ServicePack;
                        break;
                }
            }
            else
            {
                textBox5.Text = myos.ToString() + " " + myos.ServicePack;
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
