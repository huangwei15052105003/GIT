using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace DormMIS
{
	/// <summary>
	/// Student 的摘要说明。
	/// </summary>
	public class Student : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btDel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btQuery;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textSID;
		private System.Windows.Forms.TextBox textName;
		private OleDbConnection oleConnection1 = null;

		public Student()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.oleConnection1 = new OleDbConnection(DormMIS.database.dbConnection.connection);

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.btClose = new System.Windows.Forms.Button();
			this.btDel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textSID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btQuery = new System.Windows.Forms.Button();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btAdd = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.AllowSorting = false;
			this.dataGrid1.AlternatingBackColor = System.Drawing.Color.LightGray;
			this.dataGrid1.BackColor = System.Drawing.Color.DarkGray;
			this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.dataGrid1.CaptionBackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.dataGrid1.CaptionFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.dataGrid1.CaptionForeColor = System.Drawing.Color.Navy;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.ForeColor = System.Drawing.Color.Black;
			this.dataGrid1.GridLineColor = System.Drawing.Color.Black;
			this.dataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
			this.dataGrid1.HeaderBackColor = System.Drawing.Color.Silver;
			this.dataGrid1.HeaderForeColor = System.Drawing.Color.Black;
			this.dataGrid1.LinkColor = System.Drawing.Color.Navy;
			this.dataGrid1.Location = new System.Drawing.Point(8, 88);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.White;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectionBackColor = System.Drawing.Color.Navy;
			this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
			this.dataGrid1.Size = new System.Drawing.Size(528, 192);
			this.dataGrid1.TabIndex = 27;
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(400, 291);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 7;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btDel
			// 
			this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btDel.ForeColor = System.Drawing.Color.Black;
			this.btDel.Location = new System.Drawing.Point(232, 291);
			this.btDel.Name = "btDel";
			this.btDel.TabIndex = 6;
			this.btDel.Text = "退宿";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textName);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textSID);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btQuery);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 10);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(528, 70);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "学生查询";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.Location = new System.Drawing.Point(208, 32);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(80, 21);
			this.textName.TabIndex = 2;
			this.textName.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(168, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "姓  名";
			// 
			// textSID
			// 
			this.textSID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textSID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textSID.Location = new System.Drawing.Point(72, 32);
			this.textSID.Name = "textSID";
			this.textSID.Size = new System.Drawing.Size(80, 21);
			this.textSID.TabIndex = 1;
			this.textSID.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "学  号";
			// 
			// btQuery
			// 
			this.btQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btQuery.ForeColor = System.Drawing.Color.Black;
			this.btQuery.Location = new System.Drawing.Point(440, 32);
			this.btQuery.Name = "btQuery";
			this.btQuery.TabIndex = 4;
			this.btQuery.Text = "查询";
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			// 
			// textDormID
			// 
			this.textDormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDormID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDormID.Location = new System.Drawing.Point(344, 32);
			this.textDormID.Name = "textDormID";
			this.textDormID.Size = new System.Drawing.Size(80, 21);
			this.textDormID.TabIndex = 3;
			this.textDormID.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(304, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "宿舍号";
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.Black;
			this.btAdd.Location = new System.Drawing.Point(68, 291);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 5;
			this.btAdd.Text = "修改";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// Student
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(552, 326);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btDel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btAdd);
			this.Name = "Student";
			this.Text = "学生查询";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		DataSet ds;
		private void btQuery_Click(object sender, System.EventArgs e)
		{
			oleConnection1.Open();
			string sql="select SID as 学号,SName as 姓名,SSex as 性别,class as 班级,dormID as 宿舍号 from student";
			if (textSID.Text.Trim()==""&&textName.Text.Trim()==""&&textDormID.Text.Trim()=="")
				sql=sql;
			else
			{
				if (textSID.Text.Trim()!="")
					sql=sql+" where SID='"+textSID.Text.Trim()+"'";
				else if (textName.Text.Trim()!="")
					sql=sql+" where SName='"+textName.Text.Trim()+"'";
				else
					sql=sql+" where dormID='"+textDormID.Text.Trim()+"'";
			}
			OleDbDataAdapter adp = new OleDbDataAdapter(sql,oleConnection1);
			ds = new DataSet();
			ds.Clear();
			adp.Fill(ds,"student");
			dataGrid1.DataSource = ds.Tables[0].DefaultView;
			dataGrid1.CaptionText = "共有"+ds.Tables[0].Rows.Count+"条记录";
			oleConnection1.Close();
		}

		StudentModify studentModify;
		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource!=null&&dataGrid1.CurrentRowIndex>=0&&dataGrid1[dataGrid1.CurrentCell]!=null)
			{
				studentModify = new StudentModify();
				studentModify.textSID.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim();
				studentModify.textName.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim();
				studentModify.comboSex.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][2].ToString().Trim();
				studentModify.textClass.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][3].ToString().Trim();
				studentModify.textDormID.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][4].ToString().Trim();
				studentModify.ShowDialog();
			}
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource!=null&&dataGrid1.CurrentRowIndex>=0&&dataGrid1[dataGrid1.CurrentCell]!=null)
			{
				oleConnection1.Open();
				string sql="delete * from student where SID='"+ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim()+"'";
				OleDbCommand cmd = new OleDbCommand(sql,oleConnection1);
				cmd.ExecuteNonQuery();
				MessageBox.Show("删除'"+ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim()+"'同学成功","提示");
				oleConnection1.Close();
			} 
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
