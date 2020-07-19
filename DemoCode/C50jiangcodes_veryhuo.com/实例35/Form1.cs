using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 新添加的命名空间。
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SocketClient
{
	/// <summary>
	/// 客户端。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
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
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(168, 272);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "连接";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// richTextBox1
			//
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(432, 264);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(432, 304);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.richTextBox1,
                this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "客户端";
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
		    // 服务器IP地址
			IPAddress		serIP;
			// 服务器节点
			IPEndPoint		server;
			// 客户端Socket
			Socket			client;
			// 客户端网络数据流
			NetworkStream	stream;
			// 客户端写数据流
			TextWriter		writer;
			// 客户端读数据流
			TextReader		reader;

			serIP = IPAddress.Parse("127.0.0.1");
			server = new IPEndPoint(serIP,34567);
			client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			client.Connect(server);
			if(client.Connected)
			{
				stream = new NetworkStream(client);
				writer = new StreamWriter(stream);
				reader = new StreamReader(stream);
				richTextBox1.Text += "连接到服务器...\n";
				richTextBox1.Text += reader.ReadLine();
				richTextBox1.Text += reader.ReadLine();
				richTextBox1.Text += "\n";
				writer.WriteLine("你好，服务器，很高兴能和你通讯，谢谢。");
				writer.Flush();
				reader.Close();
				writer.Close();
				stream.Close();
				client.Close();
			}
		}
	}
}
