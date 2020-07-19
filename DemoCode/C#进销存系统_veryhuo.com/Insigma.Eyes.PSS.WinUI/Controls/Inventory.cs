using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Insigma.Eyes.PSS.WinUI.BLLCommodity;
using Insigma.Eyes.PSS.Model;

namespace Insigma.Eyes.PSS.WinUI.Controls
{
    public partial class Inventory : UserControl
    {
        public Inventory()
        {
            InitializeComponent();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        BLLCommodity.CommodityManagerServiceClient client = WCFServiceBLL.GetCommodityService();
        private void DataRefresh()
        {
            List<CommodityModel> listCommodities = client.GetCommodities("","","","","").ToList();
            dataGridViewCommodity.DataSource = listCommodities;
        }
        private void Search()
        {
            //BLLCommodity.CommodityManagerServiceClient client = WCFServiceBLL.GetCommodityService();
            //当查询之后，再按新增商品不加载是因为下面参数中有条件，所以datagridview看似无法“刷新”，解决：为了不清空界面控件上的值，所以还是加上一个Add方法吧
            List<CommodityModel> listCommodities=client.GetCommodities(textBoxName.Text, textBoxType.Text, textBoxManufacturer.Text, textBoxPriceLow.Text, textBoxPriceHigh.Text).ToList();
            dataGridViewCommodity.DataSource = listCommodities;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddCommodity addCommodity = new AddCommodity();
            if (addCommodity.ShowDialog()==DialogResult.OK)
            {
                //重新刷新界面
                DataRefresh();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridViewCommodity.SelectedRows.Count>0)
            {
                int sID = int.Parse(dataGridViewCommodity.SelectedRows[0].Cells["idColumn"].Value.ToString());
                AddCommodity updateCommodity = new AddCommodity(sID);
                if (updateCommodity.ShowDialog()==DialogResult.OK)
                {
                    MessageBox.Show("更新成功");
                    DataRefresh();
                }
            }
            else
            {
                MessageBox.Show("请选择一条记录");
                return;
            }
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            //页面进入绑定列表
            Search();
        }
    }
}
