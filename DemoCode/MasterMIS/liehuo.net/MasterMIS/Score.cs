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
	/// Score 的摘要说明。
	/// </summary>
	public class Score : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btDel;
		private System.Windows.Forms.Button btModify;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private OleDbConnection oleConnection2 = null;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label label2;
		private OleDbCommand oleCommand1 = null;

		public Score()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.oleConnection1 = new OleDbConnection(MasterMIS.database.dbConnection.connection);
			this.oleConnection2 = new OleDbConnection(MasterMIS.database.dbConnection.connection);
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
			this.btClose = new System.Windows.Forms.Button();
			this.btDel = new System.Windows.Forms.Button();
			this.btModify = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(440, 270);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 24;
			this.btClose.Text = "退出";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btDel
			// 
			this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btDel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btDel.Location = new System.Drawing.Point(312, 270);
			this.btDel.Name = "btDel";
			this.btDel.TabIndex = 23;
			this.btDel.Text = "删除";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// btModify
			// 
			this.btModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btModify.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btModify.Location = new System.Drawing.Point(184, 270);
			this.btModify.Name = "btModify";
			this.btModify.TabIndex = 22;
			this.btModify.Text = "修改";
			this.btModify.Click += new System.EventHandler(this.btModify_Click);
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
			this.dataGrid1.Location = new System.Drawing.Point(144, 46);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.White;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectionBackColor = System.Drawing.Color.Navy;
			this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
			this.dataGrid1.Size = new System.Drawing.Size(408, 208);
			this.dataGrid1.TabIndex = 21;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(144, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(183, 24);
			this.label1.TabIndex = 20;
			this.label1.Text = "学 生 成 绩 列 表";
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 46);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(128, 250);
			this.treeView1.TabIndex = 25;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("楷体_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 24);
			this.label2.TabIndex = 26;
			this.label2.Text = "专业/课程";
			// 
			// Score
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(562, 306);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btDel);
			this.Controls.Add(this.btModify);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.label1);
			this.Name = "Score";
			this.Text = "学生成绩";
			this.Load += new System.EventHandler(this.Score_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		DataSet ds;
		private void Score_Load(object sender, System.EventArgs e)
		{
			OleDbCommand cmd1,cmd2;
			cmd1=new OleDbCommand("",oleConnection1);
			cmd2=new OleDbCommand("",oleConnection2);

			OleDbDataReader rd1,rd2;
			string sql;
			sql="select MName from majorinfo";
			cmd1.CommandText=sql;
			oleConnection1.Open();
			
			rd1=cmd1.ExecuteReader();
			while (rd1.Read())
			{
				TreeNode node=new TreeNode();
				node.Text=rd1.GetString(0).ToString();
				treeView1.Nodes.Add(node);
				oleConnection2.Open();
				sql="select CID,CName from courseinfo where MName='"+node.Text+"' order by CName desc";
				cmd2.CommandText=sql;
				rd2=cmd2.ExecuteReader();
				while (rd2.Read())
				{
					TreeNode node1=new TreeNode();
					node1.Text=rd2.GetString(1);
					node1.Tag=rd2.GetValue(0);
					node.Nodes.Add(node1);
				}
				rd2.Close();
				oleConnection2.Close();
			}
			rd1.Close();
			oleConnection1.Close();
		}

		ScoreModify scoreModify;
		private void btModify_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource != null || dataGrid1[dataGrid1.CurrentCell] != null)
			{
				scoreModify = new ScoreModify();
				scoreModify.textSName.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim();
				scoreModify.textCName.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim();
				scoreModify.textScore.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][2].ToString().Trim();
				scoreModify.Tag=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][3].ToString().Trim();
				scoreModify.ShowDialog();
			}
			else
				MessageBox.Show("没有指定成绩信息！","提示");
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.CurrentRowIndex>=0 && dataGrid1.DataSource != null && dataGrid1[dataGrid1.CurrentCell] != null)
			{
				string sql="delete * from scoreinfo where RID="+ds.Tables["score"].Rows[dataGrid1.CurrentCell.RowNumber][3].ToString().Trim()+"";
				oleConnection1.Open();
				oleCommand1.CommandText = sql;
				oleCommand1.ExecuteNonQuery();
				MessageBox.Show("删除'"+ds.Tables["score"].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim()+"'成绩成功","提示");
				oleConnection1.Close();
			}
			else
				MessageBox.Show("没有指定成绩信息！","提示");
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			string sql="";
			OleDbDataAdapter adp=new OleDbDataAdapter(sql,oleConnection1);
			ds=new DataSet();
			ds.Clear();
			oleConnection1.Open();
			if (e.Node.Tag!=null)
			{
				sql="select distinct (select SName from studentinfo where studentinfo.SID=scoreinfo.SID) as 姓名,"+
					"CName as 课程名称,Score as 成绩,RID as 编号 from scoreinfo,majorinfo where CName='"+e.Node.Text.ToString()+"'";
				adp.SelectCommand.CommandText=sql;
				adp.Fill(ds,"course");
				dataGrid1.DataSource=ds.Tables[0].DefaultView;
				dataGrid1.CaptionText=e.Node.Parent.Text.ToString()+"专业 "+e.Node.Text+"课程成绩表";
			}
			else
			{
				dataGrid1.DataSource=null;
				dataGrid1.CaptionText="";
			}
			oleConnection1.Close();
		}
	}
}
