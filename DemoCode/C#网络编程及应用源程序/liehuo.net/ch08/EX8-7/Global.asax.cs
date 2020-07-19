using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.IO;

namespace EX8_7 
{
	/// <summary>
	/// Global ��ժҪ˵����
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			string file=Server.MapPath("UserCounter.txt");
			if(File.Exists(file))
			{
				StreamReader sr=File.OpenText(file);
				Application["userCounter"]=Int32.Parse(sr.ReadLine());
				sr.Close();
			}
			else
			{
				Application["userCounter"]=0;
			}

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
			Application.Lock();
			Application["userCounter"]=(int)Application["userCounter"]+1;
			StreamWriter sw=File.CreateText(Server.MapPath("UserCounter.txt"));
			sw.WriteLine((int)Application["userCounter"]);
			sw.Close();
			Application.UnLock();

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

