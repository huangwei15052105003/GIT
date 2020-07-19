using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.IO;

namespace Directorys
{
	/// <summary>
	/// ��ʾ����Ŀ¼��Ϣ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.DirectoryServices.DirectoryEntry directoryEntry1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TreeView treeView1;
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
			this.button1.Text = "��ʾ";
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
			this.Text = "��ʾ����Ŀ¼��Ϣ";
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
		// ��ʾ����Ŀ¼��Ϣ��
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
