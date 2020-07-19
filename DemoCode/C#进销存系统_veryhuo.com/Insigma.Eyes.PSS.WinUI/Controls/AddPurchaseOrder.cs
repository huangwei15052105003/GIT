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
    public partial class AddPurchaseOrder : Form
    {
        public int AddSalesID { get; set; }
        public AddPurchaseOrder()
        {
            InitializeComponent();
            comboBoxState.SelectedIndex = 0;
        }
        public AddPurchaseOrder(int id)
        {
            InitializeComponent();
            this.Text = "修改订单";
            this.orderID = id;
            isUpdate = true;
        }
        private bool isUpdate = false;
        public int orderID { get; set; }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOrderNumber.Text))
            {
                MessageBox.Show("客户编号不能为空");
                return;
            }
            Model.PurchaseOrdersModel purchaseOrder = new Model.PurchaseOrdersModel();
            purchaseOrder.Address = textBoxAddress.Text;
            purchaseOrder.Contract = textBoxContract.Text;
            purchaseOrder.SupplierName= textBoxCustomerName.Text;
            purchaseOrder.Tel = textBoxTel.Text;
            purchaseOrder.Status = comboBoxState.Text;
            purchaseOrder.OrderNumber = textBoxOrderNumber.Text;
            purchaseOrder.OrderDate = DateTime.Now;
            BLLPurchaseOrders.PurchaseManagerServiceClient purchaseClient = WCFServiceBLL.GetPurchaseService();
            if (isUpdate)
            {
                purchaseOrder.ID = orderID;
                if (!purchaseClient.UpdatePurchaseOrder(purchaseOrder))
                {
                    MessageBox.Show("订单更新失败");
                    this.DialogResult = DialogResult.None;
                }
            }
            else
            {
                var order = purchaseClient.AddPurchaseOrder(purchaseOrder);
                AddSalesID=order.ID;
                if (order == null)
                {
                    MessageBox.Show("新增订单失败");
                    this.DialogResult = DialogResult.None;
                    return;
                }
                

            }
        }

        private void AddPurchaseOrder_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                BLLPurchaseOrders.PurchaseManagerServiceClient purchaseClient = WCFServiceBLL.GetPurchaseService();
                var order = purchaseClient.GetOnePurchaseOrder(orderID);
                textBoxAddress.Text = order.Address;
                textBoxContract.Text = order.Contract;
                textBoxCustomerName.Text = order.SupplierName;
                textBoxTel.Text = order.Tel;
                textBoxOrderNumber.Text = order.OrderNumber;
                comboBoxState.Text = order.Status;
            }
        }
    }
}
