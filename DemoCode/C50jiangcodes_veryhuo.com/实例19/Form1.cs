using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ReadingXML
{
	/// <summary>
	/// 读取XML文件并将其内容显示在DataGrid组件中。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnReadXML;
		private System.Windows.Forms.Button btnShowSchema;
		private System.Windows.Forms.TextBox textBox1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
				if (components != null) 
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
			this.label1 = new System.Windows.Forms.Label();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnShowSchema = new System.Windows.Forms.Button();
			this.btnReadXML = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(570, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "读取XML文件中的数据";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(0, 23);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(570, 403);
			this.dataGrid1.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.textBox1,
																				 this.btnShowSchema,
																				 this.btnReadXML});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 258);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(570, 168);
			this.panel1.TabIndex = 5;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(570, 120);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "textBox1";
			// 
			// btnShowSchema
			// 
			this.btnShowSchema.Location = new System.Drawing.Point(160, 128);
			this.btnShowSchema.Name = "btnShowSchema";
			this.btnShowSchema.Size = new System.Drawing.Size(75, 32);
			this.btnShowSchema.TabIndex = 5;
			this.btnShowSchema.Text = "显示架构";
			this.btnShowSchema.Click += new System.EventHandler(this.btnShowSchema_Click);
			// 
			// btnReadXML
			// 
			this.btnReadXML.Location = new System.Drawing.Point(24, 128);
			this.btnReadXML.Name = "btnReadXML";
			this.btnReadXML.Size = new System.Drawing.Size(88, 32);
			this.btnReadXML.TabIndex = 4;
			this.btnReadXML.Text = "读取XML";
			this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(570, 426);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.dataGrid1,
																		  this.label1});
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "读取XML数据";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        // 申明一个数据集
		DataSet dsAddress = new DataSet("address");
		// 读取XML文件，并将读到的数据内容显示在DataGrid组件中。
		private void btnReadXML_Click(object sender, System.EventArgs e)
		{
			string filePath = "address.xml";
			dsAddress.ReadXml(filePath);
			dataGrid1.DataSource = dsAddress;
			dataGrid1.DataMember = "address";
			dataGrid1.CaptionText = dataGrid1.DataMember;
		}
		// 显示XML文件的框件结构
		private void btnShowSchema_Click(object sender, System.EventArgs e)
		{
			System.IO.StringWriter swXML = new System.IO.StringWriter();
			dsAddress.WriteXmlSchema(swXML);
			textBox1.Text = swXML.ToString();
		}
	}
}
