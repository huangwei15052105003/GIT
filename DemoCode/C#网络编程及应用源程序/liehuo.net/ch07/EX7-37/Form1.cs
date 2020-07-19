using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Data.SqlClient;

namespace TestEvent
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		
		//private ComboBox combobox;
		private DataSet ds;
		private ComboBox comboBox;

		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private System.Windows.Forms.Button button1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		private System.Data.SqlClient.SqlConnection sqlConnection1;
		private EX7_34.DataSet1 dataSet11;
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			comboBox = new ComboBox(); 
			//this.FillcomboBox();
			comboBox.Items.AddRange(new object[] {"a1","a2","a3","a4","a5","a6"});
			comboBox.SelectedIndex=0;
			comboBox.Visible=false;
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			this.dataGrid1.Controls.AddRange(new System.Windows.Forms.Control[] {comboBox});

			this.sqlDataAdapter1.Fill(this.dataSet11,"test");

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

		#region Windows Form Designer generated code
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.button1 = new System.Windows.Forms.Button();
			this.dataSet11 = new EX7_34.DataSet1();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.DataSource = this.dataSet11.test;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(48, 16);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(328, 232);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
			this.dataGrid1.Scroll += new System.EventHandler(this.dataGrid1_Scroll);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "test", new System.Data.Common.DataColumnMapping[] {
																																																			  new System.Data.Common.DataColumnMapping("id", "id"),
																																																			  new System.Data.Common.DataColumnMapping("����", "����"),
																																																			  new System.Data.Common.DataColumnMapping("����", "����")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = "DELETE FROM test WHERE (id = @Original_id) AND (���� = @Original_���� OR @Original_����" +
				" IS NULL AND ���� IS NULL) AND (���� = @Original_���� OR @Original_���� IS NULL AND ���� I" +
				"S NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "id", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_����", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "����", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_����", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "����", System.Data.DataRowVersion.Original, null));
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "workstation id=RAIN1;packet size=4096;integrated security=SSPI;initial catalog=we" +
				"bdevelop;persist security info=False";
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = "INSERT INTO test(����, ����) VALUES (@����, @����); SELECT id, ����, ���� FROM test WHERE (id" +
				" = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@����", System.Data.SqlDbType.VarChar, 10, "����"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@����", System.Data.SqlDbType.VarChar, 10, "����"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT id, ����, ���� FROM test";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = "UPDATE test SET ���� = @����, ���� = @���� WHERE (id = @Original_id) AND (���� = @Original_" +
				"���� OR @Original_���� IS NULL AND ���� IS NULL) AND (���� = @Original_���� OR @Original_��" +
				"�� IS NULL AND ���� IS NULL); SELECT id, ����, ���� FROM test WHERE (id = @id)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@����", System.Data.SqlDbType.VarChar, 10, "����"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@����", System.Data.SqlDbType.VarChar, 10, "����"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_id", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "id", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_����", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "����", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_����", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "����", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@id", System.Data.SqlDbType.Int, 4, "id"));
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(176, 256);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataSet11
			// 
			this.dataSet11.DataSetName = "DataSet1";
			this.dataSet11.Locale = new System.Globalization.CultureInfo("zh-CN");
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(440, 285);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGrid1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
		{
			int col=this.dataGrid1.CurrentCell.ColumnNumber;
			if(col==1)
			{
				Rectangle rect = this.dataGrid1.GetCurrentCellBounds();
				this.comboBox.Location = this.dataGrid1.PointToClient( new Point( rect.Left, rect.Top ) );
				comboBox.SetBounds( rect.Left, rect.Top, rect.Width, rect.Height );			
				comboBox.Visible = true;
				DataGridCell currentCell = this.dataGrid1.CurrentCell;
				for(int i=0;i<comboBox.Items.Count;i++)
				{
					if(comboBox.Items[i].ToString().Trim()==this.dataGrid1[currentCell.RowNumber,currentCell.ColumnNumber].ToString().Trim())
					{
						comboBox.SelectedIndex=i;
						break;
					}
				}
				this.dataGrid1[currentCell.RowNumber,currentCell.ColumnNumber]=comboBox.SelectedItem;
			}
			else
			{
				comboBox.Visible=false;
			}
		}

		private void dataGrid1_Scroll(object sender, System.EventArgs e)
		{
			this.comboBox.Visible = false;

		}

		private void comboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridCell currentCell = this.dataGrid1.CurrentCell;
			this.comboBox.Text=comboBox.SelectedItem.ToString();
			this.dataGrid1[currentCell.RowNumber,currentCell.ColumnNumber]=comboBox.Text;
		}
		private void FillcomboBox()
		{
			string sql="select * from bmmz order by ����";
			SqlConnection conn=new SqlConnection("Server=localhost;uid=sa;pwd=rainmajun;database=webdevelop");
			conn.Open();
			try
			{
				SqlCommand command=new SqlCommand(sql,conn);
				SqlDataReader r=command.ExecuteReader();
				while(r.Read())
				{
					comboBox.Items.Add(r["����"]);
				}
				r.Close();
				conn.Close();
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
			conn.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.sqlDataAdapter1.Update(this.dataSet11,"test");
				MessageBox.Show("����ɹ�","��ϲ");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"����ʧ��");
			}
		}

	}
}
