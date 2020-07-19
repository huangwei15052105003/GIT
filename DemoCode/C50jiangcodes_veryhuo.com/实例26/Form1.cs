using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using DxVBLib;

namespace DirectSound
{
	/// <summary>
	/// DirectSoundʵ����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// ����������������
		/// </summary>
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
			// ��ʼ������˽�б�����
			directX = new DirectX7();
			performance = directX.DirectMusicPerformanceCreate();
			composer = directX.DirectMusicComposerCreate();
			loader = directX.DirectMusicLoaderCreate();
			// ��ʼ��Performance����
			performance.Init(null, 0);
			performance.SetPort(-1, 4);
			performance.SetMasterAutoDownload(true);
			try
			{
				chordMap = loader.LoadChordMap("CHORDMAP.CDM");
			}
			catch (Exception)
			{
				MessageBox.Show("Could not load ChordMap.  Please ensure that CHORDMAP.CDM is in the directory of this executable.");
			}
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			// �ͷ���Դ
			if (performance != null) performance.CloseDown();
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
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			// button1
			//
			this.button1.Location = new System.Drawing.Point(43, 40);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "����һ";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(161, 40);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "������";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// button3
			//
			this.button3.Location = new System.Drawing.Point(279, 40);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "������";
			this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			//
			// Form1
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(408, 232);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.button3,
                this.button2,
                this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DirectSound";
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
		// ����˽�б�����
		private DirectX7 directX;
		private DirectMusicChordMap chordMap;
		private DirectMusicComposer composer;
		private DirectMusicPerformance performance;
		private DirectMusicSegment segment;
		private DirectMusicLoader loader;
		private DirectMusicStyle style;
		// ���Ÿ���һ
		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("disco.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "���� Disco";
			}
			catch (Exception)
			{
				MessageBox.Show("���ܲ���Disco����ȷ��disco.sty��Ӧ�ó����ͬһ��Ŀ¼���档");
			}
		}
		// ���Ÿ�����
		private void button2_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("dance.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "���� Dance";
			}
			catch (Exception)
			{
				MessageBox.Show("���ܲ���Dance����ȷ��dance.sty��Ӧ�ó����ͬһ��Ŀ¼���档");
			}
		}
		// ���Ÿ�����
		private void button3_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("jazz.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "���� Jazz";
			}
			catch (Exception)
			{
				MessageBox.Show("���ܲ���Disco����ȷ��jazz.sty��Ӧ�ó����ͬһ��Ŀ¼���档");
			}
		}
	}
}
