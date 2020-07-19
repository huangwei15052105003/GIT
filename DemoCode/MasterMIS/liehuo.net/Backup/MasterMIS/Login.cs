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
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection oleConnection1 = null;
		private System.Windows.Forms.Button btClose;
		private System.Windows.Forms.Button btAdd;
		private OleDbCommand oleCommand1 = null;

		public Login()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			oleConnection1 = new OleDbConnection(MasterMIS.database.dbConnection.connection);
			oleCommand1 = new OleDbCommand();
			oleCommand1.Connection = oleConnection1;

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
				if (components != null) 
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
			this.password = new System.Windows.Forms.TextBox();
			this.name = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btClose
			// 
			this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btClose.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btClose.Location = new System.Drawing.Point(168, 160);
			this.btClose.Name = "btClose";
			this.btClose.TabIndex = 13;
			this.btClose.Text = "ȡ��";
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			// 
			// btAdd
			// 
			this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btAdd.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.btAdd.Location = new System.Drawing.Point(48, 160);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 12;
			this.btAdd.Text = "ȷ��";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// password
			// 
			this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.password.Location = new System.Drawing.Point(128, 120);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.TabIndex = 11;
			this.password.Text = "admin";
			// 
			// name
			// 
			this.name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.name.Location = new System.Drawing.Point(128, 80);
			this.name.Name = "name";
			this.name.TabIndex = 10;
			this.name.Text = "admin";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label3.Location = new System.Drawing.Point(64, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "��  ��";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label2.Location = new System.Drawing.Point(64, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "�û���";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����_GB2312", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.label1.Location = new System.Drawing.Point(32, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 40);
			this.label1.TabIndex = 7;
			this.label1.Text = "�о���������Ϣϵͳ";
			// 
			// Login
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Lavender;
			this.ClientSize = new System.Drawing.Size(296, 198);
			this.Controls.Add(this.btClose);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.password);
			this.Controls.Add(this.name);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��¼";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Login());
		}

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			if(name.Text.Trim()==""||password.Text.Trim()=="")
				MessageBox.Show("�������û���������","��ʾ");
			else
			{
				oleConnection1.Open();
				OleDbCommand cmd=new OleDbCommand("",oleConnection1);
				string sql="select RoleName from userinfo where UName='"+name.Text.Trim()+"' and PWD='"+password.Text.Trim()+"'";
				cmd.CommandText=sql;
				string rolename;

				if (null!=cmd.ExecuteScalar())
				{
					rolename=cmd.ExecuteScalar().ToString();
					//���ص�¼����
					this.Visible=false;  
					//��������������
					Main main=new Main();
					main.Tag=this.FindForm();
					sql="select * from roles where RoleName='"+rolename+"'";
					OleDbDataReader dr;
					cmd.CommandText=sql;
					dr=cmd.ExecuteReader();
					dr.Read();
     
					main.menuItem1.Visible=(bool)(dr.GetValue(1));
					
					main.menuItem4.Visible=(bool)(dr.GetValue(1));
			
					main.menuItem5.Visible=(bool)(dr.GetValue(2));

					main.menuItem6.Visible=(bool)(dr.GetValue(2));

					main.menuItem7.Visible=(bool)(dr.GetValue(3));

					main.statusBarPanel2.Text=name.Text.Trim();
			
					main.ShowDialog(); 
				}
				else
					MessageBox.Show("�û������������","����");

				oleConnection1.Close();
			}
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
