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

namespace EX7_27
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
			if(!this.IsPostBack)
			{
				string sqlstr="select * from sales";
				string connString= "server=localhost;uid=sa;pwd=;database=pubs";
				SqlConnection conn=new SqlConnection(connString);
				SqlDataAdapter adapter=new SqlDataAdapter(sqlstr,conn);
				DataSet ds=new DataSet();
				adapter.Fill(ds,"sales");
				SetColumn();
				this.DataGrid1.DataSource=ds.Tables["sales"];
				this.DataGrid1.DataBind();
			}

		}
		private void SetColumn()
		{
			this.DataGrid1.AutoGenerateColumns=false;
			BoundColumn column1 = new BoundColumn();
			BoundColumn column2 = new BoundColumn();
			BoundColumn column3 = new BoundColumn();
			column1.HeaderText = "id";
			column1.DataField = "stor_id";
			column2.HeaderText = "num";
			column2.DataField = "ord_num";
			column3.DataFormatString="{0:yyyy年MM月}";
			column3.HeaderText = "date";
			column3.DataField = "ord_date";
			this.DataGrid1.Columns.Add(column1);
			this.DataGrid1.Columns.Add(column2);
			this.DataGrid1.Columns.Add(column3);
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
