using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace EX7_29
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
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
			string sqlstr="select * from sales";
			string connString= "server=localhost;integrated Security=SSPI;database=pubs";
			SqlConnection conn=new SqlConnection(connString);
			SqlDataAdapter adapter=new SqlDataAdapter(sqlstr,conn);
			DataSet ds=new DataSet();
			adapter.Fill(ds,"sales");
			SetColumn();
			this.dataGrid1.SetDataBinding(ds,"sales");
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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(16, 32);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(480, 192);
			this.dataGrid1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(520, 273);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
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
		private void SetColumn()
		{
			//���������Ӧ��
			DataGridTextBoxColumn column1 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn column2 = new	DataGridTextBoxColumn();
			DataGridTextBoxColumn column3 = new DataGridTextBoxColumn();
			//����������Ӧ��Ϣ
			column1.HeaderText = "id";
			column1.MappingName = "stor_id";
			column2.HeaderText = "num";
			column2.MappingName = "ord_num";
			column3.Format = "yyyy��MM��";
			column3.HeaderText = "date";
			column3.MappingName = "ord_date";
			//���������Ӧ��
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName="sales";
			//����������
			tableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[]{column1,column2,column3});
			//��Ӷ�Ӧ��
			this.dataGrid1.TableStyles.Add(tableStyle);
		}

	}
}
