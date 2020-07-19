using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//Download by http://www.srcfans.com
namespace MDIApp
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem11;
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem7,
																					  this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem2,
																					  this.menuItem4,
																					  this.menuItem5});
			this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuItem1.Text = "�ļ���&F��";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MergeOrder = 2;
			this.menuItem2.Text = "�򿪣�&O��";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.MergeOrder = 1;
			this.menuItem3.Text = "�½���&N��";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MergeOrder = 10;
			this.menuItem4.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.MergeOrder = 11;
			this.menuItem5.Text = "�˳���&X��";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 1;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem11,
																					  this.menuItem10});
			this.menuItem7.MergeOrder = 20;
			this.menuItem7.Text = "���ڣ�&W��";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "�����&C��";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.Text = "����ƽ�̣�&T��";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 3;
			this.menuItem10.Text = "����ͼ�꣨&A��";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.MergeOrder = 30;
			this.menuItem6.Text = "���ڣ�&A��";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 2;
			this.menuItem11.Text = "����ƽ�̣�&V��";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "MDIEditor";

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

		private int ChildCount = 0;		//�Ӵ���ı��

		private void menuItem3_Click(object sender, System.EventArgs e)
		{	//�½��ĵ��������µ��Ӵ���
			MDIChild aChild = new MDIChild ();
			
			aChild.Text = "Document" + ++ChildCount;
			aChild.MdiParent = this;
			aChild.Show ();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{	//���ĵ��������µ��Ӵ���
			OpenFileDialog openFileDg = new OpenFileDialog ();
			openFileDg.Filter = "Rich Text Format Files(*.rtf)|*.rtf|All Files(*.*)|*.*";
			if (openFileDg.ShowDialog () == DialogResult.OK )
			{
				MDIChild aChild = new MDIChild ();
				aChild.Text = openFileDg.FileName ;
				aChild.richTextBox1.LoadFile ( openFileDg.FileName );
				aChild.MdiParent = this;
				aChild.Show ();
			}
		
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Application.Exit ();
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{	//���ڲ��
			this.LayoutMdi (MdiLayout.Cascade );
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{	//���ں���ƽ��
			this.LayoutMdi (MdiLayout.TileHorizontal);
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{	//���ں���ƽ��
			this.LayoutMdi (MdiLayout.TileVertical );
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{	//����ͼ������
			this.LayoutMdi (MdiLayout.ArrangeIcons );
		}
	}
}
