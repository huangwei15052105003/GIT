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

namespace EX8_1
{
	/// <summary>
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			// �ڴ˴������û������Գ�ʼ��ҳ��
			Response.Write("��ǰ�ͻ�ʹ�õĲ���ϵͳΪ��"+Request.Browser.Platform+"<br>");
			Response.Write("��ǰ�ͻ�ʹ�õ������Ϊ��"
				+Request.Browser.Type+"."+Request.Browser.MinorVersion+"<br>");
			Response.Write("��ǰ�ͻ���IP��ַΪ��"+Request.UserHostAddress+"<br>");
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
