using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.IO;

namespace FileInfo
{
	/// <summary>
	/// ��ʾ�ļ���Ϣ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(312, 248);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "��ʾ";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(72, 247);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(224, 25);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 250);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "�ļ���:";
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(408, 240);
			this.listView1.TabIndex = 12;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(408, 280);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1,
																		  this.label1,
																		  this.textBox1,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��ʾ�ļ���Ϣ";
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
		// ��ʾ�ļ���Ϣ��
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;
				System.IO.FileInfo file = new System.IO.FileInfo(openFileDialog1.FileName);
				listView1.Clear();
				listView1.Columns.Add("�ļ���", 100, HorizontalAlignment.Left);
				listView1.Columns.Add("��С", 100, HorizontalAlignment.Left);
				listView1.Columns.Add("������ʱ��", 100, HorizontalAlignment.Left);
				listView1.Columns.Add("����޸�ʱ��", 100, HorizontalAlignment.Left);
				listView1.Columns.Add("·��", 200, HorizontalAlignment.Left);
				string[] str =
					{
						file.Name,
						file.Length.ToString(),
						file.LastAccessTime.ToString(),
						file.LastWriteTime.ToString(),
						file.DirectoryName
					};
				ListViewItem item = new ListViewItem(str);
				listView1.Items.Add(item);
			}
		}
	}
}
