using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
//download by http://www.codesc.net
namespace EmailSendAndReceive
{
    public partial class frmReceive : Form
    {
        public frmReceive()
        {
            InitializeComponent();
        }
        public static string strserver;     //存储邮件服务器
        public static string pwd;           //存储用户登录邮箱的密码
        public static int k;                //存储邮件数目
        public static TcpClient tcpclient;  //实例化TcpClient对象，用来建立客户端连接
        public static StreamReader sreader; //通过流读取邮件内容
        public static string strRet;        //存储邮件内容
        //向服务器发送命令
        private string SendPopCmd(TcpClient tcpclient, string strCmd)
        {
            Byte[] arrCmd;
            string strRet;
            Stream stream;
            stream = tcpclient.GetStream();
            strCmd = strCmd + "\r\n";
            arrCmd = Encoding.Default.GetBytes(strCmd.ToCharArray());
            stream.Write(arrCmd, 0, strCmd.Length);
            strRet = sreader.ReadLine();
            return strRet;
        }
        //该方法用来对获得连接的用户身份进行验证
        private string Logon(TcpClient tcpclient, string user, string pass)
        {
            string strRet;                                   //存储用户登录信息
            strRet = SendPopCmd(tcpclient, "user " + user);　//向登录服务器发送用户名
            strRet = SendPopCmd(tcpclient, "pass " + pass);  //想登录服务器发送密码
            return strRet;
        }
        //获取登录邮箱的各种信息
        private string[] StaticMailBox(TcpClient tcpclient)
        {
            string strRet;
            strRet = SendPopCmd(tcpclient, "stat");          //向服务器发送信息返回邮箱的统计资料
            if (JudgeString(strRet) != "+OK")
            {
                return "-ERR -ERR".Split(" ".ToCharArray());
            }
            else
            {
                string[] arrRet = strRet.Split(" ".ToCharArray());
                return arrRet;
            }
        }
        //判断返回的字符串信息，如果是“+OK”，证明登录成功，否则登录失败
        private string JudgeString(string strCheck)
        {
            if (strCheck.Substring(0, 3) != "+OK")
            {
                return "-ERR";
            }
            else
                return "+OK";
        }
        //根据输入的邮件编号读取邮件信息
        private string[] PopMail(TcpClient tcpclient, int i)
        {
            string strRet;
            string[] arrRet = new string[20];
            bool strBody = false;
            string[] arrTemp;
            //获取由参数标识的邮件的全部文本
            strRet = SendPopCmd(tcpclient, "retr " + i.ToString());
            if (JudgeString(strRet) != "+OK")
            {
                return "-ERR -ERR".Split(" ".ToCharArray());
            }
            arrRet[0] = "+OK";
            while (sreader.Peek() != 46)
            {
                strRet = sreader.ReadLine();
                arrTemp = strRet.Split(":".ToCharArray());
                if (strRet == "")
                    strBody = true;                     //现在开始接收 Body 的信息
                if (arrTemp[0].ToLower() == "date")
                    arrRet[1] = arrTemp[1];             //信件的发送日期
                if (arrTemp[0].ToLower() == "from")
                    arrRet[2] = (arrTemp[1].Substring(arrTemp[1].LastIndexOf("<") + 1)).Replace(">", " ");  //发信人
                if (arrTemp[0].ToLower() == "to")
                    arrRet[3] = (arrTemp[1].Substring(arrTemp[1].LastIndexOf("<") + 1)).Replace(">", " ");  //收信人
                if (arrTemp[0].ToLower() == "subject")
                    arrRet[4] = arrTemp[1].ToString();  //主题
                if (strBody)
                    arrRet[5] = arrRet[5] + strRet + "\n";
            }
            return arrRet;
        }
        //对读取的邮件内容进行Base64编码
        public static string Base64Decode(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        //“登录”按钮事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtMail.Text;
            string pass = txtPwd.Text;
            string[] arrRet;
            pwd = pass;
            strserver = txtPOP.Text;
            tcpclient = new TcpClient();
            try
            {
                tcpclient.Connect(strserver, 110);       //连接远程主机
                //初始化StreamReader对象，以便以流的形式读取远程主机中的内容
                sreader = new StreamReader(tcpclient.GetStream(), Encoding.Default);
                sreader.ReadLine();
                strRet = Logon(tcpclient, user, pass);   //登录远程主机
                if (JudgeString(strRet) != "+OK")
                {
                    MessageBox.Show("用户名或密码不匹配");
                    return;
                }
                arrRet = StaticMailBox(tcpclient);       //获取远程主机中指定用户的邮件信息
                if (arrRet[0] != "+OK")
                {
                    MessageBox.Show("出错了！");
                    return;
                }
                richTextBox1.AppendText("当前用户：" + user + "\n");
                richTextBox1.AppendText("邮箱中共有：" + arrRet[1] + "封邮件" + "\n");
                richTextBox1.AppendText("共占：" + arrRet[2] + "Byte");
                k = Convert.ToInt32(arrRet[1]);
                txtPOP.ReadOnly = txtMail.ReadOnly = txtPwd.ReadOnly = true;
                btnLogin.Enabled = false;
                btnReceive.Enabled = true;
                MessageBox.Show("登录成功！");
            }
            catch
            {
                MessageBox.Show("连接服务器失败！");
            }
        }
        //“退出”按钮事件
        private void btnExit_Click(object sender, EventArgs e)
        {
            tcpclient.Close();                          //关闭远程主机连接
        }
        //“接收”邮件按钮事件
        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNum.Text != "")
                {
                    if (Convert.ToInt32(txtNum.Text) > k || Convert.ToInt32(txtNum.Text) <= 0)
                    {
                        MessageBox.Show("输入的索引错误");
                    }
                    else
                    {
                        richTextBox1.Clear();
                        string[] arrRets;
                        //获得远程主机上指定邮件的相关信息，存储到一个string类型的数组中
                        arrRets = PopMail(tcpclient, Convert.ToInt32(txtNum.Text));
                        richTextBox1.AppendText("当前是第" + txtNum.Text + "封信" + "\n");
                        richTextBox1.AppendText("邮件日期：" + arrRets[1] + "\n");
                        richTextBox1.AppendText("发信人：" + arrRets[2] + "\n");
                        richTextBox1.AppendText("收信人：" + arrRets[3] + "\n");
                        richTextBox1.AppendText("邮件主题：" + Base64Decode(arrRets[4]) + "\n");
                        richTextBox1.AppendText("邮件内容：" + Base64Decode(arrRets[5]));
                    }
                }
                else
                {
                    MessageBox.Show("邮件索引错误");
                }
            }
            catch
            {
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin.Focus();
        }

        private void txtPOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtMail.Focus();
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPwd.Focus();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnReceive.Focus();
        }

        private void frmReceive_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                tcpclient.Close();
            }
            catch { }
        }
    }
}