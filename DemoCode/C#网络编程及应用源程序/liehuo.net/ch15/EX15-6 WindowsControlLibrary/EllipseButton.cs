using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace WindowsControlLibrary
{
	/// <summary>
	/// EllipseButton ��ժҪ˵����
	/// </summary>
	public class EllipseButton : System.Windows.Forms.Control
	{
		private Color startColor=Color.White; //������ʼɫ 
		private Color endColor=Color.Red;     //������ֹɫ 

		[Description("�趨�������ʼɫ"),Category("Appearance")] 
		public Color StartColor 
		{ 
			get 
			{ 
				return startColor; 
			} 
			set 
			{
				startColor=value; 
				RePaint();
			} 
		}

		[Description("�趨�������ֹɫ"),Category("Appearance")] 
		public Color EndColor 
		{ 
			get 
			{ 
				return endColor; 
			} 
			set 
			{ 
				endColor=value; 
				RePaint();
			} 
		} 
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EllipseButton()
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
			// EllipseButton
			// 
			this.Size = new System.Drawing.Size(100, 100);
			this.Resize += new System.EventHandler(this.EllipseButton_Resize);
			this.TextChanged += new System.EventHandler(this.EllipseButton_TextChanged);

		}
		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{
			// ���û��� OnPaint
			base.OnPaint(pe);
			// TODO: �ڴ�����Զ���滭����
			Graphics g=pe.Graphics;
			g.Clear(this.BackColor);
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height);
			//�����ϵ����µĽ���
			LinearGradientBrush myBrush = new LinearGradientBrush(rect,startColor,endColor,LinearGradientMode.ForwardDiagonal);
			g.FillEllipse(myBrush,rect);
			myBrush.Dispose();
			StringFormat format=new StringFormat(); 
			format.LineAlignment=StringAlignment.Center; 
			format.Alignment=StringAlignment.Center; 
			g.DrawString(this.Text,this.Font,new SolidBrush(this.ForeColor),rect,format); 
		}

		private void EllipseButton_Resize(object sender, System.EventArgs e)
		{
			RePaint();
		}

		private void EllipseButton_TextChanged(object sender, System.EventArgs e)
		{
			RePaint();
		}
		private void RePaint()
		{
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height);
			OnPaint(new PaintEventArgs(this.CreateGraphics(),rect));
		}
	}
}
