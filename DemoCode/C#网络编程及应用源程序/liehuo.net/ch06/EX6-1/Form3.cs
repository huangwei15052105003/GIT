using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace book6_1
{
	/// <summary>
	/// Form3 ��ժҪ˵����
	/// </summary>
	public class Form3 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form3()
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
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "��ѡ�����ҵ�మ�ã��ɶ�ѡ��";
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.Items.AddRange(new object[] {
																 "����",
																 "����",
																 "����",
																 "ƹ����",
																 "��ë��",
																 "ȭ��",
																 "���",
																 "���",
																 "�ܲ�",
																 "���",
																 "����",
																 "Χ��",
																 "����",
																 "����",
																 "������",
																 "����",
																 "˫��",
																 "����"});
			this.checkedListBox1.Location = new System.Drawing.Point(8, 40);
			this.checkedListBox1.MultiColumn = true;
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(352, 132);
			this.checkedListBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(136, 184);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(72, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(368, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1,
																		  this.checkedListBox1,
																		  this.label1});
			this.Name = "Form3";
			this.Text = "Form3";
			this.Load += new System.EventHandler(this.Form3_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			string str="ѡ������";
			for(int i=0;i<checkedListBox1.CheckedIndices.Count;i++)
			{
				str+=checkedListBox1.CheckedItems[i]+"��";
				
			}
			if(str[str.Length-1]=='��')
			{
				str=str.Substring(0,str.Length-1);
			}
			MessageBox.Show(str);

		}

		private void Form3_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
