using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestInherit
{
	/// <summary>
	/// bmxx ��ժҪ˵����
	/// </summary>
	public class bmxx : MinZu
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public bmxx()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
			this.SuspendLayout();
			// 
			// bmxx
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(592, 325);
			this.Name = "bmxx";
			this.Text = "bmxx";
			((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		public override void GetTableInformation()
		{
			tableName="bmxx";
			selectString="select * from bmxx order by ����";
			fieldsLength=new int[]{2,6,1};  //����2������8
		}

	}
}
