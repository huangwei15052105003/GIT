using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EquipmentMS.BaseInfo
{
    public partial class frmBaseInfo : Form
    {
        BaseClass.Operation oper = new EquipmentMS.BaseClass.Operation();
        public frmBaseInfo()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnZCMC_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseZcmc().Tables[0].DefaultView;
            lbName.DisplayMember = "zcmc";
            lbName.ValueMember = "id";
            labTitle.Text = btnZCMC.Text;
        }

        private void btnZJFS_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseZjfs().Tables[0].DefaultView;
            lbName.DisplayMember = "zjfs";
            lbName.ValueMember = "id";
            labTitle.Text = btnZJFS.Text;
        }

        private void btnSYBM_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseSybm().Tables[0].DefaultView;
            lbName.DisplayMember = "sybm";
            lbName.ValueMember = "id";
            labTitle.Text = btnSYBM.Text;
        }

        private void btnSYQK_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseSyqk().Tables[0].DefaultView;
            lbName.DisplayMember = "syqk";
            lbName.ValueMember = "id";
            labTitle.Text = btnSYQK.Text;
        }

        private void btnCFDD_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseCfdd().Tables[0].DefaultView;
            lbName.DisplayMember = "cfdd";
            lbName.ValueMember = "id";
            labTitle.Text = btnCFDD.Text;
        }

        private void btnBGRY_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseBgry().Tables[0].DefaultView;
            lbName.DisplayMember = "bgry";
            lbName.ValueMember = "id";
            labTitle.Text = btnBGRY.Text;
        }

        private void frmBaseInfo_Load(object sender, EventArgs e)
        {
            this.btnZCMC_Click(sender, e);
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtName.Focus();
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("添加新项不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }
            else
            {
                switch (labTitle.Text)
                { 
                    case "资产名称":
                        int u = oper.InsertBaseZcmc(txtName.Text);
                        this.btnZCMC_Click(sender, e);
                        break;
                    case "增加方式":
                        int i = oper.InsertBaseZjfs(txtName.Text);
                        this.btnZJFS_Click(sender, e);
                        break;
                    case "使用部门":
                        int j = oper.InsertBaseSybm(txtName.Text);
                        this.btnSYBM_Click(sender, e);
                        break;
                    case"使用情况":
                        int k = oper.InsertBaseSyqk(txtName.Text);
                        this.btnSYQK_Click(sender, e);
                        break;
                    case "存放地点":
                        int p = oper.InsertBaseCfdd(txtName.Text);
                        this.btnCFDD_Click(sender, e);
                        break;
                    case "保管人员":
                        int o = oper.InsertBaseBgry(txtName.Text);
                        this.btnBGRY_Click(sender, e);
                        break;
                    case "计量单位":
                        int m = oper.InsertBaseJldw(txtName.Text);
                        this.btnJLDW_Click(sender, e);
                        break;
                    case "清理方式":
                        int l = oper.InsertBaseQlfs(txtName.Text);
                        this.btnQLFS_Click(sender, e);
                        break;
                }
                btnAdd.Enabled = true;
                btnSave.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            switch (labTitle.Text)
                { 
                    case "资产名称":
                        int u = oper.DeleteBaseZcmc(lbName.SelectedValue.ToString());
                        this.btnZCMC_Click(sender, e);
                        break;
                    case "增加方式":
                        int i = oper.DeleteBaseZjfs(lbName.SelectedValue.ToString());
                        this.btnZJFS_Click(sender, e);
                        break;
                    case "使用部门":
                        int j = oper.DeleteBaseSybm(lbName.SelectedValue.ToString());
                        this.btnSYBM_Click(sender, e);
                        break;
                    case"使用情况":
                        int k = oper.DeleteBaseSyqk(lbName.SelectedValue.ToString());
                        this.btnSYQK_Click(sender, e);
                        break;
                    case "存放地点":
                        int p = oper.DeleteBaseCfdd(lbName.SelectedValue.ToString());
                        this.btnCFDD_Click(sender, e);
                        break;
                    case "保管人员":
                        int o = oper.DeleteBaseBgry(lbName.SelectedValue.ToString());
                        this.btnBGRY_Click(sender, e);
                        break;
                    case "计量单位":
                        int l = oper.DeleteBaseJldw(lbName.SelectedValue.ToString());
                        this.btnJLDW_Click(sender, e);
                        break;
                    case "清理方式":
                        int m = oper.DeleteBaseQlfs(lbName.SelectedValue.ToString());
                        this.btnQLFS_Click(sender, e);
                        break;
                }
        }

        private void btnJLDW_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseJldw().Tables[0].DefaultView;
            lbName.DisplayMember = "jldw";
            lbName.ValueMember = "id";
            labTitle.Text = btnJLDW.Text;
        }

        private void btnQLFS_Click(object sender, EventArgs e)
        {
            lbName.DataSource = oper.GetDataSetBaseQlfs().Tables[0].DefaultView;
            lbName.DisplayMember = "qlfs";
            lbName.ValueMember = "id";
            labTitle.Text = btnQLFS.Text;
        }
    }
}