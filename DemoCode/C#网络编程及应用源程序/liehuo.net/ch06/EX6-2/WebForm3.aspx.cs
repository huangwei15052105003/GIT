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
	/// WebForm3 ��ժҪ˵����
	/// </summary>
	public class WebForm3 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.CheckBox CheckBox3;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.CheckBox CheckBox2;
	
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
			this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CheckBox2_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string str="ѡ������";
			if(CheckBox1.Checked==true) str+=CheckBox1.Text+"��";
			if(CheckBox2.Checked==true) str+=CheckBox2.Text+"��";
			if(CheckBox3.Checked==true) str+=CheckBox3.Text+"��";
			if(str[str.Length-1]=='��') str=str.Substring(0,str.Length-1);
			TextBox1.Text=str;

		}
	}
}
