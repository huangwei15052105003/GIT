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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        //定义数据库的连接
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=学生绩点;Integrated Security=True");

        private void Bind()//绑定数据
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from student", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "allusers");
            this.dataGridView1.DataSource = ds.Tables["allusers"];
        }

        private void button1_Click(object sender, EventArgs e)  // 添加数据
        {
            conn.Open();
            String Sno = this.textBox1.Text.ToString();
            String Sname = this.textBox2.Text.ToString();
            String Ssex;
            if (this.radioButton1.Checked)
                Ssex = this.radioButton1.Text.ToString();
            else
                Ssex = this.radioButton2.Text.ToString();

            String sql = "insert into student values('" + Sno + "','" + Sname + "','" + Ssex + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("添加成功！");
        }

        private void button2_Click(object sender, EventArgs e)   // 修改数据
        {
            conn.Open();
            String Sno = this.textBox1.Text.ToString();
            String Sname = this.textBox2.Text.ToString();
            String Ssex;
            if (this.radioButton1.Checked)
                Ssex = this.radioButton1.Text.ToString();
            else
                Ssex = this.radioButton2.Text.ToString();

            String sql = "update student set Sname='" + Sname + "',Ssex='" + Ssex + "' where Sno='" + Sno + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("修改成功！");
        }

        private void button3_Click(object sender, EventArgs e)   // 删除数据
        {
            conn.Open();
            String Sno = this.textBox1.Text.ToString();
            String sql = "delete from student where Sno='" + Sno + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("修改成功！");
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“学生绩点DataSet.student”中。您可以根据需要移动或移除它。
            this.studentTableAdapter.Fill(this.学生绩点DataSet.student);

        }
    }
}
