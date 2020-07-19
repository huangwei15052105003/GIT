using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// 添加新的命名空间。
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Serialization
{
	/// <summary>
	/// 对象串行化。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(164, 344);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "输出";
			this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(72, 313);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(320, 25);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(402, 304);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(274, 344);
			this.button2.Name = "button2";
			this.button2.TabIndex = 3;
			this.button2.Text = "输入";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 320);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "文件名:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(54, 344);
			this.button3.Name = "button3";
			this.button3.TabIndex = 5;
			this.button3.Text = "选择图片";
			this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "图像文件 (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|自定义文件(*.ser)|*.ser";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileName = "doc1";
			this.saveFileDialog1.Filter = "自定义文件(*.ser)|*.ser";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
			this.ClientSize = new System.Drawing.Size(402, 370);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button3,
																		  this.label1,
																		  this.button2,
																		  this.pictureBox1,
																		  this.textBox1,
																		  this.button1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "对象串行化";
			this.ResumeLayout(false);
// Downloads By http://www.veryhuo.com
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
		// 输出文件流对象。
		public Stream s;
		// 串行化对象。
		public BinaryFormatter f;
		// 串行化对象输出。
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(pictureBox1.Image != null)
			{
				saveFileDialog1.Filter = "自定义文件(*.ser)|*.ser";
				if(saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					s = File.Create(saveFileDialog1.FileName);
					f = new BinaryFormatter();
					// 存储图形文件和对应的文件名。
					f.Serialize(s, pictureBox1.Image);
					f.Serialize(s, textBox1.Text);
					s.Close();
				}
			}
		}
		// 串行化对象输入。
		private void button2_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "自定义文件(*.ser)|*.ser";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				s = File.OpenRead(openFileDialog1.FileName);
				f = new BinaryFormatter();
				pictureBox1.Image = (Image)f.Deserialize(s);
				textBox1.Text = (string)f.Deserialize(s);
				s.Close();
			}
		}
		// 选择要存储的图形文件。
		private void button3_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "图形文件(*.bmp;*.jpg;*.jpeg;*.gif)|*.bmp;*.jpg;*.jpeg;*.gif";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
				textBox1.Text = openFileDialog1.FileName;
			}
		}
	}
}
