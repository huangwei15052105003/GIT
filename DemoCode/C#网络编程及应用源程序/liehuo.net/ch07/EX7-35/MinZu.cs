using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;

namespace TestInherit
{
	/// <summary>
	/// XueHao 的摘要说明。
	/// </summary>
	public class MinZu : System.Windows.Forms.Form
	{
		
		protected SqlDataAdapter adapter;
		protected DataSet dataset;
		protected DataTable table;
		protected string tableName;
		protected string selectString;
		protected int[] fieldsLength;

		private System.Windows.Forms.Button buttonAppend;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.DataGrid dataGrid1;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MinZu()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//

			GetTableInformation();
			BindData();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonAppend = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonAppend
			// 
			this.buttonAppend.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonAppend.Location = new System.Drawing.Point(120, 264);
			this.buttonAppend.Name = "buttonAppend";
			this.buttonAppend.Size = new System.Drawing.Size(71, 23);
			this.buttonAppend.TabIndex = 5;
			this.buttonAppend.Text = "添加";
			this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonCancel.Location = new System.Drawing.Point(399, 264);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(71, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "放弃";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonDelete.Location = new System.Drawing.Point(213, 264);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(71, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "删除";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonSave.Location = new System.Drawing.Point(306, 264);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(71, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "保存";
			this.buttonSave.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(32, 32);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(520, 208);
			this.dataGrid1.TabIndex = 7;
			// 
			// MinZu
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(600, 317);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.buttonAppend);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonSave);
			this.Name = "MinZu";
			this.Text = "MinZu";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MinZu_Closing);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		//使用virtual表明允许扩充类重写该方法
		public virtual void GetTableInformation()
		{
			tableName="bmmz";
			selectString="select * from bmmz order by 编码";
			fieldsLength=new int[]{2,8};  //编码2，名称8
		}

		//使用virtual表明允许扩充类重写该方法
		public virtual void AddNewRow()
		{
			int num=table.Rows.Count+1;
			DataRow newRow=table.NewRow();
			while(true)
			{
				try
				{
					newRow["编码"]=num.ToString("d"+fieldsLength[0].ToString());
					table.Rows.Add(newRow);
					break;
				}
				catch
				{
					num++;
					continue;
				}
			}
		}

		private void BindData()
		{
			//实际应用中，最好把连接字符串放在一个单独的类中，因为可能有多个模块使用此字符串
			//本例为了简化，直接写在此处了
			string connString="Server=localhost;Integrated Security=SSPI;database=webdevelop";
			SqlConnection conn=new SqlConnection(connString);
			adapter=new SqlDataAdapter(selectString,conn);
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			adapter.DeleteCommand=builder.GetDeleteCommand();
			adapter.InsertCommand=builder.GetInsertCommand();
			adapter.UpdateCommand=builder.GetUpdateCommand();
			dataset=new DataSet();
			adapter.Fill(dataset,tableName);
			table=dataset.Tables[tableName];
			for(int i=0;i<table.Columns.Count;i++)
			{
				table.Columns[i].MaxLength=fieldsLength[i];
			}
			//创建惟一性约束
			UniqueConstraint constraint = new UniqueConstraint
				(new DataColumn[]{table.Columns[0]});
			table.Constraints.Add(constraint);
			this.dataGrid1.SetDataBinding(dataset,tableName);
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				adapter.Update(dataset,tableName);
				MessageBox.Show("保存成功。","恭喜");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"保存信息出错！",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}
		}

		private void buttonAppend_Click(object sender, System.EventArgs e)
		{
			AddNewRow();
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			int num=table.Rows.Count-1;
			if(num>0)
			{
				ArrayList ar=new ArrayList();
				for(int i=num;i>=0;i--)
				{
					if(this.dataGrid1.IsSelected(i))
					{
						ar.Add(i);
					}
				}
				if(ar.Count>0)
				{
					if(MessageBox.Show("确实要删除选中的"+ar.Count.ToString()+"行吗？ 注意：删除后就无法恢复了！","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
					{
						foreach(int i in ar)
						{
							table.Rows[i].Delete();
						}
					}
				}
				else
				{
					MessageBox.Show("请先用鼠标点击左边灰色部分选择要删除的行(可以按住Ctrl键或者Shift键同时用鼠标选中多行)，然后再删除。","提示");
				}
			}

		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show("放弃所作的修改吗？","小心",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
			{
				this.Close();
			}

		}

		private void MinZu_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(dataset.HasChanges())
			{
				if(MessageBox.Show("数据尚未保存，放弃所作的修改吗？","小心",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
				{
					this.Close();
					e.Cancel=false;  //退出
				}
				else
				{
					e.Cancel=true;  //放弃退出
				}
			}
		}
	}
}
