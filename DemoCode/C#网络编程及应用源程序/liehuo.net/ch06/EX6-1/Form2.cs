using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace book6_1
{
	/// <summary>
	/// Form2 ��ժҪ˵����
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

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

		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(88, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "���ҵ�మ���ǣ�";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(24, 96);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 24);
			this.checkBox1.TabIndex = 1;
			this.checkBox1.Text = "�����";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(128, 96);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(80, 24);
			this.checkBox2.TabIndex = 2;
			this.checkBox2.Text = "���̳�";
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(224, 96);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(80, 24);
			this.checkBox3.TabIndex = 3;
			this.checkBox3.Text = "����";
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(88, 168);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 24);
			this.button1.TabIndex = 4;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(344, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.checkBox3,
																		  this.checkBox2,
																		  this.checkBox1,
																		  this.label1});
			this.Name = "Form2";
			this.Text = "Form2";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form2_Load(object sender, System.EventArgs e)
		{
		
		}
		private void checkBox_CheckedChanged(object sender, System.EventArgs e)
		{
			CheckBox checkbox=(CheckBox)sender;
			if(checkbox.Checked)
			{
				checkbox.ForeColor=Color.Green;
			}
			else
			{
				checkbox.ForeColor=Color.Black;
							
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			string str="ѡ������";
			str+=(this.checkBox1.Checked==true)?(this.checkBox1.Text+"��"):"";
			str+=(this.checkBox2.Checked==true)?(this.checkBox2.Text+"��"):"";
			str+=(this.checkBox3.Checked==true)?(this.checkBox3.Text+"��"):"";
			if(str[str.Length-1]=='��')
			{
				str=str.Substring(0,str.Length-1);
			}
			MessageBox.Show(str);

		}

		
	}
}
