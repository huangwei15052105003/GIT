using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace EX14_18
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		Bitmap myBitmap,bitmap;
		Graphics g;
		int width,height;
		int start;
		string selectedItem;

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button buttonSelectFile;
		private System.Windows.Forms.Button buttonStart;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			string[] items=
			{
				"左到右拉伸",
				"上到下拉伸",
				"中间到四周扩散",
				"反转",
				"中间向两边拉伸",
				"上下对接",
				"左右对接"
			};
			this.listBox1.Items.AddRange(items);
			//默认选择"左到右拉伸"
			this.listBox1.SelectedIndex=0; 
			//没有选择文件时开始按钮无效
			this.buttonStart.Enabled=false; 
			this.listBox1.Enabled=false;
			//设置定时器时间间隔为10毫秒
			this.timer1.Interval=10; 

		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		static void Main() 
		{
			Application.Run(new Form1());
		}


		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonSelectFile = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Black;
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(328, 304);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(344, 64);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(224, 208);
			this.listBox1.TabIndex = 2;
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(432, 288);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.TabIndex = 3;
			this.buttonStart.Text = "开始";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(384, 24);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(152, 23);
			this.buttonSelectFile.TabIndex = 4;
			this.buttonSelectFile.Text = "选择图像文件";
			this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(600, 325);
			this.Controls.Add(this.buttonSelectFile);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSelectFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFile=new OpenFileDialog();
			openFile.Filter="*.jpg;*.bmp;*.*|*.jpg;*.bmp;*.*";
			if(openFile.ShowDialog()==DialogResult.OK)
			{
				//得到原始大小的图像
				Bitmap srcBitmap=new Bitmap(openFile.FileName);
				//得到缩放后的图像
				myBitmap=new Bitmap(
					srcBitmap,this.pictureBox1.Width,this.pictureBox1.Height);
				this.pictureBox1.Image=myBitmap;
				this.buttonStart.Enabled=true;
				this.listBox1.Enabled=true;
			}

		}

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			//图像宽度
			width=this.pictureBox1.Width;
			//图像高度
			height=this.pictureBox1.Height;
			selectedItem=this.listBox1.SelectedItem.ToString();
			if(selectedItem=="上下对接" || selectedItem=="左右对接")
			{
				bitmap=new Bitmap(width,height);
			}
			//为在PictureBox中画图作准备
			g=this.pictureBox1.CreateGraphics();
			//清除绘图面并将背景色填充为黑色
			g.Clear(Color.Black);
			if(selectedItem=="反转")
			{
				start=-height/2;
			}
			else
			{
				start=0;
			}
			//启动定时器
			this.timer1.Enabled=true;

		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			switch(selectedItem)
			{
				case "左到右拉伸":
				{
					if(start<=width)
					{
						g.DrawImage(myBitmap,0,0,start,height);
						start++;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "上到下拉伸":
				{
					if(start<=height)
					{
						g.DrawImage(myBitmap,0,0,width,start);
						start++;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "中间到四周扩散":
				{
					if(start<=width/2)
					{
						Rectangle destRect=new Rectangle(
							width/2-start,height/2-start,2*start,2*start);
						Rectangle srcRect=new Rectangle(
							0,0,myBitmap.Width,myBitmap.Height);
						g.DrawImage(myBitmap,destRect,srcRect,GraphicsUnit.Pixel);
						start++;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "反转":
				{
					if(start<=height/2)
					{
						Rectangle destRect=new Rectangle(0,height/2-start,width,2*start);
						Rectangle srcRect=new Rectangle(
							0,0,myBitmap.Width,myBitmap.Height);
						g.DrawImage(myBitmap,destRect,srcRect,GraphicsUnit.Pixel);
						start++;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "中间向两边拉伸":
				{
					if(start<=width/2)
					{
						Rectangle destRect=new Rectangle(width/2-start,0,2*start,height);
						Rectangle srcRect=new Rectangle(
							0,0,myBitmap.Width,myBitmap.Height);
						g.DrawImage(myBitmap,destRect,srcRect,GraphicsUnit.Pixel);
						start++;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "上下对接":
				{
					if(start<=height/2)
					{
						for(int i=0;i<=width-1;i++)
						{
							bitmap.SetPixel(i,start,myBitmap.GetPixel(i,start));
						}
						for(int i=0;i<=width-1;i++)
						{
							bitmap.SetPixel(
								i,height-start-1,myBitmap.GetPixel(i,height-start-1));
						}
						start++;
						this.pictureBox1.Refresh();
						this.pictureBox1.Image=bitmap;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
				case "左右对接":
				{
					if(start<=width/2)
					{
						for(int i=0;i<=height-1;i++)
						{
							bitmap.SetPixel(start,i,myBitmap.GetPixel(start,i));
						}
						for(int i=0;i<=height-1;i++)
						{
							bitmap.SetPixel(
								width-start-1,i,myBitmap.GetPixel(width-start-1,i));
						}
						start++;
						this.pictureBox1.Refresh();
						this.pictureBox1.Image=bitmap;
					}
					else
					{
						this.timer1.Enabled=false;
					}
					break;
				}
			}

		}
	}
}
