using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestSyncClient
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

		private Socket socket;
		private Thread thread;


		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.Button buttonRequest;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.ListBox listBoxState;
		private System.Windows.Forms.RichTextBox richTextBoxSend;
		private System.Windows.Forms.RichTextBox richTextBoxReceive;
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

			this.richTextBoxSend.Text="";
			this.richTextBoxReceive.Text="";
			this.listBoxState.Items.Clear();
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxState = new System.Windows.Forms.ListBox();
			this.textBoxIP = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.buttonRequest = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
			this.richTextBoxReceive = new System.Windows.Forms.RichTextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "������IP";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "����˿�";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "������Ϣ";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 208);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "������Ϣ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBoxState);
			this.groupBox1.Location = new System.Drawing.Point(200, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "����״̬";
			// 
			// listBoxState
			// 
			this.listBoxState.ItemHeight = 12;
			this.listBoxState.Location = new System.Drawing.Point(8, 16);
			this.listBoxState.Name = "listBoxState";
			this.listBoxState.Size = new System.Drawing.Size(184, 76);
			this.listBoxState.TabIndex = 0;
			// 
			// textBoxIP
			// 
			this.textBoxIP.Location = new System.Drawing.Point(80, 24);
			this.textBoxIP.Name = "textBoxIP";
			this.textBoxIP.ReadOnly = true;
			this.textBoxIP.TabIndex = 2;
			this.textBoxIP.Text = "127.0.0.1";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(80, 72);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.ReadOnly = true;
			this.textBoxPort.TabIndex = 2;
			this.textBoxPort.Text = "6788";
			// 
			// buttonRequest
			// 
			this.buttonRequest.Location = new System.Drawing.Point(48, 296);
			this.buttonRequest.Name = "buttonRequest";
			this.buttonRequest.TabIndex = 3;
			this.buttonRequest.Text = "��������";
			this.buttonRequest.Click += new System.EventHandler(this.buttonRequest_Click);
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(160, 296);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.TabIndex = 3;
			this.buttonSend.Text = "������Ϣ";
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(272, 296);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "�ر�����";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// richTextBoxSend
			// 
			this.richTextBoxSend.Location = new System.Drawing.Point(80, 208);
			this.richTextBoxSend.Name = "richTextBoxSend";
			this.richTextBoxSend.Size = new System.Drawing.Size(320, 80);
			this.richTextBoxSend.TabIndex = 5;
			this.richTextBoxSend.Text = "richTextBoxSend";
			// 
			// richTextBoxReceive
			// 
			this.richTextBoxReceive.Location = new System.Drawing.Point(80, 120);
			this.richTextBoxReceive.Name = "richTextBoxReceive";
			this.richTextBoxReceive.Size = new System.Drawing.Size(320, 80);
			this.richTextBoxReceive.TabIndex = 5;
			this.richTextBoxReceive.Text = "richTextBoxReceive";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(416, 325);
			this.Controls.Add(this.richTextBoxSend);
			this.Controls.Add(this.buttonRequest);
			this.Controls.Add(this.textBoxIP);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.richTextBoxReceive);
			this.Name = "Form1";
			this.Text = "�ͻ���";
			this.groupBox1.ResumeLayout(false);
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

		private void buttonRequest_Click(object sender, System.EventArgs e)
		{
			IPEndPoint server=new IPEndPoint(IPAddress.Parse("127.0.0.1"),6788);
			socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			try
			{
				socket.Connect(server);
			}
			catch
			{
				MessageBox.Show("�����������ʧ�ܣ�");
				return;
			}
			this.buttonRequest.Enabled=false;
			this.listBoxState.Items.Add("����������ӳɹ�");
			thread=new Thread(new ThreadStart(AcceptMessage));
			thread.Start();

		}

		private void AcceptMessage()
		{
			NetworkStream netStream=new NetworkStream(socket);
			while(true)
			{
				try
				{
					byte[] datasize=new byte[4];
					netStream.Read(datasize,0,4);
					int size=System.BitConverter.ToInt32(datasize,0);
					Byte[] message=new byte[size];
					int dataleft=size;
					int start=0;
					while(dataleft>0)
					{
						int recv=netStream.Read(message,start,dataleft);
						start+=recv;
						dataleft-=recv;
					}
					this.richTextBoxReceive.Rtf=System.Text.Encoding.Unicode.GetString(message);
				}
				catch
				{
					break;
				}
			}
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(socket.Connected)
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
					thread.Abort();
				}
				this.listBoxState.Items.Add("�������Ͽ�����");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
			this.buttonRequest.Enabled=true;
		}

		private void buttonSend_Click(object sender, System.EventArgs e)
		{
			string str=this.richTextBoxSend.Rtf;
			int i=str.Length;
			if(i==0)
			{
				return;
			}
			else
			{
				 //��ΪstrΪUnicode���룬ÿ���ַ�ռ2�ֽڣ�����ʵ���ֽ���Ӧ*2
				i*=2;
			}
			byte[] datasize=new byte[4];
			//��32λ����ֵת��Ϊ�ֽ�����
			datasize=System.BitConverter.GetBytes(i); 
			byte[] sendbytes=System.Text.Encoding.Unicode.GetBytes(str);
			try
			{
				NetworkStream netStream=new NetworkStream(socket);
				netStream.Write(datasize,0,4);
				netStream.Write(sendbytes,0,sendbytes.Length);
				netStream.Flush();
				this.richTextBoxSend.Text="";
			}
			catch
			{
				MessageBox.Show("�޷�����!");
			}
		}
	}
}
