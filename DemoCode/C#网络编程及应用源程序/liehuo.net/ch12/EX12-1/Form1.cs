using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace FTPServer
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		TcpListener myListener;

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(24, 24);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(368, 184);
			this.listBox1.TabIndex = 0;
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(256, 232);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.TabIndex = 1;
			this.buttonStart.Text = "开始监听";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(352, 232);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "停止监听";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Location = new System.Drawing.Point(16, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 216);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "传递情况";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "主目录";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 232);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(152, 21);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "d:\\myftp";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 261);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.buttonStop);
			this.Name = "Form1";
			this.Text = "FTPServer";
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
			this.listBox1.Items.Add("开始监听……");
			//端口号6788
			myListener=new TcpListener(IPAddress.Parse("127.0.0.1"),21);
			myListener.Start();
			Thread myThread=new Thread(new ThreadStart(ReceiveData));
			myThread.Start();
		}

		private void ReceiveData()
		{
			TcpClient newClient;
			while(true)
			{
				try
				{
					//等待用户进入
					newClient=myListener.AcceptTcpClient();
				}
				catch
				{
					//当单击“停止监听”或者退出时AcceptTcpClient()会产生异常
					myListener.Stop();
					break;
				}
				Receive tp=new Receive(newClient,ref listBox1,ref textBox1);
				Thread thread=new Thread(new ThreadStart(tp.processService));
				thread.Start();
			}
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				myListener.Stop();
			}
			catch
			{}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			buttonStop_Click(null,null);
		}
	}

	public class Receive
	{
		private TcpClient client;
		private ListBox listbox;
		private TextBox textbox;
		public Receive(TcpClient tcpclient,ref ListBox listbox,ref TextBox textbox)
		{
			client=tcpclient;
			this.listbox=listbox;
			this.textbox=textbox;
		}

		public void processService()
		{
			NetworkStream netStream=client.GetStream();
			StreamReader sr=new StreamReader(netStream,System.Text.Encoding.Unicode);
			StreamWriter sw=new StreamWriter(netStream,System.Text.Encoding.Unicode);
			sw.WriteLine("220");
			sw.Flush();
			listbox.Items.Add("有用户连接，发送:220");
			while(true)
			{
				string str="";
				try
				{
					//当用户非正常断开连接时，可能会产生异常
					str=sr.ReadLine();
				}
				catch
				{
					listbox.Items.Add("用户意外断开连接。");
					break;
				}
				//当用户非正常断开连接时，即使不出现异常，接收字符串也会为null
				if(str==null)
				{
					listbox.Items.Add("用户意外断开连接。");
					break;
				}
				else
				{
					listbox.Items.Add("收到:"+str);
				}
				string command="";
				string parameter="";
				int index=str.IndexOf(" ");
				if(index>-1)
				{
					command=str.Substring(0,index);
					parameter=str.Substring(index+1);
					//将用户发送的虚拟目录转换为实际目录
					parameter=parameter.Replace("server:",textbox.Text);
				}
				else
				{
					command=str;
				}
				FileStream fs=null;
				switch(command)
				{
					case "LIST":
						//LIST表示用户希望得到指定目录下的子目录列表
						string[] dir=Directory.GetDirectories(parameter);
						string sendstr="";
						for(int i=0;i<dir.Length;i++)
						{
							//将实际目录换为虚拟目录
							dir[i]=dir[i].Replace(textbox.Text,"server:");
							sendstr+=dir[i]+"@";
						}
						//去掉最后一个@
						if(sendstr.Length>0)
						{
							sendstr=sendstr.Substring(0,sendstr.Length-1);
						}
						// 发送125到用户，意思为服务器准备传送数据
						sw.WriteLine("125");
						sw.Flush();
						listbox.Items.Add("发送:125");
						//发送子目录到用户
						sw.WriteLine(sendstr);
						sw.Flush();
						listbox.Items.Add("发送目录名:"+sendstr);
						//得到文件列表
						string[] files=Directory.GetFiles(parameter);
						sendstr="";
						for(int i=0;i<files.Length;i++)
						{
							//将实际目录换为虚拟目录
							files[i]=files[i].Replace(textbox.Text,"server:");
							sendstr+=files[i]+"@";
						}
						//去掉最后一个@
						if(sendstr.Length>0)
						{
							sendstr=sendstr.Substring(0,sendstr.Length-1);
						}
						//发送文件名到用户
						sw.WriteLine(sendstr);
						sw.Flush();
						listbox.Items.Add("发送文件名:"+sendstr);
						break;
					case "RETR":
						//下载文件
						try
						{
							//如果文件有问题，构造文件流会产生异常
							fs=new FileStream(parameter,FileMode.Open,FileAccess.Read);
						}
						catch
						{
							fs.Close();
							break;
						}
						// 发送150到用户，意思为服务器文件状态良好
						sw.WriteLine("150");
						sw.Flush();
						listbox.Items.Add("发送:150");
						//发送文件长度
						sw.WriteLine(fs.Length.ToString());
						sw.Flush();
						listbox.Items.Add("发送:"+fs.Length.ToString()+"字节");
						for(int i=0;i<fs.Length;i++)
						{
							netStream.WriteByte((byte)fs.ReadByte());
							netStream.Flush();
						}
						fs.Close();
						break;
					case "QUIT":
						//关闭TCP连接并释放与其关联的所有资源
						client.Close();
						listbox.Items.Add("关闭与该用户的连接");
						return;
				}
			}
			//关闭TCP连接并释放与其关联的所有资源
			client.Close();
		}
	}
}
