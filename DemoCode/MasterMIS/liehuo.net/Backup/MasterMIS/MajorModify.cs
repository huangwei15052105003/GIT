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
	/// MajorModify ��ժҪ˵����
	/// </summary>
	public class MajorModify : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.TextBox textRemark;
		public System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox textID;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private OleDbCommand oleCommand1 = null;

		public MajorModify()
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
			this.btAdd = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textRemark = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textID = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.Location = new System.Drawing.Point(208, 172);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 7;
			this.btClose.Text = "ȡ��";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.Location = new System.Drawing.Point(56, 172);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 6;
			this.btAdd.Text = "ȷ��";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textRemark);
			this.groupBox1.Controls.Add(this.textName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.groupBox1.Location = new System.Drawing.Point(6, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(322, 144);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�޸�רҵ";
			// 
			// textRemark
			// 
			this.textRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRemark.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textRemark.Location = new System.Drawing.Point(88, 80);
			this.textRemark.Multiline = true;
			this.textRemark.Name = "textRemark";
			this.textRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textRemark.Size = new System.Drawing.Size(224, 48);
			this.textRemark.TabIndex = 2;
			this.textRemark.Text = "";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.Location = new System.Drawing.Point(208, 40);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(104, 21);
			this.textName.TabIndex = 1;
			this.textName.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(32, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "רҵ����";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(152, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "רҵ����";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(32, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "רҵ���";
			// 
			// textID
			// 
			this.textID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textID.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textID.Location = new System.Drawing.Point(88, 40);
			this.textID.Name = "textID";
			this.textID.ReadOnly = true;
			this.textID.Size = new System.Drawing.Size(48, 21);
			this.textID.TabIndex = 4;
			this.textID.Text = "";
			// 
			// MajorModify
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(336, 206);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.groupBox1);
			this.Name = "MajorModify";
			this.Text = "�޸�רҵ";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if ((textName.Text.Trim()=="") || (textRemark.Text.Trim()==""))
				MessageBox.Show("��ʾ","������������רҵ��Ϣ");
			else
			{ 
				oleConnection1.Open();
				string sql1 = "select * from majorinfo where MName='"+textName.Text.Trim()+"' and MID<>"+textID.Text.Trim();
				oleCommand1.CommandText = sql1;
				if (null!=oleCommand1.ExecuteScalar())
					MessageBox.Show("רҵ���Ʒ����ظ�","��ʾ");
				else
				{
					string sql2="update majorinfo set MName='"+textName.Text.Trim()+"',MRemark='"+textRemark.Text.Trim()+"' where MID="+this.textID.Text.Trim();
					oleCommand1.CommandText=sql2;
					oleCommand1.ExecuteNonQuery();
					MessageBox.Show("רҵ��Ϣ�޸ĳɹ�","��ʾ");
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
