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
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl IFRAME1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button ButtonPosition;
		protected System.Web.UI.WebControls.Button Button3;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				Session["index"]=1;
				this.ButtonPosition.Text="1 of 8";
				Session["filename"]=Server.MapPath(".")+"\\t1.jpg";
				this.IFRAME1.Attributes["src"]="WebForm2.aspx";
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			int index=(int)Session["index"];
			if(index>1)
			{
				index--;
				this.ButtonPosition.Text=index.ToString()+" of 8";
				Session["index"]=index;
				Session["filename"]=Server.MapPath(".")+@"\t"+index.ToString()+".jpg";
				this.IFRAME1.Attributes["src"]="WebForm2.aspx";
			}

		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			int index=(int)Session["index"];
			if(index<8)
			{
				index++;
				this.ButtonPosition.Text=index.ToString()+" of 8";
				Session["index"]=index;
				Session["filename"]=Server.MapPath(".")+@"\t"+index.ToString()+".jpg";
				this.IFRAME1.Attributes["src"]="WebForm2.aspx";
			}

		}
	}
}
