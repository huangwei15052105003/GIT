using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace MyForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {//查询数据
            var MyConnectString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=运货商.xlsx;Extended Properties=\"Excel 12.0;HDR=YES\";";
            //var MySQL = "SELECT *  FROM [运货商$A1:C2]";
            var MySQL = "SELECT *  FROM [运货商$]";
            var MyAdapter=new OleDbDataAdapter(MySQL, MyConnectString);
            var MyTable=new DataTable();
            MyAdapter.Fill(MyTable);
            this.DataGridView1.DataSource = MyTable;
        }
        private void Button2_Click(object sender, EventArgs e)
        {//插入数据
            var MyConnectString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=运货商.xlsx;Extended Properties=\"Excel 12.0;HDR=YES\";";
            var MyConnection=new OleDbConnection(MyConnectString);
            if (MyConnection.State == ConnectionState.Closed) 
                MyConnection.Open();
            var MySQL = "INSERT INTO [运货商$](公司名称,电话)VALUES ('" + this.TextBox1.Text+ "','"+ this.TextBox2.Text +"')";
            var MyCommand=new OleDbCommand(MySQL, MyConnection);            
            MyCommand.ExecuteNonQuery();
            Button1_Click(null, null);
        }
        private void Button3_Click(object sender, EventArgs e)
        {//修改数据
            var MyConnectString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=运货商.xlsx;Extended Properties=\"Excel 12.0;HDR=YES\";";
            var MyConnection=new OleDbConnection(MyConnectString);
            if(MyConnection.State == ConnectionState.Closed) 
                MyConnection.Open();
            var MySQL = "UPDATE [运货商$] SET 电话='" +this.TextBox2.Text+"' WHERE  公司名称= '"+this.TextBox1.Text+"'";
            var MyCommand=new OleDbCommand(MySQL, MyConnection);
            MyCommand.ExecuteNonQuery();
            Button1_Click(null, null);
        }
        private void Button4_Click(object sender, EventArgs e)
        {//新建工作表(在Excel文件型数据源中不支持删除操作)
            var MyConnectString =@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=运货商.xlsx;Extended Properties=""Excel 12.0;HDR=YES"";";
            var MyConnection=new OleDbConnection(MyConnectString);
            if(MyConnection.State == ConnectionState.Closed)
                MyConnection.Open();
            var MySQL = "CREATE TABLE 新运货商(公司名称 char(50), 电话 char(20));";
            var MyCommand=new OleDbCommand(MySQL, MyConnection);
            MyCommand.ExecuteNonQuery();
            MySQL = "INSERT INTO [新运货商$](公司名称,电话)VALUES ('" +this.TextBox1.Text+"','"+this.TextBox2.Text+"')";
            MyCommand = new OleDbCommand(MySQL, MyConnection);
            MyCommand.ExecuteNonQuery();
            MySQL = "SELECT *  FROM [新运货商$]";
            var MyAdapter=new OleDbDataAdapter(MySQL, MyConnectString);
            var MyTable=new DataTable();
            MyAdapter.Fill(MyTable);
            this.DataGridView1.DataSource = MyTable;
        }
    }
}
