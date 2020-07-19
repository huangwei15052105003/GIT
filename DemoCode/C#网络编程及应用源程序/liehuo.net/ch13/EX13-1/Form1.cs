using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace book14_1
	//源码来自：烈火下载 http://down.liehuo.net
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelSrv;
		private System.Windows.Forms.TextBox tBSrv;
		private System.Windows.Forms.TextBox tBpwd;
		private System.Windows.Forms.Label labelPwd;
		private System.Windows.Forms.TextBox tBUser;
		private System.Windows.Forms.Label labelUser;
		private System.Windows.Forms.Label labelSender;
		private System.Windows.Forms.TextBox tBSend;
		private System.Windows.Forms.Label labelRev;
		private System.Windows.Forms.TextBox tBRev;
		private System.Windows.Forms.TextBox tBSubject;
		private System.Windows.Forms.Label labelsubject;
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.TextBox tBMailText;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.ProgressBar pb1;
		private System.Windows.Forms.Label labelp;
		private System.Windows.Forms.Label labelTxt;
		private System.Windows.Forms.Label labelMsg;
		private System.Windows.Forms.ListBox listBoxMsg;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.labelSrv = new System.Windows.Forms.Label();
			this.tBSrv = new System.Windows.Forms.TextBox();
			this.tBpwd = new System.Windows.Forms.TextBox();
			this.labelPwd = new System.Windows.Forms.Label();
			this.tBUser = new System.Windows.Forms.TextBox();
			this.labelUser = new System.Windows.Forms.Label();
			this.labelSender = new System.Windows.Forms.Label();
			this.tBSend = new System.Windows.Forms.TextBox();
			this.labelRev = new System.Windows.Forms.Label();
			this.tBRev = new System.Windows.Forms.TextBox();
			this.tBSubject = new System.Windows.Forms.TextBox();
			this.labelsubject = new System.Windows.Forms.Label();
			this.labelText = new System.Windows.Forms.Label();
			this.tBMailText = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.pb1 = new System.Windows.Forms.ProgressBar();
			this.labelp = new System.Windows.Forms.Label();
			this.labelTxt = new System.Windows.Forms.Label();
			this.labelMsg = new System.Windows.Forms.Label();
			this.listBoxMsg = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// labelSrv
			// 
			this.labelSrv.Location = new System.Drawing.Point(16, 8);
			this.labelSrv.Name = "labelSrv";
			this.labelSrv.Size = new System.Drawing.Size(96, 16);
			this.labelSrv.TabIndex = 1;
			this.labelSrv.Text = "SMTP服务器地址";
			// 
			// tBSrv
			// 
			this.tBSrv.Location = new System.Drawing.Point(112, 8);
			this.tBSrv.Name = "tBSrv";
			this.tBSrv.Size = new System.Drawing.Size(368, 21);
			this.tBSrv.TabIndex = 2;
			this.tBSrv.Text = "henu.edu.cn";
			// 
			// tBpwd
			// 
			this.tBpwd.Location = new System.Drawing.Point(272, 40);
			this.tBpwd.Name = "tBpwd";
			this.tBpwd.PasswordChar = '*';
			this.tBpwd.Size = new System.Drawing.Size(104, 21);
			this.tBpwd.TabIndex = 20;
			this.tBpwd.Text = "";
			// 
			// labelPwd
			// 
			this.labelPwd.Location = new System.Drawing.Point(216, 48);
			this.labelPwd.Name = "labelPwd";
			this.labelPwd.Size = new System.Drawing.Size(56, 32);
			this.labelPwd.TabIndex = 19;
			this.labelPwd.Text = "口令";
			// 
			// tBUser
			// 
			this.tBUser.Location = new System.Drawing.Point(80, 40);
			this.tBUser.Name = "tBUser";
			this.tBUser.Size = new System.Drawing.Size(80, 21);
			this.tBUser.TabIndex = 18;
			this.tBUser.Text = "";
			// 
			// labelUser
			// 
			this.labelUser.Location = new System.Drawing.Point(16, 48);
			this.labelUser.Name = "labelUser";
			this.labelUser.Size = new System.Drawing.Size(48, 24);
			this.labelUser.TabIndex = 17;
			this.labelUser.Text = "用户名";
			// 
			// labelSender
			// 
			this.labelSender.Location = new System.Drawing.Point(16, 112);
			this.labelSender.Name = "labelSender";
			this.labelSender.Size = new System.Drawing.Size(48, 16);
			this.labelSender.TabIndex = 23;
			this.labelSender.Text = "发信人";
			// 
			// tBSend
			// 
			this.tBSend.Location = new System.Drawing.Point(56, 104);
			this.tBSend.Name = "tBSend";
			this.tBSend.Size = new System.Drawing.Size(192, 21);
			this.tBSend.TabIndex = 24;
			this.tBSend.Text = "";
			// 
			// labelRev
			// 
			this.labelRev.Location = new System.Drawing.Point(280, 112);
			this.labelRev.Name = "labelRev";
			this.labelRev.Size = new System.Drawing.Size(88, 24);
			this.labelRev.TabIndex = 25;
			this.labelRev.Text = "收信人";
			// 
			// tBRev
			// 
			this.tBRev.Location = new System.Drawing.Point(328, 104);
			this.tBRev.Name = "tBRev";
			this.tBRev.Size = new System.Drawing.Size(192, 21);
			this.tBRev.TabIndex = 26;
			this.tBRev.Text = "";
			// 
			// tBSubject
			// 
			this.tBSubject.Location = new System.Drawing.Point(64, 152);
			this.tBSubject.Name = "tBSubject";
			this.tBSubject.Size = new System.Drawing.Size(448, 21);
			this.tBSubject.TabIndex = 28;
			this.tBSubject.Text = "";
			// 
			// labelsubject
			// 
			this.labelsubject.Location = new System.Drawing.Point(16, 160);
			this.labelsubject.Name = "labelsubject";
			this.labelsubject.Size = new System.Drawing.Size(96, 32);
			this.labelsubject.TabIndex = 27;
			this.labelsubject.Text = "主题";
			// 
			// labelText
			// 
			this.labelText.Location = new System.Drawing.Point(16, 224);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(88, 24);
			this.labelText.TabIndex = 29;
			this.labelText.Text = "邮件内容";
			// 
			// tBMailText
			// 
			this.tBMailText.Location = new System.Drawing.Point(16, 216);
			this.tBMailText.Multiline = true;
			this.tBMailText.Name = "tBMailText";
			this.tBMailText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tBMailText.Size = new System.Drawing.Size(496, 216);
			this.tBMailText.TabIndex = 30;
			this.tBMailText.Text = "";
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(-8, 88);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(552, 8);
			this.button1.TabIndex = 31;
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(184, 448);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Size = new System.Drawing.Size(128, 32);
			this.buttonSend.TabIndex = 32;
			this.buttonSend.Text = "邮件发送";
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// pb1
			// 
			this.pb1.Location = new System.Drawing.Point(120, 488);
			this.pb1.Maximum = 16;
			this.pb1.Name = "pb1";
			this.pb1.Size = new System.Drawing.Size(384, 16);
			this.pb1.TabIndex = 33;
			this.pb1.Visible = false;
			// 
			// labelp
			// 
			this.labelp.Location = new System.Drawing.Point(16, 488);
			this.labelp.Name = "labelp";
			this.labelp.Size = new System.Drawing.Size(80, 16);
			this.labelp.TabIndex = 34;
			this.labelp.Text = "正在发送邮件";
			this.labelp.Visible = false;
			// 
			// labelTxt
			// 
			this.labelTxt.Location = new System.Drawing.Point(16, 192);
			this.labelTxt.Name = "labelTxt";
			this.labelTxt.Size = new System.Drawing.Size(64, 16);
			this.labelTxt.TabIndex = 35;
			this.labelTxt.Text = "邮件内容";
			// 
			// labelMsg
			// 
			this.labelMsg.Location = new System.Drawing.Point(16, 536);
			this.labelMsg.Name = "labelMsg";
			this.labelMsg.Size = new System.Drawing.Size(80, 16);
			this.labelMsg.TabIndex = 36;
			this.labelMsg.Text = "信息查看";
			// 
			// listBoxMsg
			// 
			this.listBoxMsg.ItemHeight = 12;
			this.listBoxMsg.Location = new System.Drawing.Point(120, 520);
			this.listBoxMsg.Name = "listBoxMsg";
			this.listBoxMsg.ScrollAlwaysVisible = true;
			this.listBoxMsg.Size = new System.Drawing.Size(408, 64);
			this.listBoxMsg.TabIndex = 37;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(544, 597);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listBoxMsg,
																		  this.labelMsg,
																		  this.labelTxt,
																		  this.labelp,
																		  this.pb1,
																		  this.buttonSend,
																		  this.button1,
																		  this.tBMailText,
																		  this.labelText,
																		  this.tBSubject,
																		  this.labelsubject,
																		  this.tBRev,
																		  this.labelRev,
																		  this.tBSend,
																		  this.labelSender,
																		  this.tBpwd,
																		  this.labelPwd,
																		  this.tBUser,
																		  this.labelUser,
																		  this.tBSrv,
																		  this.labelSrv});
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "邮件发送实例";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
        TcpClient smtpSrv;
		NetworkStream netStrm;
		string CRLF="\r\n";
		private void WriteStream(string strCmd)
		{
            strCmd+=CRLF;
			byte[] bw=System.Text.Encoding.Default.GetBytes(strCmd.ToCharArray ());
			netStrm.Write (bw,0,bw.Length );
         }
		
		private string AuthStream(string strCmd)
		{
			try
			{
				byte[] by=System.Text.Encoding.Default.GetBytes(strCmd.ToCharArray ());
				strCmd=Convert.ToBase64String(by);
			}
			catch(Exception ex)
			{
				return ex.ToString();
				
			}
			return strCmd;

		}


		

		private void buttonSend_Click(object sender, System.EventArgs e)
		{
			listBoxMsg.Items.Clear();
			try
			{
				string data;
	            pb1.Visible=true;
 			    labelp.Visible=true;
				pb1.Value=0;
                smtpSrv=new TcpClient (tBSrv.Text,25);
				netStrm=smtpSrv.GetStream ();
				StreamReader rdStrm=new StreamReader(smtpSrv.GetStream());
				WriteStream("EHLO Local");
                listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				WriteStream("AUTH LOGIN");
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				data=tBUser.Text;
				data=AuthStream(data);
				WriteStream(data);
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				data=tBpwd.Text;
				data=AuthStream(data);
				WriteStream(data);
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				data="MAIL FROM: <"+tBSend.Text+">";
				WriteStream(data);
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				data="RCPT TO:<"+tBRev.Text +">";
				WriteStream(data);
				listBoxMsg.Items.Add(rdStrm.ReadLine());
			    pb1.Value++;
				WriteStream("DATA");
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				pb1.Value++;
				data="Date:"+DateTime.Now;
				WriteStream(data);
				pb1.Value++;
				data="From:"+tBSend.Text;
				WriteStream(data);
				pb1.Value++;
				data="TO:"+tBRev.Text;
				WriteStream(data);
				pb1.Value++;
				data="SUBJECT:"+tBSubject.Text;
				WriteStream(data);
				pb1.Value++;
				data="Reply-TO:"+tBSend.Text;
				WriteStream(data);
				pb1.Value++;
				WriteStream("");
				pb1.Value++;
				WriteStream(tBMailText.Text);
				pb1.Value++;
				WriteStream(".");
				pb1.Value++;
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				WriteStream("QUIT");
				pb1.Value++;
				listBoxMsg.Items.Add(rdStrm.ReadLine());
				netStrm.Close();
				rdStrm.Close();
				pb1.Visible=false;
				labelp.Visible=false;
				MessageBox.Show("邮件发送成功","成功");
			}
			catch (Exception ex)
			{
                  MessageBox.Show(ex.ToString(),"操作错误！");
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
