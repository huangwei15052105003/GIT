using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

namespace WebServiceSayHello
{
	/// <summary>
	/// Service1 ��ժҪ˵����
	/// </summary>
	[WebService(Namespace="http://localhost/sayhello",Description="��һ��WebService�������")]
	public class Service1 : System.Web.Services.WebService
	{
		public Service1()
		{
			//CODEGEN���õ����� ASP.NET Web ����������������
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Web ����������������
		private IContainer components = null;
				
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		[WebMethod(Description="�ṩ��һ������ӿڡ�")]
		public string HelloWorld(string name)
		{
			return "Hello " + name + ", welcome to the webservice.";
		}
	}
}
