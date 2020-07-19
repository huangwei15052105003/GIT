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

namespace TestAsyncServer
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Socket serverSocket,clientSocket;
		private const int dataSize=1024;
		private byte[] data=new byte[dataSize];

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox listBoxState;
		private System.Windows.Forms.RichTextBox richTextBoxReceive;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.GroupBox groupBox2;
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
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listBoxState = new System.Windows.Forms.ListBox();
			this.richTextBoxReceive = new System.Windows.Forms.RichTextBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "����������";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "�����˿�";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(88, 32);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.TabIndex = 1;
			this.textBoxName.Text = "textBoxName";
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(88, 80);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.TabIndex = 1;
			this.textBoxPort.Text = "textBoxPort";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBoxState);
			this.groupBox1.Location = new System.Drawing.Point(224, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "������״̬";
			// 
			// listBoxState
			// 
			this.listBoxState.ItemHeight = 12;
			this.listBoxState.Location = new System.Drawing.Point(8, 16);
			this.listBoxState.Name = "listBoxState";
			this.listBoxState.Size = new System.Drawing.Size(184, 76);
			this.listBoxState.TabIndex = 0;
			// 
			// richTextBoxReceive
			// 
			this.richTextBoxReceive.Location = new System.Drawing.Point(8, 24);
			this.richTextBoxReceive.Name = "richTextBoxReceive";
			this.richTextBoxReceive.ReadOnly = true;
			this.richTextBoxReceive.Size = new System.Drawing.Size(392, 96);
			this.richTextBoxReceive.TabIndex = 3;
			this.richTextBoxReceive.Text = "richTextBoxReceive";
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(120, 248);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.TabIndex = 4;
			this.buttonStart.Text = "��ʼ����";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(256, 248);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 4;
			this.buttonStop.Text = "ֹͣ����";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.richTextBoxReceive);
			this.groupBox2.Location = new System.Drawing.Point(16, 112);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 128);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "������Ϣ";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(448, 285);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxPort);
			this.Controls.Add(this.buttonStop);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
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

		private void buttonStart_Click(object sender, System.EventArgs e)
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
			this.listBoxState.Items.Add("��ʼ����...");
			this.buttonStart.Enabled=false;
			serverSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			serverSocket.Bind(iep);
			serverSocket.Listen(10);
			serverSocket.BeginAccept(new AsyncCallback(AcceptConnection),serverSocket);
		}

		private void AcceptConnection(IAsyncResult ar)
		{
			Socket oldServer=(Socket)ar.AsyncState;
			//�첽���մ�������ӣ��������µ�Socket������Զ������ͨ��
			clientSocket=oldServer.EndAccept(ar);
			this.listBoxState.Items.Add("��ͻ�"+clientSocket.RemoteEndPoint.ToString()+"�������ӡ�");
			byte[] message=System.Text.Encoding.Unicode.GetBytes("�ͻ���ã�");
			clientSocket.BeginSend(message,0,message.Length,SocketFlags.None,new AsyncCallback(SendData),clientSocket);
		}


		private void SendData(IAsyncResult ar)
		{
			Socket client=(Socket)ar.AsyncState;
			try
			{
				clientSocket.EndSend(ar);
				client.BeginReceive(data,0,dataSize,SocketFlags.None,new AsyncCallback(ReceiveData),client);
			}
			catch
			{
				client.Close();
				this.listBoxState.Items.Add("�ͻ��ѹر����ӣ��ȴ��¿ͻ�����");
				serverSocket.BeginAccept(new AsyncCallback(AcceptConnection),serverSocket);
			}
		}

		private void ReceiveData(IAsyncResult ar)
		{
			Socket client=(Socket)ar.AsyncState;
			try
			{
				//������ȡ����ö�ȡ���ֽ���
				int receiveDataLength=client.EndReceive(ar);
				string str=System.Text.Encoding.Unicode.GetString(data,0,receiveDataLength);
				this.richTextBoxReceive.Text=str;
				byte[] message=System.Text.Encoding.Unicode.GetBytes("�������յ���Ϣ��"+str);
				clientSocket.BeginSend(message,0,message.Length,SocketFlags.None,new AsyncCallback(SendData),clientSocket);
			}
			catch
			{
				client.Close();
				this.listBoxState.Items.Add("�ͻ��ѹر����ӣ��ȴ��¿ͻ�����");
				serverSocket.BeginAccept(new AsyncCallback(AcceptConnection),serverSocket);
			}
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				serverSocket.Close();
				this.listBoxState.Items.Add("ֹͣ����!");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"����");
			}
		}
	}
}
