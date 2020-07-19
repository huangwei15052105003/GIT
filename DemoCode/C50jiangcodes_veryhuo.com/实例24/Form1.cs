using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����Զ���������ռ䡣
using DxVBLib;

namespace DirectXShowCard
{
	/// <summary>
	/// ��ʾ�Կ�����Ϣ��
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			// Windows ���������֧���������
			InitializeComponent();
			// TODO: �� InitializeComponent ���ú�����κι��캯������
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
			this.button1 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// button1
			this.button1.Location = new System.Drawing.Point(167, 264);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "��ʾ";
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
			this.Text = "��ʾ�Կ�����Ϣ";
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
		// ����Զ����˽�б�����
		private static DirectX7 DirectX;
		private void button1_Click(object sender, System.EventArgs e)
		{
			DirectX = new DirectX7();
			DirectDrawEnum ddEnum;
			string guid;

			ddEnum = DirectX.GetDDEnum();
			richTextBox1.Text = "�Կ���Ϣ:\n";
			for (int i = 1; i <= ddEnum.GetCount(); i++)
			{
				richTextBox1.Text += "�Կ����룺" + i.ToString() + "\n";
				richTextBox1.Text += "������    " + ddEnum.GetDescription(i) + "\n";
				richTextBox1.Text += "�Կ����ƣ�" + ddEnum.GetName(i) + "\n";
				guid = ddEnum.GetGuid(i);
				if(guid == "")
					richTextBox1.Text += "Guid���룺��\n";
				else
					richTextBox1.Text += "Guid���룺" + ddEnum.GetGuid(i) + "\n";
				GetDDCaps(guid);
				GetD3DDevices(guid);
				GetDisplayModes(guid);
			}
		}
		private void GetDDCaps(string guid)
		{
			DirectDraw7 DirectDraw;
			DDCAPS hwCaps = new DDCAPS();		// Ӳ����Ϣ
			DDCAPS helCaps = new DDCAPS();		// �����Ϣ

			DirectDraw = DirectX.DirectDrawCreate(guid);
			DirectDraw.GetCaps(ref hwCaps, ref helCaps);
			richTextBox1.Text += "\nӲ����Ϣ��\n";
			richTextBox1.Text += "    �ܵ��Դ棺  " + hwCaps.lVidMemTotal.ToString() + "\n";
			richTextBox1.Text += "    ���õ��Դ棺" + hwCaps.lVidMemFree.ToString() + "\n";

			// ֧�ֵĵ�ɫ�塣
			CONST_DDPCAPSFLAGS value = hwCaps.lPalCaps;
			if (value == 0)
				richTextBox1.Text += "    û�е�ɫ�塣\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_1BIT) > 0)
				richTextBox1.Text += "    ֧�� 1 Bit��ɫ�塣\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_2BIT) > 0)
				richTextBox1.Text += "    ֧�� 2 Bit��ɫ�塣\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_8BIT) > 0)
				richTextBox1.Text += "    ֧�� 8 Bit��ɫ�塣\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_8BITENTRIES) > 0)
				richTextBox1.Text += "    ֧�� 8 Bit Entries ��ɫ�塣\n";
			if ((value & CONST_DDPCAPSFLAGS.DDPCAPS_ALLOW256) > 0)
				richTextBox1.Text += "    ֧�� 256ɫ��ɫ�塣\n";

			// �Ƿ�֧��gamma ramp�ӿ�
			if (((int)hwCaps.ddsCaps.lCaps2 &
			     (int)CONST_DDCAPS2FLAGS.DDCAPS2_CANCALIBRATEGAMMA) > 0)
				richTextBox1.Text += "    ֧��gamma���ӡ�\n";
			else
				richTextBox1.Text += "    ��֧��gamma���ӡ�\n";
		}

