using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//www.srcfans.com
namespace MyForm
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{//显示数据
			DataClasses1DataContext MyDC = new DataClasses1DataContext();
			var MyQuery = from Shipper in MyDC.Shippers
			              select Shipper;
			this.dataGridView1.DataSource = MyQuery;
		}
		private void button2_Click(object sender, EventArgs e)
        {//插入数据(在LINQ中直接执行SQL语句插入数据)
			DataClasses1DataContext MyDC = new DataClasses1DataContext();
			MyDC.ExecuteCommand("INSERT Shippers Values('Valueplus','023-40405690')");
		}
		private void button3_Click(object sender, EventArgs e)
        {//修改数据(在LINQ中直接执行SQL语句修改数据)		
			DataClasses1DataContext MyDC = new DataClasses1DataContext();
			MyDC.ExecuteCommand("Update Shippers Set Phone='(023)40405690' WHERE CompanyName='Valueplus'");
		}
		private void button4_Click(object sender, EventArgs e)
        {//删除数据(在LINQ中直接执行SQL语句删除数据)		
			DataClasses1DataContext MyDC = new DataClasses1DataContext();
			MyDC.ExecuteCommand("Delete From Shippers  WHERE CompanyName='Valueplus'");
		}
	}
}
