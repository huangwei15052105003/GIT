using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.Diagnostics;

namespace ListApp
{
	/// <summary>
	/// ��ʾϵͳ������Ϣ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView listView1;
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(183, 288);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "��ʾ";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(440, 280);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(440, 322);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�鿴ϵͳ����";
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
		// ��ʾϵͳ�еĽ�����Ϣ��
		private void button1_Click(object sender, System.EventArgs e)
		{
			Process[] p = Process.GetProcesses();
			listView1.Clear();
			listView1.Columns.Add("����ID��", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("��������", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("�������ȼ�", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("�����ڴ�", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("�����ڴ�", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("�ܵĴ�����ʱ��", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("�û�������ʱ��", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("����ʱ��", 100, HorizontalAlignment.Left);
			foreach(Process i in p)
			{
				string[] str =
					{
						i.Id.ToString(),
						i.ProcessName,
						i.BasePriority.ToString(),
						i.VirtualMemorySize.ToString(),
						i.WorkingSet.ToString(),
						i.TotalProcessorTime.ToString(),
						i.UserProcessorTime.ToString(),
						i.StartTime.ToString()
					};
				ListViewItem n = new ListViewItem(str);
				listView1.Items.Add(n);
			}
		}
	}
}
