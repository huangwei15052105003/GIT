using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestSyncServer
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		private Socket socket;
		private Socket clientSocket;
		Thread thread;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.ListBox listBoxState;
		private System.Windows.Forms.RichTextBox richTextBoxAccept;
		private System.Windows.Forms.RichTextBox richTextBoxSend;
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
			this.listBoxState.Items.Clear();
			this.richTextBoxAccept.Text="";
			this.richTextBoxSend.Text="";

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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxState = new System.Windows.Forms.ListBox();
			this.textBoxIP = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.richTextBoxAccept = new System.Windows.Forms.RichTextBox();
			this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "服务器IP";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "监听端口";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "接收信息";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 224);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "发送信息";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBoxState);
			this.groupBox1.Location = new System.Drawing.Point(216, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "服务器状态";
			// 
			// listBoxState
			// 
			this.listBoxState.ItemHeight = 12;
			this.listBoxState.Location = new System.Drawing.Point(8, 16);
			this.listBoxState.Name = "listBoxState";
			this.listBoxState.Size = new System.Drawing.Size(184, 76);
			this.listBoxState.TabIndex = 0;
			// 
			// textBoxIP
			// 
			this.textBoxIP.Location = new System.Drawing.Point(72, 32);
			this.textBoxIP.Name = "textBoxIP";
			this.textBoxIP.ReadOnly = true;
			this.textBoxIP.Size = new System.Drawing.Size(128, 21);
			this.textBoxIP.TabIndex = 2;
			this.textBoxIP.Text = "127.0.0.1";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(72, 72);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.ReadOnly = true;
			this.textBoxPort.TabIndex = 3;
			this.textBoxPort.Text = "6788";
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(56, 320);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.TabIndex = 5;
			this.buttonStart.Text = "开始监听";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(168, 320);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.TabIndex = 5;
			this.buttonSend.Text = "发送信息";
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(288, 320);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 5;
			this.buttonStop.Text = "停止监听";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// richTextBoxAccept
			// 
			this.richTextBoxAccept.Location = new System.Drawing.Point(72, 120);
			this.richTextBoxAccept.Name = "richTextBoxAccept";
			this.richTextBoxAccept.Size = new System.Drawing.Size(344, 96);
			this.richTextBoxAccept.TabIndex = 6;
			this.richTextBoxAccept.Text = "richTextBoxAccept";
			// 
			// richTextBoxSend
			// 
			this.richTextBoxSend.Location = new System.Drawing.Point(72, 224);
			this.richTextBoxSend.Name = "richTextBoxSend";
			this.richTextBoxSend.Size = new System.Drawing.Size(344, 80);
			this.richTextBoxSend.TabIndex = 6;
			this.richTextBoxSend.Text = "richTextBoxSend";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(432, 349);
			this.Controls.Add(this.richTextBoxAccept);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.textBoxIP);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.richTextBoxSend);
			this.Name = "Form1";
			this.Text = "服务器";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.groupBox1.ResumeLayout(false);
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

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			this.buttonStart.Enabled=false;
			IPAddress ip=IPAddress.Parse(this.textBoxIP.Text);
			IPEndPoint server=new IPEndPoint(ip,Int32.Parse(this.textBoxPort.Text));
			socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			socket.Bind(server);
			//监听客户端连接
			socket.Listen(10);
			clientSocket=socket.Accept();
			//显示客户IP和端口号
			this.listBoxState.Items.Add("与客户 "+clientSocket.RemoteEndPoint.ToString()+" 建立连接");
			//创建一个线程接收客户信息
			thread=new Thread(new ThreadStart(AcceptMessage));
			thread.Start();
		}

		private void AcceptMessage()
		{
			NetworkStream netStream=new NetworkStream(clientSocket);
			while(true)
			{
				try
				{
					byte[] datasize=new byte[4];
					netStream.Read(datasize,0,4);
					int size=System.BitConverter.ToInt32(datasize,0);
					Byte[] message=new byte[size];
					int dataleft=size;
					int start=0;
					while(dataleft>0)
					{
						int recv=netStream.Read(message,start,dataleft);
						start+=recv;
						dataleft-=recv;
					}
					this.richTextBoxAccept.Rtf=System.Text.Encoding.Unicode.GetString(message);
				}
				catch
				{
					this.listBoxState.Items.Add("与客户断开连接");
					break;
				}
			}

		}

		private void buttonSend_Click(object sender, System.EventArgs e)
		{
			string str=this.richTextBoxSend.Rtf;
			int i=str.Length;
			if(i==0)
			{
				return;
			}
			else
			{
				 //因为str为Unicode编码，每个字符占2字节，所以实际字节数应*2
				i*=2;
			}
			byte[] datasize=new byte[4];
			 //将32位整数值转换为字节数组
			datasize=System.BitConverter.GetBytes(i);
			byte[] sendbytes=System.Text.Encoding.Unicode.GetBytes(str);
			try
			{
				NetworkStream netStream=new NetworkStream(clientSocket);
				netStream.Write(datasize,0,4);
				netStream.Write(sendbytes,0,sendbytes.Length);
				netStream.Flush();
				this.richTextBoxSend.Rtf="";
			}
			catch
			{
				MessageBox.Show("无法发送!");
			}
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			this.buttonStart.Enabled=true;
			try
			{
				socket.Close();
				if(clientSocket.Connected)
				{
					clientSocket.Shutdown(SocketShutdown.Both);
					clientSocket.Close();
					thread.Abort();
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.buttonStop_Click(null,null);
		}
	}
}