		private void GetD3DDevices(string guid)
		{
			Direct3DEnumDevices d3dEnum;
			D3DDEVICEDESC7 hwDesc = new D3DDEVICEDESC7();
			DirectDraw7 DirectDraw;
			Direct3D7 d3d;

			DirectDraw = DirectX.DirectDrawCreate(guid);
			d3d = DirectDraw.GetDirect3D();
			richTextBox1.Text += "\nD3D�豸��Ϣ��\n";
			d3dEnum = d3d.GetDevicesEnum();
			for (int i = 1; i <= d3dEnum.GetCount(); i++)
			{
				d3dEnum.GetDesc(i, ref hwDesc);
				richTextBox1.Text += "    ������" + d3dEnum.GetDescription(i) + "\n";
				richTextBox1.Text += "    ���ƣ�" + d3dEnum.GetName(i) + "\n";
				richTextBox1.Text += "    Guid��" + d3dEnum.GetGuid(i) + "\n";
				richTextBox1.Text += "    �������߶ȣ�" +
				    hwDesc.lMaxTextureHeight.ToString() + "\n";
				richTextBox1.Text += "    ��������ȣ�" +
				    hwDesc.lMaxTextureWidth.ToString() + "\n";

				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_8) > 0)
					richTextBox1.Text += "    ֧��8λ��Ⱦ��\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_16) > 0)
					richTextBox1.Text += "    ֧��16λ��Ⱦ��\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_24) > 0)
					richTextBox1.Text += "    ֧��24λ��Ⱦ��\n";
				if ((hwDesc.lDeviceRenderBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_32) > 0)
					richTextBox1.Text += "    ֧��32λ��Ⱦ��\n";

				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_8) > 0)
					richTextBox1.Text += "    ֧��8���ص�Z���档\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_16) > 0)
					richTextBox1.Text += "    ֧��16���ص�Z���档\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)(int)CONST_DDBITDEPTHFLAGS.DDBD_24) > 0)
					richTextBox1.Text += "    ֧��24���ص�Z���档\n";
				if ((hwDesc.lDeviceZBufferBitDepth & (int)CONST_DDBITDEPTHFLAGS.DDBD_32) > 0)
					richTextBox1.Text += "    ֧��32���ص�Z���档\n";
				if (hwDesc.lDeviceZBufferBitDepth == 0)
					richTextBox1.Text += "    ��֧��Z���档\n";

				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_TEXTURENONLOCALVIDMEM) > 0)
					richTextBox1.Text += "    ֧��AGP����\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTDECREASINGZ) > 0)
					richTextBox1.Text += "    IM���Ǳ��밲�ݼ��������\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTEXACT) > 0)
					richTextBox1.Text += "    IM���Ǳ�����ȫ����\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_SORTINCREASINGZ) > 0)
					richTextBox1.Text += "    IM���Ǳ��밲�����������\n";
				if ((hwDesc.lDevCaps & CONST_D3DDEVICEDESCCAPS.D3DDEVCAPS_TEXTUREVIDEOMEMORY) > 0)
					richTextBox1.Text += "    IM���ǿ���ʹ���Կ��ڴ�洢���ʡ�\n";
			}
		}
		// ȡ����ʾģʽ��Ϣ��
		public void GetDisplayModes(string guid)
		{
			DirectDrawEnumModes DisplayModesEnum;
			DDSURFACEDESC2 ddsd2 = new DDSURFACEDESC2();
			DirectDraw7 DirectDraw;

			DirectDraw = DirectX.DirectDrawCreate(guid);
			DisplayModesEnum = DirectDraw.GetDisplayModesEnum(0, ref ddsd2);
			richTextBox1.Text += "\n��ʾģʽ��Ϣ��\n";
			// �о�֧�ֵ���ʾģʽ��
			for (int i = 1; i <= DisplayModesEnum.GetCount(); i++)
			{
				DisplayModesEnum.GetItem(i, ref ddsd2);
				richTextBox1.Text += "    ��ʾģʽ[" + i.ToString() + "]��";
				richTextBox1.Text += "���[" + ddsd2.lWidth.ToString() + "]��";
				richTextBox1.Text += "�߶�[" + ddsd2.lHeight.ToString() + "]��";
				richTextBox1.Text += "ÿ���صı�����[" + ddsd2.ddpfPixelFormat.lRGBBitCount.ToString() + "]��";
				richTextBox1.Text += "ˢ��Ƶ��[" + ddsd2.lRefreshRate.ToString()  + "]��" + "\n";
			}
		}
	}
}
