using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace EX14_7
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
			this.ClientSize = new System.Drawing.Size(496, 301);
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
			//��Բ͸����80%
			g.FillEllipse(new SolidBrush(Color.FromArgb(80,Color.Red)),120,30,200,100);
			//˳ʱ����ת30��
			g.RotateTransform(30.0f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(80,Color.Blue)),120,30,200,100);
			//ˮƽ��������ƽ��200�����أ���ֱ��������ƽ��100������
			g.TranslateTransform(200.0f,-100.0f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(50,Color.Green)),120,30,200,100);
			//��С��һ��
			g.ScaleTransform(0.5f,0.5f);
			g.FillEllipse(new 
				SolidBrush(Color.FromArgb(100,Color.Red)),120,30,200,100);

		}
	}
}
