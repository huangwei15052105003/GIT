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
	/// WebForm4 ��ժҪ˵����
	/// </summary>
	public class WebForm4 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.CheckBoxList CheckBoxList1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				ListItem[] item=new ListItem[10];
				for(int i=0;i<item.Length;i++)
				{
					item[i]=new ListItem("ѡ��"+i.ToString());
				}
				CheckBoxList1.Items.AddRange(item);
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
			string str="ѡ������";
			for(int i=0;i<CheckBoxList1.Items.Count;i++)
			{
				if(CheckBoxList1.Items[i].Selected)
				{
					str+=CheckBoxList1.Items[i].Text+"��";
				}
			}
			if(str[str.Length-1]=='��') str=str.Substring(0,str.Length-1);
			Response.Write("<script>window.alert('"+str+"');</script>");

		}
	}
}
