using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data;
using System.Data.Odbc;


namespace EX7_4
{
	/// <summary>
	/// Form2 ��ժҪ˵����
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//

//			string str = "MaxBufferSize=2048;DSN=dBASE Files;PageTimeout=5;DefaultDir=D:\\;DBQ=D:\\;DriverId=533";

			string str="Driver=Microsoft Visual FoxPro Driver;"+
				"SourceType=DBC;SourceDB=d:\\mydbc\\test.dbc";
			//			string str="Driver=Microsoft Visual FoxPro Driver; SourceType=DBF;SourceDB=d:\\mydbc ";
			//			string str="Driver=dBASE Driver; SourceType=DBF;SourceDB=d:\\mydbc ";

//			string str = "dsn=dbase files;defaultdir=d:\\";

			OdbcConnection conn=new OdbcConnection(str);
			string sql="select * from table1";
			OdbcDataAdapter adapter=new OdbcDataAdapter(sql,conn);
			DataSet dataset=new DataSet();
			adapter.Fill(dataset,"table1");
			this.dataGrid1.DataSource=dataset.Tables["table1"];

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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(136, 48);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(448, 120);
			this.dataGrid1.TabIndex = 0;
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 302);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form2";
			this.Text = "Form2";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
