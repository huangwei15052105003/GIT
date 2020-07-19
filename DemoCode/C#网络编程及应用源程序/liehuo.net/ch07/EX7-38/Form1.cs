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
	/// Form1 的摘要说明。
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
			string sqlstr="select * from pub_info";
			conn=new SqlConnection(connString);
			adapter=new SqlDataAdapter(sqlstr,conn);
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			adapter.UpdateCommand=builder.GetUpdateCommand();
			dataset=new DataSet();
			adapter.Fill(dataset,"pub_info");
			//将text1Box1的Text属性绑定到dataset中的pub_info表的pr_info字段
			this.textBox1.DataBindings.Add(new Binding("Text",dataset,"pub_info.pr_info"));
			for(int i=0;i<dataset.Tables[0].Rows.Count;i++)
			{
				this.listBox1.Items.Add(dataset.Tables[0].Rows[i][0]);
			}
			this.listBox1.SelectedIndex=0;

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
			this.buttonSave.Text = "保存";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonUpdateImage
			// 
			this.buttonUpdateImage.Location = new System.Drawing.Point(288, 256);
			this.buttonUpdateImage.Name = "buttonUpdateImage";
			this.buttonUpdateImage.Size = new System.Drawing.Size(112, 23);
			this.buttonUpdateImage.TabIndex = 4;
			this.buttonUpdateImage.Text = "更换图片";
			this.buttonUpdateImage.Click += new System.EventHandler(this.buttonUpdateImage_Click);
			// 
			// buttonMoveImage
			// 
			this.buttonMoveImage.Location = new System.Drawing.Point(104, 256);
			this.buttonMoveImage.Name = "buttonMoveImage";
			this.buttonMoveImage.Size = new System.Drawing.Size(112, 23);
			this.buttonMoveImage.TabIndex = 4;
			this.buttonMoveImage.Text = "移除图片";
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
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		private void ShowImage()
		{
			//释放占用的资源
			if (this.pictureBox1.Image != null) 
			{
				this.pictureBox1.Image.Dispose();
			}
			//如果当前纪录的图像字段不是SQL Server的空值，则显示图像
			//注意在C#中用Convert.DBNull表示SQL Server的空值null
			if(dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1]!=Convert.DBNull)
			{
				//将数据集中表的image字段值存入bytes字节数组中。
				byte[] bytes=(byte[])dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1];
				//利用字节数组产生MemoryStream对象
				MemoryStream memStream=new MemoryStream(bytes);
				//利用MemoryStream对象产生Bitmap位图对象
				Bitmap myImage = new Bitmap(memStream);
				//将Bitmap对象赋值给pictureBox1对象，显示图像
				this.pictureBox1.Image= myImage;
			}
			else
			{
				this.pictureBox1.Image = null;
			}

		}

		private void buttonUpdateImage_Click(object sender, System.EventArgs e)
		{
			//利用选择文件对话框选取图像文件
			OpenFileDialog openFileDialog1=new OpenFileDialog();
			openFileDialog1.Filter="*.jpg;*.bmp;*.*|*.jpg;*.bmp;*.*";
			//如果用户选取了图像文件
			if(openFileDialog1.ShowDialog()==DialogResult.OK)
			{
				//产生Stream流对象
				Stream myStream = openFileDialog1.OpenFile();
				int length=(int)myStream.Length;
				//产生字节数组对象
				byte[] bytes=new byte[length];
				//读取图像文件，将数据放入字节数组中。
				myStream.Read(bytes,0,length);
				myStream.Close();
				//将字节数组中的值存入数据集的表对应的image字段
				dataset.Tables[0].Rows[this.listBox1.SelectedIndex][1]=bytes;
				//重新显示图像
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
				MessageBox.Show("保存成功");
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message,"保存失败");
			}

		}

		private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ShowImage();
			this.BindingContext[dataset,"pub_info"].Position =this.listBox1.SelectedIndex;

		}
	}
}
