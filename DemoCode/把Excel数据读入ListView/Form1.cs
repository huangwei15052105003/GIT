using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//www.codesc.net
namespace ExcelReader
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private Excel.Application ExcelObj = null; 


		public Form1()
		{
			InitializeComponent();
			ExcelObj = new Excel.Application();
			//  看 Excel Application 对象是否已经成功生成
			if (ExcelObj == null)
			{
				MessageBox.Show("ERROR: EXCEL couldn't be started!");
				System.Windows.Forms.Application.Exit();
			}
			//  使程序可见
			ExcelObj.Visible = true;
		}

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.listView1 = new System.Windows.Forms.ListView();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuFile});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFileOpen,
																					this.menuItem2,
																					this.mnuFileExit});
			this.mnuFile.Text = "文件(&F)";
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Index = 0;
			this.mnuFileOpen.Text = "打开(&O)";
			this.mnuFileOpen.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 2;
			this.mnuFileExit.Text = "退出(&X)";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6,
																						this.columnHeader7});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(384, 225);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 20;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(384, 225);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView1});
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Excel文件阅读器";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			// 使打开文件对话框（openfiledialog）只显示Excel文件 
			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				// ***********  这里调用Open方法打开Excel工作簿 ****************
				// 多数使用缺省值 (除了 read-only我们设置它为 true)
				Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open(openFileDialog1.FileName, 0, true, 5,"", "", true, Excel.XlPlatform.xlWindows, "\t", false, false,	0, true);
				// 取得工作簿（workbook）中表单的集合（sheets）
				Excel.Sheets sheets = theWorkbook.Worksheets;
				// 取得表单集合中唯一的一个表（worksheet）
				Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);
				// 读取前10行，置入listview
				for (int i = 1; i <= 10; i++)
				{
					Excel.Range range = worksheet.get_Range("A"+i.ToString(), "J" + i.ToString());
					System.Array myvalues = (System.Array)range.Cells.Value;
					string[] strArray = ConvertToStringArray(myvalues);
					listView1.Items.Add(new ListViewItem(strArray));
				}
			}
		}

		private void mnuFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		string[] ConvertToStringArray(System.Array values)
		{ 
			// 生成新的字符串数组
			string[] theArray = new string[values.Length];

			// 把二维的数组转化为一维的字符串数组loop through the 2-D System.Array and populate the 1-D String Array
			for (int i = 1; i <= values.Length; i++)
			{
				if (values.GetValue(1, i) == null)
					theArray[i-1] = "";
				else
					theArray[i-1] = (string)values.GetValue(1, i).ToString();
			}

			return theArray;
		}

	}
}