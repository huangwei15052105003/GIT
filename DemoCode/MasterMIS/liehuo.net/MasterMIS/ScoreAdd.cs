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
	/// ScoreAdd 的摘要说明。
	/// </summary>
	public class ScoreAdd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textScore;
		private System.Windows.Forms.ComboBox comboSName;
		private System.Windows.Forms.ComboBox comboCName;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btClose;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private OleDbCommand oleCommand1 = null;

		public ScoreAdd()
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
			this.comboCName = new System.Windows.Forms.ComboBox();
			this.comboSName = new System.Windows.Forms.ComboBox();
			this.textScore = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btAdd = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboCName);
			this.groupBox1.Controls.Add(this.comboSName);
			this.groupBox1.Controls.Add(this.textScore);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(272, 144);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "成绩";
			// 
			// comboCName
			// 
			this.comboCName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboCName.Location = new System.Drawing.Point(104, 72);
			this.comboCName.Name = "comboCName";
			this.comboCName.Size = new System.Drawing.Size(152, 20);
			this.comboCName.TabIndex = 7;
			// 
			// comboSName
			// 
			this.comboSName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboSName.Location = new System.Drawing.Point(104, 32);
			this.comboSName.Name = "comboSName";
			this.comboSName.Size = new System.Drawing.Size(152, 20);
			this.comboSName.TabIndex = 6;
			this.comboSName.SelectedIndexChanged += new System.EventHandler(this.comboSName_SelectedIndexChanged);
			// 
			// textScore
			// 
			this.textScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textScore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textScore.Location = new System.Drawing.Point(104, 112);
			this.textScore.Name = "textScore";
			this.textScore.Size = new System.Drawing.Size(152, 21);
			this.textScore.TabIndex = 5;
			this.textScore.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(40, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "成    绩";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(40, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "课程名称";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(40, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "学生姓名";
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btAdd.Location = new System.Drawing.Point(40, 176);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 1;
			this.btAdd.Text = "确定";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(168, 176);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 2;
			this.btClose.Text = "取消";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// ScoreAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(288, 214);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.groupBox1);
			this.Name = "ScoreAdd";
			this.Text = "添加成绩";
			this.Load += new System.EventHandler(this.ScoreAdd_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ScoreAdd_Load(object sender, System.EventArgs e)
		{
			try
			{
				oleConnection1.Open();
				string sql="select SID,SName,MName from studentinfo";
				OleDbDataAdapter adp=new OleDbDataAdapter(sql,oleConnection1);
			
				DataSet ds=new DataSet();
				adp.Fill(ds,"student");
				comboSName.DataSource=ds.Tables["student"].DefaultView;
				comboSName.DisplayMember="SName";
				comboSName.ValueMember="SID";
				oleConnection1.Close();
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.Message);
			}
		}

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (comboSName.Text.Trim()=="" || comboCName.Text.Trim()=="" || textScore.Text.Trim()=="")
				MessageBox.Show("请填写完整信息","提示");
			else
			{
				oleConnection1.Open();
				string sql;
				sql="select * from scoreinfo where SID='"+comboSName.SelectedValue.ToString().Trim()+"' and CName='"+comboCName.Text.Trim()+"'";
				OleDbCommand cmd=new OleDbCommand(sql,oleConnection1);
				if (null==cmd.ExecuteScalar())
				{
					sql="insert into scoreinfo (SID,CName,Score) values ('"+comboSName.SelectedValue.ToString().Trim()+"',"+
						"'"+comboCName.Text.Trim()+"','"+textScore.Text.Trim()+"')";       			
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("成绩添加成功","提示");
					clear();
				}
				else
					MessageBox.Show("在同一学生不能添加相同的课程的成绩","提示");
				oleConnection1.Close();
			}
		}

		private void clear()
		{
			comboSName.Text = "";
			comboCName.Text = "";
			textScore.Text = "";
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void comboSName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				oleConnection1.Open();
				string sql="select CID,CName from courseinfo where MName="+
					"(select MName from studentinfo where studentinfo.MName=courseinfo.MName and SName='"+comboSName.Text.Trim()+"')";
				OleDbDataAdapter adp=new OleDbDataAdapter(sql,oleConnection1);
			
				DataSet ds=new DataSet();
				adp.Fill(ds,"course");
				comboCName.DataSource=ds.Tables["course"].DefaultView;
				comboCName.DisplayMember="CName";
				comboCName.ValueMember="CID";
				oleConnection1.Close();
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.Message);
			}
		}
	}
}
