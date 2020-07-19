using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“PurchaseManagerService”。
    public class PurchaseManagerService : IPurchaseManagerService
    {

        private AbstractFactory.DalFactory dataFactory = null;
        public PurchaseManagerService()
        {
            dataFactory = DefaultProviderDal.DefaultDataProviderFactory;
        }



       


        public Model.PurchaseOrdersModel[] GetPurchaseOrders(string orderNumber, string orderDateStart, string orderDateEnd, string status)
        {
            string sqlWhere = "";
            if (!string.IsNullOrWhiteSpace(orderNumber))
            {
                sqlWhere += string.Format(" and orderNumber like '%{0}%'", orderNumber);
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
                    throw new FaultException<Exception>(oe, "查询条件开始时间有误!");
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
                sqlWhere += string.Format(" and Status='{0}'", status);
            }
            //IDAL.IPurchaseOrdersService purchaseOrdersService = new OracleDAL.PurchaseOrderService();
            //return purchaseOrdersService.GetEntities(sqlWhere).ToArray();
            return dataFactory.PurchaseOrderDal.GetEntities(sqlWhere).ToArray();
        }

        public Model.PurchaseCommodityModel[] GetPurchaseCommoditiesByID(int purchaseID)
        {
            string sqlWhere = string.Format(" and PurchaseOrderID={0}",purchaseID);//看看数据库里面的字段
            //IDAL.IPurchaseCommodityService purchaseCommodityService =new OracleDAL.PurchaseCommodityService();
            //return purchaseCommodityService.GetEntities(sqlWhere).ToArray();

            return dataFactory.PurchaseCommdityDal.GetEntities(sqlWhere).ToArray();
        }

        public Model.PurchaseCommodityModel AddPurchaseCommodityModel(Model.PurchaseCommodityModel onePurchaseCommodity)
        {
            //return new OracleDAL.PurchaseCommodityService().AddEntity(onePurchaseCommodity);
            return dataFactory.PurchaseCommdityDal.AddEntity(onePurchaseCommodity);
        }

       //几个ID要分清楚
        public bool PostPurchaseOrder(int id)
        {
            Model.PurchaseOrdersModel oneOrder=GetOnePurchaseOrder(id);
            if (oneOrder.Status.Equals("已入库"))
            {
                Exception oe = new Exception();
                throw new FaultException<Exception>(oe,"订单已经提交，请务重复提交");
            }
            List<Model.PurchaseCommodityModel> purchaseCommoditiesList = GetPurchaseCommoditiesByID(id).ToList();
            IDAL.ICommodityService commodityService = new OracleDAL.CommodityService();
            foreach (Model.PurchaseCommodityModel onePurchaseCommodity in purchaseCommoditiesList)
            {
                Model.CommodityModel commodityModel = new Model.CommodityModel();
                commodityModel.ID = onePurchaseCommodity.CommodityID;
                commodityModel.Manufacturer = onePurchaseCommodity.CommodityManufacturer;
                commodityModel.Name = onePurchaseCommodity.CommodityName;
                commodityModel.Type = onePurchaseCommodity.CommodityType;
                commodityModel.Unit = onePurchaseCommodity.CommodityUnit;
                commodityModel.UnitPrice = onePurchaseCommodity.CommodityUnitPrice;
                commodityModel.Inventory = onePurchaseCommodity.CommodityInventory + onePurchaseCommodity.Count;
                //这儿不会出现异常了吧，否则要回滚
                commodityService.UpdateEntity(commodityModel);
            }
            oneOrder.Status = "已入库";
            return new OracleDAL.PurchaseOrderService().UpdateEntity(oneOrder);

        }

        public Model.PurchaseOrdersModel GetOnePurchaseOrder(int id)
        {
            //return new OracleDAL.PurchaseOrderService().GetOneEntityByID(id);
            return dataFactory.PurchaseOrderDal.GetOneEntityByID(id);
        }


        public Model.PurchaseCommodityModel GetOnePurchaseCommoditiesByID(int purchaseCommodityID)
        {
            //return new OracleDAL.PurchaseCommodityService().GetOneEntityByID(purchaseCommodityID);
            return dataFactory.PurchaseCommdityDal.GetOneEntityByID(purchaseCommodityID);
        }


        public bool UpdatePurchaseCommodity(Model.PurchaseCommodityModel model)
        {
            //return new OracleDAL.PurchaseCommodityService().UpdateEntity(model);
            return dataFactory.PurchaseCommdityDal.UpdateEntity(model);
        }

        public bool DeletePurchaseCommodity(int id)
        {
            string sqlWhere = " and id=" + id;
            //return new OracleDAL.PurchaseCommodityService().DeleteEntity(sqlWhere);
            return dataFactory.PurchaseCommdityDal.DeleteEntity(sqlWhere);
        }


        public Model.PurchaseOrdersModel AddPurchaseOrder(Model.PurchaseOrdersModel purchaseOrder)
        {
            //return new OracleDAL.PurchaseOrderService().AddEntity(purchaseOrder);
            return dataFactory.PurchaseOrderDal.AddEntity(purchaseOrder);
        }

        public bool UpdatePurchaseOrder(Model.PurchaseOrdersModel onePurchaseOrder)
        {
            //return new OracleDAL.PurchaseOrderService().UpdateEntity(onePurchaseOrder);
            return dataFactory.PurchaseOrderDal.UpdateEntity(onePurchaseOrder);
        }


        public bool DeletePurchaseCommoditiesByPurchaseOrderID(int purchaseOrderID)
        {
            string sqlWhere = " and PurchaseOrderID=" + purchaseOrderID;
            //调用另一个模块，调用BLL比较好
            //return new OracleDAL.PurchaseCommodityService().DeleteEntity(sqlWhere);
           return dataFactory.PurchaseCommdityDal.DeleteEntity(sqlWhere);
        }

        public bool DeleteOrderID(int id)
        {
            string sqlWhere = string.Format(" and id={0}", id);
            //return new OracleDAL.PurchaseOrderService().DeleteEntity(sqlWhere);
            return dataFactory.PurchaseOrderDal.DeleteEntity(sqlWhere);
        }
    }
}
