using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ����µ������ռ䡣
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Serialization
{
	/// <summary>
	/// �����л���
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
			this.button1.Text = "���";
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
			this.button2.Text = "����";
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
			this.label1.Text = "�ļ���:";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(54, 344);
			this.button3.Name = "button3";
			this.button3.TabIndex = 5;
			this.button3.Text = "ѡ��ͼƬ";
			this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "ͼ���ļ� (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|�Զ����ļ�(*.ser)|*.ser";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileName = "doc1";
			this.saveFileDialog1.Filter = "�Զ����ļ�(*.ser)|*.ser";
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
			this.Text = "�����л�";
			this.ResumeLayout(false);
// Downloads By http://www.veryhuo.com
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
		// ����ļ�������
		public Stream s;
		// ���л�����
		public BinaryFormatter f;
		// ���л����������
		private void button1_Click(object sender, System.EventArgs e)
		{
			if(pictureBox1.Image != null)
			{
				saveFileDialog1.Filter = "�Զ����ļ�(*.ser)|*.ser";
				if(saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					s = File.Create(saveFileDialog1.FileName);
					f = new BinaryFormatter();
					// �洢ͼ���ļ��Ͷ�Ӧ���ļ�����
					f.Serialize(s, pictureBox1.Image);
					f.Serialize(s, textBox1.Text);
					s.Close();
				}
			}
		}
		// ���л��������롣
		private void button2_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "�Զ����ļ�(*.ser)|*.ser";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				s = File.OpenRead(openFileDialog1.FileName);
				f = new BinaryFormatter();
				pictureBox1.Image = (Image)f.Deserialize(s);
				textBox1.Text = (string)f.Deserialize(s);
				s.Close();
			}
		}
		// ѡ��Ҫ�洢��ͼ���ļ���
		private void button3_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "ͼ���ļ�(*.bmp;*.jpg;*.jpeg;*.gif)|*.bmp;*.jpg;*.jpeg;*.gif";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
				textBox1.Text = openFileDialog1.FileName;
			}
		}
	}
}
