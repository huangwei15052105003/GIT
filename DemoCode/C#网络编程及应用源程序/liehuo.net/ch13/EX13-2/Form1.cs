using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace book14_2
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxcon;
		private System.Windows.Forms.Label labelSrv;
		private System.Windows.Forms.TextBox tBSrv;
		private System.Windows.Forms.Label labelUser;
		private System.Windows.Forms.TextBox tBUser;
		private System.Windows.Forms.Label labelPwd;
		private System.Windows.Forms.TextBox tBPwd;
		private System.Windows.Forms.Button buttonCon;
		private System.Windows.Forms.Button buttonDiscon;
		private System.Windows.Forms.RichTextBox rTBText;
		private System.Windows.Forms.GroupBox groupBoxTxt;
		private System.Windows.Forms.GroupBox groupBoxOpe;
		private System.Windows.Forms.ListBox listBoxOpe;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.Button buttonDel;
		private System.Windows.Forms.GroupBox groupBoxStat;
		private System.Windows.Forms.ListBox listBoxStatus;
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
			this.groupBoxcon = new System.Windows.Forms.GroupBox();
			this.buttonDiscon = new System.Windows.Forms.Button();
			this.buttonCon = new System.Windows.Forms.Button();
			this.tBPwd = new System.Windows.Forms.TextBox();
			this.labelPwd = new System.Windows.Forms.Label();
			this.tBUser = new System.Windows.Forms.TextBox();
			this.labelUser = new System.Windows.Forms.Label();
			this.tBSrv = new System.Windows.Forms.TextBox();
			this.labelSrv = new System.Windows.Forms.Label();
			this.groupBoxTxt = new System.Windows.Forms.GroupBox();
			this.rTBText = new System.Windows.Forms.RichTextBox();
			this.groupBoxOpe = new System.Windows.Forms.GroupBox();
			this.buttonDel = new System.Windows.Forms.Button();
			this.buttonRead = new System.Windows.Forms.Button();
			this.listBoxOpe = new System.Windows.Forms.ListBox();
			this.groupBoxStat = new System.Windows.Forms.GroupBox();
			this.listBoxStatus = new System.Windows.Forms.ListBox();
			this.groupBoxcon.SuspendLayout();
			this.groupBoxTxt.SuspendLayout();
			this.groupBoxOpe.SuspendLayout();
			this.groupBoxStat.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxcon
			// 
			this.groupBoxcon.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.buttonDiscon,
																					  this.buttonCon,
																					  this.tBPwd,
																					  this.labelPwd,
																					  this.tBUser,
																					  this.labelUser,
																					  this.tBSrv,
																					  this.labelSrv});
			this.groupBoxcon.Location = new System.Drawing.Point(8, 24);
			this.groupBoxcon.Name = "groupBoxcon";
			this.groupBoxcon.Size = new System.Drawing.Size(608, 120);
			this.groupBoxcon.TabIndex = 0;
			this.groupBoxcon.TabStop = false;
			this.groupBoxcon.Text = "连接操作";
			// 
			// buttonDiscon
			// 
			this.buttonDiscon.Enabled = false;
			this.buttonDiscon.Location = new System.Drawing.Point(496, 72);
			this.buttonDiscon.Name = "buttonDiscon";
			this.buttonDiscon.Size = new System.Drawing.Size(72, 24);
			this.buttonDiscon.TabIndex = 7;
			this.buttonDiscon.Text = "断开连接";
			this.buttonDiscon.Click += new System.EventHandler(this.buttonDiscon_Click);
			// 
			// buttonCon
			// 
			this.buttonCon.Location = new System.Drawing.Point(496, 32);
			this.buttonCon.Name = "buttonCon";
			this.buttonCon.Size = new System.Drawing.Size(72, 24);
			this.buttonCon.TabIndex = 6;
			this.buttonCon.Text = "建立连接";
			this.buttonCon.Click += new System.EventHandler(this.buttonCon_Click);
			// 
			// tBPwd
			// 
			this.tBPwd.Location = new System.Drawing.Point(136, 88);
			this.tBPwd.Name = "tBPwd";
			this.tBPwd.PasswordChar = '*';
			this.tBPwd.Size = new System.Drawing.Size(304, 21);
			this.tBPwd.TabIndex = 5;
			this.tBPwd.Text = "";
			// 
			// labelPwd
			// 
			this.labelPwd.Location = new System.Drawing.Point(40, 88);
			this.labelPwd.Name = "labelPwd";
			this.labelPwd.Size = new System.Drawing.Size(32, 24);
			this.labelPwd.TabIndex = 4;
			this.labelPwd.Text = "密码";
			// 
			// tBUser
			// 
			this.tBUser.Location = new System.Drawing.Point(136, 56);
			this.tBUser.Name = "tBUser";
			this.tBUser.Size = new System.Drawing.Size(304, 21);
			this.tBUser.TabIndex = 3;
			this.tBUser.Text = "";
			// 
			// labelUser
			// 
			this.labelUser.Location = new System.Drawing.Point(40, 56);
			this.labelUser.Name = "labelUser";
			this.labelUser.Size = new System.Drawing.Size(48, 16);
			this.labelUser.TabIndex = 2;
			this.labelUser.Text = "用户名";
			// 
			// tBSrv
			// 
			this.tBSrv.Location = new System.Drawing.Point(136, 24);
			this.tBSrv.Name = "tBSrv";
			this.tBSrv.Size = new System.Drawing.Size(304, 21);
			this.tBSrv.TabIndex = 1;
			this.tBSrv.Text = "";
			// 
			// labelSrv
			// 
			this.labelSrv.Location = new System.Drawing.Point(40, 24);
			this.labelSrv.Name = "labelSrv";
			this.labelSrv.Size = new System.Drawing.Size(96, 24);
			this.labelSrv.TabIndex = 0;
			this.labelSrv.Text = "POP3邮件服务器";
			// 
			// groupBoxTxt
			// 
			this.groupBoxTxt.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.rTBText});
			this.groupBoxTxt.Location = new System.Drawing.Point(8, 184);
			this.groupBoxTxt.Name = "groupBoxTxt";
			this.groupBoxTxt.Size = new System.Drawing.Size(456, 304);
			this.groupBoxTxt.TabIndex = 1;
			this.groupBoxTxt.TabStop = false;
			this.groupBoxTxt.Text = "邮件内容";
			// 
			// rTBText
			// 
			this.rTBText.Location = new System.Drawing.Point(16, 24);
			this.rTBText.Name = "rTBText";
			this.rTBText.Size = new System.Drawing.Size(432, 272);
			this.rTBText.TabIndex = 0;
			this.rTBText.Text = "";
			this.rTBText.TextChanged += new System.EventHandler(this.rTBText_TextChanged);
			// 
			// groupBoxOpe
			// 
			this.groupBoxOpe.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.buttonDel,
																					  this.buttonRead,
																					  this.listBoxOpe});
			this.groupBoxOpe.Location = new System.Drawing.Point(464, 184);
			this.groupBoxOpe.Name = "groupBoxOpe";
			this.groupBoxOpe.Size = new System.Drawing.Size(160, 304);
			this.groupBoxOpe.TabIndex = 2;
			this.groupBoxOpe.TabStop = false;
			this.groupBoxOpe.Text = "操作";
			// 
			// buttonDel
			// 
			this.buttonDel.Enabled = false;
			this.buttonDel.Location = new System.Drawing.Point(88, 256);
			this.buttonDel.Name = "buttonDel";
			this.buttonDel.Size = new System.Drawing.Size(64, 32);
			this.buttonDel.TabIndex = 2;
			this.buttonDel.Text = "删除信件";
			this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
			// 
			// buttonRead
			// 
			this.buttonRead.Enabled = false;
			this.buttonRead.Location = new System.Drawing.Point(16, 256);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(64, 32);
			this.buttonRead.TabIndex = 1;
			this.buttonRead.Text = "阅读信件";
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// listBoxOpe
			// 
			this.listBoxOpe.Enabled = false;
			this.listBoxOpe.ItemHeight = 12;
			this.listBoxOpe.Location = new System.Drawing.Point(8, 24);
			this.listBoxOpe.Name = "listBoxOpe";
			this.listBoxOpe.ScrollAlwaysVisible = true;
			this.listBoxOpe.Size = new System.Drawing.Size(144, 208);
			this.listBoxOpe.TabIndex = 0;
			// 
			// groupBoxStat
			// 
			this.groupBoxStat.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.listBoxStatus});
			this.groupBoxStat.Location = new System.Drawing.Point(8, 496);
			this.groupBoxStat.Name = "groupBoxStat";
			this.groupBoxStat.Size = new System.Drawing.Size(616, 88);
			this.groupBoxStat.TabIndex = 3;
			this.groupBoxStat.TabStop = false;
			this.groupBoxStat.Text = "状态";
			// 
			// listBoxStatus
			// 
			this.listBoxStatus.ItemHeight = 12;
			this.listBoxStatus.Location = new System.Drawing.Point(8, 16);
			this.listBoxStatus.Name = "listBoxStatus";
			this.listBoxStatus.ScrollAlwaysVisible = true;
			this.listBoxStatus.Size = new System.Drawing.Size(600, 64);
			this.listBoxStatus.TabIndex = 17;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(632, 597);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBoxStat,
																		  this.groupBoxOpe,
																		  this.groupBoxTxt,
																		  this.groupBoxcon});
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "邮件接收处理实例";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBoxcon.ResumeLayout(false);
			this.groupBoxTxt.ResumeLayout(false);
			this.groupBoxOpe.ResumeLayout(false);
			this.groupBoxStat.ResumeLayout(false);
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
		public TcpClient Server;
		public NetworkStream NetStrm;
		public StreamReader  RdStrm;
		public string Data;
		public byte[] szData;
		public string CRLF = "\r\n";

		private void rTBText_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonCon_Click(object sender, System.EventArgs e)
		{
			Server = new TcpClient(tBSrv.Text,110);								
			
			try
			{
				NetStrm = Server.GetStream();
				RdStrm= new StreamReader(Server.GetStream(),System.Text.Encoding.Default);
				listBoxStatus.Items.Add(RdStrm.ReadLine());
				Data = "USER "+ tBUser.Text+CRLF;				
				szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(szData,0,szData.Length);
				listBoxStatus.Items.Add(RdStrm.ReadLine());
				Data = "PASS "+ tBPwd.Text+CRLF;				
				szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(szData,0,szData.Length);
				listBoxStatus.Items.Add(RdStrm.ReadLine());
				Data = "STAT"+CRLF;				
				szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(szData,0,szData.Length);
				string st=RdStrm.ReadLine();
				listBoxStatus.Items.Add(st);
				st=st.Substring (4,st.IndexOf(" ",5)-4);
				int count=Int32.Parse(st);
				if(count>0)
				{
					listBoxOpe.Enabled=true;
					buttonRead.Enabled=true;
					buttonDel.Enabled=true;
					listBoxStatus.Items.Clear();
				    listBoxOpe.Items.Clear();
					for(int i=0;i< count;i++)
					  listBoxOpe.Items.Add("第"+(i+1)+"封邮件");
					listBoxOpe.SelectedIndex=0;
				}
				else
				{
					groupBoxOpe.Text="信箱中没有邮件";
					listBoxOpe.Enabled=false;
					buttonRead.Enabled=false;
					buttonDel.Enabled=false;
				}
				buttonCon.Enabled=false;
				buttonDiscon.Enabled = true;
				
			}
			catch(InvalidOperationException err)
			{
				listBoxStatus.Items.Add("Error: "+err.ToString());
			}
		}

		private void buttonDiscon_Click(object sender, System.EventArgs e)
		{
			
			Data = "QUIT"+CRLF;				
			szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
			NetStrm.Write(szData,0,szData.Length);
			listBoxStatus.Items.Add(RdStrm.ReadLine());
			NetStrm.Close();
			RdStrm.Close();
			listBoxOpe.Items.Clear();
			rTBText.Clear();
			listBoxOpe.Enabled=false;
			buttonRead.Enabled=false;
			buttonDel.Enabled=false;
			buttonCon.Enabled = true;
			buttonDiscon.Enabled = false;
			
		}

		private void buttonRead_Click(object sender, System.EventArgs e)
		{
			
			String szTemp;						
			rTBText.Clear();
			try
			{
				string st=listBoxOpe.SelectedItem.ToString();
				st=	st.Substring (1,st.IndexOf("封")-1);
				Data = "RETR "+st+CRLF;				
				szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(szData,0,szData.Length);				
				szTemp = RdStrm.ReadLine();
				if(szTemp[0]!='-') 
				{
					while(szTemp!=".")
					{
					    rTBText.Text +=szTemp+CRLF;
						szTemp = RdStrm.ReadLine();
					}
				}
				else
				{
					listBoxStatus.Items.Add(szTemp);
				}
			}
			catch(InvalidOperationException err)
			{
				listBoxStatus.Items.Add("Error: "+err.ToString());
			}
		}

		private void buttonDel_Click(object sender, System.EventArgs e)
		{
			String szTemp;	
			rTBText.Clear();		
			try
			{
				string st=listBoxOpe.SelectedItem.ToString();
				st=	st.Substring (1,st.IndexOf("封")-1);
				Data = "DELE "+ st+CRLF;				
				szData = System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(szData,0,szData.Length);	
				listBoxStatus.Items.Add(RdStrm.ReadLine());
				int j=listBoxOpe.SelectedIndex;
				listBoxOpe.Items.Remove(listBoxOpe.Items[j].ToString() );
				MessageBox.Show("删除成功","操作成功");
				
			}
			catch(InvalidOperationException err)
			{
				listBoxStatus.Items.Add("Error: "+err.ToString());
			}

		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
