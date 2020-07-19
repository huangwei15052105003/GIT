using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace sgFrmCheckWork
{
    public partial class sgFrmManage : Office2007Form
    {
        public sgFrmManage()
        {
            InitializeComponent();
        }

        private void sgFrmManage_Load(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = sgClass.DbHelperSQL.Query("select ID as 工号,Name as 姓名,PinYin as 拼音,Sex as 性别, InDate as 入职日期, OutDate as 离职日期,Sound as 声音文件 from [user]").Tables[0];
            
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            sgFrmAdd sgFrmAdd = new sgFrmAdd();
            sgFrmAdd.auMD += new aupManData(sgFrmAdd_auMD);
            sgFrmAdd.Show();
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value.ToString());
            string strID = dataGridViewX1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string strName = dataGridViewX1.Rows[e.RowIndex].Cells[1].Value.ToString();
            string strPinYin = dataGridViewX1.Rows[e.RowIndex].Cells[2].Value.ToString();
            string strSex = dataGridViewX1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string strInDate = dataGridViewX1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string strOutDate = dataGridViewX1.Rows[e.RowIndex].Cells[5].Value.ToString();
            string strSound = dataGridViewX1.Rows[e.RowIndex].Cells[6].Value.ToString();

            sgFrmUpdate sgFrmUpdate = new sgFrmUpdate(strID,strName,strPinYin,strSex,strInDate,strOutDate,strSound);
            sgFrmUpdate.uMD += new upManData(sgFrmUpdate_uMD);
            sgFrmUpdate.Show();
        }

        void sgFrmUpdate_uMD()
        {
            dataGridViewX1.DataSource = sgClass.DbHelperSQL.Query("select ID as 工号,Name as 姓名,PinYin as 拼音,Sex as 性别, InDate as 入职日期, OutDate as 离职日期,Sound as 声音文件 from [user]").Tables[0];
        }
        void sgFrmAdd_auMD()
        {
            dataGridViewX1.DataSource = sgClass.DbHelperSQL.Query("select ID as 工号,Name as 姓名,PinYin as 拼音,Sex as 性别, InDate as 入职日期, OutDate as 离职日期,Sound as 声音文件 from [user]").Tables[0];
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            string strID = dataGridViewX1.SelectedCells[0].Value.ToString().Trim();
            string strName = dataGridViewX1.SelectedCells[1].Value.ToString().Trim();
            string strPinYin = dataGridViewX1.SelectedCells[2].Value.ToString().Trim();
            string strSex = dataGridViewX1.SelectedCells[3].Value.ToString().Trim();
            string strInDate = dataGridViewX1.SelectedCells[4].Value.ToString().Trim();
            string strOutDate = dataGridViewX1.SelectedCells[5].Value.ToString().Trim();
            string strSound = dataGridViewX1.SelectedCells[6].Value.ToString().Trim();

            sgFrmUpdate sgFrmUpdate = new sgFrmUpdate(strID, strName, strPinYin, strSex, strInDate, strOutDate, strSound);
            sgFrmUpdate.uMD += new upManData(sgFrmUpdate_uMD);
            sgFrmUpdate.Show();
        }
    }
}
