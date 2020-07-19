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
	/// Major ��ժҪ˵����
	/// </summary>
	public class Major : System.Windows.Forms.Form
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
		private OleDbCommand oleCommand1 = null;

		public Major()
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
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(408, 264);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 14;
			this.btClose.Text = "�˳�";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btDel
			// 
			this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btDel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btDel.Location = new System.Drawing.Point(248, 264);
			this.btDel.Name = "btDel";
			this.btDel.TabIndex = 13;
			this.btDel.Text = "ɾ��";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// btModify
			// 
			this.btModify.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btModify.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btModify.Location = new System.Drawing.Point(88, 264);
			this.btModify.Name = "btModify";
			this.btModify.TabIndex = 12;
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
			this.dataGrid1.Location = new System.Drawing.Point(8, 40);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.White;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectionBackColor = System.Drawing.Color.Navy;
			this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
			this.dataGrid1.Size = new System.Drawing.Size(544, 208);
			this.dataGrid1.TabIndex = 11;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(208, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 24);
			this.label1.TabIndex = 10;
			this.label1.Text = "ר ҵ �� ��";
			// 
			// Major
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(560, 294);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btDel);
			this.Controls.Add(this.btModify);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.label1);
			this.Name = "Major";
			this.Text = "רҵ���";
			this.Load += new System.EventHandler(this.Major_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		DataSet ds;
		private void Major_Load(object sender, System.EventArgs e)
		{
			oleConnection1.Open();
			string sql ="select MName as רҵ����,MRemark as רҵ����,MID as רҵ��� from majorinfo";
			OleDbDataAdapter adp = new OleDbDataAdapter(sql,oleConnection1);
			ds=new DataSet();
			ds.Clear();
			adp.Fill(ds,"major");
			dataGrid1.DataSource=ds.Tables[0].DefaultView;
			dataGrid1.CaptionText="����"+ds.Tables[0].Rows.Count+"����¼";
			oleConnection1.Close();
		}

		MajorModify majorModify;
		private void btModify_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource != null || dataGrid1[dataGrid1.CurrentCell] != null)
			{
				majorModify = new MajorModify();
				majorModify.textID.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][2].ToString().Trim();
				majorModify.textName.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim();
				majorModify.textRemark.Text=ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][1].ToString().Trim();
				majorModify.ShowDialog();
			}
			else
				MessageBox.Show("û��ָ��רҵ��Ϣ��","��ʾ");
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.CurrentRowIndex>=0 && dataGrid1.DataSource!=null && dataGrid1[dataGrid1.CurrentCell]!=null)
			{
				oleConnection1.Open();
				string sql ="select * from courseinfo where MName='"+ds.Tables["major"].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim()+"'";
				OleDbCommand cmd=new OleDbCommand(sql,oleConnection1);
				OleDbDataReader dr;
				dr=cmd.ExecuteReader();
				if (dr.Read())
				{
					MessageBox.Show("ɾ��רҵ'"+ds.Tables["major"].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim()+"'ʧ�ܣ�����ɾ�����רҵ��صĿγ�","��ʾ");
					dr.Close();
				}
				else
				{
					dr.Close();
					sql="delete * from majorinfo where MName not in (select distinct MName from courseinfo) and MID="+ds.Tables["major"].Rows[dataGrid1.CurrentCell.RowNumber][2].ToString().Trim();
					
					cmd.CommandText=sql;
					cmd.ExecuteNonQuery();
					MessageBox.Show("ɾ��רҵ'"+ds.Tables["major"].Rows[dataGrid1.CurrentCell.RowNumber][0].ToString().Trim()+"'�ɹ�","��ʾ");
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
