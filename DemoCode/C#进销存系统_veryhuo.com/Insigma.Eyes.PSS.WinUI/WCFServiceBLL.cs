using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.WinUI.BLLCommodity;
using Insigma.Eyes.PSS.WinUI.BLLSalesOrders;
using System.ServiceModel;
namespace Insigma.Eyes.PSS.WinUI
{
    public class WCFServiceBLL
    {
       //对WCF而言，对象实例化越多，对服务器压力越大。
        static BLLCommodity.CommodityManagerServiceClient commodityClient;
        static BLLSalesOrders.SalesManagerServiceClient salesClient;
        static BLLUsers.UserManagerServiceClient userClient;
        static BLLPurchaseOrders.PurchaseManagerServiceClient purchaseClient;
        public static CommodityManagerServiceClient GetCommodityService()
        {
            if (commodityClient==null)
            {
                commodityClient = new CommodityManagerServiceClient();
            }
            if (commodityClient.State==CommunicationState.Closed)
            {
                commodityClient = new CommodityManagerServiceClient();
            }
            if (commodityClient.State==CommunicationState.Faulted)
            {
                commodityClient = new CommodityManagerServiceClient();
            }
            return commodityClient;
        }
        public static BLLUsers.UserManagerServiceClient GetUserService()
        {
            if (userClient==null)
            {
                userClient = new BLLUsers.UserManagerServiceClient();
            }
            if (userClient.State==CommunicationState.Closed)
            {
                userClient = new BLLUsers.UserManagerServiceClient();
            }
            if (userClient.State==CommunicationState.Faulted)
            {
                userClient = new BLLUsers.UserManagerServiceClient();
            }
            return userClient;
            
        }
        public static SalesManagerServiceClient GetSalesService()
        {
            if (salesClient == null)
            {
                salesClient = new SalesManagerServiceClient();
            }
            if (salesClient.State == CommunicationState.Closed)
            {
                salesClient = new SalesManagerServiceClient();
            }
            if (salesClient.State == CommunicationState.Faulted)
            {
                salesClient = new SalesManagerServiceClient();
            }
            return salesClient;
        }
        public static BLLPurchaseOrders.PurchaseManagerServiceClient GetPurchaseService()
        {
            if (purchaseClient==null)
            {
                purchaseClient = new BLLPurchaseOrders.PurchaseManagerServiceClient();
            }
            if (purchaseClient.State==CommunicationState.Closed)
            {
                purchaseClient = new BLLPurchaseOrders.PurchaseManagerServiceClient();
            }
            if (purchaseClient.State==CommunicationState.Faulted)
            {
                purchaseClient = new BLLPurchaseOrders.PurchaseManagerServiceClient();
            }
            return purchaseClient;
        }
    }
}
