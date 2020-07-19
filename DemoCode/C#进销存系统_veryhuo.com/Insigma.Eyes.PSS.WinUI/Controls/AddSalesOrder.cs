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
    public partial class AddSalesOrder : Form
    {
        public AddSalesOrder()
        {
            InitializeComponent();
            comboBoxState.SelectedIndex = 0;
        }
        public int AddSalesID { get; set; }
        private bool isUpdate = false;
        public int orderID { get; set; }
        public AddSalesOrder(int id)
        {
            InitializeComponent();
            this.Text = "修改订单";
            this.orderID = id;
            isUpdate = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOrderNumber.Text))
            {
                MessageBox.Show("客户编号不能为空");
                return;
            }
            Model.SalesOrdersModel salesOrder = new Model.SalesOrdersModel();
            salesOrder.Address = textBoxAddress.Text;
            salesOrder.Contract = textBoxContract.Text;
            salesOrder.CustomerName = textBoxCustomerName.Text;
            salesOrder.Tel = textBoxTel.Text;
            salesOrder.Status = comboBoxState.Text;
            salesOrder.OrderNumber = textBoxOrderNumber.Text;
            salesOrder.OrderDate = DateTime.Now;
            BLLSalesOrders.SalesManagerServiceClient salesClient = WCFServiceBLL.GetSalesService();
            if (isUpdate)
            {
                salesOrder.ID = orderID;
                if (!salesClient.UpdateSalesOrder(salesOrder))
                {
                    MessageBox.Show("订单更新失败");
                    this.DialogResult = DialogResult.None;
                }
            }
            else
            {
                var order = salesClient.AddSalesOrder(salesOrder);
                if (order == null)
                {
                    MessageBox.Show("新增订单失败");
                    this.DialogResult = DialogResult.None;
                }
                AddSalesID = order.ID;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void AddSalesOrder_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                //Model.SalesOrdersModel salesOrder =new  BLLSalesOrders.SalesManagerServiceClient()
                BLLSalesOrders.SalesManagerServiceClient salesClient = WCFServiceBLL.GetSalesService();
                var order=salesClient.GetOneSalesOrder(orderID);
                textBoxAddress.Text = order.Address;
                textBoxContract.Text = order.Contract;
                textBoxCustomerName.Text = order.CustomerName;
                textBoxTel.Text = order.Tel;
                textBoxOrderNumber.Text = order.OrderNumber;
                comboBoxState.Text = order.Status;
            }
        }
    }
}
