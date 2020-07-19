using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using System.IO;

namespace Directorys
{
	/// <summary>
	/// 显示磁盘目录信息。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TreeView treeView1;
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
			this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
			this.button1 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(179, 280);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "显示";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(432, 272);
			this.treeView1.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(432, 312);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.treeView1,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "显示磁盘目录信息";
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
		// 显示磁盘目录信息。
		private void button1_Click(object sender, System.EventArgs e)
		{
			string[] drives = System.IO.Directory.GetLogicalDrives();
			button1.Enabled = false;
			treeView1.Nodes.Clear();
			treeView1.BeginUpdate();
			foreach(string str in drives)
			{
				TreeNode n = new TreeNode(str);
				treeView1.Nodes.Add(n);
			}
			string[] files = System.IO.Directory.GetFiles(drives[1]);
			foreach(string f in files)
			{
				TreeNode sn = new TreeNode(f);
				treeView1.Nodes[1].Nodes.Add(sn);
			}
			string[] subdir = System.IO.Directory.GetDirectories(drives[1]);
			foreach(string sd in subdir)
			{
				TreeNode d = new TreeNode(sd);
				treeView1.Nodes[1].Nodes.Add(d);
			}
			treeView1.EndUpdate();
		}
	}
}
