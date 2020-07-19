using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
//Download by http://down.liehuo.net
namespace MasterMIS
{
	/// <summary>
	/// CourseAdd 的摘要说明。
	/// </summary>
	public class CourseAdd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private System.Windows.Forms.ComboBox comboMajor;
		private System.Windows.Forms.TextBox textDate;
		private System.Windows.Forms.TextBox textRemark;
		private System.Windows.Forms.TextBox textNum;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.TextBox textName;
		private OleDbCommand oleCommand1 = null;

		public CourseAdd()
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
			this.textNum = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textRemark = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textDate = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboMajor = new System.Windows.Forms.ComboBox();
			this.btAdd = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textNum);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textDate);
			this.groupBox1.Controls.Add(this.textName);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.comboMajor);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(368, 200);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "添加课程";
			// 
			// textNum
			// 
			this.textNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textNum.Location = new System.Drawing.Point(248, 80);
			this.textNum.Name = "textNum";
			this.textNum.Size = new System.Drawing.Size(104, 21);
			this.textNum.TabIndex = 20;
			this.textNum.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textRemark);
			this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox2.Location = new System.Drawing.Point(24, 120);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(328, 72);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "课程描述";
			// 
			// textRemark
			// 
			this.textRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRemark.Location = new System.Drawing.Point(16, 24);
			this.textRemark.Multiline = true;
			this.textRemark.Name = "textRemark";
			this.textRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRemark.Size = new System.Drawing.Size(304, 40);
			this.textRemark.TabIndex = 0;
			this.textRemark.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(192, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 17;
			this.label3.Text = "学  分";
			// 
			// textDate
			// 
			this.textDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDate.Location = new System.Drawing.Point(64, 80);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(104, 21);
			this.textDate.TabIndex = 16;
			this.textDate.Text = "";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.Location = new System.Drawing.Point(248, 40);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(103, 21);
			this.textName.TabIndex = 15;
			this.textName.Text = "";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label5.Location = new System.Drawing.Point(184, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 17);
			this.label5.TabIndex = 14;
			this.label5.Text = "课程名称";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label2.Location = new System.Drawing.Point(24, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "专业";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(24, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 17);
			this.label1.TabIndex = 10;
			this.label1.Text = "学时";
			// 
			// comboMajor
			// 
			this.comboMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMajor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboMajor.Location = new System.Drawing.Point(64, 40);
			this.comboMajor.Name = "comboMajor";
			this.comboMajor.Size = new System.Drawing.Size(103, 20);
			this.comboMajor.TabIndex = 13;
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btAdd.Location = new System.Drawing.Point(72, 232);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 1;
			this.btAdd.Text = "确定";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(224, 232);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 2;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// CourseAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(392, 270);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.groupBox1);
			this.Name = "CourseAdd";
			this.Text = "添加课程";
			this.Load += new System.EventHandler(this.CourseAdd_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (comboMajor.Text.Trim()=="" || textName.Text.Trim()=="" || textDate.Text.Trim()=="" || textNum.Text.Trim()=="")
				MessageBox.Show("请填写完整信息","提示");
			else
			{
				oleConnection1.Open();
				string sql;
				sql="select * from courseinfo where MName='"+comboMajor.Text.ToString()+"' and CName='"+textName.Text.Trim()+"'";
				OleDbCommand cmd=new OleDbCommand(sql,oleConnection1);
				if (null==cmd.ExecuteScalar())
				{
					sql="insert into courseinfo (MName,CName,CDate,CNum,CRemark) values ('"+comboMajor.Text.Trim()+"',"+
						"'"+textName.Text.Trim()+"','"+textDate.Text.Trim()+"','"+textNum.Text.Trim()+"','"+textRemark.Text.Trim()+"')";       			
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("课程添加成功","提示");
					clear();
				}
				else
					MessageBox.Show("在同一专业不能添加相同的课程","提示");
				oleConnection1.Close();
			}
		}

		private void clear()
		{
			textDate.Text = "";
			textNum.Text = "";
			textRemark.Text = "";
			textName.Text = "";
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void CourseAdd_Load(object sender, System.EventArgs e)
		{
			try
			{
				oleConnection1.Open();
				string sql="select MID,MName from majorinfo";
				OleDbDataAdapter adp=new OleDbDataAdapter(sql,oleConnection1);
			
				DataSet ds=new DataSet();
				adp.Fill(ds,"major");
				comboMajor.DataSource=ds.Tables["major"].DefaultView;
				comboMajor.DisplayMember="MName";
				comboMajor.ValueMember="MID";
				oleConnection1.Close();
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.Message);
			}
		}
	}
}
