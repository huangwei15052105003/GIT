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
	/// WebForm9 ��ժҪ˵����
	/// </summary>
	public class WebForm9 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Table Table1;
	
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Table1.Rows.Clear();
			AddRow("�Ѻ���ҵ����","www.sohu.net","�����������ʡ���ҵ��������Ϣ��");
			AddRow("263����","www.263.net","�������֡�������ε���Ϣ��");

		}
		private void AddRow(string str1,string url,string str2)
		{
			TableRow row=new TableRow();

			HyperLink hyperlink=new HyperLink();
			hyperlink.Text=str1;
			hyperlink.NavigateUrl=url;
			hyperlink.Target="_top";
			TableCell cell1=new TableCell();
			cell1.Font.Size=FontUnit.XSmall;
			cell1.Controls.Add(hyperlink);
			row.Cells.Add(cell1);

			TableCell cell2=new TableCell();
			cell2.Font.Size=FontUnit.XSmall;
			cell2.ForeColor=Color.Blue;
			cell2.Text=str2;
			row.Cells.Add(cell2);
			Table1.Rows.Add(row);
		}

	}
}
