using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Data;

namespace TestInherit
{
	public class xuehao : TestInherit.MinZu
	{
		private System.ComponentModel.IContainer components = null;

		public xuehao()
		{
			// �õ����� Windows ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region ��������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
			// 
			// buttonAppend
			// 
			// 
			// xuehao
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(600, 317);
			this.Name = "xuehao";
			((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();

		}
		#endregion

		public override void GetTableInformation()
		{
			tableName="bmxh";
			selectString="select * from bmxh order by ����";
			fieldsLength=new int[]{10,10};  //����10������10
		}
		public override void AddNewRow()
		{
			long num;
			if(table.Rows.Count==0)
			{
				num=0301010001;
			}
			else
			{
				num=Convert.ToInt64(table.Rows[table.Rows.Count-1]["����"])+1;
			}
			DataRow newRow=table.NewRow();
			do
			{
				try
				{
					newRow["����"]=num.ToString("d"+fieldsLength[0].ToString());
					table.Rows.Add(newRow);
				}
				catch
				{
					num++;
					continue;
				}
				break;
			}while(true);
		}
	}
}

