using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Insigma.Eyes.PSS.Model;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPurchaseManagerService”。
    [ServiceContract]
    public interface IPurchaseManagerService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        Model.PurchaseOrdersModel[] GetPurchaseOrders(string orderNumber, string orderDateStart, string orderDateEnd, string status);

        [OperationContract]
        Model.PurchaseCommodityModel[] GetPurchaseCommoditiesByID(int purchaseID);

        [OperationContract]
        Model.PurchaseCommodityModel GetOnePurchaseCommoditiesByID(int purchaseCommodityID);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool UpdatePurchaseCommodity(Model.PurchaseCommodityModel model);

        [OperationContract]
        Model.PurchaseCommodityModel AddPurchaseCommodityModel(Model.PurchaseCommodityModel onePurchaseCommodity);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool PostPurchaseOrder(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Model.PurchaseOrdersModel GetOnePurchaseOrder(int id);

        [OperationContract]
        bool DeletePurchaseCommodity(int id);

        [OperationContract]
        Model.PurchaseOrdersModel AddPurchaseOrder(Model.PurchaseOrdersModel purchaseOrder);

        [OperationContract]
        bool UpdatePurchaseOrder(Model.PurchaseOrdersModel onePurchaseOrder);

        [OperationContract]
        bool DeletePurchaseCommoditiesByPurchaseOrderID(int purchaseOrderID);

        [OperationContract]
        bool DeleteOrderID(int id);
    }
}
