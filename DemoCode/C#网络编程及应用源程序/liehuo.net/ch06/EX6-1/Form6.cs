using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace book6_1
{
	/// <summary>
	/// Form6 ��ժҪ˵����
	/// </summary>
	public class Form6 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		public Form6()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			toolTip1.SetToolTip(comboBox1,"ѡ��μӵ�С�顣");
			toolTip1.SetToolTip(comboBox2,"ѡ�����С��μӵ���Ŀ��");
			toolTip1.SetToolTip(listBox1,"��С��μӵ���Ŀ�б�");

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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "С��";
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "��һ��",
														   "�ڶ���",
														   "������"});
			this.comboBox1.Location = new System.Drawing.Point(56, 24);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(88, 20);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.Text = "comboBox1";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(168, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "�μ���Ŀ";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// comboBox2
			// 
			this.comboBox2.Items.AddRange(new object[] {
														   "����һ",
														   "���̶�",
														   "������"});
			this.comboBox2.Location = new System.Drawing.Point(240, 24);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(88, 20);
			this.comboBox2.TabIndex = 3;
			this.comboBox2.Text = "comboBox2";
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(24, 64);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(304, 88);
			this.listBox1.TabIndex = 4;
			// 
			// Form6
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(352, 173);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listBox1,
																		  this.comboBox2,
																		  this.label2,
																		  this.comboBox1,
																		  this.label1});
			this.Name = "Form6";
			this.Text = "Form6";
			this.Load += new System.EventHandler(this.Form6_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void label2_Click(object sender, System.EventArgs e)
		{
		
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboBox1.SelectedIndex>-1 && comboBox2.SelectedIndex>-1)
			{
				listBox1.Items.Add (comboBox1.SelectedItem.ToString()+"  "+comboBox2.SelectedItem.ToString());
			}

		}

		private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboBox1.SelectedIndex>-1 && this.comboBox2.SelectedIndex>-1)
			{
				this.listBox1.Items.Add(this.comboBox1.SelectedItem.ToString()+"  "+this.comboBox2.SelectedItem.ToString());
			}

		}

		private void Form6_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
