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

namespace book6_3
{
	/// <summary>
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
			this.Button1.ServerClick += new System.EventHandler(this.Button1_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string str=this.File1.PostedFile.FileName;
				if(str.Length<5) return;
				string str1=str.Substring(str.LastIndexOf(".")+1).ToLower();
				if(str1!="doc")
				{
					Response.Write("<script language=JavaScript>window.alert('��Ǹ���������ϴ���word��ʽ���ļ���');</script>");
				}
				else
				{
					if(str.LastIndexOf("\\")>-1)
					{
						str1=str.Substring(str.LastIndexOf("\\")+1);
						this.File1.PostedFile.SaveAs(Server.MapPath(".")+"\\�ϴ�\\"+str1);
					}
					else
					{
						this.File1.PostedFile.SaveAs(Server.MapPath(".")+"\\�ϴ�\\"+str);
					}
					Response.Write(
						"<script language=JavaScript>window.alert('�ϴ��ɹ���лл��');</script>");
				}
			}
			catch(Exception err)
			{
				Response.Write(
					"<script language=JavaScript>window.alert('"+err.Message+"');</script>");
			}

		}
	}
}
