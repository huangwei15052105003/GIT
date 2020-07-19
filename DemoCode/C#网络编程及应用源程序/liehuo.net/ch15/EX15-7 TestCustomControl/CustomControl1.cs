using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestCustomControl
{
	/// <summary>
	/// CustomControl1 ��ժҪ˵����
	/// </summary>
	public class CustomControl1 : System.Windows.Forms.Control
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomControl1()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitComponent ���ú�����κγ�ʼ��
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region �����������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// CustomControl1
			// 
			this.Resize += new System.EventHandler(this.CustomControl1_Resize);
			this.Click += new System.EventHandler(this.CustomControl1_Click);
			this.TextChanged += new System.EventHandler(this.CustomControl1_TextChanged);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: �ڴ�����Զ���滭����
			Graphics g=pe.Graphics;
			g.Clear(this.BackColor);
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height);
			LinearGradientBrush myBrush = new LinearGradientBrush(rect,Color.White,Color.Red,LinearGradientMode.ForwardDiagonal);
			g.FillEllipse(myBrush,rect);
			myBrush.Dispose();
			StringFormat format=new StringFormat(); 
			format.LineAlignment=StringAlignment.Center; 
			format.Alignment=StringAlignment.Center; 
			g.DrawString(this.Text,this.Font,new SolidBrush(this.ForeColor),rect,format); 
			g.Dispose();
			// ���û��� OnPaint
			base.OnPaint(pe);
		}

		private void CustomControl1_Resize(object sender, System.EventArgs e)
		{
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height);
			OnPaint(new PaintEventArgs(this.CreateGraphics(),rect));
		}

		private void CustomControl1_TextChanged(object sender, System.EventArgs e)
		{
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height);
			OnPaint(new PaintEventArgs(this.CreateGraphics(),rect));
		}

		private void CustomControl1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
