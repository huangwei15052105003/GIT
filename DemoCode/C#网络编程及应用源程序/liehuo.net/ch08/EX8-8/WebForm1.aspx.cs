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

namespace EX8_8
{
	/// <summary>
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
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
			try
			{
				string str=this.File1.PostedFile.FileName;
				if(str.LastIndexOf("\\")>-1)
				{
					str=str.Substring(str.LastIndexOf("\\")+1);
					this.File1.PostedFile.SaveAs(Server.MapPath(str));
					Response.Write("<script>window.alert('�ϴ��ɹ���');</script>");
				}
				else
				{
					Response.Write("<script>window.alert('��ѡ���ϴ����ļ���');</script>");
				}
			}
			catch(Exception err)
			{
				Response.Write("<script>window.alert('�ϴ�ʧ�ܣ�"+err.Message+"');</script>");
			}

		}
	}
}
