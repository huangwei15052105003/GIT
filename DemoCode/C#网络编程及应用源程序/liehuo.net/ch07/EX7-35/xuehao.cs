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
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			selectString="select * from bmxh order by 编码";
			fieldsLength=new int[]{10,10};  //编码10，名称10
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
				num=Convert.ToInt64(table.Rows[table.Rows.Count-1]["编码"])+1;
			}
			DataRow newRow=table.NewRow();
			do
			{
				try
				{
					newRow["编码"]=num.ToString("d"+fieldsLength[0].ToString());
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

