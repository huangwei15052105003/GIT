using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GDIDrawing
{
	/// <summary>
	/// GDI+��ͼ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(440, 336);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GDI+��ͼ";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

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
		// ���л�����GDI+��ͼ������
		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(this.BackColor);
			g.DrawLine(new Pen(Color.Blue, 5.0f), 20, 20, 200, 20);
			g.DrawRectangle(new Pen(Color.DarkCyan, 3.0f), 20, 50, 300, 80);
			Point[] points =
				{
					new Point( 100,  10),
					new Point( 10, 100),
					new Point(300,  50),
					new Point(230,  120),
					new Point(100,  220),
					new Point(this.Width/2,this.Height/2),
					new Point(0, 300)
				};
			g.DrawLines(new Pen(Color.DarkGreen, 4.0f), points);
			RectangleF[] rects =
				{
					new RectangleF(  0.0F,   0.0F, 100.0F, 200.0F),
					new RectangleF(100.0F, 200.0F, 250.0F,  50.0F),
					new RectangleF(300.0F,   0.0F,  50.0F, 100.0F)
				};
			g.DrawRectangles(new Pen(Color.DarkGreen, 5.0f), rects);
		}
	}
}
