using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SalesManagerService”。
    public class SalesManagerService : ISalesManagerService
    {
       //改为List可以吗，WCF服务就要数组类型的
        public Model.SalesOrdersModel[] GetSalesOrders(string orderNumber, string orderDateStart, string orderDateEnd, string status)
        {
            string sqlWhere = "";
            if (!string.IsNullOrWhiteSpace(orderNumber))
            {
                sqlWhere += string.Format(" and orderNumber like '%{0}%'",orderNumber);
            }
            if (!string.IsNullOrWhiteSpace(orderDateStart))
            {
                try
                {
                    DateTime dt = DateTime.Parse(orderDateStart);
                    sqlWhere += string.Format(" and orderDate>=to_date('{0}','yyyy-MM-dd')", dt.ToString("yyyy-MM-dd"));
                }
                catch
                {
                    Exception oe = new Exception();
                    throw new FaultException<Exception>(oe,"查询条件开始时间有误!");
                }
            }

            if (!string.IsNullOrWhiteSpace(orderDateEnd))
            {
                try
                {
                    DateTime dt = DateTime.Parse(orderDateEnd);
                    sqlWhere += string.Format(" and orderDate<=to_date('{0}','yyyy-MM-dd')", dt.ToString("yyyy-MM-dd"));
                }
                catch
                {
                    Exception oe = new Exception();
                    throw new FaultException<Exception>(oe, "查询条件截至时间有误!");
                }
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                sqlWhere += string.Format(" and Status='{0}'",status);
            }
            IDAL.ISalesOrdersService salesOrders = new OracleDAL.SalesOrderService();
            return  salesOrders.GetSalesOrders(sqlWhere).ToArray();
            
        }

        public Model.SalesCommodityModel[] GetSalesCommoditiesByID(int salesID)
        {
            string sqlWhere = string.Format(" and SalesOrderID={0}",salesID);
            IDAL.ISalesCommodityService salesOrdersService = new OracleDAL.SalesCommodityService();
            return salesOrdersService.GetSalesCommodities(sqlWhere).ToArray();
        }

        public Model.SalesCommodityModel AddSalesCommodity(Model.SalesCommodityModel oneSalesCommodity)
        {
            return new OracleDAL.SalesCommodityService().AddSalesCommodity(oneSalesCommodity);
        }

        public bool PostSalesOrder(int id)
        {
            Model.SalesOrdersModel oneOrder = GetOneSalesOrder(id);
            if (oneOrder.Status.Equals("已出库"))
            {
                Exception oe = new Exception();
                throw new FaultException<Exception>(oe,"订单已出库");
            }
            List<Model.SalesCommodityModel> salesCommoditiesList = GetSalesCommoditiesByID(id).ToList();
            List<Model.SalesCommodityModel> errorList = salesCommoditiesList.Where(u=>u.CommodityInventory<u.Count).ToList();
            //简单处理回滚，因为一条不成功，所有记录操作所有被回滚
            //这儿确保安全无误后对库存进行更新
            if (errorList.Count>0)
            {
                Exception oe = new Exception();
                string message = "提交失败!\r\n";
                foreach (Model.SalesCommodityModel oneErrorSalesCommodity in errorList)
                {
                    message += string.Format("{0}的当前库存({1})小于订单的数量({2})!\r\n",oneErrorSalesCommodity.CommodityName,oneErrorSalesCommodity.CommodityInventory,oneErrorSalesCommodity.Count);
                }
                throw new FaultException<Exception>(oe,message);
            }
            IDAL.ICommodityService commodityService = new OracleDAL.CommodityService();
            foreach (Model.SalesCommodityModel oneSalesCommodity in salesCommoditiesList)
            {
                Model.CommodityModel oneCommodity = new Model.CommodityModel();
                oneCommodity.ID = oneSalesCommodity.CommodityID;
                oneCommodity.Name = oneSalesCommodity.CommodityName;
                oneCommodity.Type = oneSalesCommodity.CommodityType;
                oneCommodity.Manufacturer = oneSalesCommodity.CommodityManufacturer;
                oneCommodity.Unit = oneSalesCommodity.CommodityUnit;
                oneCommodity.UnitPrice = oneSalesCommodity.CommodityUnitPrice;
                oneCommodity.Inventory = oneSalesCommodity.CommodityInventory - oneSalesCommodity.Count;

                commodityService.UpdateCommodity(oneCommodity);
            }
            oneOrder.Status="已出库";
            IDAL.ISalesOrdersService salesOrderService = new OracleDAL.SalesOrderService();
            return salesOrderService.UpdateSalesOrder(oneOrder);
            
        }

        public Model.SalesOrdersModel GetOneSalesOrder(int id)
        {
            IDAL.ISalesOrdersService salesOrdersService = new OracleDAL.SalesOrderService();
            return salesOrdersService.GetOneSalesOrder(id);
        }
    }
}
