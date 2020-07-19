using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
//Download by http://down.liehuo.net
namespace DormMIS
{
	/// <summary>
	/// Register ��ժҪ˵����
	/// </summary>
	public class Register : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btDel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btQuery;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btClose;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public Register()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			this.oleConnection1 = new OleDbConnection(DormMIS.database.dbConnection.connection);

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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.btAdd = new System.Windows.Forms.Button();
			this.btDel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btQuery = new System.Windows.Forms.Button();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.dataGrid1.Location = new System.Drawing.Point(8, 88);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.White;
			this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.SelectionBackColor = System.Drawing.Color.Navy;
			this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
			this.dataGrid1.Size = new System.Drawing.Size(528, 192);
			this.dataGrid1.TabIndex = 27;
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.Black;
			this.btAdd.Location = new System.Drawing.Point(68, 291);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 24;
			this.btAdd.Text = "�޸�";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btDel
			// 
			this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btDel.ForeColor = System.Drawing.Color.Black;
			this.btDel.Location = new System.Drawing.Point(232, 291);
			this.btDel.Name = "btDel";
			this.btDel.TabIndex = 25;
			this.btDel.Text = "ɾ��";
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btQuery);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(528, 70);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�����˲�ѯ";
			// 
			// btQuery
			// 
			this.btQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btQuery.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btQuery.ForeColor = System.Drawing.Color.Black;
			this.btQuery.Location = new System.Drawing.Point(336, 32);
			this.btQuery.Name = "btQuery";
			this.btQuery.TabIndex = 2;
			this.btQuery.Text = "��ѯ";
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			// 
			// textDormID
			// 
			this.textDormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDormID.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDormID.Location = new System.Drawing.Point(144, 32);
			this.textDormID.Name = "textDormID";
			this.textDormID.Size = new System.Drawing.Size(88, 21);
			this.textDormID.TabIndex = 1;
			this.textDormID.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(96, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "�����";
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(400, 291);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 26;
			this.btClose.Text = "ȡ��";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// Register
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(552, 326);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.btDel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btClose);
			this.Name = "Register";
			this.Text = "������Ա��ѯ";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		DataSet ds;
		private void btQuery_Click(object sender, System.EventArgs e)
		{
			oleConnection1.Open();
			ds = new DataSet();
			string sql="select dormID as �����,PCome as ������,PLook as ������,DateCome as ��������,DateLeave as �뿪����,"+
				"Remark as ��ע,RegisterID as ���ñ�� from register";
			if (textDormID.Text.Trim()=="")
				sql=sql;
			else
				sql=sql+" where dormID= '"+textDormID.Text.Trim()+"'";
			OleDbDataAdapter adp = new OleDbDataAdapter(sql,oleConnection1);
			ds.Clear();
			adp.Fill(ds,"register");
			dataGrid1.DataSource = ds.Tables["register"].DefaultView;
			dataGrid1.CaptionText = "����"+ds.Tables["register"].Rows.Count+"����¼";
			oleConnection1.Close();
		}

		private void btAdd_Click(object sender, System.EventArgs e)
		{
		
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			if (dataGrid1.DataSource!=null&&dataGrid1.CurrentRowIndex>=0&&dataGrid1[dataGrid1.CurrentCell]!=null)
			{
				oleConnection1.Open();
				string sql = "delete * from register where RegisterID="+ds.Tables[0].Rows[dataGrid1.CurrentCell.RowNumber][6]+"";
				OleDbCommand cmd = new OleDbCommand(sql,oleConnection1);
				cmd.ExecuteNonQuery();
				MessageBox.Show("ɾ���ɹ�","��ʾ");
				oleConnection1.Close();
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
