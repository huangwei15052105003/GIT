using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����ӵ�����ռ�
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace SocketServer
{
	/// <summary>
	/// �������ˡ�
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button1;
        // �ͻ��˽ڵ�
        private IPEndPoint      client;
        // ��������Socket
        private Socket          server;
        // �������˼����߳�
        private Thread          thdSvr;
        // ����������
        NetworkStream	        stream;
        // д������
        TextWriter		        writer;
        // ��������
        TextReader		        reader;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		// �����߳�
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
					richTextBox1.Text += "���յ�һ������...\n";
					writer.WriteLine("��ӭ���ӵ���������");
					writer.Flush();
					writer.WriteLine("�����ڿ���˵����...");
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
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.button1.Text = "����������";
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
			this.Text = "������";
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
        // ��ʼ�������˵����С�
		private void button1_Click(object sender, System.EventArgs e)
		{
			thdSvr = new Thread(new ThreadStart(this.ThreadServer));
			thdSvr.Start();
			button1.Enabled = false;
		}
	}
}
