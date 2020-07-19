using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
//download by http://www.codesc.net
namespace SendEmailAddAtt
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string file = txtAttachment.Text;
                MailAddress from = new MailAddress(txtFrom.Text);//设置邮件发送人
                MailAddress to = new MailAddress(txtGet.Text);	//设置邮件接收人
                MailMessage message = new MailMessage(from, to);//实例化一个MaileMessage类对象
                message.Subject = txtSubject.Text;//设置发送邮件的主题
                message.Body = richTextBox1.Text;//设置发送邮件的内容
                //为要发送的邮件创建附件信息
                Attachment myAttachment = new Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
                //为附件添加时间信息
                System.Net.Mime.ContentDisposition disposition = myAttachment.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                message.Attachments.Add(myAttachment);//将创建的附件添加到邮件中
                //实例化SmtpClient邮件发送类对象
                SmtpClient client = new SmtpClient(txtServer.Text, 25);
                //设置用于验证发件人身份的凭据
                client.Credentials = new System.Net.NetworkCredential(txtName.Text, txtPwd.Text);
                //发送邮件
                client.Send(message);
                MessageBox.Show("发送成功");
            }
            catch { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            txtAttachment.Text = openFileDialog1.FileName;
        }
    }
}