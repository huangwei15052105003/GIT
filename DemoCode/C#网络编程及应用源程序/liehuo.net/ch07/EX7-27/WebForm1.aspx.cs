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
using System.Data.SqlClient;

namespace EX7_24
{
	/// <summary>
	/// WebForm1 ��ժҪ˵����
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
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
			string connString="server=localhost;uid=sa;pwd=;database=northwind";
			SqlConnection conn=new SqlConnection(connString);
			SqlCommand command=new SqlCommand("sp_CustomersByState",conn);
			conn.Open();
			//����CommandType���Ե�ֵ���������Ϊ�洢����
			command.CommandType=CommandType.StoredProcedure;
			//�½���Ϊ@Region������Ϊnvarchar(15)�Ĳ���������洢�����е�������ƥ��
			SqlParameter param=new SqlParameter("@Region",SqlDbType.NVarChar,15);
			//��������ӵ���������Parameters���ϣ����������Ǹò���
			command.Parameters.Add(param);
			//���������Direction���Ծ������Ƿ�����ڽ���Ϣ���ݸ��洢���̣������������Ϣ
			param.Direction=ParameterDirection.Input;
			param.Value=this.TextBox1.Text;
			this.DataGrid1.DataSource=command.ExecuteReader();
			this.DataGrid1.DataBind();
			conn.Close();
		}
	}
}
