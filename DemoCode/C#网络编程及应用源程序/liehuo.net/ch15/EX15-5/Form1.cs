using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestPrintTable
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TestPrintTable.PrintDataTable printDataTable1;
		private TestPrintTable.PrintDataTable printDataTable2;
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
			this.printDataTable2 = new TestPrintTable.PrintDataTable();
			this.SuspendLayout();
			// 
			// printDataTable2
			// 
			this.printDataTable2.ConnectionString = "server=localhost;Integrated Security=SSPI;database=pubs";
			this.printDataTable2.Location = new System.Drawing.Point(32, 16);
			this.printDataTable2.Name = "printDataTable2";
			this.printDataTable2.Size = new System.Drawing.Size(600, 288);
			this.printDataTable2.TabIndex = 0;
			this.printDataTable2.TableName = "sales";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 325);
			this.Controls.Add(this.printDataTable2);
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
	}
}
