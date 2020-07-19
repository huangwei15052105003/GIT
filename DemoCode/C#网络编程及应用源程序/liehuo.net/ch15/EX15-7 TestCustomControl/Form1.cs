using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestCustomControl
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TestCustomControl.CustomControl1 customControl11;
		private WindowsControlLibrary.EllipseButton ellipseButton1;
		private WindowsControlLibrary.EllipseButton ellipseButton2;
		private WindowsControlLibrary.EllipseButton ellipseButton3;
		private WindowsControlLibrary.EllipseButton ellipseButton4;
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.customControl11 = new TestCustomControl.CustomControl1();
			this.ellipseButton1 = new WindowsControlLibrary.EllipseButton();
			this.ellipseButton2 = new WindowsControlLibrary.EllipseButton();
			this.ellipseButton3 = new WindowsControlLibrary.EllipseButton();
			this.ellipseButton4 = new WindowsControlLibrary.EllipseButton();
			this.SuspendLayout();
			// 
			// customControl11
			// 
			this.customControl11.Location = new System.Drawing.Point(48, 24);
			this.customControl11.Name = "customControl11";
			this.customControl11.Size = new System.Drawing.Size(120, 64);
			this.customControl11.TabIndex = 0;
			this.customControl11.Text = "椭圆控件";
			// 
			// ellipseButton1
			// 
			this.ellipseButton1.EndColor = System.Drawing.Color.Blue;
			this.ellipseButton1.Location = new System.Drawing.Point(368, 16);
			this.ellipseButton1.Name = "ellipseButton1";
			this.ellipseButton1.Size = new System.Drawing.Size(104, 88);
			this.ellipseButton1.StartColor = System.Drawing.Color.White;
			this.ellipseButton1.TabIndex = 1;
			this.ellipseButton1.Text = "ellipseButton1";
			// 
			// ellipseButton2
			// 
			this.ellipseButton2.EndColor = System.Drawing.Color.Green;
			this.ellipseButton2.Location = new System.Drawing.Point(56, 120);
			this.ellipseButton2.Name = "ellipseButton2";
			this.ellipseButton2.Size = new System.Drawing.Size(100, 100);
			this.ellipseButton2.StartColor = System.Drawing.Color.White;
			this.ellipseButton2.TabIndex = 2;
			this.ellipseButton2.Text = "ellipseButton2";
			// 
			// ellipseButton3
			// 
			this.ellipseButton3.EndColor = System.Drawing.Color.Yellow;
			this.ellipseButton3.Location = new System.Drawing.Point(384, 136);
			this.ellipseButton3.Name = "ellipseButton3";
			this.ellipseButton3.Size = new System.Drawing.Size(100, 100);
			this.ellipseButton3.StartColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.ellipseButton3.TabIndex = 3;
			this.ellipseButton3.Text = "ellipseButton3";
			// 
			// ellipseButton4
			// 
			this.ellipseButton4.EndColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.ellipseButton4.Location = new System.Drawing.Point(224, 56);
			this.ellipseButton4.Name = "ellipseButton4";
			this.ellipseButton4.Size = new System.Drawing.Size(80, 152);
			this.ellipseButton4.StartColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.ellipseButton4.TabIndex = 4;
			this.ellipseButton4.Text = "ellipseButton4";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(560, 269);
			this.Controls.Add(this.ellipseButton4);
			this.Controls.Add(this.ellipseButton3);
			this.Controls.Add(this.ellipseButton2);
			this.Controls.Add(this.ellipseButton1);
			this.Controls.Add(this.customControl11);
			this.Name = "Form1";
			this.Text = "Form1";
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
	}
}
