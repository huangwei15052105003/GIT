using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//Download by http://www.srcfans.com
namespace MDIApp
{
	/// <summary>
	/// MDIChild ��ժҪ˵����
	/// </summary>
	public class MDIChild : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		public System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MDIChild()
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
				if(components != null)
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItem1.Text = "�ļ���&F��";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.MergeOrder = 3;
			this.menuItem2.Text = "���棨&S��";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6});
			this.menuItem3.MergeOrder = 10;
			this.menuItem3.Text = "�༭��&E��";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "���ƣ�&C��";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.Text = "ճ����&V��";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.Text = "���У�&X��";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(292, 273);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// MDIChild
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1});
			this.Menu = this.mainMenu1;
			this.Name = "MDIChild";
			this.Text = "MDIChild";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{	//�����ĵ����ر��Ӵ���
			SaveFileDialog saveFileDg = new SaveFileDialog ();
			saveFileDg.Filter = "Rich Text Format Files(*.rtf)|*.rtf|All Files(*.*)|*.*";
			if (saveFileDg.ShowDialog () == DialogResult.OK )
			{
				richTextBox1.SaveFile (saveFileDg.FileName );
				this.Text = saveFileDg.FileName ; 
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{	//������ϵͳ������
			richTextBox1.Copy ();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{	//��ϵͳ������ճ��
			richTextBox1.Paste ();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{	//���е�ϵͳ���а�
			richTextBox1.Cut ();
		}
	}
}
