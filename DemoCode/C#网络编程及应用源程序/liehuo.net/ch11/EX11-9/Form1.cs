using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace EX11_9
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		Socket socket;
		Thread recvThread;
		IPAddress address=IPAddress.Parse("224.100.0.1");
		IPEndPoint multiIPEndPoint;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
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
			this.textBox1.Text="";
			multiIPEndPoint=new IPEndPoint(address,6788);
			socket=new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			IPEndPoint iep=new IPEndPoint(IPAddress.Any,6788);
			EndPoint ep=(EndPoint)iep;
			socket.Bind(ep);
			socket.SetSocketOption(SocketOptionLevel.IP,
				SocketOptionName.AddMembership,new MulticastOption(address));
			recvThread=new Thread(new ThreadStart(ReceiveMessage));
			//设置该线程在后台运行
			recvThread.IsBackground=true;
			recvThread.Start();
			byte[] bytes=System.Text.Encoding.Unicode.GetBytes("#");
			socket.SendTo(bytes,SocketFlags.None,multiIPEndPoint);
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(16, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(296, 200);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "讨论内容";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listBox2);
			this.groupBox2.Location = new System.Drawing.Point(328, 24);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(224, 200);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "参加讨论人员";
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(32, 40);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(264, 172);
			this.listBox1.TabIndex = 2;
			// 
			// listBox2
			// 
			this.listBox2.ItemHeight = 12;
			this.listBox2.Location = new System.Drawing.Point(8, 24);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(208, 160);
			this.listBox2.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 240);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "发言";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 240);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(456, 21);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "textBox1";
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(584, 273);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.groupBox2.ResumeLayout(false);
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
		private void ReceiveMessage()
		{
			EndPoint ep=(EndPoint)multiIPEndPoint;
			byte[] bytes=new byte[1024];
			string str;
			int length;
			while(true)
			{
				length=socket.ReceiveFrom(bytes,ref ep);
				string epAddress=ep.ToString();
				epAddress=epAddress.Substring(0,epAddress.LastIndexOf(":"));
				str=System.Text.Encoding.Unicode.GetString(bytes,0,length);
				switch(str[0])
				{
					case '#':  //进入会议室
						this.listBox1.Items.Add("["+epAddress+"]进入。");
						string str1="&:"+epAddress;
						for(int i=0;i<this.listBox2.Items.Count;i++)
						{
							str1+=":"+this.listBox2.Items[i].ToString();
						}
						byte[] users=System.Text.Encoding.Unicode.GetBytes(str1);
						socket.SendTo(users,SocketFlags.None,multiIPEndPoint);
						break;
					case '@': //退出会议室
						this.listBox1.Items.Add("["+epAddress+"]退出。");
						this.listBox2.Items.Remove(epAddress);
						break;
					case '&': //参加会议人员名单
						string[] strArray=str.Split(':');
						for(int i=1;i<strArray.Length;i++)
						{
							bool isExist=false;
							for(int j=0;j<this.listBox2.Items.Count;j++)
							{
								if(strArray[i]==this.listBox2.Items[j].ToString())
								{
									isExist=true;
									break;
								}
							}
							if(isExist==false)
							{
								this.listBox2.Items.Add(strArray[i]);
							}
						}
						break;
					case '!':  //发言内容
						this.listBox1.Items.Add("["+epAddress+"]说：");
						this.listBox1.Items.Add(str.Substring(1));
						this.listBox1.SelectedIndex=this.listBox1.Items.Count-1;
						break;
				}
			}
		}

		private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar==(char)Keys.Return)
			{
				if(this.textBox1.Text.Trim().Length>0)
				{
					byte[] bytes=System.Text.Encoding.Unicode.GetBytes("!"+this.textBox1.Text);
					this.textBox1.Text="";
					socket.SendTo(bytes,SocketFlags.None,multiIPEndPoint);
				}
			}

		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			byte[] bytes=System.Text.Encoding.Unicode.GetBytes("@");
			socket.SendTo(bytes,SocketFlags.None,multiIPEndPoint);
			recvThread.Abort();
			socket.Close();

		}

	}
}
