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
	/// WebForm8 ��ժҪ˵����
	/// </summary>
	public class WebForm8 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				ListItem[] stra={  new ListItem("����ϵͳ"),new ListItem("���ݽṹ"),new ListItem("���ݿ�")};
				DropDownList1.Items.AddRange(stra);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN���õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.DropDownList1.SelectedIndexChanged += new System.EventHandler(this.DropDownList1_SelectedIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropDownList1.SelectedIndex>-1)
			{
				Label1.Text=DropDownList1.SelectedItem.Text;
			}

		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(DropDownList1.SelectedIndex>-1)
			{
				Response.Write("<script>window.alert('��ѡ����ǣ�"+DropDownList1.SelectedItem.Text+"');</script>");
			}

		}
	}
}
