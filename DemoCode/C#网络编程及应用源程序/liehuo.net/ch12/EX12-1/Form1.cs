using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace FTPServer
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		TcpListener myListener;

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(24, 24);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(368, 184);
			this.listBox1.TabIndex = 0;
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(256, 232);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.TabIndex = 1;
			this.buttonStart.Text = "��ʼ����";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(352, 232);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "ֹͣ����";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Location = new System.Drawing.Point(16, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 216);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�������";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 232);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "��Ŀ¼";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 232);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(152, 21);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "d:\\myftp";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 261);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.buttonStop);
			this.Name = "Form1";
			this.Text = "FTPServer";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
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

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			this.listBox1.Items.Add("��ʼ��������");
			//�˿ں�6788
			myListener=new TcpListener(IPAddress.Parse("127.0.0.1"),21);
			myListener.Start();
			Thread myThread=new Thread(new ThreadStart(ReceiveData));
			myThread.Start();
		}

		private void ReceiveData()
		{
			TcpClient newClient;
			while(true)
			{
				try
				{
					//�ȴ��û�����
					newClient=myListener.AcceptTcpClient();
				}
				catch
				{
					//��������ֹͣ�����������˳�ʱAcceptTcpClient()������쳣
					myListener.Stop();
					break;
				}
				Receive tp=new Receive(newClient,ref listBox1,ref textBox1);
				Thread thread=new Thread(new ThreadStart(tp.processService));
				thread.Start();
			}
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				myListener.Stop();
			}
			catch
			{}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			buttonStop_Click(null,null);
		}
	}

	public class Receive
	{
		private TcpClient client;
		private ListBox listbox;
		private TextBox textbox;
		public Receive(TcpClient tcpclient,ref ListBox listbox,ref TextBox textbox)
		{
			client=tcpclient;
			this.listbox=listbox;
			this.textbox=textbox;
		}

		public void processService()
		{
			NetworkStream netStream=client.GetStream();
			StreamReader sr=new StreamReader(netStream,System.Text.Encoding.Unicode);
			StreamWriter sw=new StreamWriter(netStream,System.Text.Encoding.Unicode);
			sw.WriteLine("220");
			sw.Flush();
			listbox.Items.Add("���û����ӣ�����:220");
			while(true)
			{
				string str="";
				try
				{
					//���û��������Ͽ�����ʱ�����ܻ�����쳣
					str=sr.ReadLine();
				}
				catch
				{
					listbox.Items.Add("�û�����Ͽ����ӡ�");
					break;
				}
				//���û��������Ͽ�����ʱ����ʹ�������쳣�������ַ���Ҳ��Ϊnull
				if(str==null)
				{
					listbox.Items.Add("�û�����Ͽ����ӡ�");
					break;
				}
				else
				{
					listbox.Items.Add("�յ�:"+str);
				}
				string command="";
				string parameter="";
				int index=str.IndexOf(" ");
				if(index>-1)
				{
					command=str.Substring(0,index);
					parameter=str.Substring(index+1);
					//���û����͵�����Ŀ¼ת��Ϊʵ��Ŀ¼
					parameter=parameter.Replace("server:",textbox.Text);
				}
				else
				{
					command=str;
				}
				FileStream fs=null;
				switch(command)
				{
					case "LIST":
						//LIST��ʾ�û�ϣ���õ�ָ��Ŀ¼�µ���Ŀ¼�б�
						string[] dir=Directory.GetDirectories(parameter);
						string sendstr="";
						for(int i=0;i<dir.Length;i++)
						{
							//��ʵ��Ŀ¼��Ϊ����Ŀ¼
							dir[i]=dir[i].Replace(textbox.Text,"server:");
							sendstr+=dir[i]+"@";
						}
						//ȥ�����һ��@
						if(sendstr.Length>0)
						{
							sendstr=sendstr.Substring(0,sendstr.Length-1);
						}
						// ����125���û�����˼Ϊ������׼����������
						sw.WriteLine("125");
						sw.Flush();
						listbox.Items.Add("����:125");
						//������Ŀ¼���û�
						sw.WriteLine(sendstr);
						sw.Flush();
						listbox.Items.Add("����Ŀ¼��:"+sendstr);
						//�õ��ļ��б�
						string[] files=Directory.GetFiles(parameter);
						sendstr="";
						for(int i=0;i<files.Length;i++)
						{
							//��ʵ��Ŀ¼��Ϊ����Ŀ¼
							files[i]=files[i].Replace(textbox.Text,"server:");
							sendstr+=files[i]+"@";
						}
						//ȥ�����һ��@
						if(sendstr.Length>0)
						{
							sendstr=sendstr.Substring(0,sendstr.Length-1);
						}
						//�����ļ������û�
						sw.WriteLine(sendstr);
						sw.Flush();
						listbox.Items.Add("�����ļ���:"+sendstr);
						break;
					case "RETR":
						//�����ļ�
						try
						{
							//����ļ������⣬�����ļ���������쳣
							fs=new FileStream(parameter,FileMode.Open,FileAccess.Read);
						}
						catch
						{
							fs.Close();
							break;
						}
						// ����150���û�����˼Ϊ�������ļ�״̬����
						sw.WriteLine("150");
						sw.Flush();
						listbox.Items.Add("����:150");
						//�����ļ�����
						sw.WriteLine(fs.Length.ToString());
						sw.Flush();
						listbox.Items.Add("����:"+fs.Length.ToString()+"�ֽ�");
						for(int i=0;i<fs.Length;i++)
						{
							netStream.WriteByte((byte)fs.ReadByte());
							netStream.Flush();
						}
						fs.Close();
						break;
					case "QUIT":
						//�ر�TCP���Ӳ��ͷ����������������Դ
						client.Close();
						listbox.Items.Add("�ر�����û�������");
						return;
				}
			}
			//�ر�TCP���Ӳ��ͷ����������������Դ
			client.Close();
		}
	}
}
