using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����ӵ������ռ䡣
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SocketClient
{
	/// <summary>
	/// �ͻ��ˡ�
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(168, 272);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "����";
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
			this.Text = "�ͻ���";
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

		private void button1_Click(object sender, System.EventArgs e)
		{
		    // ������IP��ַ
			IPAddress		serIP;
			// �������ڵ�
			IPEndPoint		server;
			// �ͻ���Socket
			Socket			client;
			// �ͻ�������������
			NetworkStream	stream;
			// �ͻ���д������
			TextWriter		writer;
			// �ͻ��˶�������
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
				richTextBox1.Text += "���ӵ�������...\n";
				richTextBox1.Text += reader.ReadLine();
				richTextBox1.Text += reader.ReadLine();
				richTextBox1.Text += "\n";
				writer.WriteLine("��ã����������ܸ����ܺ���ͨѶ��лл��");
				writer.Flush();
				reader.Close();
				writer.Close();
				stream.Close();
				client.Close();
			}
		}
	}
}
