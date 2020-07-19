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
using System.Data.OleDb;

namespace EX7_3
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			//创建连接字符串，“.\db1.mdb”指当前目录下的db1.mdb文件。
			//Server.MapPath(path)的功能是将虚拟目录转换为实际目录
			string connString=@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
				+Server.MapPath(@".\db1.mdb");
			OleDbConnection conn=new OleDbConnection(connString);
			string sql="select * from table1";
			OleDbCommand cmd=new OleDbCommand(sql,conn);
			conn.Open();
			this.DataGrid1.DataSource=cmd.ExecuteReader();
			this.DataGrid1.DataBind();
			conn.Close();
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
