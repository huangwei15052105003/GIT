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
	/// Service1 的摘要说明。
	/// </summary>
	[WebService(Namespace="http://localhost/sayhello",Description="第一个WebService服务程序。")]
	public class Service1 : System.Web.Services.WebService
	{
		public Service1()
		{
			//CODEGEN：该调用是 ASP.NET Web 服务设计器所必需的
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Web 服务设计器所必需的
		private IContainer components = null;
				
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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


		[WebMethod(Description="提供的一个服务接口。")]
		public string HelloWorld(string name)
		{
			return "Hello " + name + ", welcome to the webservice.";
		}
	}
}
