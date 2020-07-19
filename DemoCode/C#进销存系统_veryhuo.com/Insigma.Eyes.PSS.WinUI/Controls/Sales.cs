using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Insigma.Eyes.PSS.WinUI.BLLSalesOrders;
using System.ServiceModel;

////销售管理：销售管理员管理销售订单，每个订单中包含多个商品信息，订单之后在提交后才能出库
namespace Insigma.Eyes.PSS.WinUI.Controls
{
    public partial class Sales : UserControl
    {
        public Sales()
        {
            InitializeComponent();
            dataGridViewCommoditiesList.AutoGenerateColumns = false;
        }
        //void Bind()
        //{
        //    BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();
        //    List<Model.SalesCommodityModel> salesCommoditiesList = client.GetSalesCommoditiesByID(selectOrder.ID).ToList();
        //    dataGridViewCommoditiesList.DataSource = salesCommoditiesList;
        //}
        public void GetSalesOrdersList()
        { 
            //Tag里面绑定了对象的详细信息
            BLLSalesOrders.SalesManagerServiceClient salesServiceClient = WCFServiceBLL.GetSalesService();
            List<Model.SalesOrdersModel> listSalesOrders = salesServiceClient.GetSalesOrders(textBoxOrderNumber.Text, textBoxdateStart.Text, textBoxdateEnd.Text, comboBoxStatus.Text).ToList();
            listViewOrders.Items.Clear();
            foreach (Model.SalesOrdersModel salesOneOrder in listSalesOrders)
            {
                ListViewItem item = new ListViewItem();
                item.Text = salesOneOrder.OrderNumber;
                item.Tag = salesOneOrder;
                listViewOrders.Items.Add(item);
            }
        }
        Model.SalesOrdersModel selectOrder;
        private void GetOrderDetail()
        {
            //通过调试得知：第一次进入时GetOrderDetail执行一次，且第一个就是listViewOrders.SelectedItems.Count>0
            //之后在触发事件，调用这个方法时，要执行两遍GetOrderDetail，第一遍listViewOrders.SelectedItems.Count>)不成立，第二遍成立
            if (listViewOrders.SelectedItems.Count>0)
            {
                toolStripButton4.Enabled = true;
                toolStripButton5.Enabled = true;
                toolStripButton6.Enabled = true;

                ListViewItem item = listViewOrders.SelectedItems[0];
                selectOrder = (Model.SalesOrdersModel)item.Tag;
                labelOrderNumber.Text = "["+selectOrder.OrderNumber+"]";
                labelOrderDate.Text = "[" + selectOrder.OrderDate.ToString("yyyy-MM-dd") + "]";
                labelStatus.Text = "[" + selectOrder.Status + "]";
                if (selectOrder.Status.Equals("已出库"))
                {
                    toolStripButton4.Enabled = false;
                    toolStripButton5.Enabled = false;
                    toolStripButton6.Enabled = false;
                }
                labelTel.Text = "[" + selectOrder.Tel + "]";
                labelCustomerName.Text = "[" + selectOrder.CustomerName + "]";
                labelAddress.Text = "[" + selectOrder.Address + "]";

                BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();

                List<Model.SalesCommodityModel> salesCommoditiesList = client.GetSalesCommoditiesByID(selectOrder.ID).ToList();
                dataGridViewCommoditiesList.DataSource = salesCommoditiesList;

            }
        }
        /// <summary>
        /// 新增Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddSalesOrder oneOrder = new AddSalesOrder();
            if (oneOrder.ShowDialog()==DialogResult.OK)
            {
                salesID = oneOrder.AddSalesID;
                GetSalesOrdersList();
                GetUpdateOrderDetail();
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            GetSalesOrdersList();
        }

        private void textBoxOrderNumber_TextChanged(object sender, EventArgs e)
        {
            GetSalesOrdersList();
        }

        private void textBoxdateStart_TextChanged(object sender, EventArgs e)
        {
            GetSalesOrdersList();
        }

        private void comboBoxStatus_TextChanged(object sender, EventArgs e)
        {
            GetSalesOrdersList();
        }

        private void textBoxdateEnd_TextChanged(object sender, EventArgs e)
        {
            GetSalesOrdersList();
        }

