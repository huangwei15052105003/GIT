using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.Odbc;

namespace EX7_4
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Data.Odbc.OdbcDataAdapter odbcDataAdapter1;
		private System.Data.Odbc.OdbcCommand odbcSelectCommand1;
		private System.Data.Odbc.OdbcCommand odbcInsertCommand1;
		private System.Data.Odbc.OdbcConnection odbcConnection1;
		private EX7_4.DataSet1 dataSet11;
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
			//string str="Driver=Microsoft Visual FoxPro Driver;"+
			//	"SourceType=DBC;SourceDB=d:\\mydbc\\test.dbc";
//			string str="Driver=Microsoft Visual FoxPro Driver; SourceType=DBF;SourceDB=d:\\mydbc ";
//			string str="Driver=dBASE Driver; SourceType=DBF;SourceDB=d:\\mydbc ";

//			string str = "MaxBufferSize=2048;DSN=dBASE Files;PageTimeout=5;DefaultDir=D:\\;DBQ=D:\\;DriverId=533";
//
//			OdbcConnection conn=new OdbcConnection(str);
//			string sql="select * from t_tdd";
//			OdbcDataAdapter adapter=new OdbcDataAdapter(sql,conn);
//			DataSet dataset=new DataSet();
//			adapter.Fill(dataset,"t_tdd");
//			this.dataGrid1.DataSource=dataset.Tables["t_tdd"];
//
			

		this.odbcDataAdapter1.Fill(this.dataSet11,"table1");
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
			this.odbcDataAdapter1 = new System.Data.Odbc.OdbcDataAdapter();
			this.odbcSelectCommand1 = new System.Data.Odbc.OdbcCommand();
			this.odbcInsertCommand1 = new System.Data.Odbc.OdbcCommand();
			this.odbcConnection1 = new System.Data.Odbc.OdbcConnection();
			this.dataSet11 = new EX7_4.DataSet1();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.DataSource = this.dataSet11.table1;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(16, 32);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(480, 216);
			this.dataGrid1.TabIndex = 0;
			// 
			// odbcDataAdapter1
			// 
			this.odbcDataAdapter1.InsertCommand = this.odbcInsertCommand1;
			this.odbcDataAdapter1.SelectCommand = this.odbcSelectCommand1;
			this.odbcDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "table1", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("a1", "a1"),
																																																				 new System.Data.Common.DataColumnMapping("a2", "a2"),
																																																				 new System.Data.Common.DataColumnMapping("a3", "a3")})});
			// 
			// odbcSelectCommand1
			// 
			this.odbcSelectCommand1.CommandText = "SELECT a1, a2, a3 FROM table1";
			this.odbcSelectCommand1.Connection = this.odbcConnection1;
			// 
			// odbcInsertCommand1
			// 
			this.odbcInsertCommand1.CommandText = "INSERT INTO table1(a1, a2, a3) VALUES (?, ?, ?)";
			this.odbcInsertCommand1.Connection = this.odbcConnection1;
			this.odbcInsertCommand1.Parameters.Add(new System.Data.Odbc.OdbcParameter("a1", System.Data.Odbc.OdbcType.VarChar, 10, "a1"));
			this.odbcInsertCommand1.Parameters.Add(new System.Data.Odbc.OdbcParameter("a2", System.Data.Odbc.OdbcType.VarChar, 10, "a2"));
			this.odbcInsertCommand1.Parameters.Add(new System.Data.Odbc.OdbcParameter("a3", System.Data.Odbc.OdbcType.VarChar, 10, "a3"));
			// 
			// odbcConnection1
			// 
			this.odbcConnection1.ConnectionString = "SourceType=DBC;DSN=Visual FoxPro Database;BackgroundFetch=Yes;Deleted=Yes;Collate" +
				"=Machine;Exclusive=No;SourceDB=D:\\mydbc\\test.dbc;Null=Yes;UID=";
			// 
			// dataSet11
			// 
			this.dataSet11.DataSetName = "DataSet1";
			this.dataSet11.Locale = new System.Globalization.CultureInfo("zh-CN");
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(544, 286);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
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
	}
}
