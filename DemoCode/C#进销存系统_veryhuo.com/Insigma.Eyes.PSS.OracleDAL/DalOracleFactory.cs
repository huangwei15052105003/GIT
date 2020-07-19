using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insigma.Eyes.PSS.OracleDAL
{
    public class DalOracleFactory:Insigma.Eyes.PSS.AbstractFactory.DalFactory
    {
        public override IDAL.ICommodityService CommdityDal
        {
            //缓存里拿
            get {

                OracleDAL.CommodityService instance = System.Web.HttpRuntime.Cache["CommodityDal"] as OracleDAL.CommodityService;
                if (instance == null)
                {
                    instance = new OracleDAL.CommodityService();
                    System.Web.HttpRuntime.Cache.Add("CommodityDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
            
        }

        public override IDAL.IPurchaseCommodityService PurchaseCommdityDal
        {
            get
            {

                OracleDAL.PurchaseCommodityService instance = System.Web.HttpRuntime.Cache["PurchaseCommdityDal"] as OracleDAL.PurchaseCommodityService;
                if (instance == null)
                {
                    instance = new OracleDAL.PurchaseCommodityService();
                    System.Web.HttpRuntime.Cache.Add("PurchaseCommdityDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
        }

        public override IDAL.IPurchaseOrdersService PurchaseOrderDal
        {
            get
            {

                OracleDAL.PurchaseOrderService instance = System.Web.HttpRuntime.Cache["PurchaseOrderDal"] as OracleDAL.PurchaseOrderService;
                if (instance == null)
                {
                    instance = new OracleDAL.PurchaseOrderService();
                    System.Web.HttpRuntime.Cache.Add("PurchaseOrderDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
        }

        public override IDAL.ISalesCommodityService SalesCommodityDal
        {
            get
            {

                OracleDAL.SalesCommodityService instance = System.Web.HttpRuntime.Cache["SalesCommodityDal"] as OracleDAL.SalesCommodityService;
                if (instance == null)
                {
                    instance = new OracleDAL.SalesCommodityService();
                    System.Web.HttpRuntime.Cache.Add("SalesCommodityDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
        }

        public override IDAL.ISalesOrdersService SalesOrderDal
        {
            get
            {

                OracleDAL.SalesOrderService instance = System.Web.HttpRuntime.Cache["SalesOrdersDal"] as OracleDAL.SalesOrderService;
                if (instance == null)
                {
                    instance = new OracleDAL.SalesOrderService();
                    System.Web.HttpRuntime.Cache.Add("SalesOrdersDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
        }

        public override IDAL.IUserService UserDal
        {
            get
            {

                OracleDAL.UserService instance = System.Web.HttpRuntime.Cache["UserDal"] as OracleDAL.UserService;
                if (instance == null)
                {
                    instance = new OracleDAL.UserService();
                    System.Web.HttpRuntime.Cache.Add("UserDal", instance, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
                }
                return instance;

            }
        }
    }
}
