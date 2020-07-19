using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace Insigma.Eyes.PSS.WinUI.Controls
{
    public partial class AddPurchaseCommodities : Form
    {
        public AddPurchaseCommodities()
        {
            InitializeComponent();
        }
        public int CID { get; set; }
        public bool IsUpdate = false;
        public AddPurchaseCommodities(int id)
        {
            InitializeComponent();
            this.CID = id;//31
            IsUpdate = true;
        }
        public int PurchaseOrderID { get; set; }
        public int PurchaseCommodityID { get; set; }
        private void buttonEditName_Click(object sender, EventArgs e)
        {
            SelectCommodity selectCommodityForm=new SelectCommodity ();
            if (selectCommodityForm.ShowDialog()==DialogResult.OK)
            {
                this.PurchaseCommodityID = selectCommodityForm.SelectedCommodityID;
                BLLCommodity.CommodityManagerServiceClient commodityClient = WCFServiceBLL.GetCommodityService();
                Model.CommodityModel oneCommodity=commodityClient.GetOneCommodity(PurchaseCommodityID);
                textBoxName.Text = oneCommodity.Name;
                textBoxPrice.Text = oneCommodity.UnitPrice.ToString();
                labelManufacturer.Text = oneCommodity.Manufacturer;
                labelInventory.Text = oneCommodity.Inventory.ToString();
                labelType.Text = oneCommodity.Type.ToString();
                labelUnit.Text = oneCommodity.Unit.ToString();
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
          
            if (IsUpdate)
            {
                int count = 0;
                decimal price = 0.0M;
                try
                {
                    count = int.Parse(textBoxCount.Text);
                    price = decimal.Parse(textBoxPrice.Text);
                }
                catch
                {
                    Exception oe = new Exception();
                    throw new FaultException<Exception>(oe, "数量或金额有误");
                }
                Model.PurchaseCommodityModel purchaseCommodity = new Model.PurchaseCommodityModel();
                purchaseCommodity.Count = count;
                purchaseCommodity.PurchasePrice = price;
                purchaseCommodity.TotalPrice = count * price;
                purchaseCommodity.PurchaseOrderID = PurchaseOrderID;
                purchaseCommodity.CommodityID = PurchaseCommodityID;
                purchaseCommodity.ID = CID;
                BLLPurchaseOrders.PurchaseManagerServiceClient purchaseClient = WCFServiceBLL.GetPurchaseService();
                if (purchaseClient.UpdatePurchaseCommodity(purchaseCommodity))
                {
                    MessageBox.Show("更新成功!");
                }

            }
            else
            {
                if (PurchaseCommodityID == 0)
                {
                    MessageBox.Show("请选择一件产品");
                    return;
                }
                int count = 0;
                decimal price = 0.0M;
                try
                {
                    count = int.Parse(textBoxCount.Text);
                    price = decimal.Parse(textBoxPrice.Text);
                }
                catch
                {
                    Exception oe = new Exception();
                    throw new FaultException<Exception>(oe, "数量或金额有误");
                }
                Model.PurchaseCommodityModel purchaseCommodity = new Model.PurchaseCommodityModel();
                purchaseCommodity.Count = count;
                purchaseCommodity.PurchasePrice = price;
                purchaseCommodity.TotalPrice = count * price;
                purchaseCommodity.PurchaseOrderID = PurchaseOrderID;
                purchaseCommodity.CommodityID = PurchaseCommodityID;
                purchaseCommodity = new BLLPurchaseOrders.PurchaseManagerServiceClient().AddPurchaseCommodityModel(purchaseCommodity);
                if (purchaseCommodity.Equals(null))
                {
                    MessageBox.Show("保存失败");
                    this.DialogResult = DialogResult.None;
                }
            }
        }
        private int UpdateID;
        private void AddPurchaseCommodities_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                BLLPurchaseOrders.PurchaseManagerServiceClient purchaseClient = WCFServiceBLL.GetPurchaseService();
                Model.PurchaseCommodityModel purchaseCommodity = purchaseClient.GetOnePurchaseCommoditiesByID(CID);
                UpdateID = purchaseCommodity.ID;
                PurchaseCommodityID = purchaseCommodity.CommodityID;
                textBoxName.Text = purchaseCommodity.CommodityName;
                labelManufacturer.Text = purchaseCommodity.CommodityManufacturer;
                labelType.Text = purchaseCommodity.CommodityType;
                labelInventory.Text = purchaseCommodity.CommodityInventory.ToString();
                labelUnit.Text = purchaseCommodity.CommodityUnit;
                textBoxCount.Text = purchaseCommodity.Count.ToString();//几个价钱别搞错
                textBoxPrice.Text = purchaseCommodity.PurchasePrice.ToString();
            }
        }
    }
}
