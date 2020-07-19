using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;


namespace EX11_6
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
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
			AcceptMessage();
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "你已经选择了不再接收广播消息，请确认";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(40, 96);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "继续接收";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(152, 96);
			this.button2.Name = "button2";
			this.button2.TabIndex = 2;
			this.button2.Text = "退出系统";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 159);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "提示";
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
		private void AcceptMessage()
		{
			Socket socket=new Socket(AddressFamily.InterNetwork,
				SocketType.Dgram,ProtocolType.Udp);
			IPEndPoint iep = new IPEndPoint(IPAddress.Any,6788);
			socket.Bind(iep);
			EndPoint ep=(EndPoint)iep;
			Byte[] bytes = new byte[1024];
			while(true)
			{
				socket.ReceiveFrom(bytes,ref ep);
				string receiveData=System.Text.Encoding.Unicode.GetString(bytes);
				//注意不能省略receiveData.TrimEnd('\u0000')，否则看不到后面的信息
				receiveData=receiveData.TrimEnd('\u0000')+"\n\n你想继续接收此类消息吗？\n";
				string message="来自"+ep.ToString()+"的消息";
				DialogResult result=
					MessageBox.Show(receiveData,message,MessageBoxButtons.YesNo);
				if(result==DialogResult.No)
				{
					break;
				}
			}
			socket.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			AcceptMessage();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
