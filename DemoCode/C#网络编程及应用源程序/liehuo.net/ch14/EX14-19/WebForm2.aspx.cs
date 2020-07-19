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
	/// WebForm2 的摘要说明。
	/// </summary>
	public class WebForm2 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
