using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EX14_19
{
	/// <summary>
	/// WebForm2 ��ժҪ˵����
	/// </summary>
	public class WebForm2 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				Pen blackPen = new Pen(Color.Black, 3);
				Point[] bezierPoints =
					{
						new Point(50, 100),
						new Point(100,  10),
						new Point(150,290),
						new Point(200, 100),
						new Point(250,10),
						new Point(300, 290),
						new Point(350,100)
					};
				int width=400;
				int height=300;
				Bitmap bitmap=new Bitmap(width,height);
				Graphics g=Graphics.FromImage(bitmap);
				g.Clear(Color.White);
				g.DrawBeziers(blackPen, bezierPoints);
				Response.ContentType="image/gif";
				bitmap.Save(Response.OutputStream,System.Drawing.Imaging.ImageFormat.Gif);
			}

		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
