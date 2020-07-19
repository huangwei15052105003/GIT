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
	/// AddCharge 的摘要说明。
	/// </summary>
	public class AddCharge : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DateTimePicker date1;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btSure;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBuyer;
		private System.Windows.Forms.TextBox textMoney;
		private System.Windows.Forms.TextBox textElec;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public AddCharge()
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
			this.textDormID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btSure = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.textBuyer = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textMoney = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textElec = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textMoney);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textElec);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textBuyer);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.date1);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 160);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "水电信息";
			// 
			// date1
			// 
			this.date1.CalendarFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.date1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.date1.Location = new System.Drawing.Point(88, 120);
			this.date1.Name = "date1";
			this.date1.Size = new System.Drawing.Size(136, 21);
			this.date1.TabIndex = 3;
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
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "缴费日期";
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
			this.btSure.Location = new System.Drawing.Point(48, 184);
			this.btSure.Name = "btSure";
			this.btSure.TabIndex = 12;
			this.btSure.Text = "确定";
			this.btSure.Click += new System.EventHandler(this.btSure_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(232, 184);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 13;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// textBuyer
			// 
			this.textBuyer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBuyer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBuyer.Location = new System.Drawing.Point(248, 40);
			this.textBuyer.Name = "textBuyer";
			this.textBuyer.Size = new System.Drawing.Size(88, 21);
			this.textBuyer.TabIndex = 5;
			this.textBuyer.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(192, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "购 买 人";
			// 
			// textMoney
			// 
			this.textMoney.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textMoney.Location = new System.Drawing.Point(248, 80);
			this.textMoney.Name = "textMoney";
			this.textMoney.ReadOnly = true;
			this.textMoney.Size = new System.Drawing.Size(88, 21);
			this.textMoney.TabIndex = 9;
			this.textMoney.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(192, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "价   钱";
			// 
			// textElec
			// 
			this.textElec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textElec.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textElec.Location = new System.Drawing.Point(88, 80);
			this.textElec.Name = "textElec";
			this.textElec.Size = new System.Drawing.Size(88, 21);
			this.textElec.TabIndex = 7;
			this.textElec.Text = "";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(32, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "购买电量";
			// 
			// AddCharge
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(368, 222);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btSure);
			this.Controls.Add(this.btClose);
			this.Name = "AddCharge";
			this.Text = "水电缴费";
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
					textMoney.Text = Convert.ToString(Convert.ToDouble(textElec.Text.Trim())*0.45);
					sql="insert into charge (dormID,MDate,EBuy,CPerson,CMoney) values ('"+textDormID.Text.Trim()+"','"+date1.Text.Trim()+"',"+
						"'"+textElec.Text.Trim()+"','"+textBuyer.Text.Trim()+"','"+textMoney.Text.Trim()+"')";
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
