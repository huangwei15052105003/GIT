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

namespace FTPClient
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		TcpClient client;
		NetworkStream netStream;
        StreamReader sr;
		StreamWriter sw;

		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonDisConnect;
		private System.Windows.Forms.ListBox listBoxFile;
		private System.Windows.Forms.ListBox listBoxDir;
		private System.Windows.Forms.ListBox listBoxInfo;
		private System.Windows.Forms.Button buttonUpDir;
		private System.Windows.Forms.GroupBox groupBoxDir;
		private System.Windows.Forms.Button buttonDownload;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.GroupBox groupBoxFile;
		private System.Windows.Forms.GroupBox groupBoxInfo;
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
			this.buttonUpDir.Enabled=false;
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
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonDisConnect = new System.Windows.Forms.Button();
			this.listBoxFile = new System.Windows.Forms.ListBox();
			this.groupBoxDir = new System.Windows.Forms.GroupBox();
			this.listBoxDir = new System.Windows.Forms.ListBox();
			this.buttonUpDir = new System.Windows.Forms.Button();
			this.groupBoxFile = new System.Windows.Forms.GroupBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.buttonDownload = new System.Windows.Forms.Button();
			this.groupBoxInfo = new System.Windows.Forms.GroupBox();
			this.listBoxInfo = new System.Windows.Forms.ListBox();
			this.groupBoxDir.SuspendLayout();
			this.groupBoxFile.SuspendLayout();
			this.groupBoxInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(104, 8);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "建立连接";
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonDisConnect
			// 
			this.buttonDisConnect.Location = new System.Drawing.Point(264, 8);
			this.buttonDisConnect.Name = "buttonDisConnect";
			this.buttonDisConnect.TabIndex = 0;
			this.buttonDisConnect.Text = "关闭连接";
			this.buttonDisConnect.Click += new System.EventHandler(this.buttonDisConnect_Click);
			// 
			// listBoxFile
			// 
			this.listBoxFile.ItemHeight = 12;
			this.listBoxFile.Location = new System.Drawing.Point(16, 24);
			this.listBoxFile.Name = "listBoxFile";
			this.listBoxFile.Size = new System.Drawing.Size(200, 100);
			this.listBoxFile.TabIndex = 1;
			this.listBoxFile.SelectedIndexChanged += new System.EventHandler(this.listBoxFile_SelectedIndexChanged);
			// 
			// groupBoxDir
			// 
			this.groupBoxDir.Controls.Add(this.listBoxDir);
			this.groupBoxDir.Controls.Add(this.buttonUpDir);
			this.groupBoxDir.Location = new System.Drawing.Point(16, 40);
			this.groupBoxDir.Name = "groupBoxDir";
			this.groupBoxDir.Size = new System.Drawing.Size(232, 200);
			this.groupBoxDir.TabIndex = 4;
			this.groupBoxDir.TabStop = false;
			this.groupBoxDir.Text = "目录";
			// 
			// listBoxDir
			// 
			this.listBoxDir.ItemHeight = 12;
			this.listBoxDir.Location = new System.Drawing.Point(16, 24);
			this.listBoxDir.Name = "listBoxDir";
			this.listBoxDir.Size = new System.Drawing.Size(200, 136);
			this.listBoxDir.TabIndex = 1;
			this.listBoxDir.SelectedIndexChanged += new System.EventHandler(this.listBoxDir_SelectedIndexChanged);
			// 
			// buttonUpDir
			// 
			this.buttonUpDir.Location = new System.Drawing.Point(72, 168);
			this.buttonUpDir.Name = "buttonUpDir";
			this.buttonUpDir.Size = new System.Drawing.Size(88, 23);
			this.buttonUpDir.TabIndex = 7;
			this.buttonUpDir.Text = "上层目录";
			this.buttonUpDir.Click += new System.EventHandler(this.buttonUpDir_Click);
			// 
			// groupBoxFile
			// 
			this.groupBoxFile.Controls.Add(this.progressBar1);
			this.groupBoxFile.Controls.Add(this.listBoxFile);
			this.groupBoxFile.Controls.Add(this.buttonDownload);
			this.groupBoxFile.Location = new System.Drawing.Point(256, 40);
			this.groupBoxFile.Name = "groupBoxFile";
			this.groupBoxFile.Size = new System.Drawing.Size(232, 200);
			this.groupBoxFile.TabIndex = 5;
			this.groupBoxFile.TabStop = false;
			this.groupBoxFile.Text = "文件";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 136);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(200, 23);
			this.progressBar1.TabIndex = 8;
			// 
			// buttonDownload
			// 
			this.buttonDownload.Location = new System.Drawing.Point(80, 168);
			this.buttonDownload.Name = "buttonDownload";
			this.buttonDownload.Size = new System.Drawing.Size(64, 23);
			this.buttonDownload.TabIndex = 7;
			this.buttonDownload.Text = "下载";
			this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
			// 
			// groupBoxInfo
			// 
			this.groupBoxInfo.Controls.Add(this.listBoxInfo);
			this.groupBoxInfo.Location = new System.Drawing.Point(16, 248);
			this.groupBoxInfo.Name = "groupBoxInfo";
			this.groupBoxInfo.Size = new System.Drawing.Size(472, 96);
			this.groupBoxInfo.TabIndex = 6;
			this.groupBoxInfo.TabStop = false;
			this.groupBoxInfo.Text = "传递情况";
			// 
			// listBoxInfo
			// 
			this.listBoxInfo.ItemHeight = 12;
			this.listBoxInfo.Location = new System.Drawing.Point(8, 16);
			this.listBoxInfo.Name = "listBoxInfo";
			this.listBoxInfo.Size = new System.Drawing.Size(448, 76);
			this.listBoxInfo.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(504, 349);
			this.Controls.Add(this.groupBoxInfo);
			this.Controls.Add(this.groupBoxFile);
			this.Controls.Add(this.groupBoxDir);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.buttonDisConnect);
			this.Name = "Form1";
			this.Text = "FTPClient";
			this.groupBoxDir.ResumeLayout(false);
			this.groupBoxFile.ResumeLayout(false);
			this.groupBoxInfo.ResumeLayout(false);
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

		private void buttonConnect_Click(object sender, System.EventArgs e)
		{
			try
			{
				//与服务器建立连接
				client=new TcpClient("127.0.0.1",21);
			}
			catch
			{
				MessageBox.Show("与服务器连接失败！");
				return;
			}
			netStream=client.GetStream();
			sr=new StreamReader(netStream,System.Text.Encoding.Unicode);
			string str=sr.ReadLine();
			this.listBoxInfo.Items.Add("收到:"+str);
			sw=new StreamWriter(netStream,System.Text.Encoding.Unicode);
			//获取FTP根目录下的子目录和文件列表
			GetDirAndFiles(@"server:\");
		}

		private void buttonUpDir_Click(object sender, System.EventArgs e)
		{
			string path=this.groupBoxDir.Text;
			//找到最后一个\，截去\
			path=path.Substring(0,path.LastIndexOf("\\"));
			//再从右到左找一个\
			int num=path.LastIndexOf("\\");
			//截去\后面的子串
			path=path.Substring(0,num+1);
			GetDirAndFiles(path);
		}

		private void buttonDisConnect_Click(object sender, System.EventArgs e)
		{
			sw.WriteLine("QUIT");
			sw.Flush();
			this.listBoxInfo.Items.Add("发送:QUIT");
			client.Close();
		}

		private void listBoxDir_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listBoxDir.SelectedIndex==-1)
			{
				//获取当前目录下的子目录和文件列表
				GetDirAndFiles(this.groupBoxDir.Text);
			}
			else
			{
				//获取所选择目录下的子目录和文件列表
				GetDirAndFiles(this.listBoxDir.SelectedItem.ToString());
			}
		}

		private void GetDirAndFiles(string path)
		{
			if(this.listBoxDir.SelectedIndex>-1)
			{
				this.groupBoxDir.Text=this.listBoxDir.SelectedItem.ToString();
			}
			else
			{
				this.groupBoxDir.Text=path;
			}
			//-------判断当前目录是否为虚拟根目录---------
			if(path==@"server:\")
			{
				this.buttonUpDir.Enabled=false;
			}
			else
			{
				this.buttonUpDir.Enabled=true;
			}
			//获取目录和文件列表
			sw.WriteLine("LIST "+path);
			sw.Flush();
			this.listBoxInfo.Items.Add("发送:LIST "+path);
			this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
			string str=sr.ReadLine();
			this.listBoxInfo.Items.Add("收到:"+str);
			this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
			if(str=="125")  //表示服务器已经准备好数据，并开始传送
			{
				//获取子目录列表
				this.listBoxDir.Items.Clear();
				str=sr.ReadLine();
				this.listBoxInfo.Items.Add("收到:"+str);
				this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
				string[] strarray;
				//字符串长度为0，说明当前目录下没有子目录
				if(str.Length>0)
				{
					strarray=str.Split('@');
					for(int i=0;i<strarray.Length;i++)
					{
						this.listBoxDir.Items.Add(strarray[i]+"\\");
					}
				}
				//获取文件列表
				this.listBoxFile.Items.Clear();
				str=sr.ReadLine();
				this.listBoxInfo.Items.Add("收到:"+str);
				this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
				//字符串长度为0，说明当前目录下没有文件
				if(str.Length>0)
				{
					strarray=str.Split('@');
					for(int i=0;i<strarray.Length;i++)
					{
						this.listBoxFile.Items.Add(strarray[i]);
					}
				}
				this.buttonDownload.Enabled=false;
			}
		}

		private void buttonDownload_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog myfile=new SaveFileDialog();
			if(myfile.ShowDialog()==DialogResult.OK)
			{
				//重画窗体内的所有控件，使窗体显示完整
				foreach(Control control in this.Controls)
				{
					control.Update();
				}
				//base.OnPaint(new PaintEventArgs(this.CreateGraphics(),this.ClientRectangle));
				string path=this.listBoxFile.SelectedItem.ToString();
				sw.WriteLine("RETR "+path);
				sw.Flush();
				this.listBoxInfo.Items.Add("发送:RETR "+path);
				this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
				string str=sr.ReadLine();
				this.listBoxInfo.Items.Add("收到:"+str);
				this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
				if(str=="150")  //表示服务器文件状态良好
				{
					string str1=sr.ReadLine();
					this.listBoxInfo.Items.Add("文件长度:"+str1+"字节");
					this.listBoxInfo.SelectedIndex=this.listBoxInfo.Items.Count-1;
					int length=Convert.ToInt32(str1);
					this.progressBar1.Minimum=0;
					this.progressBar1.Maximum=length;
					FileStream fs=new FileStream(myfile.FileName,FileMode.Create,FileAccess.Write);
					for(int i=0;i<length;i++)
					{
						fs.WriteByte((byte)netStream.ReadByte());
						fs.Flush();
						this.progressBar1.Value=i;
					}
					fs.Close();
					MessageBox.Show("下载完毕！");
					this.progressBar1.Value=0;
				}
			}
		}

		private void listBoxFile_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.listBoxFile.SelectedIndex==-1)
			{
				this.buttonDownload.Enabled=false;
			}
			else
			{
				this.buttonDownload.Enabled=true;
			}
		}
	}
}
