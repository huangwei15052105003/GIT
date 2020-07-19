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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //查询数据
        {
            String sql = "select * from student where " + this.comboBox1.SelectedItem.ToString() + "='" + this.textBox1.Text.ToString()+"'";
            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection("server=(local);user id=sa;pwd=;database=学生绩点");
            SqlCommand cm = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
            cn.Close();
        }
    }
}
