using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GDIDrawing3
{
	/// <summary>
	/// GDIDrawing3����GDI+��ͼ��
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
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		// ��������ʾ�¼���
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
