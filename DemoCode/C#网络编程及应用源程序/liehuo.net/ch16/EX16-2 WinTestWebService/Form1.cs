using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace WinTestWebService
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			this.radioButton2.Checked=true;

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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "���룺";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 128);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "�����";
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(72, 80);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(160, 21);
			this.textBoxInput.TabIndex = 1;
			this.textBoxInput.Text = "abcDeFgHijk";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(72, 128);
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.Size = new System.Drawing.Size(160, 21);
			this.textBoxOutput.TabIndex = 1;
			this.textBoxOutput.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(80, 240);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "ת��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(160, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "���ַ�����Ϊ��д��Сд";
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(16, 16);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(72, 24);
			this.radioButton1.TabIndex = 5;
			this.radioButton1.Text = "��д��ʾ";
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(104, 16);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(72, 24);
			this.radioButton2.TabIndex = 5;
			this.radioButton2.Text = "Сд��ʾ";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Location = new System.Drawing.Point(32, 160);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 48);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ת����ʽ";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(248, 273);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBoxInput);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBoxOutput);
			this.Name = "Form1";
			this.Text = "����WebService";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			localhost.Service1 cc=new localhost.Service1();
			bool toLower=true;
			if(this.radioButton1.Checked==true)
			{
				toLower=false;
			}
			this.textBoxOutput.Text=cc.ConvertString(this.textBoxInput.Text,toLower);
		}
	}
}
