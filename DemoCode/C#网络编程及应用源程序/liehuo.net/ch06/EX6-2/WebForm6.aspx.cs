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
	/// WebForm6 的摘要说明。
	/// </summary>
	public class WebForm6 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string[] stra=new string[]{"足球","篮球","排球","乒乓球","羽毛球",
										  "拳击","体操","射击","跑步","逛街",
										  "象棋","围棋","跳棋","军旗","五子棋"};
			if(!Page.IsPostBack)
			{
				ListItem[] items=new ListItem[15];
				for(int i=0;i<items.Length;i++)
					items[i]=new ListItem(stra[i]);
				RadioButtonList1.Items.AddRange(items);
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string str="你选择的是：";
			if(RadioButtonList1.SelectedIndex>-1)
			{
				str+=RadioButtonList1.SelectedItem.Text;
			}
			Response.Write("<script>window.alert('"+str+"')</script>");

		}
	}
}
