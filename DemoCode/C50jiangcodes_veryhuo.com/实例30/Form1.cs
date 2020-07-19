using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using System.Diagnostics;

namespace ListApp
{
	/// <summary>
	/// 显示系统进程信息。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListView listView1;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			this.button1.Text = "显示";
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
			this.Text = "查看系统进程";
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
		// 显示系统中的进程信息。
		private void button1_Click(object sender, System.EventArgs e)
		{
			Process[] p = Process.GetProcesses();
			listView1.Clear();
			listView1.Columns.Add("进程ID号", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("进程名称", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("进程优先级", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("虚拟内存", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("物理内存", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("总的处理器时间", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("用户处理器时间", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("启动时间", 100, HorizontalAlignment.Left);
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
