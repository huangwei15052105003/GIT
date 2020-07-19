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

namespace EX14_20
{
	/// <summary>
	/// WebForm2 ��ժҪ˵����
	/// </summary>
	public class WebForm2 : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				//�õ�Ҫ��ʾ��ͼ���ļ���
				string filename=(string)Session["filename"];
				//�����ļ�������ԭʼ��С��Bitmap����
				Bitmap bitmap=new Bitmap(filename);
				//�������ŵ�250��200
				bitmap=new Bitmap(bitmap,250,200);
				//�������ź��Bitmap���󴴽���ˢ
				TextureBrush myBrush = new TextureBrush(bitmap);
				//���´���250���ؿ�200���ظߵĿ�Bitmap����
				bitmap=new Bitmap(250,200);
				Graphics g=Graphics.FromImage(bitmap);
				//���������ͼƽ��ԭ�����ݣ������û�ͼƽ�汳��ɫΪ��ɫ
				g.Clear(Color.White);
				//�����Բ
				g.FillEllipse(myBrush,0,0,250,200);
				//�����������
				Response.ContentType="image/gif";
				//������ͻ������
				bitmap.Save(Response.OutputStream,System.Drawing.Imaging.ImageFormat.Gif);
			}

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
