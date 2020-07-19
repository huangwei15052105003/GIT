using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace testAsyncClient
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Socket clientSocket;
		private const int dataSize=1024;
		private byte[] data=new byte[dataSize];

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listBoxState;
		private System.Windows.Forms.Button buttonRequest;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.RichTextBox richTextBoxReceive;
		private System.Windows.Forms.RichTextBox richTextBoxSend;
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
			this.textBoxName.Text=Dns.GetHostName(); //��ȡ���ؼ����������
			this.textBoxPort.Text="6788";
			this.listBoxState.Items.Clear();
			this.richTextBoxReceive.Text="";
			this.richTextBoxSend.Text="";

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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxState = new System.Windows.Forms.ListBox();
			this.richTextBoxReceive = new System.Windows.Forms.RichTextBox();
			this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
			this.buttonRequest = new System.Windows.Forms.Button();
			this.buttonSend = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "����������";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "����˿�";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "������Ϣ";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 216);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "������Ϣ";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(88, 24);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "textBoxName";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(88, 72);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.TabIndex = 1;
			this.textBoxPort.Text = "textBoxPort";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBoxState);
			this.groupBox1.Location = new System.Drawing.Point(208, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 104);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "����״̬";
			// 
			// listBoxState
			// 
			this.listBoxState.ItemHeight = 12;
			this.listBoxState.Location = new System.Drawing.Point(8, 16);
			this.listBoxState.Name = "listBoxState";
			this.listBoxState.Size = new System.Drawing.Size(240, 76);
			this.listBoxState.TabIndex = 3;
			// 
			// richTextBoxReceive
			// 
			this.richTextBoxReceive.Location = new System.Drawing.Point(88, 120);
			this.richTextBoxReceive.Name = "richTextBoxReceive";
			this.richTextBoxReceive.Size = new System.Drawing.Size(376, 80);
			this.richTextBoxReceive.TabIndex = 3;
			this.richTextBoxReceive.Text = "richTextBoxReceive";
			// 
			// richTextBoxSend
			// 
			this.richTextBoxSend.Location = new System.Drawing.Point(88, 208);
			this.richTextBoxSend.Name = "richTextBoxSend";
			this.richTextBoxSend.Size = new System.Drawing.Size(376, 80);
			this.richTextBoxSend.TabIndex = 3;
			this.richTextBoxSend.Text = "richTextBoxSend";
			// 
			// buttonRequest
			// 
			this.buttonRequest.Location = new System.Drawing.Point(72, 304);
			this.buttonRequest.Name = "buttonRequest";
			this.buttonRequest.TabIndex = 4;
			this.buttonRequest.Text = "��������";
			this.buttonRequest.Click += new System.EventHandler(this.buttonRequest_Click);
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(192, 304);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.TabIndex = 4;
			this.buttonSend.Text = "������Ϣ";
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(312, 304);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 4;
			this.buttonStop.Text = "�ر�����";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(480, 333);
			this.Controls.Add(this.buttonRequest);
			this.Controls.Add(this.richTextBoxReceive);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.richTextBoxSend);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.buttonStop);
			this.Name = "Form1";
			this.Text = "Form1";
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
			
			IPAddress myIP;
			IPEndPoint iep;
			try
			{
				IPHostEntry myHost=new IPHostEntry();
				myHost=Dns.GetHostByName(this.textBoxName.Text);
				myIP=IPAddress.Parse(myHost.AddressList[0].ToString());
				iep=new IPEndPoint(myIP,Int32.Parse(this.textBoxPort.Text));
			}
			catch
			{
				MessageBox.Show("������ķ���������˿ںŸ�ʽ����ȷ�����������룡");
				return;
			}
			Socket socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			socket.BeginConnect(iep,new AsyncCallback(ConnectServer),socket);
		}

		private void ConnectServer(IAsyncResult ar)
		{
			clientSocket=(Socket)ar.AsyncState;
			try
			{
				clientSocket.EndConnect(ar);
				this.listBoxState.Items.Add("�������"+clientSocket.RemoteEndPoint.ToString()+"���ӳɹ���");
				clientSocket.BeginReceive(data,0,dataSize,SocketFlags.None,new AsyncCallback(ReceiveData),clientSocket);
			}
			catch
			{
				MessageBox.Show("�����������ʧ�ܣ�");
			}
		}

		private void ReceiveData(IAsyncResult ar)
		{
			try
			{
				Socket server=(Socket)ar.AsyncState;
				int receiveDataLength=server.EndReceive(ar);
				string str=System.Text.Encoding.Unicode.GetString(data,0,receiveDataLength);
				this.richTextBoxReceive.Text=str;
			}
			catch
			{}

		}

		private void buttonSend_Click(object sender, System.EventArgs e)
		{
			try
			{
				byte[] message=System.Text.Encoding.Unicode.GetBytes(this.richTextBoxSend.Text);
				this.richTextBoxSend.Clear();
				clientSocket.BeginSend(message,0,message.Length,SocketFlags.None,new AsyncCallback(SendData),clientSocket);
			}
			catch
			{
				MessageBox.Show("��δ��������������ӣ�����ʧ�ܣ�");
			}
		}

		private void SendData(IAsyncResult ar)
		{
			Socket socket=(Socket)ar.AsyncState;
			int send=socket.EndSend(ar);
			socket.BeginReceive(data,0,dataSize,SocketFlags.None,new AsyncCallback(ReceiveData),socket);
		}


		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				clientSocket.Close();
				this.listBoxState.Items.Add("��������Ͽ�����!");
			}
			catch
			{
				MessageBox.Show("������δ��ʼ���Ͽ���Ч��");
			}

		}

	}
}
