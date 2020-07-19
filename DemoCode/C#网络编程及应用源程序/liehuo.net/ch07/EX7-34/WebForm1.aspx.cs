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
using System.Data.SqlClient;
namespace EX7_31
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataList DataList1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			string str="user ID=sa;Password=;Server=localhost;Database=pubs"; 
			SqlConnection conn=new SqlConnection(str);
			SqlDataAdapter adapter=new SqlDataAdapter("SELECT * FROM stores",conn);
			DataSet ds=new DataSet();
			adapter.Fill(ds,"stores");
			this.DataList1.DataSource=ds.Tables["stores"];
			this.DataList1.DataBind ();

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
