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
        public static string strserver;     //�洢�ʼ�������
        public static string pwd;           //�洢�û���¼���������
        public static int k;                //�洢�ʼ���Ŀ
        public static TcpClient tcpclient;  //ʵ����TcpClient�������������ͻ�������
        public static StreamReader sreader; //ͨ������ȡ�ʼ�����
        public static string strRet;        //�洢�ʼ�����
        //���������������
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
        //�÷��������Ի�����ӵ��û���ݽ�����֤
        private string Logon(TcpClient tcpclient, string user, string pass)
        {
            string strRet;                                   //�洢�û���¼��Ϣ
            strRet = SendPopCmd(tcpclient, "user " + user);��//���¼�����������û���
            strRet = SendPopCmd(tcpclient, "pass " + pass);  //���¼��������������
            return strRet;
        }
        //��ȡ��¼����ĸ�����Ϣ
        private string[] StaticMailBox(TcpClient tcpclient)
        {
            string strRet;
            strRet = SendPopCmd(tcpclient, "stat");          //�������������Ϣ���������ͳ������
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
        //�жϷ��ص��ַ�����Ϣ������ǡ�+OK����֤����¼�ɹ��������¼ʧ��
        private string JudgeString(string strCheck)
        {
            if (strCheck.Substring(0, 3) != "+OK")
            {
                return "-ERR";
            }
            else
                return "+OK";
        }
        //����������ʼ���Ŷ�ȡ�ʼ���Ϣ
        private string[] PopMail(TcpClient tcpclient, int i)
        {
            string strRet;
            string[] arrRet = new string[20];
            bool strBody = false;
            string[] arrTemp;
            //��ȡ�ɲ�����ʶ���ʼ���ȫ���ı�
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
                    strBody = true;                     //���ڿ�ʼ���� Body ����Ϣ
                if (arrTemp[0].ToLower() == "date")
                    arrRet[1] = arrTemp[1];             //�ż��ķ�������
                if (arrTemp[0].ToLower() == "from")
                    arrRet[2] = (arrTemp[1].Substring(arrTemp[1].LastIndexOf("<") + 1)).Replace(">", " ");  //������
                if (arrTemp[0].ToLower() == "to")
                    arrRet[3] = (arrTemp[1].Substring(arrTemp[1].LastIndexOf("<") + 1)).Replace(">", " ");  //������
                if (arrTemp[0].ToLower() == "subject")
                    arrRet[4] = arrTemp[1].ToString();  //����
                if (strBody)
                    arrRet[5] = arrRet[5] + strRet + "\n";
            }
            return arrRet;
        }
        //�Զ�ȡ���ʼ����ݽ���Base64����
        public static string Base64Decode(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        //����¼����ť�¼�
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
                tcpclient.Connect(strserver, 110);       //����Զ������
                //��ʼ��StreamReader�����Ա���������ʽ��ȡԶ�������е�����
                sreader = new StreamReader(tcpclient.GetStream(), Encoding.Default);
                sreader.ReadLine();
                strRet = Logon(tcpclient, user, pass);   //��¼Զ������
                if (JudgeString(strRet) != "+OK")
                {
                    MessageBox.Show("�û��������벻ƥ��");
                    return;
                }
                arrRet = StaticMailBox(tcpclient);       //��ȡԶ��������ָ���û����ʼ���Ϣ
                if (arrRet[0] != "+OK")
                {
                    MessageBox.Show("�����ˣ�");
                    return;
                }
                richTextBox1.AppendText("��ǰ�û���" + user + "\n");
                richTextBox1.AppendText("�����й��У�" + arrRet[1] + "���ʼ�" + "\n");
                richTextBox1.AppendText("��ռ��" + arrRet[2] + "Byte");
                k = Convert.ToInt32(arrRet[1]);
                txtPOP.ReadOnly = txtMail.ReadOnly = txtPwd.ReadOnly = true;
                btnLogin.Enabled = false;
                btnReceive.Enabled = true;
                MessageBox.Show("��¼�ɹ���");
            }
            catch
            {
                MessageBox.Show("���ӷ�����ʧ�ܣ�");
            }
        }
        //���˳�����ť�¼�
        private void btnExit_Click(object sender, EventArgs e)
        {
            tcpclient.Close();                          //�ر�Զ����������
        }
        //�����ա��ʼ���ť�¼�
        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNum.Text != "")
                {
                    if (Convert.ToInt32(txtNum.Text) > k || Convert.ToInt32(txtNum.Text) <= 0)
                    {
                        MessageBox.Show("�������������");
                    }
                    else
                    {
                        richTextBox1.Clear();
                        string[] arrRets;
                        //���Զ��������ָ���ʼ��������Ϣ���洢��һ��string���͵�������
                        arrRets = PopMail(tcpclient, Convert.ToInt32(txtNum.Text));
                        richTextBox1.AppendText("��ǰ�ǵ�" + txtNum.Text + "����" + "\n");
                        richTextBox1.AppendText("�ʼ����ڣ�" + arrRets[1] + "\n");
                        richTextBox1.AppendText("�����ˣ�" + arrRets[2] + "\n");
                        richTextBox1.AppendText("�����ˣ�" + arrRets[3] + "\n");
                        richTextBox1.AppendText("�ʼ����⣺" + Base64Decode(arrRets[4]) + "\n");
                        richTextBox1.AppendText("�ʼ����ݣ�" + Base64Decode(arrRets[5]));
                    }
                }
                else
                {
                    MessageBox.Show("�ʼ���������");
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