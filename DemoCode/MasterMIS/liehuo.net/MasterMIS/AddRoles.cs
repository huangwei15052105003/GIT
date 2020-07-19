using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MasterMIS
{
	/// <summary>
	/// AddRoles ��ժҪ˵����
	/// </summary>
	public class AddRoles : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textRole;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ckSys;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btAdd;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private System.Windows.Forms.CheckBox ckCourse;
		private System.Windows.Forms.CheckBox ckMajor;
		private System.Windows.Forms.CheckBox ckScore;
		private OleDbCommand oleCommand1 = null;

		public AddRoles()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			this.oleConnection1=new OleDbConnection(MasterMIS.database.dbConnection.connection);
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textRole = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ckCourse = new System.Windows.Forms.CheckBox();
			this.ckMajor = new System.Windows.Forms.CheckBox();
			this.ckSys = new System.Windows.Forms.CheckBox();
			this.ckScore = new System.Windows.Forms.CheckBox();
			this.btClose = new System.Windows.Forms.Button();
			this.btAdd = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textRole);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(18, 11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(228, 56);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "��ɫ";
			// 
			// textRole
			// 
			this.textRole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textRole.Location = new System.Drawing.Point(66, 19);
			this.textRole.Name = "textRole";
			this.textRole.Size = new System.Drawing.Size(144, 21);
			this.textRole.TabIndex = 0;
			this.textRole.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "��ɫ����";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ckCourse);
			this.groupBox1.Controls.Add(this.ckMajor);
			this.groupBox1.Controls.Add(this.ckSys);
			this.groupBox1.Controls.Add(this.ckScore);
			this.groupBox1.Location = new System.Drawing.Point(18, 75);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(228, 81);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ȩ��";
			// 
			// ckCourse
			// 
			this.ckCourse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ckCourse.Location = new System.Drawing.Point(32, 50);
			this.ckCourse.Name = "ckCourse";
			this.ckCourse.Size = new System.Drawing.Size(76, 18);
			this.ckCourse.TabIndex = 5;
			this.ckCourse.Text = "�γ̹���";
			// 
			// ckMajor
			// 
			this.ckMajor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ckMajor.Location = new System.Drawing.Point(128, 19);
			this.ckMajor.Name = "ckMajor";
			this.ckMajor.Size = new System.Drawing.Size(80, 18);
			this.ckMajor.TabIndex = 4;
			this.ckMajor.Text = "רҵ����";
			// 
			// ckSys
			// 
			this.ckSys.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ckSys.Location = new System.Drawing.Point(32, 19);
			this.ckSys.Name = "ckSys";
			this.ckSys.Size = new System.Drawing.Size(76, 18);
			this.ckSys.TabIndex = 3;
			this.ckSys.Text = "ϵͳ����";
			// 
			// ckScore
			// 
			this.ckScore.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ckScore.Location = new System.Drawing.Point(128, 50);
			this.ckScore.Name = "ckScore";
			this.ckScore.Size = new System.Drawing.Size(80, 18);
			this.ckScore.TabIndex = 5;
			this.ckScore.Text = "�ɼ�����";
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.Location = new System.Drawing.Point(162, 163);
			this.btClose.Name = "btClose";
			this.btClose.Size = new System.Drawing.Size(66, 25);
			this.btClose.TabIndex = 21;
			this.btClose.Text = "ȡ��";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.Location = new System.Drawing.Point(34, 163);
			this.btAdd.Name = "btAdd";
			this.btAdd.Size = new System.Drawing.Size(66, 25);
			this.btAdd.TabIndex = 18;
			this.btAdd.Text = "����";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// AddRoles
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(264, 198);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Name = "AddRoles";
			this.Text = "�½���ɫ";
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			oleConnection1.Open();
			OleDbCommand cmd = new OleDbCommand("",oleConnection1);
			if (textRole.Text.Trim()!="")
			{
				string sql = "select * from roles where RoleName = '"+textRole.Text.Trim()+"'";
				cmd.CommandText = sql;
				if (null == cmd.ExecuteScalar())
				{
					string sql1 = "insert into roles values ('"+textRole.Text.Trim()+"',"+ckSys.Checked+","+ckMajor.Checked+","+
						""+ckCourse.Checked+","+ckScore.Checked+")";
					cmd.CommandText = sql1;
					cmd.ExecuteNonQuery();
					MessageBox.Show("�½���ɫ�ɹ���","��ʾ");
				}
				else
					MessageBox.Show("��ɫ�����ظ���","����");
			} 
			else
				MessageBox.Show("��ɫ���Ʋ���Ϊ�գ�","����");
			oleConnection1.Close();
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
