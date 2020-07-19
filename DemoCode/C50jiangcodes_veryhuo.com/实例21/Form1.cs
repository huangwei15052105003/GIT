using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GDIDrawing2
{
	/// <summary>
	/// GDIDrawing2 —— GDI+绘图（二）。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(424, 312);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GDIDrawing2";
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
		// 处理窗体显示事件。
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen blackPen= new Pen(Color.Black, 3);
			g.DrawArc(blackPen, 0, 0, 100, 200, 45, 270);
			Rectangle r1 = new Rectangle(110, 0, 210, 200);
			g.DrawArc(blackPen, r1, 45, 270);
			g.DrawEllipse(blackPen, 50, 50, 200, 200);
			g.DrawEllipse(blackPen, 180, 50, 220, 80);
			Rectangle r2 = new Rectangle(270, 60, 50, 200);
			g.DrawEllipse(blackPen, r2);
			g.DrawPie(blackPen, 20, 50, 120, 100, 30, 240);
			Rectangle r3 = new Rectangle(260, 150, 100, 100);
			g.DrawPie(blackPen, r3, 0, 130);
		}
	}
}
