using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EquipmentMS
{
    public partial class frmLogin : Form
    {
        EquipmentMS.BaseClass.Operation oper = new EquipmentMS.BaseClass.Operation();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (oper.LoginSystem(txtUserName.Text, txtPWD.Text).Tables[0].Rows.Count > 0)
            {
                new frmMain().Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("操作员用户名称或密码错误,请重新输入！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtUserName.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                txtPWD.Focus();
        }

        private void txtPWD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnOK.Focus();
        }
    }
}