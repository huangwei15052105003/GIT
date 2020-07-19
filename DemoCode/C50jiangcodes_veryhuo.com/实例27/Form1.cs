using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ClipBoard
{
	/// <summary>
	/// ������Ӧ�ó���
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.RichTextBox richTextBox1;
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
// Downloads By http://www.veryhuo.com
		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(80, 280);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(105, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "�鿴������";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(231, 280);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(105, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "ճ��������";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(416, 272);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(416, 306);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.richTextBox1,
																		  this.button2,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "������Ӧ�ó���";
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
		// �鿴���������ݡ�
		private void button1_Click(object sender, System.EventArgs e)
		{
			IDataObject iData = Clipboard.GetDataObject();
			if(iData.GetDataPresent(DataFormats.Text)) 
			{
				// ��ʽ��ȷ��д���ı�������ʾ��.
				richTextBox1.Text = (String)iData.GetData(DataFormats.Text); 
			}
			else 
			{
				// ���ݸ�ʽ����ȷ��
				MessageBox.Show("���������ݸ�ʽ����ȷ��");
			}
		}
		// ճ�����ݵ������塣
		private void button2_Click(object sender, System.EventArgs e)
		{
			if(richTextBox1.Text.Length != 0)
			{
				Clipboard.SetDataObject(richTextBox1.Text, false);
			}
		}
	}
}
