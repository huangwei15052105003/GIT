using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EX7_32
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		DataSet ds;
		string dataMember;
		int recordCount;

		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button buttonFirst;
		private System.Windows.Forms.Button buttonPre;
		private System.Windows.Forms.Button buttonCurrent;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonLast;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
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
			dataMember="employee";
			BuildData();
			BindData();
			recordCount=this.BindingContext[ds,dataMember].Count;
			ShowPosition();
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
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.buttonFirst = new System.Windows.Forms.Button();
			this.buttonPre = new System.Windows.Forms.Button();
			this.buttonCurrent = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonLast = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(24, 48);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(296, 144);
			this.dataGrid1.TabIndex = 0;
			this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
			this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
			// 
			// buttonFirst
			// 
			this.buttonFirst.Location = new System.Drawing.Point(56, 232);
			this.buttonFirst.Name = "buttonFirst";
			this.buttonFirst.Size = new System.Drawing.Size(32, 23);
			this.buttonFirst.TabIndex = 1;
			this.buttonFirst.Text = "|<";
			this.buttonFirst.Click += new System.EventHandler(this.buttonFirst_Click);
			// 
			// buttonPre
			// 
			this.buttonPre.Location = new System.Drawing.Point(88, 232);
			this.buttonPre.Name = "buttonPre";
			this.buttonPre.Size = new System.Drawing.Size(32, 23);
			this.buttonPre.TabIndex = 1;
			this.buttonPre.Text = "<";
			this.buttonPre.Click += new System.EventHandler(this.buttonPre_Click);
			// 
			// buttonCurrent
			// 
			this.buttonCurrent.Location = new System.Drawing.Point(120, 232);
			this.buttonCurrent.Name = "buttonCurrent";
			this.buttonCurrent.Size = new System.Drawing.Size(88, 23);
			this.buttonCurrent.TabIndex = 1;
			this.buttonCurrent.Text = "无记录";
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(208, 232);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(32, 23);
			this.buttonNext.TabIndex = 1;
			this.buttonNext.Text = ">";
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonLast
			// 
			this.buttonLast.Location = new System.Drawing.Point(240, 232);
			this.buttonLast.Name = "buttonLast";
			this.buttonLast.Size = new System.Drawing.Size(32, 23);
			this.buttonLast.TabIndex = 1;
			this.buttonLast.Text = ">|";
			this.buttonLast.Click += new System.EventHandler(this.buttonLast_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(456, 40);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(120, 21);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "textBox1";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(456, 88);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(120, 21);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "textBox2";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(456, 128);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(120, 21);
			this.textBox3.TabIndex = 3;
			this.textBox3.Text = "textBox3";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(456, 176);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(120, 21);
			this.textBox4.TabIndex = 3;
			this.textBox4.Text = "textBox4";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(392, 240);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(200, 45);
			this.trackBar1.TabIndex = 4;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(384, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "emp_id";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(384, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "fname";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(384, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "lname";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(384, 176);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 23);
			this.label4.TabIndex = 5;
			this.label4.Text = "hire_date";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(624, 309);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.buttonFirst);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.buttonPre);
			this.Controls.Add(this.buttonCurrent);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonLast);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
		//该方法用于显示当前记录位置，并改变trackBar1控件值
		private void ShowPosition()
		{
			if(recordCount==0)
			{
				this.buttonCurrent.Text="无记录";
				textBox1.Text=textBox2.Text=textBox3.Text=textBox4.Text="";
			}
			else
			{
				int position=this.BindingContext[ds,dataMember].Position+1;
				this.buttonCurrent.Text=string.Format("{0} of {1}",position,recordCount);
				this.trackBar1.Value=position-1;
			}
		}

		//该方法用于建立数据集
		private void BuildData()
		{
			string connString= "server=localhost;integrated Security=SSPI;database=pubs";
			string sqlstr="select * from employee";
			SqlConnection conn=new SqlConnection(connString);
			SqlDataAdapter adapter=new SqlDataAdapter(sqlstr,conn);
			SqlCommandBuilder builder=new SqlCommandBuilder(adapter);
			ds=new DataSet();
			adapter.Fill(ds,dataMember);
		}

		//该方法用于绑定数据到dataGrid1和四个文本框控件
		private void BindData()
		{
			this.dataGrid1.AllowSorting=false;
			this.dataGrid1.SetDataBinding(ds,dataMember);
			this.textBox1.DataBindings.Add(new Binding("Text",ds,"employee.emp_id"));
			this.textBox2.DataBindings.Add(new Binding("Text",ds,"employee.fname"));
			this.textBox3.DataBindings.Add(new Binding("Text",ds,"employee.lname"));
			this.textBox4.DataBindings.Add(new Binding("Text",ds,"employee.hire_date"));
			this.trackBar1.Minimum=0;
			this.trackBar1.Maximum=this.BindingContext[ds,dataMember].Count-1;
		}

		private void buttonFirst_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[ds,dataMember].Position = 0;
			ShowPosition();

		}

		private void buttonPre_Click(object sender, System.EventArgs e)
		{
			if(this.BindingContext[ds,dataMember].Position > 0)
			{
				this.BindingContext[ds,dataMember].Position -= 1;
				ShowPosition();
			}

		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			if(this.BindingContext[ds,dataMember].Position < recordCount-1)
			{
				this.BindingContext[ds,dataMember].Position += 1;
				ShowPosition();
			}

		}

		private void buttonLast_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[ds,dataMember].Position=recordCount-1;
			ShowPosition();

		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			this.BindingContext[ds,dataMember].Position=this.trackBar1.Value;
			ShowPosition();

		}

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[ds,dataMember].Position=this.dataGrid1.CurrentRowIndex;
			ShowPosition();

		}

		private void dataGrid1_CurrentCellChanged(object sender, System.EventArgs e)
		{
			if(recordCount<=this.dataGrid1.CurrentCell.RowNumber)
			{
				recordCount=this.dataGrid1.CurrentCell.RowNumber+1;
				this.trackBar1.Maximum=recordCount-1;
			}
			this.BindingContext[ds,dataMember].Position=this.dataGrid1.CurrentCell.RowNumber;
			ShowPosition();

		}

	}
}
