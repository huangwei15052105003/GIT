using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DormMIS
{
	/// <summary>
	/// AddDorm 的摘要说明。
	/// </summary>
	public class AddDorm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox textPhone;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.TextBox textBed;
		private System.Windows.Forms.TextBox textDesk;
		private System.Windows.Forms.TextBox textChair;
		private System.Windows.Forms.TextBox textRemark;
		private System.Windows.Forms.TextBox textMoney;
		private System.Windows.Forms.Label label7;
		private OleDbConnection oleConnection1 = null;

		public AddDorm()
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
			this.btClose = new System.Windows.Forms.Button();
			this.btAdd = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textMoney = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textRemark = new System.Windows.Forms.TextBox();
			this.textChair = new System.Windows.Forms.TextBox();
			this.textDesk = new System.Windows.Forms.TextBox();
			this.textBed = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.textPhone = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(216, 272);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 9;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.Black;
			this.btAdd.Location = new System.Drawing.Point(64, 272);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 8;
			this.btAdd.Text = "确定";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textMoney);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textRemark);
			this.groupBox1.Controls.Add(this.textChair);
			this.groupBox1.Controls.Add(this.textDesk);
			this.groupBox1.Controls.Add(this.textBed);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.textPhone);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 248);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "宿舍信息";
			// 
			// textMoney
			// 
			this.textMoney.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textMoney.Location = new System.Drawing.Point(88, 80);
			this.textMoney.Name = "textMoney";
			this.textMoney.Size = new System.Drawing.Size(88, 21);
			this.textMoney.TabIndex = 3;
			this.textMoney.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(40, 80);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 10;
			this.label7.Text = "住宿费";
			// 
			// textRemark
			// 
			this.textRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textRemark.Location = new System.Drawing.Point(40, 176);
			this.textRemark.Multiline = true;
			this.textRemark.Name = "textRemark";
			this.textRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRemark.Size = new System.Drawing.Size(288, 56);
			this.textRemark.TabIndex = 7;
			this.textRemark.Text = "";
			// 
			// textChair
			// 
			this.textChair.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textChair.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textChair.Location = new System.Drawing.Point(88, 120);
			this.textChair.Name = "textChair";
			this.textChair.Size = new System.Drawing.Size(88, 21);
			this.textChair.TabIndex = 5;
			this.textChair.Text = "";
			// 
			// textDesk
			// 
			this.textDesk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDesk.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDesk.Location = new System.Drawing.Point(240, 120);
			this.textDesk.Name = "textDesk";
			this.textDesk.Size = new System.Drawing.Size(88, 21);
			this.textDesk.TabIndex = 6;
			this.textDesk.Text = "";
			// 
			// textBed
			// 
			this.textBed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBed.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textBed.Location = new System.Drawing.Point(240, 80);
			this.textBed.Name = "textBed";
			this.textBed.Size = new System.Drawing.Size(88, 21);
			this.textBed.TabIndex = 4;
			this.textBed.Text = "";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label6.Location = new System.Drawing.Point(40, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "备  注";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(192, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "桌子数";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(40, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "椅子数";
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
			// textPhone
			// 
			this.textPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPhone.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textPhone.Location = new System.Drawing.Point(240, 40);
			this.textPhone.Name = "textPhone";
			this.textPhone.Size = new System.Drawing.Size(88, 21);
			this.textPhone.TabIndex = 2;
			this.textPhone.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(192, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "床位数";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(192, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "电  话";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(40, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "宿舍号";
			// 
			// AddDorm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(360, 310);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.groupBox1);
			this.Name = "AddDorm";
			this.Text = "添加宿舍";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (textDormID.Text.Trim()==""||textBed.Text.Trim()=="")
				MessageBox.Show("填写完整信息","提示");
			else
			{
				oleConnection1.Open();
				string sql = "select * from dorm where dormID='"+textDormID.Text.Trim()+"'";
				OleDbCommand cmd = new OleDbCommand(sql,oleConnection1);
				if (null == cmd.ExecuteScalar())
				{
					sql = "insert into dorm values ('"+textDormID.Text.Trim()+"','"+textPhone.Text.Trim()+"','"+textMoney.Text.Trim()+"',"+
						""+textBed.Text.Trim()+","+textChair.Text.Trim()+","+textDesk.Text.Trim()+",'"+textRemark.Text.Trim()+"')";
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("添加成功","提示");
					clear();
				}
				else
					MessageBox.Show("宿舍号重复","提示");
				oleConnection1.Close();
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void clear()
		{
			textDormID.Text="";
			textPhone.Text="";
			textMoney.Text="";
			textBed.Text="";
			textChair.Text="";
			textDesk.Text="";
			textRemark.Text="";
		}
	}
}
