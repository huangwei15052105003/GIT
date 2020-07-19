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
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		private void SetColumn()
		{
			//产生网格对应列
			DataGridTextBoxColumn column1 = new DataGridTextBoxColumn();
			DataGridTextBoxColumn column2 = new	DataGridTextBoxColumn();
			DataGridTextBoxColumn column3 = new DataGridTextBoxColumn();
			//设置列中相应信息
			column1.HeaderText = "id";
			column1.MappingName = "stor_id";
			column2.HeaderText = "num";
			column2.MappingName = "ord_num";
			column3.Format = "yyyy年MM月";
			column3.HeaderText = "date";
			column3.MappingName = "ord_date";
			//产生网格对应表
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName="sales";
			//向表中添加列
			tableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[]{column1,column2,column3});
			//添加对应表
			this.dataGrid1.TableStyles.Add(tableStyle);
		}

	}
}
