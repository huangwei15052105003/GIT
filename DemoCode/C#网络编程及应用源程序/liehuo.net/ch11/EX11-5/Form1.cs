using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;


namespace TestUdpServer
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(80, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "���뷢�͵Ĺ㲥��Ϣ";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 80);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(232, 104);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 216);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "����";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(432, 273);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
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
			//ֻ����UDPЭ�鷢�͹㲥������ProtocolType����ΪUdp
			Socket socket=new Socket(
				AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
			//�����Զ��ṩ�����е�IP�㲥��ַ
			//��̨��������ʱʹ�� IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast,6788);
			//ͬһ̨��������ʱʹ�� IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6788);
			IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),6788);
			//����BroadcastֵΪ1��ʾ�����׽��ַ��͹㲥��Ϣ����ֵĬ��Ϊ0��������
			socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.Broadcast,1);
			//����������ת��Ϊ�ֽ�����
			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(this.textBox1.Text);
			//������������Ϣ
			socket.SendTo(bytes,iep);
			socket.Close();

		}
	}
}
