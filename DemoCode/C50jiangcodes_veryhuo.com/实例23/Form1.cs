using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DrawPictureString
{
	/// <summary>
	/// ��ʾͼƬ�����֡�
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Windows ���������֧���������
			InitializeComponent();
			// TODO: �� InitializeComponent ���ú�����κι��캯������
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

		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(360, 296);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "��";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(440, 328);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "��ʾͼƬ�Լ�����";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
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
		public string m_FileName;
		public Image m_Image;
		public bool m_Init = false;
		// ��ͼƬ�ļ���
		private void button1_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "ͼ���ļ�(*.bmp;*.jpg;*.jpeg)|*.bmp;*.jpg;*.jpeg";
			openFileDialog1.FilterIndex = 1;
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				m_FileName = openFileDialog1.FileName;
				m_Image = Image.FromFile(openFileDialog1.FileName);
				m_Init = true;
				this.Validate();
			}
		}
		// ��������ʾ�¼���
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(m_Init)
			{
				Rectangle rect = new Rectangle(0,0,this.ClientRectangle.Width,
					this.ClientRectangle.Height-button1.Height-20);
				Graphics g = e.Graphics;
				// ��ʾͼƬ��
				g.DrawImage(m_Image, rect);
				// ��ʾͼƬ��Ӧ�ļ�����Ϣ��
				SolidBrush redBrush = new SolidBrush(Color.Red);
				g.DrawString(m_FileName, new Font("����", 10), redBrush,
					new Point(0,this.ClientRectangle.Height-30), StringFormat.GenericDefault);
			}
		}
	}
}
