using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace EX14_1
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
			this.ClientSize = new System.Drawing.Size(448, 205);
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
			Pen pen = new Pen(Color.Blue,10.5f); //构造一个宽度为10.5个像素的红色笔
			g.DrawString("蓝色，宽度为10.5",this.Font,new SolidBrush(Color.Black),5,5);
			g.DrawLine(pen,new Point(110,10),new Point(380,10));
			pen.Width=2;
			pen.Color=Color.Red;
			g.DrawString("红色，宽度为2",this.Font,new SolidBrush(Color.Black),5,25);
			g.DrawLine(pen,new Point(110,30),new Point(380,30));
			pen.StartCap=LineCap.Flat;
			pen.EndCap=LineCap.ArrowAnchor;
			pen.Width=9;
			g.DrawString("红色箭头线",this.Font,new SolidBrush(Color.Black),5,45);
			g.DrawLine(pen,new Point(110,50),new Point(380,50));
			pen.DashStyle = DashStyle.Custom;
			pen.DashPattern=new float[]{4,4};
			pen.Width=2;
			pen.EndCap=LineCap.NoAnchor;
			g.DrawString("自定义虚线",this.Font,new SolidBrush(Color.Black),5,65);
			g.DrawLine(pen,new Point(110,70),new Point(380,70));
			pen.DashStyle=DashStyle.Dot;
			g.DrawString("点划线",this.Font,new SolidBrush(Color.Black),5,85);
			g.DrawLine(pen,new Point(110,90),new Point(380,90));
			g.Dispose();
		}
	}
}
