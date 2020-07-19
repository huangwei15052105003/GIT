using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//download by http://down.liehuo.net
namespace 学生绩点管理系统
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“学生绩点DataSet3.grade”中。您可以根据需要移动或移除它。
            this.gradeTableAdapter3.Fill(this.学生绩点DataSet3.grade);
            // TODO: 这行代码将数据加载到表“学生绩点DataSet1.grade”中。您可以根据需要移动或移除它。
            this.gradeTableAdapter2.Fill(this.学生绩点DataSet1.grade);
            // TODO: 这行代码将数据加载到表“学生绩点DataSet.course”中。您可以根据需要移动或移除它。
            this.courseTableAdapter.Fill(this.学生绩点DataSet.course);
            // TODO: 这行代码将数据加载到表“学生绩点DataSet2.grade”中。您可以根据需要移动或移除它。
            this.gradeTableAdapter1.Fill(this.学生绩点DataSet2.grade);
            // TODO: 这行代码将数据加载到表“学生绩点DataSet.grade”中。您可以根据需要移动或移除它。
            this.gradeTableAdapter.Fill(this.学生绩点DataSet.grade);

        }

        //定义数据库连接
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=学生绩点;Integrated Security=True");


        private void Bind()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from grade", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "allusers");
            this.dataGridView1.DataSource = ds.Tables["allusers"];
        }

        private void button1_Click(object sender, EventArgs e)  //添加数据
        {
            conn.Open();
            String Sno=this.textBox1.Text.ToString();
            String Cno=this.textBox2.Text.ToString();
            String Cgrade = this.textBox3.Text.ToString();
            String Ccredit=this.textBox4.Text.ToString();
            String sql = "insert into grade values('" + Sno + "','" + Cno + "','" + Cgrade + "','" + Ccredit + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("添加成功！");
        }

        private void button2_Click(object sender, EventArgs e)  //修改数据
        {
            conn.Open();
            String Sno = this.textBox1.Text.ToString();
            String Cno = this.textBox2.Text.ToString();
            String Cgrade = this.textBox3.Text.ToString();
            String sql = "update grade set Cgrade='" + Cgrade + "' where Sno='" + Sno + "' and Cno='" + Cno + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("修改成功！");
        }

        private void button3_Click(object sender, EventArgs e)  //删除数据
        {
            conn.Open();
            String Sno=this.textBox1.Text.ToString();
            String Cno=this.textBox2.Text.ToString();
            String sql = "delete from grade where Sno='" + Sno + "'and Cno='" + Cno + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("删除成功！");
        }

        private void 计算_Click(object sender, EventArgs e)  //计算平均绩点
        {
           
        }
    }
}
