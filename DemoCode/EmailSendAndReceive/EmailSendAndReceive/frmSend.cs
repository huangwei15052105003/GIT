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
        //对邮件内容进行编码
        public static string Base64Encode(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress(txtSend.Text);   //设置邮件发送人
                MailAddress to = new MailAddress(txtTo.Text);       //设置邮件接收人
                MailMessage message = new MailMessage(from, to);    //实例化一个MaileMessage类对象
                message.Subject = Base64Encode(txtSubject.Text);    //设置发送邮件的主题
                message.Body = Base64Encode(txtContent.Text);       //设置发送邮件的内容
                //实例化SmtpClient邮件发送类对象
                SmtpClient client = new SmtpClient(txtServer.Text, Convert.ToInt32(txtPort.Text));
                //设置用于验证发件人身份的凭据
                client.Credentials = new System.Net.NetworkCredential(txtName.Text, txtPwd.Text);
                //发送邮件
                client.Send(message);
                MessageBox.Show("发送成功");
            }
            catch
            {
                MessageBox.Show("发送失败!");
            }
        }
    }
}