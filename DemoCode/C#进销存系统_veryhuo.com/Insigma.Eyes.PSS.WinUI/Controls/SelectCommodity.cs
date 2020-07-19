using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Insigma.Eyes.PSS.WinUI.Controls
{
    public partial class SelectCommodity : Form
    {
        public SelectCommodity()
        {
            InitializeComponent();
        }

        private void textBoxConditons_TextChanged(object sender, EventArgs e)
        {
            GetCommodities();
        }
        public int SelectedCommodityID;
        private void SelectOneCommodity()
        {
            if (dataGridViewCommodity.SelectedRows.Count > 0)
            {
                SelectedCommodityID = int.Parse(dataGridViewCommodity.SelectedRows[0].Cells["idColumn"].Value.ToString());
            }
            else
            {
                MessageBox.Show("请选择一条记录");
                this.DialogResult = DialogResult.None;
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            
        }
        private void GetCommodities()
        {
            dataGridViewCommodity.DataSource = new BLLCommodity.CommodityManagerServiceClient().GetCommoditiesByCondition(textBoxConditons.Text).ToList();
        }

        private void SelectCommodity_Load(object sender, EventArgs e)
        {
            GetCommodities();
        }

        private void dataGridViewCommodity_DoubleClick(object sender, EventArgs e)
        {
            SelectOneCommodity();
            this.DialogResult = DialogResult.OK;
        }
    }
}
