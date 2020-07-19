using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insigma.Eyes.PSS.AbstractFactory
{
    public abstract class DalFactory
    {
        public abstract IDAL.ICommodityService CommodityDal
        {
            get;
        }
        public abstract IDAL.IPurchaseCommodityService PurchaseCommodityDal
        {
            get;
        }
        public abstract IDAL.IPurchaseOrdersService PurchaseOrderDal
        {
            get;
        }
        public abstract IDAL.ISalesCommodityService SalesCommodityDal
        {
            get;
        }
        public abstract IDAL.ISalesOrdersService SalesOrderDal
        {
            get;
        }
        public abstract IDAL.IUserService User
        {
            get;
        }
    }
}
