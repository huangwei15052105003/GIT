using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using System.Net;
using System.IO;

namespace Proxy
{
	/// <summary>
	/// 使用代理服务器。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
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
			this.button2 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(120, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "默认";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(232, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "专门";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// richTextBox1
			//
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.richTextBox1.Location = new System.Drawing.Point(0, 40);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(408, 256);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(408, 296);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1,
																		  this.button2,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Proxy";
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
		public HttpWebRequest req;
		public HttpWebResponse res;
		public WebProxy proxy;
		public Stream s;
		public StreamReader r;
		// 使用默认代理。
		private void button1_Click(object sender, System.EventArgs e)
		{
			button1.Enabled = false;
			proxy = new WebProxy("http://127.0.0.1:8888", true);
			GlobalProxySelection.Select = proxy;
			req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
			res = (HttpWebResponse)req.GetResponse();
			s = res.GetResponseStream();
			r = new StreamReader(s);
			richTextBox1.Text = r.ReadToEnd();
			r.Close();
			s.Close();
			res.Close();
			button1.Enabled = true;
		}
		// 使用专门代理。
		private void button2_Click(object sender, System.EventArgs e)
		{
			button2.Enabled = false;
			proxy = new WebProxy("http://127.0.0.1:8887", true);
			req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
			req.Proxy = proxy;
			res = (HttpWebResponse)req.GetResponse();
			s = res.GetResponseStream();
			r = new StreamReader(s);
			richTextBox1.Text = r.ReadToEnd();
			r.Close();
			s.Close();
			res.Close();
			button2.Enabled = true;
		}
	}
}
