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
	/// WebForm7 ��ժҪ˵����
	/// </summary>
	public class WebForm7 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.ListBox ListBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!Page.IsPostBack)
			{
				ListItem[] stra={  new ListItem("��һ��"),new ListItem("�ڶ���"),new ListItem("������")};
				ListBox1.Items.AddRange(stra);
			}

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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//������ظ���Ŀ
			bool ifExist=false;
			if (TextBox1.Text!="") 
				//for������������Ƿ����ظ���Ŀ��ListBox1.Items.Count�����б����
				//��Ŀ�ĸ���
				for(int i=0;i<ListBox1.Items.Count;i++)
				{
					if(ListBox1.Items[i].ToString()==TextBox1.Text)
					{
						ifExist=true;
						//�ҵ�ʱ�ж��˳�
						break;
					}
				}
			if(!ifExist)
			{
				ListBox1.Items.Add(TextBox1.Text);
			}

		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//����ɾ��
			for(int i=ListBox1.Items.Count-1;i>=0;i--)
			{
				if(ListBox1.Items[i].Selected)
				{
					ListBox1.Items.Remove(this.ListBox1.Items[i]);
				}
			}


		}
	}
}
