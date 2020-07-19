using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace CDemoLib
{
	/// <summary>
	/// CDemo ��ժҪ˵����
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
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CDemo(System.ComponentModel.IContainer container)
		{
			/// <summary>
			/// Windows.Forms ��׫д�����֧���������
			/// </summary>
			container.Add(this);
			InitializeComponent();
			// TODO: �� InitializeComponent ���ú�����κι��캯������
		}

		public CDemo()
		{
			/// <summary>
			/// Windows.Forms ��׫д�����֧���������
			/// </summary>
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κι��캯������
			InstanceID = NextInstanceID ++;
			ClassInstanceCount ++;
		}
		~CDemo()
		{
			ClassInstanceCount --;
		}

		#region Component Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
