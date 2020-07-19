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
	/// Course ��ժҪ˵����
	/// </summary>
	public class Course : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btDel;
		private System.Windows.Forms.Button btModify;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label label2;
		private OleDbCommand oleCommand1 = null;

		public Course()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			this.oleConnection1 = new OleDbConnection(MasterMIS.database.dbConnection.connection);
			this.oleCommand1 = new OleDbCommand();
			this.oleCommand1.Connection = this.oleConnection1;

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.btClose.TabIndex = 19;
			this.btClose.Text = "�˳�";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btDel
			// 
			this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btDel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btDel.Location = new System.Drawing.Point(312, 270);
			this.btDel.Name = "btDel";
			this.btDel.TabIndex = 18;
			this.btDel.Text = "ɾ��";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// btModify
			// 
			this.btModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btModify.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btModify.Location = new System.Drawing.Point(184, 270);
			this.btModify.Name = "btModify";
			this.btModify.TabIndex = 17;
			this.btModify.Text = "�޸�";
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
			this.dataGrid1.TabIndex = 16;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(144, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 24);
			this.label1.TabIndex = 15;
			this.label1.Text = "�� �� �� ��";
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 46);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(128, 248);
			this.treeView1.TabIndex = 20;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 21;
			this.label2.Text = "רҵ";
			// 
			// Course
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
			this.Name = "Course";
			this.Text = "�γ���Ϣ";
			this.Load += new System.EventHandler(this.Course_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		DataSet ds;
		private void Course_Load(object sender, System.EventArgs e)
		{
			OleDbDataReader rd;
			string sql;
			sql="select MName from majorinfo";
			oleCommand1.CommandText=sql;
			oleConnection1.Open();
			
			rd=oleCommand1.ExecuteReader();
			while (rd.Read())
			{
				TreeNode node=new TreeNode();
				node.Text=rd.GetString(0).ToString();
				treeView1.Nodes.Add(node);
			}
			rd.Close();
			oleConnection1.Close();
		}

		CourseModify courseModify;
		private void btModify_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource != null || dataGrid1[dataGrid1.CurrentCell] != null)
			{
				courseModify = new CourseModify();
				courseModify.textName.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim();
				courseModify.textDate.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim();
				courseModify.textNum.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][2].ToString().Trim();
				courseModify.textMajor.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][3].ToString().Trim();
				courseModify.textRemark.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][4].ToString().Trim();
				courseModify.Tag=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][5].ToString().Trim();
				courseModify.ShowDialog();
			}
			else
				MessageBox.Show("û��ָ��רҵ��Ϣ��","��ʾ");
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.CurrentRowIndex>=0 && dataGrid1.DataSource != null && dataGrid1[dataGrid1.CurrentCell] != null)
			{
				string sql="delete * from courseinfo where CID="+ds.Tables["course"].Rows[dataGrid1.CurrentCell.RowNumber][5].ToString().Trim()+"";
				oleConnection1.Open();
				oleCommand1.CommandText = sql;
				oleCommand1.ExecuteNonQuery();
				MessageBox.Show("ɾ���γ�'"+ds.Tables["course"].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim()+"'�ɹ�","��ʾ");
				oleConnection1.Close();
			}
			else
				MessageBox.Show("û��ָ���γ���Ϣ��","��ʾ");
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

			sql="select CName as �γ�����,CDate as ѧʱ,CNum as ѧ��,MName as רҵ����,CRemark as �γ�����,CID as �γ̱��"+
				" from courseinfo where MName='"+e.Node.Text.ToString()+"'";
			adp.SelectCommand.CommandText=sql;
			adp.Fill(ds,"course");
			dataGrid1.DataSource=ds.Tables[0].DefaultView;
			dataGrid1.CaptionText=e.Node.Text+"רҵ�γ̱�";
		
			oleConnection1.Close();
		}
	}
}
