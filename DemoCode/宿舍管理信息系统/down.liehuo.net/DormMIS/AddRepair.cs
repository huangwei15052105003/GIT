using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
//Download by http://down.liehuo.net

namespace DormMIS
{
	/// <summary>
	/// AddRepair 的摘要说明。
	/// </summary>
	public class AddRepair : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btSure;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textPerson;
		private System.Windows.Forms.DateTimePicker dateIn;
		private System.Windows.Forms.DateTimePicker dateRepair;
		private System.Windows.Forms.TextBox textRepair;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public AddRepair()
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
			this.textPerson = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dateIn = new System.Windows.Forms.DateTimePicker();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btSure = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.dateRepair = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textRepair = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textRepair);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.dateRepair);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textPerson);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.dateIn);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(384, 192);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "报修信息";
			// 
			// textPerson
			// 
			this.textPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPerson.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textPerson.Location = new System.Drawing.Point(264, 40);
			this.textPerson.Name = "textPerson";
			this.textPerson.Size = new System.Drawing.Size(104, 21);
			this.textPerson.TabIndex = 5;
			this.textPerson.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(208, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "报 修 人";
			// 
			// dateIn
			// 
			this.dateIn.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dateIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dateIn.Location = new System.Drawing.Point(88, 80);
			this.dateIn.Name = "dateIn";
			this.dateIn.Size = new System.Drawing.Size(104, 21);
			this.dateIn.TabIndex = 3;
			// 
			// textDormID
			// 
			this.textDormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDormID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDormID.Location = new System.Drawing.Point(88, 40);
			this.textDormID.Name = "textDormID";
			this.textDormID.Size = new System.Drawing.Size(104, 21);
			this.textDormID.TabIndex = 1;
			this.textDormID.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "报修日期";
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
			this.btSure.Location = new System.Drawing.Point(64, 212);
			this.btSure.Name = "btSure";
			this.btSure.TabIndex = 15;
			this.btSure.Text = "确定";
			this.btSure.Click += new System.EventHandler(this.btSure_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(248, 212);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 16;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// dateRepair
			// 
			this.dateRepair.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dateRepair.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.dateRepair.Location = new System.Drawing.Point(264, 80);
			this.dateRepair.Name = "dateRepair";
			this.dateRepair.Size = new System.Drawing.Size(104, 21);
			this.dateRepair.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(208, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "修理日期";
			// 
			// textRepair
			// 
			this.textRepair.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRepair.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textRepair.Location = new System.Drawing.Point(56, 130);
			this.textRepair.Multiline = true;
			this.textRepair.Name = "textRepair";
			this.textRepair.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRepair.Size = new System.Drawing.Size(312, 50);
			this.textRepair.TabIndex = 9;
			this.textRepair.Text = "";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(32, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "报修情况";
			// 
			// AddRepair
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(400, 246);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btSure);
			this.Controls.Add(this.btClose);
			this.Name = "AddRepair";
			this.Text = "报修单";
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
					sql="insert into repair (dormID,person,repair,DateIn,DateRepair) values ('"+textDormID.Text.Trim()+"','"+textPerson.Text.Trim()+"',"+
						"'"+textRepair.Text.Trim()+"','"+dateIn.Text.Trim()+"','"+dateRepair.Text.Trim()+"')";
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
