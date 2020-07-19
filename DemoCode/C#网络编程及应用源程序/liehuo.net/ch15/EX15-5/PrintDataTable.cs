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
	/// PrintDataTable 的摘要说明。
	/// </summary>
	public class PrintDataTable : System.Windows.Forms.UserControl
	{
		private string connString;
		[DefaultValue("")]
		[Description("SQL Server连接字符串")]
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
		[Description("数据库中的表名")]
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
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintDataTable()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
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
			this.button1.Text = "显示";
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
			this.button2.Text = "打印预览";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.button3.Location = new System.Drawing.Point(364, 248);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(56, 23);
			this.button3.TabIndex = 0;
			this.button3.Text = "打印";
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
			this.label1.Text = "标题";
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
				MessageBox.Show(err.Message,"出错");
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//创建打印预览对话框
			PrintPreviewDialog preview=new PrintPreviewDialog();
			//设置打印预览文档
			preview.Document=this.printDocument1;
			//显示打印预览窗口，此时会自动触发printDocument1_PrintPage事件
			preview.ShowDialog();
		}

		//将当前页输出打印时会触发printDocument1_PrintPage事件
		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Graphics g=e.Graphics;
			SolidBrush brush=new SolidBrush(Color.Black);
			//设置字符输出格式
			StringFormat format=new StringFormat(); 
			//设置行对齐方式
			format.LineAlignment=StringAlignment.Center; 
			//设置文本对齐方式
			format.Alignment=StringAlignment.Center; 
			//设置标题大小为隶书3号字加粗
			Font titleFont = new Font("隶书", 15.75f,FontStyle.Bold);
			//输出标题
			g.DrawString(this.textBox1.Text,titleFont,brush,
				new  Rectangle(e.MarginBounds.Left,e.MarginBounds.Top,
				e.MarginBounds.Width,(int)titleFont.GetHeight()),format); 
			//输出打印日期
			string dateString="[打印日期"+DateTime.Now.ToLongDateString()+"]";
			g.DrawString(dateString,this.Font,brush,
				new  Rectangle(e.MarginBounds.Left,e.MarginBounds.Top+30,
				e.MarginBounds.Width,(int)titleFont.GetHeight()),format); 
			//输出矩形框
			g.DrawRectangle(new Pen(Color.Black),e.MarginBounds.Left,
				e.MarginBounds.Top+55,e.MarginBounds.Width,e.MarginBounds.Height);
			//读取数据库中表
			SqlConnection conn=new SqlConnection(connString);
			string sql="select * from "+tableName;
			SqlDataAdapter adapter=new SqlDataAdapter(sql,conn);
			DataSet dataset=new DataSet();
			adapter.Fill(dataset,tableName);
			DataTable table=dataset.Tables[tableName];
			int[] columnLength=new int[table.Rows.Count];
			//计算每一列最大宽度
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
			//输出表头
			string str="";
			for(int col=0;col<table.Columns.Count;col++)
			{
				str+=string.Format("{0,-"+Convert.ToString(columnLength[col]+5)+"}",table.Columns[col].ColumnName);
			}
			str=str.TrimEnd();
			titleFont = new Font("宋体", 10.5f);
			g.DrawString(str,titleFont,brush,e.MarginBounds.Left,e.MarginBounds.Top+65);
			int height=e.MarginBounds.Top+90;
			g.DrawLine(new Pen(Color.Black),e.MarginBounds.Left,height,e.MarginBounds.Left+e.MarginBounds.Width,height);
			//输出数据
			for(int row=0;row<table.Rows.Count;row++)
			{
				height+=10;
				str="";
				//将表中的每一行内容按照指定的格式输出
				for(int i=0;i<table.Columns.Count;i++)
				{
					//如果是字符型，左对齐，否则右对齐
					if(table.Columns[i].DataType.Name=="String")
					{
						str+=string.Format("{0,-"+Convert.ToString(columnLength[i])+"}",table.Rows[row][i]);
					}
					else
					{
						str+=string.Format("{0,"+Convert.ToString(columnLength[i])+"}",table.Rows[row][i]);
					}
					//字符串后面补5个空格
					str+=new string(' ',5);
				}
				//输出一行
				Font font = new Font("宋体", 10.5f);
				g.DrawString(str.TrimEnd(),font,brush,e.MarginBounds.Left,height);
				height+=15;
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			//创建打印对话框
			PrintDialog printDialog=new PrintDialog();
			printDialog.Document=this.printDocument1;
			if(printDialog.ShowDialog()==DialogResult.OK)
			{
				try
				{
					//启动打印操作
					this.printDocument1.Print();
				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message,"打印出错");
					//停止当前打印
					this.printDocument1.PrintController.OnEndPrint(this.printDocument1,
						new System.Drawing.Printing.PrintEventArgs());
				}
			}
		}
	}
}
