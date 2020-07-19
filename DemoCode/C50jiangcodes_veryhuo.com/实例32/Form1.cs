using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace Exception
{
	/// <summary>
	/// 异常处理。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(49, 72);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(119, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "除 0 异常";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(219, 72);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(119, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "无效对象异常";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(134, 160);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(119, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "自定义异常";
			this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(386, 258);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button3,
																		  this.button2,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "异常处理";
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
		class MyException:ApplicationException
		{
			public MyException(String msg):base(msg)
			{
				HelpLink = "http://NotARealURL.Microsoft.com/help.html";
			}
		}
		public void ShowException(System.Exception ex)
		{
			string str;
			str = string.Format("Exception:\n\t{0}\n", ex.GetType().ToString());
			str += string.Format("Message:\n\t{0}\n", ex.Message);
			str += string.Format("Stack Trace:\n\t{0}\n", ex.StackTrace);
			str += string.Format("Help Link:\n\t{0}\n", ex.HelpLink);
			MessageBox.Show(str);
		}
		// 除以 0 异常。
		private void button1_Click(object sender, System.EventArgs e)
		{
			int x = 0;
			try
			{
				// 产生异常。
				x = 10 / x;
			}
			catch(System.Exception ex)
			{
				ShowException(ex);
			}
		}
		// 无效对象异常。
		private void button2_Click(object sender, System.EventArgs e)
		{
			object a = null;
			try
			{
				MessageBox.Show(a.ToString());
			}
			catch(System.Exception ex)
			{
				ShowException(ex);
			}
		}
		// 自定义异常。
		private void button3_Click(object sender, System.EventArgs e)
		{
			try
			{
				throw(new MyException("这是我自己定义的异常。"));
			}
			catch(System.Exception ex)
			{
				ShowException(ex);
			}
		}
	}
}
