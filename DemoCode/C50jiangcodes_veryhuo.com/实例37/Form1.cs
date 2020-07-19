using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.Net;
using System.IO;

namespace Http
{
	/// <summary>
	/// HTTP�����Ӧ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TextBox textBox1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "URL:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(320, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "����";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.richTextBox1.Location = new System.Drawing.Point(0, 40);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(400, 272);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(48, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(264, 25);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(400, 312);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1,
																		  this.richTextBox1,
																		  this.button1,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Http";
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
		public Stream s;
		public StreamReader r;
		// ����Http���󲢽���HttpӦ��
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(textBox1.Text.Length != 0)
			{
				button1.Enabled = false;
				req = (HttpWebRequest)WebRequest.Create(textBox1.Text);
				res = (HttpWebResponse)req.GetResponse();
				s   = res.GetResponseStream();
				r   = new StreamReader(s);
				richTextBox1.Text = r.ReadToEnd();
				r.Close();
				s.Close();
				res.Close();
				button1.Enabled = true;
			}
		}
	}
}
