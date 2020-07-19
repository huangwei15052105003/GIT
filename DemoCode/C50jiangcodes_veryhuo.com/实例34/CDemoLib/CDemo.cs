using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace CDemoLib
{
	/// <summary>
	/// CDemo 的摘要说明。
	/// </summary>
	public class CDemo : System.ComponentModel.Component
	{
		public readonly int InstanceID;
		private static int NextInstanceID = 0;
		private static long ClassInstanceCount = 0;
		public static long InstanceCount
		{
			get
			{
				return ClassInstanceCount;
			}
		}
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CDemo(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Windows.Forms 类撰写设计器支持所必需的
			/// </summary>
			container.Add(this);
			InitializeComponent();
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
		}

		public CDemo()
		{
			/// <summary>
			/// Windows.Forms 类撰写设计器支持所必需的
			/// </summary>
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			InstanceID = NextInstanceID ++;
			ClassInstanceCount ++;
		}
		~CDemo()
		{
			ClassInstanceCount --;
		}

		#region Component Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
