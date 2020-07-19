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
	/// WebForm7 的摘要说明。
	/// </summary>
	public class WebForm7 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.ListBox ListBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!Page.IsPostBack)
			{
				ListItem[] stra={  new ListItem("第一项"),new ListItem("第二项"),new ListItem("第三项")};
				ListBox1.Items.AddRange(stra);
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//插入非重复条目
			bool ifExist=false;
			if (TextBox1.Text!="") 
				//for语句用来查找是否有重复条目，ListBox1.Items.Count代表列表框中
				//条目的个数
				for(int i=0;i<ListBox1.Items.Count;i++)
				{
					if(ListBox1.Items[i].ToString()==TextBox1.Text)
					{
						ifExist=true;
						//找到时中断退出
						break;
					}
				}
			if(!ifExist)
			{
				ListBox1.Items.Add(TextBox1.Text);
			}

		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//多项删除
			for(int i=ListBox1.Items.Count-1;i>=0;i--)
			{
				if(ListBox1.Items[i].Selected)
				{
					ListBox1.Items.Remove(this.ListBox1.Items[i]);
				}
			}


		}
	}
}
