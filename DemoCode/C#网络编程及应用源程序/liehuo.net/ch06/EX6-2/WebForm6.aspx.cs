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
	/// WebForm6 ��ժҪ˵����
	/// </summary>
	public class WebForm6 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string[] stra=new string[]{"����","����","����","ƹ����","��ë��",
										  "ȭ��","���","���","�ܲ�","���",
										  "����","Χ��","����","����","������"};
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string str="��ѡ����ǣ�";
			if(RadioButtonList1.SelectedIndex>-1)
			{
				str+=RadioButtonList1.SelectedItem.Text;
			}
			Response.Write("<script>window.alert('"+str+"')</script>");

		}
	}
}
