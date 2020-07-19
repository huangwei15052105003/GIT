using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestInherit
{
	/// <summary>
	/// bmxx 的摘要说明。
	/// </summary>
	public class bmxx : MinZu
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public bmxx()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			selectString="select * from bmxx order by 编码";
			fieldsLength=new int[]{2,6,1};  //编码2，名称8
		}

	}
}
