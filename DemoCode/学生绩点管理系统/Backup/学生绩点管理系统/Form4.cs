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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        //定义数据库连接
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=学生绩点;Integrated Security=True");
        
        private void Bind() //绑定数据
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from course", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "allusers");
            this.dataGridView1.DataSource = ds.Tables["allusers"];
        }

        private void button1_Click(object sender, EventArgs e)   //添加一行新数据
        {
       
     /*     这是我根据网上下载的代码修改写的，但不能运行，不知道怎么解决
      * 
      *     String sql = "INSERT INTO course ( " +"Cno,Cname,Ctype,Ccredit" +") VALUES ( " + " @Cno ,@Cname,@Ctype,@Ccredit" + ")";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.Parameters.Add("@Cno", SqlDbType.VarChar, 50, "Cno");
            cmd.Parameters.Add("@Cname", SqlDbType.VarChar, 50, "Cname");
            cmd.Parameters.Add("@Ctype", SqlDbType.VarChar, 50, "Ctype");
            cmd.Parameters.Add("@Ccredit", SqlDbType.VarChar, 50, "Ccredit");
            da.InsertCommand = cmd;

            DataRow newrow = dt.NewRow();
            newrow["Cno"] = this.textBox1.Text.ToString();
            newrow["Cname"] = this.textBox2.Text.ToString();
            newrow["Ctype"] = this.textBox3.Text.ToString();
            newrow["Ccredit"] = this.textBox4.Text.ToString();
            dt.Rows.Add(newrow);
            da.Update(dt);*/

            conn.Open();
            String Cno = this.textBox1.Text.ToString();
            String Cname = this.textBox2.Text.ToString();
            String Ctype = this.textBox3.Text.ToString();
            String Ccredit = this.textBox4.Text.ToString();

            String sql = "insert into course values('" + Cno + "','" + Cname + "','" + Ctype + "','" + Ccredit + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("添加成功！");
        }

        private void button2_Click(object sender, EventArgs e)  //修改指定行的数据
        {
            conn.Open();
            String Cno = this.textBox1.Text.ToString();
            String Cname = this.textBox2.Text.ToString();
            String Ctype = this.textBox3.Text.ToString();
            String Ccredit = this.textBox4.Text.ToString();

            String sql = "update course set Cname='" + Cname + "',Ctype='" + Ctype + "',Ccredit='" + Ccredit + "'  where Cno='" + Cno + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            this.Bind();
            conn.Close();
            MessageBox.Show("修改成功！");
        }

       

        private void button3_Click(object sender, EventArgs e)  //删除指定的行
        {
            
            if (MessageBox.Show("确定要删除这些数据吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                conn.Open();
                String Cno = this.textBox1.Text.ToString();
                String userName = this.textBox1.Text.ToString();
                String sql = "delete from course where Cno='" + Cno + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                this.Bind();
                conn.Close();
                MessageBox.Show("删除成功！");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“学生绩点DataSet1.course”中。您可以根据需要移动或移除它。
            this.courseTableAdapter1.Fill(this.学生绩点DataSet1.course);
        }


    }
}
