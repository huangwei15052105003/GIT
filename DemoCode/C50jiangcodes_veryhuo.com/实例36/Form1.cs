using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 新添加的命令空间
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace SocketServer
{
	/// <summary>
	/// 服务器端。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;
        // 客户端节点
        private IPEndPoint      client;
        // 服务器端Socket
        private Socket          server;
        // 服务器端监听线程
        private Thread          thdSvr;
        // 网络数据流
        NetworkStream	        stream;
        // 写数据流
        TextWriter		        writer;
        // 读数据流
        TextReader		        reader;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		// 监听线程
		private void ThreadServer()
		{
			client = new IPEndPoint(IPAddress.Any, 34567);
			server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			server.Blocking = true;
			server.Bind(client);
			server.Listen(0);
			while(true)
			{
				Socket t = server.Accept();
				if (t != null)
				{
					stream = new NetworkStream(t);
					writer = new StreamWriter(stream);
					reader = new StreamReader(stream);
					richTextBox1.Text += "接收到一个连接...\n";
					writer.WriteLine("欢迎连接到服务器！");
					writer.Flush();
					writer.WriteLine("您现在可以说话了...");
					writer.Flush();
					richTextBox1.Text += reader.ReadLine();
					richTextBox1.Text += "\n";
					reader.Close();
					writer.Close();
					stream.Close();
					t.Close();
				}
			}
		}
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
			if(server != null)
				server.Close();
			if(!button1.Enabled)
				if(thdSvr != null)
					thdSvr.Abort();
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// richTextBox1
			//
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(416, 256);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(152, 264);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "启动服务器";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(416, 296);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.richTextBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "服务器";
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
        // 开始服务器端的运行。
		private void button1_Click(object sender, System.EventArgs e)
		{
			thdSvr = new Thread(new ThreadStart(this.ThreadServer));
			thdSvr.Start();
			button1.Enabled = false;
		}
	}
}
