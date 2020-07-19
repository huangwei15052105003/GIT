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

namespace book6_2
{
	/// <summary>
	/// WebForm9 的摘要说明。
	/// </summary>
	public class WebForm9 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Table Table1;
	
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Table1.Rows.Clear();
			AddRow("搜狐企业在线","www.sohu.net","包括招商引资、企业报道等信息。");
			AddRow("263在线","www.263.net","包括娱乐、生活、旅游等信息。");

		}
		private void AddRow(string str1,string url,string str2)
		{
			TableRow row=new TableRow();

			HyperLink hyperlink=new HyperLink();
			hyperlink.Text=str1;
			hyperlink.NavigateUrl=url;
			hyperlink.Target="_top";
			TableCell cell1=new TableCell();
			cell1.Font.Size=FontUnit.XSmall;
			cell1.Controls.Add(hyperlink);
			row.Cells.Add(cell1);

			TableCell cell2=new TableCell();
			cell2.Font.Size=FontUnit.XSmall;
			cell2.ForeColor=Color.Blue;
			cell2.Text=str2;
			row.Cells.Add(cell2);
			Table1.Rows.Add(row);
		}

	}
}
