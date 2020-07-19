using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using DxVBLib;

namespace DirectSound
{
	/// <summary>
	/// DirectSound实例。
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
			// 初始化各个私有变量。
			directX = new DirectX7();
			performance = directX.DirectMusicPerformanceCreate();
			composer = directX.DirectMusicComposerCreate();
			loader = directX.DirectMusicLoaderCreate();
			// 初始化Performance对象
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
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			// 释放资源
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
			this.button1.Location = new System.Drawing.Point(43, 40);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "歌曲一";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// button2
			//
			this.button2.Location = new System.Drawing.Point(161, 40);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "歌曲二";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// button3
			//
			this.button3.Location = new System.Drawing.Point(279, 40);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "歌曲三";
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
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}
		// 定义私有变量。
		private DirectX7 directX;
		private DirectMusicChordMap chordMap;
		private DirectMusicComposer composer;
		private DirectMusicPerformance performance;
		private DirectMusicSegment segment;
		private DirectMusicLoader loader;
		private DirectMusicStyle style;
		// 播放歌曲一
		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("disco.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "播放 Disco";
			}
			catch (Exception)
			{
				MessageBox.Show("不能播放Disco，请确认disco.sty在应用程序的同一个目录下面。");
			}
		}
		// 播放歌曲二
		private void button2_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("dance.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "播放 Dance";
			}
			catch (Exception)
			{
				MessageBox.Show("不能播放Dance，请确认dance.sty在应用程序的同一个目录下面。");
			}
		}
		// 播放歌曲三
		private void button3_Click(object sender, System.EventArgs e)
		{
			try
			{
				style = loader.LoadStyle("jazz.sty");
				segment = composer.ComposeSegmentFromShape(style, 64, 0, 2, false, false, chordMap);
				composer.AutoTransition(performance, segment, (int)CONST_DMUS_COMMANDT_TYPES.DMUS_COMMANDT_FILL, (int)CONST_DMUS_COMPOSEF_FLAGS.DMUS_COMPOSEF_IMMEDIATE, chordMap);
				this.Text = "播放 Jazz";
			}
			catch (Exception)
			{
				MessageBox.Show("不能播放Disco，请确认jazz.sty在应用程序的同一个目录下面。");
			}
		}
	}
}
