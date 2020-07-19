using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace EX14_18
{
	/// <summary>
	/// Form1 ��ժҪ˵����
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
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			string[] items=
			{
				"��������",
				"�ϵ�������",
				"�м䵽������ɢ",
				"��ת",
				"�м�����������",
				"���¶Խ�",
				"���ҶԽ�"
			};
			this.listBox1.Items.AddRange(items);
			//Ĭ��ѡ��"��������"
			this.listBox1.SelectedIndex=0; 
			//û��ѡ���ļ�ʱ��ʼ��ť��Ч
			this.buttonStart.Enabled=false; 
			this.listBox1.Enabled=false;
			//���ö�ʱ��ʱ����Ϊ10����
			this.timer1.Interval=10; 

		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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


		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			this.buttonStart.Text = "��ʼ";
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonSelectFile
			// 
			this.buttonSelectFile.Location = new System.Drawing.Point(384, 24);
			this.buttonSelectFile.Name = "buttonSelectFile";
			this.buttonSelectFile.Size = new System.Drawing.Size(152, 23);
			this.buttonSelectFile.TabIndex = 4;
			this.buttonSelectFile.Text = "ѡ��ͼ���ļ�";
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
				//�õ�ԭʼ��С��ͼ��
				Bitmap srcBitmap=new Bitmap(openFile.FileName);
				//�õ����ź��ͼ��
				myBitmap=new Bitmap(
					srcBitmap,this.pictureBox1.Width,this.pictureBox1.Height);
				this.pictureBox1.Image=myBitmap;
				this.buttonStart.Enabled=true;
				this.listBox1.Enabled=true;
			}

		}

		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			//ͼ����
			width=this.pictureBox1.Width;
			//ͼ��߶�
			height=this.pictureBox1.Height;
			selectedItem=this.listBox1.SelectedItem.ToString();
			if(selectedItem=="���¶Խ�" || selectedItem=="���ҶԽ�")
			{
				bitmap=new Bitmap(width,height);
			}
			//Ϊ��PictureBox�л�ͼ��׼��
			g=this.pictureBox1.CreateGraphics();
			//�����ͼ�沢������ɫ���Ϊ��ɫ
			g.Clear(Color.Black);
			if(selectedItem=="��ת")
			{
				start=-height/2;
			}
			else
			{
				start=0;
			}
			//������ʱ��
			this.timer1.Enabled=true;

		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			switch(selectedItem)
			{
				case "��������":
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
				case "�ϵ�������":
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
				case "�м䵽������ɢ":
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
				case "��ת":
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
				case "�м�����������":
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
				case "���¶Խ�":
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
				case "���ҶԽ�":
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
