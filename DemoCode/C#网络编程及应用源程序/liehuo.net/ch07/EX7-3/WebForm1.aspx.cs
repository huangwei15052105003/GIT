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
using System.Data.OleDb;

namespace EX7_3
{
	/// <summary>
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			//���������ַ�������.\db1.mdb��ָ��ǰĿ¼�µ�db1.mdb�ļ���
			//Server.MapPath(path)�Ĺ����ǽ�����Ŀ¼ת��Ϊʵ��Ŀ¼
			string connString=@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
				+Server.MapPath(@".\db1.mdb");
			OleDbConnection conn=new OleDbConnection(connString);
			string sql="select * from table1";
			OleDbCommand cmd=new OleDbCommand(sql,conn);
			conn.Open();
			this.DataGrid1.DataSource=cmd.ExecuteReader();
			this.DataGrid1.DataBind();
			conn.Close();
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
