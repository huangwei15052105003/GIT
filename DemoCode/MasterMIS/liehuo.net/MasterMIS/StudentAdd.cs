using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace MasterMIS
{
	/// <summary>
	/// StudentAdd 的摘要说明。
	/// </summary>
	public class StudentAdd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btClose;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private System.Windows.Forms.TextBox textRemark;
		private System.Windows.Forms.ComboBox comboMajor;
		private System.Windows.Forms.DateTimePicker date1;
		private System.Windows.Forms.ComboBox comboSex;
		private System.Windows.Forms.TextBox textNum;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.TextBox textID;
		private System.Windows.Forms.ComboBox comboTeacher;
		private OleDbCommand oleCommand1 = null;

		public StudentAdd()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.oleConnection1 = new OleDbConnection(MasterMIS.database.dbConnection.connection);
			this.oleCommand1 = new OleDbCommand();
			this.oleCommand1.Connection = this.oleConnection1;

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
			this.textRemark = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.comboMajor = new System.Windows.Forms.ComboBox();
			this.date1 = new System.Windows.Forms.DateTimePicker();
			this.comboSex = new System.Windows.Forms.ComboBox();
			this.textNum = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.textID = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btAdd = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.comboTeacher = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboTeacher);
			this.groupBox1.Controls.Add(this.textRemark);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.comboMajor);
			this.groupBox1.Controls.Add(this.date1);
			this.groupBox1.Controls.Add(this.comboSex);
			this.groupBox1.Controls.Add(this.textNum);
			this.groupBox1.Controls.Add(this.textName);
			this.groupBox1.Controls.Add(this.textID);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 160);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "学生信息";
			// 
			// textRemark
			// 
			this.textRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRemark.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textRemark.Location = new System.Drawing.Point(248, 128);
			this.textRemark.Name = "textRemark";
			this.textRemark.TabIndex = 8;
			this.textRemark.Text = "";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label8.Location = new System.Drawing.Point(184, 128);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 23);
			this.label8.TabIndex = 8;
			this.label8.Text = "备   注";
			// 
			// comboMajor
			// 
			this.comboMajor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboMajor.Location = new System.Drawing.Point(64, 128);
			this.comboMajor.Name = "comboMajor";
			this.comboMajor.Size = new System.Drawing.Size(104, 20);
			this.comboMajor.TabIndex = 7;
			// 
			// date1
			// 
			this.date1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.date1.Location = new System.Drawing.Point(248, 96);
			this.date1.Name = "date1";
			this.date1.Size = new System.Drawing.Size(104, 21);
			this.date1.TabIndex = 6;
			// 
			// comboSex
			// 
			this.comboSex.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboSex.Items.AddRange(new object[] {
														  "男",
														  "女"});
			this.comboSex.Location = new System.Drawing.Point(64, 64);
			this.comboSex.Name = "comboSex";
			this.comboSex.Size = new System.Drawing.Size(104, 20);
			this.comboSex.TabIndex = 3;
			// 
			// textNum
			// 
			this.textNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textNum.Location = new System.Drawing.Point(248, 64);
			this.textNum.Name = "textNum";
			this.textNum.TabIndex = 4;
			this.textNum.Text = "";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.Location = new System.Drawing.Point(248, 32);
			this.textName.Name = "textName";
			this.textName.TabIndex = 2;
			this.textName.Text = "";
			// 
			// textID
			// 
			this.textID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textID.Location = new System.Drawing.Point(64, 32);
			this.textID.Name = "textID";
			this.textID.TabIndex = 1;
			this.textID.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(24, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 23);
			this.label7.TabIndex = 6;
			this.label7.Text = "专业";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label6.Location = new System.Drawing.Point(184, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "出生日期";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.Location = new System.Drawing.Point(184, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "身份证号";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(184, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 3;
			this.label4.Text = "姓   名";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(24, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "导师";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "性别";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(24, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "学号";
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btAdd.Location = new System.Drawing.Point(80, 192);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 1;
			this.btAdd.Text = "确定";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(248, 192);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 2;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// comboTeacher
			// 
			this.comboTeacher.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboTeacher.Location = new System.Drawing.Point(64, 96);
			this.comboTeacher.Name = "comboTeacher";
			this.comboTeacher.Size = new System.Drawing.Size(104, 20);
			this.comboTeacher.TabIndex = 5;
			// 
			// StudentAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(416, 222);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.groupBox1);
			this.Name = "StudentAdd";
			this.Text = "添加学生信息";
			this.Load += new System.EventHandler(this.StudentAdd_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void StudentAdd_Load(object sender, System.EventArgs e)
		{
			try
			{
				oleConnection1.Open();
				string sql1="select MID,MName from majorinfo";
				string sql2="select TID,TName from teacherinfo";
				OleDbDataAdapter adp1=new OleDbDataAdapter(sql1,oleConnection1);
				OleDbDataAdapter adp2=new OleDbDataAdapter(sql2,oleConnection1);
				DataSet ds=new DataSet();
				adp1.Fill(ds,"major");
				adp2.Fill(ds,"teacher");
				comboMajor.DataSource=ds.Tables["major"].DefaultView;
				comboMajor.DisplayMember="MName";
				comboMajor.ValueMember="MID";
				comboTeacher.DataSource=ds.Tables["teacher"].DefaultView;
				comboTeacher.DisplayMember="TName";
				comboTeacher.ValueMember="TID";
				oleConnection1.Close();
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.Message);
			}
		}

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (comboMajor.Text.Trim()==""||textName.Text.Trim()==""||comboSex.Text.Trim()==""||textID.Text.Trim()==""||textNum.Text.Trim()=="")
				MessageBox.Show("请填写完整信息","提示");
			else
			{
				oleConnection1.Open();
				string sql;
				sql="select * from studentinfo where SID='"+textID.Text.ToString()+"' or SNum='"+textNum.Text.ToString()+"'";
				OleDbCommand cmd=new OleDbCommand(sql,oleConnection1);
				if (null==cmd.ExecuteScalar())
				{
					sql="insert into studentinfo (MName,SName,SBirth,SNum,SRemark,SID,SSex,TID) values ('"+comboMajor.Text.Trim()+"',"+
						"'"+textName.Text.Trim()+"','"+date1.Text.Trim()+"','"+textNum.Text.Trim()+"','"+textRemark.Text.Trim()+"',"+
						"'"+textID.Text.Trim()+"','"+comboSex.Text.Trim()+"','"+comboTeacher.SelectedValue.ToString().Trim()+"')";       			
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("学生添加成功","提示");
					clear();
				}
				else
					MessageBox.Show("身份证号或学号相同","提示");
				oleConnection1.Close();
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void clear()
		{
			comboSex.Text = "";
			comboMajor.Text = "";
			comboTeacher.Text = "";
			textName.Text = "";
			textNum.Text = "";
			textID.Text = "";
			textRemark.Text = "";
		}
	}
}
