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
	/// AddStudent ��ժҪ˵����
	/// </summary>
	public class AddStudent : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textDormID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btSure;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.TextBox textClass;
		private System.Windows.Forms.TextBox textSID;
		private System.Windows.Forms.ComboBox comboSex;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public AddStudent()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textClass = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textDormID = new System.Windows.Forms.TextBox();
			this.textSID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btSure = new System.Windows.Forms.Button();
			this.btClose = new System.Windows.Forms.Button();
			this.comboSex = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboSex);
			this.groupBox1.Controls.Add(this.textName);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textClass);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textDormID);
			this.groupBox1.Controls.Add(this.textSID);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("����_GB2312", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 160);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ѧ����Ϣ";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.Location = new System.Drawing.Point(88, 80);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(88, 21);
			this.textName.TabIndex = 3;
			this.textName.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label7.Location = new System.Drawing.Point(40, 80);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 10;
			this.label7.Text = "��  ��";
			// 
			// textClass
			// 
			this.textClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textClass.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textClass.Location = new System.Drawing.Point(88, 120);
			this.textClass.Name = "textClass";
			this.textClass.Size = new System.Drawing.Size(88, 21);
			this.textClass.TabIndex = 5;
			this.textClass.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label4.Location = new System.Drawing.Point(40, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "��  ��";
			// 
			// textDormID
			// 
			this.textDormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textDormID.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textDormID.Location = new System.Drawing.Point(88, 40);
			this.textDormID.Name = "textDormID";
			this.textDormID.Size = new System.Drawing.Size(88, 21);
			this.textDormID.TabIndex = 1;
			this.textDormID.Text = "";
			// 
			// textSID
			// 
			this.textSID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textSID.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textSID.Location = new System.Drawing.Point(240, 40);
			this.textSID.Name = "textSID";
			this.textSID.Size = new System.Drawing.Size(88, 21);
			this.textSID.TabIndex = 2;
			this.textSID.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.Location = new System.Drawing.Point(192, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "��  ��";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.Location = new System.Drawing.Point(192, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "ѧ  ��";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(40, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "�����";
			// 
			// btSure
			// 
			this.btSure.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btSure.ForeColor = System.Drawing.Color.Black;
			this.btSure.Location = new System.Drawing.Point(64, 184);
			this.btSure.Name = "btSure";
			this.btSure.TabIndex = 6;
			this.btSure.Text = "ȷ��";
			this.btSure.Click += new System.EventHandler(this.btSure_Click);
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.Black;
			this.btClose.Location = new System.Drawing.Point(224, 184);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 7;
			this.btClose.Text = "ȡ��";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// comboSex
			// 
			this.comboSex.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.comboSex.Items.AddRange(new object[] {
														  "��",
														  "Ů"});
			this.comboSex.Location = new System.Drawing.Point(240, 80);
			this.comboSex.Name = "comboSex";
			this.comboSex.Size = new System.Drawing.Size(88, 20);
			this.comboSex.TabIndex = 4;
			// 
			// AddStudent
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(360, 222);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btSure);
			this.Controls.Add(this.btClose);
			this.Name = "AddStudent";
			this.Text = "ѧ����ס";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btSure_Click(object sender, System.EventArgs e)
		{
			if (textDormID.Text.Trim()==""||textSID.Text.Trim()==""||textName.Text.Trim()=="")
				MessageBox.Show("����д������Ϣ","ȷ��");
			else
			{
				oleConnection1.Open();
				string sql="select * from dorm where dormID='"+textDormID.Text.Trim()+"'";
				OleDbCommand cmd = new OleDbCommand(sql,oleConnection1);
				if (null==cmd.ExecuteScalar())
					MessageBox.Show("û�и÷���ţ�����������","��ʾ");
				else
				{
					sql = "select bedNum-(select count(*) from student where dormID = '"+textDormID.Text.Trim()+"') from dorm "+
						"where dormID = '"+textDormID.Text.Trim()+"'";
					cmd.CommandText=sql;
					if (Convert.ToInt16(cmd.ExecuteScalar().ToString().Trim())==0)
						MessageBox.Show("�÷�������","��ʾ");
					else
					{
						sql="select * from student where SID='"+textSID.Text.Trim()+"'";
						cmd.CommandText = sql;
						if (null!=cmd.ExecuteScalar())
							MessageBox.Show("ѧ���ظ�","��ʾ");
						else
						{
							sql = "insert into student values ('"+textSID.Text.Trim()+"','"+textName.Text.Trim()+"','"+comboSex.Text.Trim()+"',"+
								"'"+textClass.Text.Trim()+"','"+textDormID.Text.Trim()+"')";
							cmd.CommandText = sql;
							cmd.ExecuteNonQuery();
							MessageBox.Show("��ӳɹ�","��ʾ");
							clear();
						}
					}
				}
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
			textSID.Text="";
			textName.Text="";
			comboSex.Text="";
			textClass.Text="";
		}
	}
}
