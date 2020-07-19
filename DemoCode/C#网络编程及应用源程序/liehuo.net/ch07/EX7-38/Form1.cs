using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EX7_38
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private string connString="server=localhost; integrated security=sspi; database=pubs";
		SqlConnection conn;
		SqlDataAdapter adapter;
		DataSet dataset;

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonUpdateImage;
		private System.Windows.Forms.Button buttonMoveImage;
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
			string sqlstr="select * from pub_info";
			conn=new SqlConnection(connString);
			adapter=new SqlDataAdapter(sqlstr,conn);
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			adapter.UpdateCommand=builder.GetUpdateCommand();
			dataset=new DataSet();
			adapter.Fill(dataset,"pub_info");
			//��text1Box1��Text���԰󶨵�dataset�е�pub_info���pr_info�ֶ�
			this.textBox1.DataBindings.Add(new Binding("Text",dataset,"pub_info.pr_info"));
			for(int i=0;i<dataset.Tables[0].Rows.Count;i++)
			{
				this.listBox1.Items.Add(dataset.Tables[0].Rows[i][0]);
			}
			this.listBox1.SelectedIndex=0;

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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonUpdateImage = new System.Windows.Forms.Button();
			this.buttonMoveImage = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(24, 16);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(144, 184);
			this.listBox1.TabIndex = 1;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(512, 24);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(152, 184);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "textBox1";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Location = new System.Drawing.Point(184, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(320, 240);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(488, 256);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "����";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonUpdateImage
			// 
			this.buttonUpdateImage.Location = new System.Drawing.Point(288, 256);
			this.buttonUpdateImage.Name = "buttonUpdateImage";
			this.buttonUpdateImage.Size = new System.Drawing.Size(112, 23);
			this.buttonUpdateImage.TabIndex = 4;
			this.buttonUpdateImage.Text = "����ͼƬ";
			this.buttonUpdateImage.Click += new System.EventHandler(this.buttonUpdateImage_Click);
			// 
			// buttonMoveImage
			// 
			this.buttonMoveImage.Location = new System.Drawing.Point(104, 256);
			this.buttonMoveImage.Name = "buttonMoveImage";
			this.buttonMoveImage.Size = new System.Drawing.Size(112, 23);
			this.buttonMoveImage.TabIndex = 4;
			this.buttonMoveImage.Text = "�Ƴ�ͼƬ";
			this.buttonMoveImage.Click += new System.EventHandler(this.buttonMoveImage_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 317);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.buttonUpdateImage);
			this.Controls.Add(this.buttonMoveImage);
			this.Name = "Form1";
			this.Text = "Form1";
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
		private void ShowImage()
		{
			//�ͷ�ռ�õ���Դ
			if (this.pictureBox1.Image != null) 
			{
				this.pictureBox1.Image.Dispose();
			}
			//�����ǰ��¼��ͼ���ֶβ���SQL Server�Ŀ�ֵ������ʾͼ��
			//ע����C#����Convert.DBNull��ʾSQL Server�Ŀ�ֵnull
			if(dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1]!=Convert.DBNull)
			{
				//�����ݼ��б��image�ֶ�ֵ����bytes�ֽ������С�
				byte[] bytes=(byte[])dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1];
				//�����ֽ��������MemoryStream����
				MemoryStream memStream=new MemoryStream(bytes);
				//����MemoryStream�������Bitmapλͼ����
				Bitmap myImage = new Bitmap(memStream);
				//��Bitmap����ֵ��pictureBox1������ʾͼ��
				this.pictureBox1.Image= myImage;
			}
			else
			{
				this.pictureBox1.Image = null;
			}

		}

		private void buttonUpdateImage_Click(object sender, System.EventArgs e)
		{
			//����ѡ���ļ��Ի���ѡȡͼ���ļ�
			OpenFileDialog openFileDialog1=new OpenFileDialog();
			openFileDialog1.Filter="*.jpg;*.bmp;*.*|*.jpg;*.bmp;*.*";
			//����û�ѡȡ��ͼ���ļ�
			if(openFileDialog1.ShowDialog()==DialogResult.OK)
			{
				//����Stream������
				Stream myStream = openFileDialog1.OpenFile();
				int length=(int)myStream.Length;
				//�����ֽ��������
				byte[] bytes=new byte[length];
				//��ȡͼ���ļ��������ݷ����ֽ������С�
				myStream.Read(bytes,0,length);
				myStream.Close();
				//���ֽ������е�ֵ�������ݼ��ı��Ӧ��image�ֶ�
				dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1]=bytes;
				//������ʾͼ��
				ShowImage();

			}
		}

		private void buttonMoveImage_Click(object sender, System.EventArgs e)
		{
			dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1]=Convert.DBNull;
			ShowImage();

		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				adapter.Update(dataset,"pub_info");
				MessageBox.Show("����ɹ�");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"����ʧ��");
			}

		}

		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowImage();
			this.BindingContext[dataset,"pub_info"].Position =this.listBox1.SelectedIndex;

		}
	}
}
