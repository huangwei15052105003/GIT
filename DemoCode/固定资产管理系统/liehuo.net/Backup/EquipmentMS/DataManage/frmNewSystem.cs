using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EquipmentMS.DataManage
{
    public partial class frmNewSystem : Form
    {
        EquipmentMS.BaseClass.Operation oper = new EquipmentMS.BaseClass.Operation();
        public frmNewSystem()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!chkBase.Checked && !chkOperation.Checked)
                return;
            if (chkBase.Checked)
            {
                int i = oper.DeleteBaseTable();
                int j = oper.DeleteOperationTable();
            }
            else if (chkOperation.Checked)
            {
                int j = oper.DeleteOperationTable();
            }
            MessageBox.Show("系统数据初始化成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}