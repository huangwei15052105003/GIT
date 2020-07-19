using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestPrintTable
{
	/// <summary>
	/// PrintDataTable ��ժҪ˵����
	/// </summary>
	public class PrintDataTable : System.Windows.Forms.UserControl
	{
		private string connString;
		[DefaultValue("")]
		[Description("SQL Server�����ַ���")]
		public string ConnectionString
		{
			get
			{
				return connString;
			}
			set
			{
				connString=value;
			}
		}
		private string tableName;
		[DefaultValue("")]
		[Description("���ݿ��еı���")]
		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName=value;
			}
		}
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintDataTable()
		{
			// �õ����� Windows.Forms ���������������ġ�
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button1.Location = new System.Drawing.Point(164, 248);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(52, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "��ʾ";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(32, 64);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(536, 176);
			this.dataGrid1.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button2.Location = new System.Drawing.Point(252, 248);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(68, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "��ӡԤ��";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button3.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.button3.Location = new System.Drawing.Point(364, 248);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(56, 23);
			this.button3.TabIndex = 0;
			this.button3.Text = "��ӡ";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// printDocument1
			// 
			this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "����";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(72, 24);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(456, 21);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "textBox1";
			// 
			// PrintDataTable
			// 
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button3);
			this.Name = "PrintDataTable";
			this.Size = new System.Drawing.Size(600, 288);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlConnection conn=new SqlConnection(connString);
				string sql="select * from "+tableName;
				SqlDataAdapter adapter=new SqlDataAdapter(sql,conn);
				DataSet dataset=new DataSet();
				adapter.Fill(dataset,tableName);
				this.dataGrid1.DataSource=dataset.Tables[tableName];
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"����");
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//������ӡԤ���Ի���
			PrintPreviewDialog preview=new PrintPreviewDialog();
			//���ô�ӡԤ���ĵ�
			preview.Document=this.printDocument1;
			//��ʾ��ӡԤ�����ڣ���ʱ���Զ�����printDocument1_PrintPage�¼�
			preview.ShowDialog();
		}

		//����ǰҳ�����ӡʱ�ᴥ��printDocument1_PrintPage�¼�
		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Graphics g=e.Graphics;
			SolidBrush brush=new SolidBrush(Color.Black);
			//�����ַ������ʽ
			StringFormat format=new StringFormat(); 
			//�����ж��뷽ʽ
			format.LineAlignment=StringAlignment.Center; 
			//�����ı����뷽ʽ
			format.Alignment=StringAlignment.Center; 
			//���ñ����СΪ����3���ּӴ�
			Font titleFont = new Font("����", 15.75f,FontStyle.Bold);
			//�������
			g.DrawString(this.textBox1.Text,titleFont,brush,
				new  Rectangle(e.MarginBounds.Left,e.MarginBounds.Top,
				e.MarginBounds.Width,(int)titleFont.GetHeight()),format); 
			//�����ӡ����
			string dateString="[��ӡ����"+DateTime.Now.ToLongDateString()+"]";
			g.DrawString(dateString,this.Font,brush,
				new  Rectangle(e.MarginBounds.Left,e.MarginBounds.Top+30,
				e.MarginBounds.Width,(int)titleFont.GetHeight()),format); 
			//������ο�
			g.DrawRectangle(new Pen(Color.Black),e.MarginBounds.Left,
				e.MarginBounds.Top+55,e.MarginBounds.Width,e.MarginBounds.Height);
			//��ȡ���ݿ��б�
			SqlConnection conn=new SqlConnection(connString);
			string sql="select * from "+tableName;
			SqlDataAdapter adapter=new SqlDataAdapter(sql,conn);
			DataSet dataset=new DataSet();
			adapter.Fill(dataset,tableName);
			DataTable table=dataset.Tables[tableName];
			int[] columnLength=new int[table.Rows.Count];
			//����ÿһ�������
			for(int row=0;row<table.Rows.Count;row++)
			{
				for(int i=0;i<table.Columns.Count;i++)
				{
					if(table.Rows[row][i].ToString().Length>columnLength[i])
					{
						columnLength[i]=table.Rows[row][i].ToString().Length;
					}
				}
			}
			//�����ͷ
			string str="";
			for(int col=0;col<table.Columns.Count;col++)
			{
				str+=string.Format("{0,-"+Convert.ToString(columnLength[col]+5)+"}",table.Columns[col].ColumnName);
			}
			str=str.TrimEnd();
			titleFont = new Font("����", 10.5f);
			g.DrawString(str,titleFont,brush,e.MarginBounds.Left,e.MarginBounds.Top+65);
			int height=e.MarginBounds.Top+90;
			g.DrawLine(new Pen(Color.Black),e.MarginBounds.Left,height,e.MarginBounds.Left+e.MarginBounds.Width,height);
			//�������
			for(int row=0;row<table.Rows.Count;row++)
			{
				height+=10;
				str="";
				//�����е�ÿһ�����ݰ���ָ���ĸ�ʽ���
				for(int i=0;i<table.Columns.Count;i++)
				{
					//������ַ��ͣ�����룬�����Ҷ���
					if(table.Columns[i].DataType.Name=="String")
					{
						str+=string.Format("{0,-"+Convert.ToString(columnLength[i])+"}",table.Rows[row][i]);
					}
					else
					{
						str+=string.Format("{0,"+Convert.ToString(columnLength[i])+"}",table.Rows[row][i]);
					}
					//�ַ������油5���ո�
					str+=new string(' ',5);
				}
				//���һ��
				Font font = new Font("����", 10.5f);
				g.DrawString(str.TrimEnd(),font,brush,e.MarginBounds.Left,height);
				height+=15;
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			//������ӡ�Ի���
			PrintDialog printDialog=new PrintDialog();
			printDialog.Document=this.printDocument1;
			if(printDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					//������ӡ����
					this.printDocument1.Print();
				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message,"��ӡ����");
					//ֹͣ��ǰ��ӡ
					this.printDocument1.PrintController.OnEndPrint(this.printDocument1,
						new System.Drawing.Printing.PrintEventArgs());
				}
			}
		}
	}
}
