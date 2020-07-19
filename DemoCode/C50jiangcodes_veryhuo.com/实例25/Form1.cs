using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using DxVBLib;

namespace DirectDraw1
{
	/// <summary>
	/// DirectDraw实例：显示图片。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
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
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码。
			//
			DirectX = new DirectX7();
			InitializeDirectX();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			//
			// panel1
			//
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(440, 320);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(440, 320);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
				  this.panel1});
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "显示图片";
			this.Resize += new System.EventHandler(this.Form1_Resize);
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
		private DirectX7 DirectX = null;
		private DirectDraw7 DirectDraw = null;
		private DirectDrawSurface7 Surface = null;
		private DirectDrawSurface7 PrimarySurface = null;
		private DDSURFACEDESC2 Surface1;
		private DDSURFACEDESC2 Surface2;
		private DirectDrawClipper Clipper = null;
		private Boolean bInit;
		private void Blt()
		{
			// 判断是否初始化成功。
			if (bInit == false)
				return;

			DxVBLib.RECT r1 = new DxVBLib.RECT();
			DxVBLib.RECT r2 = new DxVBLib.RECT();

			// 得到窗口边界大小。
			DirectX.GetWindowRect(panel1.Handle.ToInt32(), ref r1);
			// 按照新的边界大小显示图片。
			r2.Bottom = Surface2.lHeight;
			r2.Right = Surface2.lWidth;
			PrimarySurface.Blt(ref r1, Surface, ref r2, CONST_DDBLTFLAGS.DDBLT_WAIT);
		}
		private void InitializeDirectX()
		{
			// 初始化各个变量。
			DirectDraw = DirectX.DirectDrawCreate("");
			DirectDraw.SetCooperativeLevel(this.Handle.ToInt32(), CONST_DDSCLFLAGS.DDSCL_NORMAL);

			Surface1.lFlags = CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			Surface1.ddsCaps.lCaps = CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE;
			PrimarySurface = DirectDraw.CreateSurface(ref Surface1);

			Surface2.lFlags = CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			Surface2.ddsCaps.lCaps = CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN;

			try
			{
				Surface = DirectDraw.CreateSurfaceFromFile("sample.bmp", ref Surface2); //background.bmp
			}
			catch(System.Runtime.InteropServices.COMException e)
			{
				// 没有找到文件。
				if ( (uint)e.ErrorCode == 0x800A0035)
				{
					MessageBox.Show("没有找到文件'sample.bmp'.\n该文件必须和程序放在一个目录下面。", "图片没有找到");
				}
				else
				{
					MessageBox.Show("异常: " + e.ToString(), "异常信息");
				}
				Application.Exit();
				Application.DoEvents();
			}
			Clipper = DirectDraw.CreateClipper(0);
			Clipper.SetHWnd(panel1.Handle.ToInt32());
			PrimarySurface.SetClipper(Clipper);
			// 初始化完成。
			bInit = true;
			Blt();
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DirectDraw.RestoreAllSurfaces();
			Blt();
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			panel1.Width = this.ClientSize.Width;
			panel1.Height = this.ClientSize.Height;
			Blt();
		}
	}
}
