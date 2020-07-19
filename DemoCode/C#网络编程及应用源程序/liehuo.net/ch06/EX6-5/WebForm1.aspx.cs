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

namespace book6_5
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			Button1.Attributes.Add("onmouseenter","this.style.color='red'");
			Button1.Attributes.Add("onmouseleave","this.style.color='black'");
			
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.alert('这是警告信息！');</script>");
            Response.Write("<script>window.confirm('你认为对么?');</script>");
            Response.Write("<script>window.prompt('请输入');</script>");
 
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{Response.Write("<script>window.showModalDialog('test1.htm','Dialog Arguments Value','dialogHeight: 341px; dialogWidth: 656px; dialogTop: 86px; dialogLeft: 100px; edge: Raised; center: Yes; help: Yes; resizable: Yes; status: Yes;');</script>");
		
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
		 Response.Write("<script>window.showModelessDialog('test1.htm','Dialog Arguments Value','dialogHeight: 341px; dialogWidth: 656px; dialogTop: 86px; dialogLeft: 100px; edge: Raised; center: Yes; help: Yes; resizable: Yes; status: Yes;');</script>");
		}
	}
}
