using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.Net;
using System.IO;

namespace Proxy
{
	/// <summary>
	/// ʹ�ô����������
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.button1.Text = "Ĭ��";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(232, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "ר��";
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
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}
		// ����˽�б�����
		public HttpWebRequest req;
		public HttpWebResponse res;
		public WebProxy proxy;
		public Stream s;
		public StreamReader r;
		// ʹ��Ĭ�ϴ���
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
		// ʹ��ר�Ŵ���
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
