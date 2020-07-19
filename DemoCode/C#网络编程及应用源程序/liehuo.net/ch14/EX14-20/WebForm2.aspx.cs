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

namespace EX14_20
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
				//得到要显示的图像文件名
				string filename=(string)Session["filename"];
				//根据文件名创建原始大小的Bitmap对象
				Bitmap bitmap=new Bitmap(filename);
				//将其缩放到250×200
				bitmap=new Bitmap(bitmap,250,200);
				//根据缩放后的Bitmap对象创建画刷
				TextureBrush myBrush = new TextureBrush(bitmap);
				//重新创建250像素宽、200像素高的空Bitmap对象
				bitmap=new Bitmap(250,200);
				Graphics g=Graphics.FromImage(bitmap);
				//设置清除绘图平面原有内容，并设置画图平面背景色为白色
				g.Clear(Color.White);
				//填充椭圆
				g.FillEllipse(myBrush,0,0,250,200);
				//设置输出类型
				Response.ContentType="image/gif";
				//输出到客户浏览器
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
