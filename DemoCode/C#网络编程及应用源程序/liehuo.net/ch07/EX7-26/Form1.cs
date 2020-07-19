using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace EX7_26
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		DataSet dataset;
		SqlDataAdapter adapter;

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
			string str="server=localhost;Integrated Security=SSPI;database=pubs";
			SqlConnection conn=new SqlConnection(str);
			string sql="select * from employee";
			adapter=new SqlDataAdapter(sql,conn);
			SqlCommandBuilder builder = new SqlCommandBuilder(adapter); 
			adapter.InsertCommand=builder.GetInsertCommand();
			adapter.DeleteCommand=builder.GetDeleteCommand();
			adapter.UpdateCommand=builder.GetUpdateCommand();
			dataset=new DataSet();
			adapter.Fill(dataset,"employee");
			this.dataGrid1.DataSource=dataset.Tables["employee"];
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
			this.dataGrid1.Location = new System.Drawing.Point(16, 8);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(456, 224);
			this.dataGrid1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(504, 273);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
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

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(dataset.HasChanges())
			{
				DialogResult result=MessageBox.Show("数据尚未保存，保存所做的修改吗",
					"提示",MessageBoxButtons.YesNoCancel);
				switch(result)
				{
					case DialogResult.Yes:
						try
						{
							this.adapter.Update(dataset,"employee");
							e.Cancel=false;
						}
						catch(Exception err)
						{
							MessageBox.Show(err.Message,"错误",
								MessageBoxButtons.OK,MessageBoxIcon.Error);
							e.Cancel=true;
						}
						break;
					case DialogResult.No:
						e.Cancel=false;
						break;
					case DialogResult.Cancel:
						e.Cancel=true;
						break;
				}
			}

		}
	}
}
