using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EquipmentMS.Equipment
{
    public partial class frmEquipmentInsert : Form
    {
        EquipmentMS.BaseClass.Operation oper = new EquipmentMS.BaseClass.Operation();
        public frmEquipmentInsert()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNX.Text == "0" || txtNX.Text == "")
            {
                errText.SetError(txtNX, "使用年限必须大于０");
                return;
            }
            if (cmbZjff.Text == "")
            {
                errText.SetError(cmbZjff, "请选择资产折旧方法！");
                return;
            }



            EquipmentMS.BaseClass.cZcMain zcMain = new EquipmentMS.BaseClass.cZcMain();
            zcMain.BH= txtBH.Text;
            zcMain.MC= cmbMC.Text;
            zcMain.XH= txtXH.Text;
            zcMain.Zclb= cmbZclb.Text;
            zcMain.Xxpz= txtXxpz.Text;
            zcMain.Gjbh= txtGjbh.Text;
            zcMain.Sccj= txtSccj.Text;
            zcMain.Ccrq= dtpCcrq.Value;
            zcMain.Zjfs= cmbZjfs.Text;
            zcMain.Sybm= cmbSybm.Text;
            zcMain.Syqk= cmbSyqk.Text;
            zcMain.Cfdd= cmbCfdd.Text;
            zcMain.Bgry= cmbBgry.Text;
            zcMain.Rzrq= dtpRzrq.Value;
            zcMain.SL= Convert.ToInt32(txtSL.Text);
            zcMain.DW= cmbDW.Text;
            zcMain.DJ = Convert.ToSingle(txtDJ.Text);
            zcMain.YZ = Convert.ToSingle(txtYZ.Text);
            zcMain.Ljzj = Convert.ToSingle(txtLjzj.Text);
            zcMain.JZ = Convert.ToSingle(txtJZ.Text);
            zcMain.Jczl = Convert.ToSingle(txtJczl.Text);
            zcMain.Zjff= cmbZjff.Text;
            zcMain.Nx= Convert.ToInt32(txtNX.Text);
            zcMain.Login = dtpLogin.Value;
            zcMain.Loginer= txtLoginer.Text;
           int i=oper.InsertZcMain(zcMain);
           this.btnSave.Enabled = false;
           MessageBox.Show("固定资产添加成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindComboBox()
        {
            cmbBgry.DataSource = oper.GetDataSetBaseBgry().Tables[0].DefaultView;
            cmbBgry.ValueMember = "bgry";

            cmbCfdd.DataSource = oper.GetDataSetBaseCfdd().Tables[0].DefaultView;
            cmbCfdd.ValueMember = "cfdd";

            cmbMC.DataSource = oper.GetDataSetBaseZcmc().Tables[0].DefaultView;
            cmbMC.ValueMember = "zcmc";

            cmbSybm.DataSource = oper.GetDataSetBaseSybm().Tables[0].DefaultView;
            cmbSybm.ValueMember = "sybm";

            cmbSyqk.DataSource = oper.GetDataSetBaseSyqk().Tables[0].DefaultView;
            cmbSyqk.ValueMember = "syqk";

            cmbZclb.DataSource = oper.GetDataSetBaseZclb().Tables[0].DefaultView;
            cmbZclb.ValueMember = "zclb";

            cmbZjfs.DataSource = oper.GetDataSetBaseZjfs().Tables[0].DefaultView;
            cmbZjfs.ValueMember = "zjfs";

            cmbDW.DataSource = oper.GetDataSetBaseJldw().Tables[0].DefaultView;
            cmbDW.ValueMember = "jldw";
        }

        private void frmEquipmentInsert_Load(object sender, EventArgs e)
        {
            this.BindComboBox();
            DataSet ds = oper.GetDataSetBaseDefaultNO();
            DataSet dsZcmain = oper.GetDataSetZC();
            string firstNO = ds.Tables[0].Rows[0]["firstNO"].ToString();
            int defaultNO = Convert.ToInt32(ds.Tables[0].Rows[0]["defaultNO"].ToString());

            if (dsZcmain.Tables[0].Rows.Count > 0)
            {
                int j = defaultNO + Convert.ToInt32(dsZcmain.Tables[0].Rows[dsZcmain.Tables[0].Rows.Count - 1]["id"]);
                txtBH.Text = firstNO + j ;
            }
            else
            {
                txtBH.Text = firstNO + defaultNO.ToString();
            }
        }

        private void txtDJ_TextChanged(object sender, EventArgs e)
        {
            if (oper.IsNumeric(txtDJ.Text))
            {
                txtYZ.Text = Convert.ToString(Convert.ToInt32(txtSL.Text) * Convert.ToSingle(txtDJ.Text) - Convert.ToSingle(txtLjzj.Text));
                txtJZ.Text = Convert.ToString(Convert.ToInt32(txtSL.Text) * Convert.ToSingle(txtDJ.Text) - Convert.ToSingle(txtLjzj.Text));
            }
            else
            {
                MessageBox.Show("请输入数字！！！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtDJ.Undo();
                txtDJ.ClearUndo();
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            if (oper.IsNumeric(txtSL.Text))
            {
                txtYZ.Text = Convert.ToString(Convert.ToInt32(txtSL.Text) * Convert.ToSingle(txtDJ.Text) - Convert.ToSingle(txtLjzj.Text));
                txtJZ.Text = Convert.ToString(Convert.ToInt32(txtSL.Text) * Convert.ToSingle(txtDJ.Text) - Convert.ToSingle(txtLjzj.Text));
            }
            else
            {
                MessageBox.Show("请输入数字！！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSL.Undo();
                txtSL.ClearUndo();
            }
        }

        private void txtLjzj_TextChanged(object sender, EventArgs e)
        {
            if (oper.IsNumeric(txtLjzj.Text))
            {
                txtJZ.Text = Convert.ToString(Convert.ToInt32(txtSL.Text) * Convert.ToSingle(txtDJ.Text) - Convert.ToSingle(txtLjzj.Text));
            }
            else
            {
                MessageBox.Show("请输入数字！！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLjzj.Undo();
                txtLjzj.ClearUndo();
            }
        }

        private void txtNX_TextChanged(object sender, EventArgs e)
        {
            if (!oper.IsNumeric(txtNX.Text))
            {
                MessageBox.Show("请输入数字！！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNX.Undo();
                txtNX.ClearUndo();
            }
        }

        private void txtJczl_TextChanged(object sender, EventArgs e)
        {
            if (!oper.IsNumeric(txtJczl.Text))
            {
                MessageBox.Show("请输入数字！！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJczl.Undo();
                txtJczl.ClearUndo();
            }
        }

    }
}