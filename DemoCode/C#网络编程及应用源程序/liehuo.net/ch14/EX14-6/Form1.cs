using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Drawing.Drawing2D;

namespace EX14_6
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
			this.ClientSize = new System.Drawing.Size(480, 273);
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
			Graphics g=e.Graphics;
			Point centerPoint=new Point(150,100);
			int R=60;
			GraphicsPath path=new GraphicsPath();
			path.AddEllipse(centerPoint.X-R,centerPoint.Y-R,2*R,2*R);
			PathGradientBrush brush=new PathGradientBrush(path);
			//指定路径中心点
			brush.CenterPoint=centerPoint;
			//指定路径中心点的颜色
			brush.CenterColor=Color.Red;
			//Color类型的数组指定与路径上每个顶点对应的颜色
			brush.SurroundColors=new Color[]{ Color.Plum };
			g.FillEllipse(brush,centerPoint.X-R,centerPoint.Y-R,2*R,2*R);
			centerPoint=new Point(350,100);
			R=20;
			path=new GraphicsPath();
			path.AddEllipse(centerPoint.X-R,centerPoint.Y-R,2*R,2*R);
			path.AddEllipse(centerPoint.X-2*R,centerPoint.Y-2*R,4*R,4*R);
			path.AddEllipse(centerPoint.X-3*R,centerPoint.Y-3*R,6*R,6*R);
			brush=new PathGradientBrush(path);
			brush.CenterPoint=centerPoint;
			brush.CenterColor=Color.Red;
			brush.SurroundColors=new Color[]{ Color.Black,Color.Blue,Color.Green };
			g.FillPath(brush,path);

		}
	}
}
