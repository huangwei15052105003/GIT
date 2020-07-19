using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GDIDrawing3
{
	/// <summary>
	/// GDIDrawing3——GDI+绘图。
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
			this.ClientSize = new System.Drawing.Size(440, 320);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GDIDrawing3";
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
			SolidBrush redBrush = new SolidBrush(Color.Red);
			g.FillEllipse(redBrush, 0, 0, 100, 60);
			g.FillPie(redBrush, 100, 0, 100, 70, 30, 300);
			g.FillRectangle(redBrush, 200, 10, 100, 50);
			Point[] points =
				{
					new Point(0, 100),
					new Point(20, 120),
					new Point(50, 100),
					new Point(60, 200),
					new Point(30, 220),
					new Point(180, 200),
					new Point(20, 110),
					new Point(0, 220)
				};
			g.FillPolygon(redBrush, points);
			Rectangle fillRect = new Rectangle(200, 100, 200, 200);
			Region fillRegion = new Region(fillRect);
			g.FillRegion(redBrush, fillRegion);
		}
	}
}
