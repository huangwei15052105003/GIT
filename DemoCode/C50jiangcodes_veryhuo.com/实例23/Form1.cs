using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DrawPictureString
{
	/// <summary>
	/// 显示图片和文字。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Windows 窗体设计器支持所必需的
			InitializeComponent();
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			this.button1.Text = "打开";
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
			this.Text = "显示图片以及文字";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		public string m_FileName;
		public Image m_Image;
		public bool m_Init = false;
		// 打开图片文件。
		private void button1_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "图形文件(*.bmp;*.jpg;*.jpeg)|*.bmp;*.jpg;*.jpeg";
			openFileDialog1.FilterIndex = 1;
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				m_FileName = openFileDialog1.FileName;
				m_Image = Image.FromFile(openFileDialog1.FileName);
				m_Init = true;
				this.Validate();
			}
		}
		// 处理窗体显示事件。
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(m_Init)
			{
				Rectangle rect = new Rectangle(0,0,this.ClientRectangle.Width,
					this.ClientRectangle.Height-button1.Height-20);
				Graphics g = e.Graphics;
				// 显示图片。
				g.DrawImage(m_Image, rect);
				// 显示图片对应文件名信息。
				SolidBrush redBrush = new SolidBrush(Color.Red);
				g.DrawString(m_FileName, new Font("宋体", 10), redBrush,
					new Point(0,this.ClientRectangle.Height-30), StringFormat.GenericDefault);
			}
		}
	}
}
