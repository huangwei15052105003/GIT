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

namespace WebApplicationDisWebClient
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected WebApplicationDisWebClient.localhost.DataSet1 dataSet11;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			WebApplicationDisWebClient.localhost.Service1 ws = 
				new WebApplicationDisWebClient.localhost.Service1();
			ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
			dataSet11.Merge(ws.GetData());
			if (! Page.IsPostBack) 
			{
				DataGrid1.DataBind();
			}
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
			this.dataSet11 = new WebApplicationDisWebClient.localhost.DataSet1();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
			// 
			// dataSet11
			// 
			this.dataSet11.DataSetName = "DataSet1";
			this.dataSet11.Locale = new System.Globalization.CultureInfo("zh-CN");
			this.dataSet11.Namespace = "http://www.tempuri.org/DataSet1.xsd";
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();

		}
		#endregion

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGrid1.EditItemIndex = e.Item.ItemIndex;
			DataGrid1.DataBind();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGrid1.EditItemIndex = -1;
			DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			for (int i=1; i<dataSet11.user.Columns.Count; i++) 
			{
				TextBox t = (TextBox)(e.Item.Cells[i].Controls[0]);
				DataRow row = dataSet11.user[e.Item.DataSetIndex];
				row[dataSet11.user.Columns[i-1].Caption] = t.Text;
			}
			if (dataSet11.HasChanges()) 
			{
				WebApplicationDisWebClient.localhost.Service1 ws = 
					new WebApplicationDisWebClient.localhost.Service1();
				ws.Credentials = System.Net.CredentialCache.DefaultCredentials;
				WebApplicationDisWebClient.localhost.DataSet1 data = 
					new WebApplicationDisWebClient.localhost.DataSet1();
				data.Merge(dataSet11.GetChanges());
				ws.UpdateData(data);
				dataSet11.Merge(data);
			}
			DataGrid1.EditItemIndex = -1;
			DataGrid1.DataBind();
		}
	}
}
