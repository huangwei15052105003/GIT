using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;


namespace TestUdpServer
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(80, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "输入发送的广播信息";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 80);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(232, 104);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 216);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "发送";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(432, 273);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			//只能用UDP协议发送广播，所以ProtocolType设置为Udp
			Socket socket=new Socket(
				AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			//让其自动提供子网中的IP广播地址
			//多台机器运行时使用 IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast,6788);
			//同一台机器运行时使用 IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6788);
			IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6788);
			//设置Broadcast值为1表示允许套接字发送广播信息，该值默认为0（不允许）
			socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.Broadcast,1);
			//将发送内容转换为字节数组
			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(this.textBox1.Text);
			//向子网发送信息
			socket.SendTo(bytes,iep);
			socket.Close();

		}
	}
}
