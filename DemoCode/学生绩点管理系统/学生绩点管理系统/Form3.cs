using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 学生绩点管理系统
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //查询
        {
            String sql = "select * from course where Cno='" + this.textBox1.Text.ToString()+"'";
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection("server=(local);user id=sa;pwd=;database=学生绩点");
            SqlCommand cm = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
           

            /*   DataSet ds = new DataSet();
               String sql = "select * from course where Cno=" + this.textBox1.Text.ToString();
               SqlConnection conn = new SqlConnection("server=.\\LBWIN7;User ID=sa;Password=;database=学生绩点");
               SqlCommand cmd = new SqlCommand(sql, conn);
               try
               {                 
                           
                   SqlDataAdapter da = new SqlDataAdapter(cmd);                
                   da.Fill(ds);
                   dataGridView1.DataSource = ds.Tables[0];
               }
               catch (SqlException ex)
               {
                   MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }
               finally
               {
                   conn.Close();
               }
             *  */
           
        }

    }
}
