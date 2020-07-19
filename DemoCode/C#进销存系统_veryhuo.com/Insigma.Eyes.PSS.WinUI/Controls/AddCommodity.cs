using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using Insigma.Eyes.PSS.WinUI.BLLCommodity;

namespace Insigma.Eyes.PSS.WinUI.Controls
{
    public partial class AddCommodity : Form
    {
        public AddCommodity()
        {
            InitializeComponent();
        }
        private int SID { get;set;}
        private bool isUpdate = false;
        public AddCommodity(int sID)
        {
            InitializeComponent();
            this.SID = sID;
            this.isUpdate = true;
        }
        //先重新生成BLLService，再运行一下commodityBLLService，在浏览器查看然，确保已经创建服务后，再Ui更新服务（这样才能发现服务器端新增的方法）。
        private void buttonSave_Click(object sender, EventArgs e)
        {
            int inventory = 0;
            decimal unitPrice = 0.0M;
            try
            {
                inventory = int.Parse(textBoxInventory.Text);
                unitPrice = decimal.Parse(textBoxUnitPrice.Text);
            }
            catch 
            {
                MessageBox.Show("单价或者库存有误！");
            }
            Model.CommodityModel oneCommodity = new Model.CommodityModel();
            oneCommodity.Name = textBoxName.Text;
            oneCommodity.Type = textBoxType.Text;
            oneCommodity.Manufacturer = textBoxManufacturer.Text;
            oneCommodity.Unit = textBoxUnit.Text;
            oneCommodity.Inventory = inventory;
            oneCommodity.UnitPrice = unitPrice;
            //整个应用程序使用一个对象
            BLLCommodity.CommodityManagerServiceClient client = WCFServiceBLL.GetCommodityService();
            try
            {
                if (isUpdate)
                {
                    //获取要更新数据的ID
                    oneCommodity.ID = SID;
                    if (!client.UpdateCommodity(oneCommodity))
                    {
                        MessageBox.Show("更新失败");
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                }
                else
                {
                    if (client.AddCommodity(oneCommodity)==null)
                    {
                        MessageBox.Show("保存失败");
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
            catch (FaultException<Exception> ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.None;
                //后面还会执行吗
            }
            

        }

        private void AddCommodity_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                ICommodityManagerService client = WCFServiceBLL.GetCommodityService();
                Model.CommodityModel commodityModel = client.GetOneCommodity(SID);
                textBoxInventory.Text = commodityModel.Inventory.ToString();
                textBoxManufacturer.Text = commodityModel.Manufacturer;
                textBoxName.Text = commodityModel.Name;
                textBoxType.Text = commodityModel.Type;
                textBoxUnit.Text = commodityModel.Unit;
                textBoxUnitPrice.Text = commodityModel.UnitPrice.ToString();
                buttonSave.Text = "更新";
                this.Text = "修改产品";
            }
        }
    }
}
