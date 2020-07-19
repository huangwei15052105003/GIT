using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Drawing.Drawing2D;

namespace EX14_16
{
	/// <summary>
	/// Form1 的摘要说明。
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

		#region Windows 窗体设计器生成的代码
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
			this.button1.Location = new System.Drawing.Point(152, 320);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "画图";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(288, 320);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "保存";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(424, 320);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "显示";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(640, 357);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
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

		private void button1_Click(object sender, System.EventArgs e)
		{
			Graphics g = this.CreateGraphics();
			DrawMyImage(g);
			g.Dispose();
		}
		private void DrawMyImage(Graphics g)
		{
			Rectangle rect1=new Rectangle(0,0,this.Width/4,this.Height-100);
			HatchBrush hatchBrush = new HatchBrush(HatchStyle.Shingle, Color.White, Color.Black);
			g.FillEllipse(hatchBrush, rect1);
			Rectangle rect2=new Rectangle(this.Width/4+50,0,this.Width/4,this.Height-100);
			hatchBrush = new HatchBrush(HatchStyle.WideUpwardDiagonal, Color.White, Color.Red);
			g.FillRectangle(hatchBrush,rect2);
			int x=this.Width-50-this.Width/4;
			Point[] points =new Point[]
			{
				new Point(x,10),
				new Point(x+50,60),
				new Point(x+150,10),
				new Point(x+200,160),
				new Point(x+150,260),
				new Point(x+50,260),
				new Point(x,160)
			};
			hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.White, Color.Red);
			TextureBrush myBrush = new TextureBrush(new Bitmap(@"e:\test\m23.jpg"));
			g.FillClosedCurve(myBrush,points);
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			//构造一个指定区域的空图像
			Bitmap image=new Bitmap(this.Width,this.Height-100);
			//根据指定区域得到Graphics对象
			Graphics g=Graphics.FromImage(image);
			//设置图像的背景色
			g.Clear(this.BackColor);
			//将图形画到Graphics对象中
			DrawMyImage(g);
			try
			{
				//保存画到Graphics对象中的图形
				image.Save(@"d:\test\tu1.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
				g=this.CreateGraphics();
				Rectangle rect=new Rectangle(0,0,this.Width,this.Height-100);
				g.FillRectangle(new SolidBrush(this.BackColor),rect);
				MessageBox.Show("保存成功！","恭喜");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
			}
			image.Dispose();
			g.Dispose();

		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Rectangle rect=new Rectangle(0,0,this.Width,this.Height-100);
			Graphics g=this.CreateGraphics();
			Image image=Image.FromFile(@"d:\test\tu1.jpg");
			//将图像缩放为指定的大小显示
			g.DrawImage(image,rect);
			image.Dispose();
			g.Dispose();
		}

	}
}
