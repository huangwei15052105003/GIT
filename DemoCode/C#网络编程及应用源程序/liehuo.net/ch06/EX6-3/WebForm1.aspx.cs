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

namespace book6_3
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
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
			this.Button1.ServerClick += new System.EventHandler(this.Button1_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string str=this.File1.PostedFile.FileName;
				if(str.Length<5) return;
				string str1=str.Substring(str.LastIndexOf(".")+1).ToLower();
				if(str1!="doc")
				{
					Response.Write("<script language=JavaScript>window.alert('抱歉：不允许上传非word格式的文件！');</script>");
				}
				else
				{
					if(str.LastIndexOf("\\")>-1)
					{
						str1=str.Substring(str.LastIndexOf("\\")+1);
						this.File1.PostedFile.SaveAs(Server.MapPath(".")+"\\上传\\"+str1);
					}
					else
					{
						this.File1.PostedFile.SaveAs(Server.MapPath(".")+"\\上传\\"+str);
					}
					Response.Write(
						"<script language=JavaScript>window.alert('上传成功，谢谢！');</script>");
				}
			}
			catch(Exception err)
			{
				Response.Write(
					"<script language=JavaScript>window.alert('"+err.Message+"');</script>");
			}

		}
	}
}
