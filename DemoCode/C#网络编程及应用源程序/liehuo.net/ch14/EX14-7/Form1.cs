using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace EX14_7
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 301);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

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

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			//椭圆透明度80%
			g.FillEllipse(new SolidBrush(Color.FromArgb(80,Color.Red)),120,30,200,100);
			//顺时针旋转30度
			g.RotateTransform(30.0f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(80,Color.Blue)),120,30,200,100);
			//水平方向向右平移200个像素，垂直方向向上平移100个像素
			g.TranslateTransform(200.0f,-100.0f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(50,Color.Green)),120,30,200,100);
			//缩小到一半
			g.ScaleTransform(0.5f,0.5f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(100,Color.Red)),120,30,200,100);

		}
	}
}
