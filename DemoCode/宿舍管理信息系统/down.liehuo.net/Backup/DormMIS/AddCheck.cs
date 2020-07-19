using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DormMIS
{
	/// <summary>
	/// AddCheck 的摘要说明。
	/// </summary>
	public class AddCheck : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btSure;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.ComboBox comboState;
		private System.Windows.Forms.TextBox textRemark;
		private System.Windows.Forms.DateTimePicker date1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public AddCheck()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.date1 = new System.Windows.Forms.DateTimePicker();
			this.comboState = new System.Windows.Forms.ComboBox();
			this.textRemark = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btSure = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.date1);
			this.groupBox1.Controls.Add(this.comboState);
			this.groupBox1.Controls.Add(this.textRemark);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 192);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "检查信息";
			// 
			// date1
			// 
			this.date1.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.date1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.date1.Location = new System.Drawing.Point(88, 80);
			this.date1.Name = "date1";
			this.date1.Size = new System.Drawing.Size(136, 21);
			this.date1.TabIndex = 3;
			// 
			// comboState
			// 
			this.comboState.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboState.Items.AddRange(new object[] {
															"优",
															"良",
															"差"});
			this.comboState.Location = new System.Drawing.Point(256, 40);
			this.comboState.Name = "comboState";
			this.comboState.Size = new System.Drawing.Size(88, 20);
			this.comboState.TabIndex = 2;
			// 
			// textRemark
			// 
			this.textRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textRemark.Location = new System.Drawing.Point(48, 136);
			this.textRemark.Multiline = true;
			this.textRemark.Name = "textRemark";
			this.textRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRemark.Size = new System.Drawing.Size(296, 48);
			this.textRemark.TabIndex = 4;
			this.textRemark.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(32, 112);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 10;
			this.label7.Text = "备   注";
			// 
			// textDormID
			// 
			this.textDormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDormID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDormID.Location = new System.Drawing.Point(88, 40);
			this.textDormID.Name = "textDormID";
			this.textDormID.Size = new System.Drawing.Size(88, 21);
			this.textDormID.TabIndex = 1;
			this.textDormID.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(200, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "检查情况";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "检查日期";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(32, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "宿 舍 号";
			// 
			// btSure
			// 
			this.btSure.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btSure.ForeColor = System.Drawing.Color.Black;
			this.btSure.Location = new System.Drawing.Point(48, 216);
			this.btSure.Name = "btSure";
			this.btSure.TabIndex = 9;
			this.btSure.Text = "确定";
			this.btSure.Click += new System.EventHandler(this.btSure_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(232, 216);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 10;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// AddCheck
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(368, 254);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btSure);
			this.Controls.Add(this.btClose);
			this.Name = "AddCheck";
			this.Text = "添加检查";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btSure_Click(object sender, System.EventArgs e)
		{
			if (textDormID.Text.Trim()=="")
				MessageBox.Show("输入宿舍号","提示");
			else
			{
				oleConnection1.Open();
				string sql="select * from dorm where dormID='"+textDormID.Text.Trim()+"'";
				OleDbCommand cmd = new OleDbCommand(sql,oleConnection1);
				if (null==cmd.ExecuteScalar())
					MessageBox.Show("没有该房间号，请重新输入","提示");
				else
				{
					sql="insert into checkinfo (dormID,CDate,CState,CRemark) values ('"+textDormID.Text.Trim()+"','"+date1.Text.Trim()+"',"+
						"'"+comboState.Text.Trim()+"','"+textRemark.Text.Trim()+"')";
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("添加完成","提示");
				}
				oleConnection1.Close();
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
