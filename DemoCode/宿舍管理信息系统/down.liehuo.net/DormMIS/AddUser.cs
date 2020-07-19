using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DormMIS
{
	/// <summary>
	/// AddUser ��ժҪ˵����
	/// </summary>
	public class AddUser : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textPWDNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btAdd;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;

		public AddUser()
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
			this.textPWDNew = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.textName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btClose = new System.Windows.Forms.Button();
			this.btAdd = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textPWDNew
			// 
			this.textPWDNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPWDNew.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textPWDNew.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textPWDNew.Location = new System.Drawing.Point(87, 96);
			this.textPWDNew.Name = "textPWDNew";
			this.textPWDNew.PasswordChar = '*';
			this.textPWDNew.Size = new System.Drawing.Size(120, 21);
			this.textPWDNew.TabIndex = 38;
			this.textPWDNew.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(23, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 17);
			this.label3.TabIndex = 44;
			this.label3.Text = "����ȷ��";
			// 
			// textPassword
			// 
			this.textPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPassword.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textPassword.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textPassword.Location = new System.Drawing.Point(87, 56);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(120, 21);
			this.textPassword.TabIndex = 37;
			this.textPassword.Text = "";
			// 
			// textName
			// 
			this.textName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textName.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.textName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.textName.Location = new System.Drawing.Point(87, 16);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(120, 21);
			this.textName.TabIndex = 36;
			this.textName.Text = "";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(23, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 43;
			this.label2.Text = "��    ��";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(23, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 17);
			this.label1.TabIndex = 42;
			this.label1.Text = "�û�����";
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btClose.Location = new System.Drawing.Point(135, 136);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 41;
			this.btClose.Text = "�˳�";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btAdd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btAdd.Location = new System.Drawing.Point(31, 136);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 40;
			this.btAdd.Text = "���";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// AddUser
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Ivory;
			this.ClientSize = new System.Drawing.Size(232, 174);
			this.Controls.Add(this.textPWDNew);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Name = "AddUser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "����û�";
			this.ResumeLayout(false);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if (textName.Text.Trim()==""||textPassword.Text.Trim()==""||textPWDNew.Text.Trim()=="")
				MessageBox.Show("������������Ϣ","��ʾ");
			else
			{
				if (textPassword.Text.Trim()!=textPWDNew.Text.Trim())
					MessageBox.Show("�����������벻һ��","����");
				else
				{
					oleConnection1.Open();
					string sql = "select * from userinfo where UName = '"+textName.Text.Trim()+"'";
					OleDbCommand cmd = new OleDbCommand("",oleConnection1);
					cmd.CommandText = sql;
					if (null == cmd.ExecuteScalar())
					{
						sql = "insert into userinfo values ('"+textName.Text.Trim()+"','"+textPassword.Text.Trim()+"')";
						cmd.CommandText = sql;
						cmd.ExecuteNonQuery();
						oleConnection1.Close();
						MessageBox.Show("��ӳɹ�","��ʾ");
					}
					else
						MessageBox.Show("�û����ظ�","��ʾ");
					oleConnection1.Close();
				}
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
