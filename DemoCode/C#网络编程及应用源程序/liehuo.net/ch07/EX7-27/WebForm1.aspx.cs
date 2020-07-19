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

namespace EX7_24
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string connString="server=localhost;uid=sa;pwd=;database=northwind";
			SqlConnection conn=new SqlConnection(connString);
			SqlCommand command=new SqlCommand("sp_CustomersByState",conn);
			conn.Open();
			//设置CommandType属性的值，将其解释为存储过程
			command.CommandType=CommandType.StoredProcedure;
			//新建名为@Region并声明为nvarchar(15)的参数，它与存储过程中的声明相匹配
			SqlParameter param=new SqlParameter("@Region",SqlDbType.NVarChar,15);
			//将参数添加到命令对象的Parameters集合，经常会忘记该操作
			command.Parameters.Add(param);
			//参数对象的Direction属性决定它是否会用于将信息传递给存储过程，或接收它的信息
			param.Direction=ParameterDirection.Input;
			param.Value=this.TextBox1.Text;
			this.DataGrid1.DataSource=command.ExecuteReader();
			this.DataGrid1.DataBind();
			conn.Close();
		}
	}
}
