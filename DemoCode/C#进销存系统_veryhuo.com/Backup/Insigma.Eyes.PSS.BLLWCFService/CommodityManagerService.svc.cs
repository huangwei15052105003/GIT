using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Insigma.Eyes.PSS.IDAL;
using Insigma.Eyes.PSS.OracleDAL;

namespace Insigma.Eyes.PSS.BLLWCFService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“CommodityManagerService”。
    public class CommodityManagerService : ICommodityManagerService
    {
        public Model.CommodityModel[] GetCommodities(string name, string type, string manufacturer, string priceLow, string priceHigh)
        {
            string sqlWhere = "";
            if (!string.IsNullOrWhiteSpace(name))
            {
                sqlWhere += string.Format(" and Name like '%{0}%'",name);
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                sqlWhere += string.Format(" and Type like '%{0}%'", type);
            }
            if (!string.IsNullOrWhiteSpace(manufacturer))
            {
                sqlWhere += string.Format(" and Manufacturer like '%{0}%'", manufacturer);
            }
            if (!string.IsNullOrWhiteSpace(priceLow))
            {
                try
                {
                    decimal dPriceLow=decimal.Parse(priceLow);//调试一下，是否需要赋值
                    sqlWhere += string.Format(" and UnitPrice>={0}", dPriceLow);
                }
                catch
                {
                    Exception e = new Exception();
                    throw new FaultException<Exception>(e,"单价下限有误！");
                }
            }
            if (!string.IsNullOrWhiteSpace(priceHigh))
            {
                try
                {
                    decimal dPriceHigh = decimal.Parse(priceHigh);
                    sqlWhere += string.Format(" and UnitPrice<={0}",dPriceHigh);
                }
                catch 
                {
                    Exception e = new Exception();
                    throw new FaultException<Exception>(e, "单价上限有误会!");
                }
            }
            ICommodityService commodityService = new OracleDAL.CommodityService();
            return commodityService.GetCommodities(sqlWhere).ToArray();
        }


        public Model.CommodityModel[] GetCommoditiesByCondition(string condition)
        {
            string sqlWhere = "";
            if (string.IsNullOrWhiteSpace(condition))
            {
                sqlWhere+=string.Format("and (Name Like '%{0}%' or Type Like '%{0}%' or Manufacturer Like '%{0}%' )",condition);
            }
            return new OracleDAL.CommodityService().GetCommodities(sqlWhere).ToArray();
        }

        /// <summary>
        /// 获取一个产品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.CommodityModel GetOneCommodity(int id)
        {
            return new OracleDAL.CommodityService().GetOneCommodityByID(id);
        }
    }
}
