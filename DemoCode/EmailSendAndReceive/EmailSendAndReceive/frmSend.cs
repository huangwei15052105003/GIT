using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
//download by http://www.codesc.net
namespace EmailSendAndReceive
{
    public partial class frmSend : Form
    {
        public frmSend()
        {
            InitializeComponent();
        }
        //���ʼ����ݽ��б���
        public static string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress(txtSend.Text);   //�����ʼ�������
                MailAddress to = new MailAddress(txtTo.Text);       //�����ʼ�������
                MailMessage message = new MailMessage(from, to);    //ʵ����һ��MaileMessage�����
                message.Subject = Base64Encode(txtSubject.Text);    //���÷����ʼ�������
                message.Body = Base64Encode(txtContent.Text);       //���÷����ʼ�������
                //ʵ����SmtpClient�ʼ����������
                SmtpClient client = new SmtpClient(txtServer.Text, Convert.ToInt32(txtPort.Text));
                //����������֤��������ݵ�ƾ��
                client.Credentials = new System.Net.NetworkCredential(txtName.Text, txtPwd.Text);
                //�����ʼ�
                client.Send(message);
                MessageBox.Show("���ͳɹ�");
            }
            catch
            {
                MessageBox.Show("����ʧ��!");
            }
        }
    }
}