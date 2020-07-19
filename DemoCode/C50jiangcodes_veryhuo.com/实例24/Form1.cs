using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加自定义的命名空间。
using DxVBLib;

namespace DirectXShowCard
{
	/// <summary>
	/// 显示显卡的信息。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Windows 窗体设计器支持所必需的
			InitializeComponent();
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// button1
			this.button1.Location = new System.Drawing.Point(167, 264);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "显示";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// richTextBox1
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.richTextBox1.Size = new System.Drawing.Size(408, 256);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// Form1
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(408, 296);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.richTextBox1,
                this.button1});
			this.FormBorderStyle =
			    System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition =
			    System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "显示显卡的信息";
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
		// 添加自定义的私有变量。
		private static DirectX7 DirectX;
		private void button1_Click(object sender, System.EventArgs e)
		{
			DirectX = new DirectX7();
			DirectDrawEnum ddEnum;
			string guid;

			ddEnum = DirectX.GetDDEnum();
			richTextBox1.Text = "显卡信息:\n";
			for (int i = 1; i <= ddEnum.GetCount(); i++)
			{
				richTextBox1.Text += "显卡号码：" + i.ToString() + "\n";
				richTextBox1.Text += "描述：    " + ddEnum.GetDescription(i) + "\n";
				richTextBox1.Text += "显卡名称：" + ddEnum.GetName(i) + "\n";
				guid = ddEnum.GetGuid(i);
				if(guid == "")
					richTextBox1.Text += "Guid号码：无\n";
				else
					richTextBox1.Text += "Guid号码：" + ddEnum.GetGuid(i) + "\n";
				GetDDCaps(guid);
				GetD3DDevices(guid);
				GetDisplayModes(guid);
			}
		}
		private void GetDDCaps(string guid)
		{
			DirectDraw7 DirectDraw;
			DDCAPS hwCaps = new DDCAPS();		// 硬件信息
			DDCAPS helCaps = new DDCAPS();		// 软件信息

			DirectDraw = DirectX.DirectDrawCreate(guid);
			DirectDraw.GetCaps(ref hwCaps, ref helCaps);
			richTextBox1.Text += "\n硬件信息：\n";
			richTextBox1.Text += "    总的显存：  " + hwCaps.lVidMemTotal.ToString() + "\n";
			richTextBox1.Text += "    可用的显存：" + hwCaps.lVidMemFree.ToString() + "\n";

			// 支持的调色板。
			CONST_DDPCAPSFLAGS value = hwCaps.lPalCaps;
			if (value == 0)
				richTextBox1.Text += "    没有调色板。\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_1BIT) > 0)
				richTextBox1.Text += "    支持 1 Bit调色板。\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_2BIT) > 0)
				richTextBox1.Text += "    支持 2 Bit调色板。\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_8BIT) > 0)
				richTextBox1.Text += "    支持 8 Bit调色板。\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_8BITENTRIES) > 0)
				richTextBox1.Text += "    支持 8 Bit Entries 调色板。\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_ALLOW256) > 0)
				richTextBox1.Text += "    支持 256色调色板。\n";

			// 是否支持gamma ramp接口
			if (((int)hwCaps.ddsCaps.lCaps2 &
			     (int)CONST_DDCAPS2FLAGS.DDCAPS2_CANCALIBRATEGAMMA) > 0)
				richTextBox1.Text += "    支持gamma连接。\n";
			else
				richTextBox1.Text += "    不支持gamma连接。\n";
		}

		private void GetD3DDevices(string guid)
		{
			Direct3DEnumDevices d3dEnum;
			D3DDEVICEDESC7 hwDesc = new D3DDEVICEDESC7();
			DirectDraw7 DirectDraw;
			Direct3D7 d3d;

			DirectDraw = DirectX.DirectDrawCreate(guid);
			d3d = DirectDraw.GetDirect3D();
			richTextBox1.Text += "\nD3D设备信息：\n";
			d3dEnum = d3d.GetDevicesEnum();
			for (int i = 1; i <= d3dEnum.GetCount(); i++)
			{
				d3dEnum.GetDesc(i, ref hwDesc);
				richTextBox1.Text += "    描述：" + d3dEnum.GetDescription(i) + "\n";
				richTextBox1.Text += "    名称：" + d3dEnum.GetName(i) + "\n";
				richTextBox1.Text += "    Guid：" + d3dEnum.GetGuid(i) + "\n";
				richTextBox1.Text += "    最大纹理高度：" +
				    hwDesc.lMaxTextureHeight.ToString() + "\n";
				richTextBox1.Text += "    最大纹理宽度：" +
				    hwDesc.lMaxTextureWidth.ToString() + "\n";

				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_8) > 0)
					richTextBox1.Text += "    支持8位渲染。\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_16) > 0)
					richTextBox1.Text += "    支持16位渲染。\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_24) > 0)
					richTextBox1.Text += "    支持24位渲染。\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_32) > 0)
					richTextBox1.Text += "    支持32位渲染。\n";

				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_8) > 0)
					richTextBox1.Text += "    支持8比特的Z缓存。\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_16) > 0)
					richTextBox1.Text += "    支持16比特的Z缓存。\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)(int)CONST_DDBITDEPTHFLAGS.DDBD_24) > 0)
					richTextBox1.Text += "    支持24比特的Z缓存。\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_32) > 0)
					richTextBox1.Text += "    支持32比特的Z缓存。\n";
				if (hwDesc.lDeviceZBufferBitDepth == 0)
					richTextBox1.Text += "    不支持Z缓存。\n";

				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_TEXTURENONLOCALVIDMEM) > 0)
					richTextBox1.Text += "    支持AGP纹理。\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTDECREASINGZ) > 0)
					richTextBox1.Text += "    IM三角必须安递减深度排序。\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTEXACT) > 0)
					richTextBox1.Text += "    IM三角必须完全排序。\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTINCREASINGZ) > 0)
					richTextBox1.Text += "    IM三角必须安递增深度排序。\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_TEXTUREVIDEOMEMORY) > 0)
					richTextBox1.Text += "    IM三角可以使用显卡内存存储材质。\n";
			}
		}
		// 取得显示模式信息。
		public void GetDisplayModes(string guid)
		{
			DirectDrawEnumModes DisplayModesEnum;
			DDSURFACEDESC2 ddsd2 = new DDSURFACEDESC2();
			DirectDraw7 DirectDraw;

			DirectDraw = DirectX.DirectDrawCreate(guid);
			DisplayModesEnum = DirectDraw.GetDisplayModesEnum(0, ref ddsd2);
			richTextBox1.Text += "\n显示模式信息：\n";
			// 列举支持的显示模式。
			for (int i = 1; i <= DisplayModesEnum.GetCount(); i++)
			{
				DisplayModesEnum.GetItem(i, ref ddsd2);
				richTextBox1.Text += "    显示模式[" + i.ToString() + "]：";
				richTextBox1.Text += "宽度[" + ddsd2.lWidth.ToString() + "]，";
				richTextBox1.Text += "高度[" + ddsd2.lHeight.ToString() + "]，";
				richTextBox1.Text += "每象素的比特数[" + ddsd2.ddpfPixelFormat.lRGBBitCount.ToString() + "]，";
				richTextBox1.Text += "刷新频率[" + ddsd2.lRefreshRate.ToString()  + "]。" + "\n";
			}
		}
	}
}
