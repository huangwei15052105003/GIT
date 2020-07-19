using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;


namespace EX9_1
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonView;
		private System.Windows.Forms.ListBox listBox1;
		private System.Diagnostics.Process myProcess;
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
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonView = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.myProcess = new System.Diagnostics.Process();
			this.SuspendLayout();
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(24, 56);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(128, 23);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "��ʼ���±�";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(24, 120);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(128, 23);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "ֹͣ���±�";
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonView
			// 
			this.buttonView.Location = new System.Drawing.Point(24, 176);
			this.buttonView.Name = "buttonView";
			this.buttonView.Size = new System.Drawing.Size(128, 23);
			this.buttonView.TabIndex = 2;
			this.buttonView.Text = "�۲����н���";
			this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(208, 24);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(208, 220);
			this.listBox1.TabIndex = 3;
			// 
			// myProcess
			// 
			this.myProcess.StartInfo.FileName = "Notepad.exe";
			this.myProcess.SynchronizingObject = this;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(448, 273);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.buttonView);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.buttonStart);
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

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			//����Notepad.exe���̡�
			myProcess.Start();

		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			Process[] myProcesses;
			//�����µ�Process��������飬����������ָ���Ľ�
			//�����ƣ�Notepad�������н�����Դ�������
			myProcesses = Process.GetProcessesByName("Notepad"); 
			foreach (Process instance in myProcesses)
			{
				//������ֹ��ǰ�߳�ǰ�ȴ��ĺ�����
				instance.WaitForExit(1000);
				instance.CloseMainWindow();
			}

		}

		private void buttonView_Click(object sender, System.EventArgs e)
		{
			this.listBox1.Items.Clear();
			Process[] processes;
			//����Process���͵����飬����������ϵͳ�����н��������
			processes = Process.GetProcesses();
			foreach (Process p in processes)
			{
				//��ÿ���������ͽ��̿�ʼʱ�����listBox1��
				this.listBox1.Items.Add(p.ProcessName+" "+p.StartTime.ToShortTimeString());
			}

		}
	}
}