        private void listViewOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrderDetail();
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //没有产品不能提交订单
            if (selectOrder==null)
            {
                MessageBox.Show("请先选择一个订单");
                return;
            }
            if (MessageBox.Show("确定要提交该订单吗？","提交",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                try
                {
                    BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();
                    if (client.PostSalesOrder(selectOrder.ID))
                    {
                        listViewOrders.SelectedItems[0].Tag = client.GetOneSalesOrder(selectOrder.ID);
                        GetOrderDetail();
                    }
                }
                catch (FaultException<Exception> ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //已出库就不能新增产品了吧
            if (selectOrder==null)
            {
                MessageBox.Show("请选择一个订单！");
                return;
            }
            AddSalesCommodities salesCommodity = new AddSalesCommodities();
            salesCommodity.SalesOrderID = selectOrder.ID;
            if (salesCommodity.ShowDialog()==DialogResult.OK)
            {
                BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();
                List<Model.SalesCommodityModel> salesCommoditiesList = client.GetSalesCommoditiesByID(selectOrder.ID).ToList();
                dataGridViewCommoditiesList.DataSource = salesCommoditiesList;
            }
        }

        private void GetUpdateOrderDetail()
        {
            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton6.Enabled = true;

            //ListViewItem item = listViewOrders.SelectedItems[0];
            //selectOrder = (Model.SalesOrdersModel)item.Tag;
            BLLSalesOrders.SalesManagerServiceClient salesClient = WCFServiceBLL.GetSalesService();
            selectOrder= salesClient.GetOneSalesOrder(salesID);
            labelOrderNumber.Text = "[" + selectOrder.OrderNumber + "]";
            labelOrderDate.Text = "[" + selectOrder.OrderDate.ToString("yyyy-MM-dd") + "]";
            labelStatus.Text = "[" + selectOrder.Status + "]";
            if (selectOrder.Status.Equals("已出库"))
            {
                toolStripButton4.Enabled = false;
                toolStripButton5.Enabled = false;
                toolStripButton6.Enabled = false;
            }
            labelTel.Text = "[" + selectOrder.Tel + "]";
            labelCustomerName.Text = "[" + selectOrder.CustomerName + "]";
            labelAddress.Text = "[" + selectOrder.Address + "]";

            BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();

            List<Model.SalesCommodityModel> salesCommoditiesList = client.GetSalesCommoditiesByID(selectOrder.ID).ToList();
            dataGridViewCommoditiesList.DataSource = salesCommoditiesList;

        
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //订单已经提交的情况下，不能修改产品
            if (dataGridViewCommoditiesList.SelectedRows.Count>0)
            {
                int cID = int.Parse(dataGridViewCommoditiesList.SelectedRows[0].Cells["ColumnID"].Value.ToString());
                AddSalesCommodities updateSalesCommodityForm = new AddSalesCommodities(cID);
                updateSalesCommodityForm.SalesOrderID = selectOrder.ID;
                if (updateSalesCommodityForm.ShowDialog()==DialogResult.OK)
                {
                    BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();
                    dataGridViewCommoditiesList.DataSource=client.GetSalesCommoditiesByID(selectOrder.ID);
                }
            }
            else
            {
                MessageBox.Show("请选择要修改的商品");
                return;
            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //订单已经提交的情况下，不能删除
            if (dataGridViewCommoditiesList.SelectedRows.Count>0)
            {
                int cID = int.Parse(dataGridViewCommoditiesList.SelectedRows[0].Cells["ColumnID"].Value.ToString());
                if (MessageBox.Show("确定要删除吗?","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    BLLSalesOrders.SalesManagerServiceClient salesClient = WCFServiceBLL.GetSalesService();
                    if ( salesClient.DeleteSalesCommodity(cID))
                    {
                        BLLSalesOrders.SalesManagerServiceClient client = WCFServiceBLL.GetSalesService();
                        dataGridViewCommoditiesList.DataSource = client.GetSalesCommoditiesByID(selectOrder.ID);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一条记录");
            }

        }
        private int salesID = -1;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listViewOrders.SelectedItems.Count>0)
            {
                Model.SalesOrdersModel oneSales=(Model.SalesOrdersModel)listViewOrders.SelectedItems[0].Tag;
                salesID = oneSales.ID;
                AddSalesOrder updateSalesOrder = new AddSalesOrder(oneSales.ID);
                if (updateSalesOrder.ShowDialog()==DialogResult.OK)
                {
                    GetSalesOrdersList();
                    //修改中ListView失去焦点
                    GetUpdateOrderDetail();
                }
            }
        }
        /// <summary>
        /// 删除订单，先删除外键表，在删主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //订单删除后 记得把Enable=False的还原
            BLLSalesOrders.SalesManagerServiceClient salesClient = WCFServiceBLL.GetSalesService();
            //删除多条数据注意回滚
            if (listViewOrders.SelectedItems.Count>0)
            {
                if (MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    salesID = (listViewOrders.SelectedItems[0].Tag as Model.SalesOrdersModel).ID;
                    if (salesClient.GetSalesCommoditiesByID(salesID).Length==0||salesClient.DeleteSalesCommoditiesBySalesOrderID(salesID))
                    {
                        if (salesClient.DeleteOrderID(salesID))
                        {
                            MessageBox.Show("删除成功");
                            //dataGridViewCommoditiesList.Rows.Clear();
                            dataGridViewCommoditiesList.DataSource = null;
                            labelOrderNumber.Text = "[]";
                            labelOrderDate.Text = "[]";
                            labelStatus.Text = "[]";
                            labelTel.Text = "[]";
                            labelCustomerName.Text = "[]";
                            labelAddress.Text = "[]";
                            GetSalesOrdersList();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一条订单");
            }
        }
    }
}
 