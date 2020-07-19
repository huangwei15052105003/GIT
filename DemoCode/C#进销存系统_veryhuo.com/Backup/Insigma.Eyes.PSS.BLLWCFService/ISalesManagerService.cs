using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Insigma.Eyes.PSS.Model;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISalesManagerService”。
    [ServiceContract]
    public interface ISalesManagerService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SalesOrdersModel[] GetSalesOrders(string orderNumber,string orderDateStart,string orderDateEnd,string status);

        [OperationContract]
        SalesCommodityModel[] GetSalesCommoditiesByID(int salesID);

        [OperationContract]
        Model.SalesCommodityModel AddSalesCommodity(Model.SalesCommodityModel oneSalesCommodity);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool PostSalesOrder(int id);

        [OperationContract]
        SalesOrdersModel GetOneSalesOrder(int id);


    }
}
