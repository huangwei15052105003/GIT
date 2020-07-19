using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TalkClient
{
	/// <summary>
	/// 客户端软件。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.RichTextBox richTextBox2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Windows 窗体设计器支持所必需的
			InitializeComponent();
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// groupBox1
			//
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
				this.button1,
				this.label2,
				this.label1,
				this.textBox2,
				this.textBox1});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(440, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = " 客户端 ";
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(328, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "连接";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(216, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 18);
			this.label2.TabIndex = 3;
			this.label2.Text = "端口:";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "服务器IP:";
			//
			// textBox2
			//
			this.textBox2.Location = new System.Drawing.Point(264, 16);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(48, 25);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "34567";
			//
			// textBox1
			//
			this.textBox1.Location = new System.Drawing.Point(88, 17);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(120, 25);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "127.0.0.1";
			//
			// panel1
			//
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.richTextBox2,
                this.richTextBox1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(440, 280);
			this.panel1.TabIndex = 1;
			//
			// richTextBox2
			//
			this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox2.Location = new System.Drawing.Point(0, 224);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox2.Size = new System.Drawing.Size(440, 56);
			this.richTextBox2.TabIndex = 0;
			this.richTextBox2.Text = "";
			this.richTextBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox2_KeyPress);
			//
			// richTextBox1
			//
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(440, 224);
			this.richTextBox1.TabIndex = 10;
			this.richTextBox1.TabStop = false;
			this.richTextBox1.Text = "";
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(440, 328);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.panel1,
                this.groupBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
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
		// 定义私有变量。
		public bool m_bConnected = false;
		public Thread t = null;
		public IPEndPoint p = null;
		public Socket s = null;
		public NetworkStream ns = null;
		public TextReader r = null;
		public TextWriter w = null;
		public void ThreadListen()
		{
			string tmp;
			while(m_bConnected)
			{
				try
				{
					tmp = r.ReadLine();
					if(tmp.Length != 0)
					{
						lock(this)
						{
							richTextBox1.Text = "他说："+tmp+"\n"+richTextBox1.Text;
						}
					}
				}
				catch
				{
					MessageBox.Show("连接断开！！");
				}
			}
			s.Shutdown(SocketShutdown.Both);
			s.Close();
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			p = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
			s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			s.Connect(p);
			if(s.Connected)
			{
				ns = new NetworkStream(s);
				r = new StreamReader(ns);
				w = new StreamWriter(ns);
				t = new Thread(new ThreadStart(this.ThreadListen));
				t.Start();
				m_bConnected = true;
				button1.Enabled = false;
			}
		}
		private void richTextBox2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				if(m_bConnected)
				{
					lock(this)
					{
						try
						{
							richTextBox1.Text = "我说："+richTextBox2.Text+richTextBox1.Text;
							w.WriteLine(richTextBox2.Text);
							w.Flush();
						}
						catch
						{
							MessageBox.Show("连接断开！！");
						}
					}
				}
				else
				{
					MessageBox.Show("对不起，还没有与对方连接，不能通信！");
				}
				richTextBox2.Text = "";
				richTextBox2.Focus();
			}
		}
	}
}
