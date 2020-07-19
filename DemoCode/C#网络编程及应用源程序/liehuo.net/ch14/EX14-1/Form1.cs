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
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen pen = new Pen(Color.Blue,10.5f); //����һ�����Ϊ10.5�����صĺ�ɫ��
			g.DrawString("��ɫ�����Ϊ10.5",this.Font,new SolidBrush(Color.Black),5,5);
			g.DrawLine(pen,new Point(110,10),new Point(380,10));
			pen.Width=2;
			pen.Color=Color.Red;
			g.DrawString("��ɫ�����Ϊ2",this.Font,new SolidBrush(Color.Black),5,25);
			g.DrawLine(pen,new Point(110,30),new Point(380,30));
			pen.StartCap=LineCap.Flat;
			pen.EndCap=LineCap.ArrowAnchor;
			pen.Width=9;
			g.DrawString("��ɫ��ͷ��",this.Font,new SolidBrush(Color.Black),5,45);
			g.DrawLine(pen,new Point(110,50),new Point(380,50));
			pen.DashStyle = DashStyle.Custom;
			pen.DashPattern=new float[]{4,4};
			pen.Width=2;
			pen.EndCap=LineCap.NoAnchor;
			g.DrawString("�Զ�������",this.Font,new SolidBrush(Color.Black),5,65);
			g.DrawLine(pen,new Point(110,70),new Point(380,70));
			pen.DashStyle=DashStyle.Dot;
			g.DrawString("�㻮��",this.Font,new SolidBrush(Color.Black),5,85);
			g.DrawLine(pen,new Point(110,90),new Point(380,90));
			g.Dispose();
		}
	}
}
