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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //查询数据
        {
            String sql = "select * from grade where Sno='" + this.textBox1.Text.ToString()+"'" ;
            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection("server=(local);user id=sa;pwd=;database=学生绩点");
            SqlCommand cm = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)  //计算绩点
        {
            Double avg = 0; //存放平均绩点
            Double sum = 0; //存放几点总和
            Double temp = 0; //存放学分总和
            Double a, b;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                a = Convert.ToDouble( dataGridView1.Rows[i].Cells[3].Value );
                b = (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value) - 60) / 10 + 1;
                temp = temp + a;
                sum = sum +b * a;
            }
            avg = Math.Round(sum / temp, 2);  //保留两位小数
            MessageBox.Show("该学生的平均绩点为：" + avg);

            String str="";
            if (avg <= 1.9)
            {
               
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (Convert.ToDouble(dataGridView1.Rows[j].Cells[2].Value) <= 70)
                    {
                        str=str+dataGridView1.Rows[j].Cells[1].Value+" ";
                    }
                }
                MessageBox.Show("建议重修课程号为：" + str + "的课程");
            }
           
        }

    }
}
