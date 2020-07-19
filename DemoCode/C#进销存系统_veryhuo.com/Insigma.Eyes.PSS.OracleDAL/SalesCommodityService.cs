using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.IDAL;
using System.Data.OracleClient;

namespace Insigma.Eyes.PSS.OracleDAL
{
    public class SalesCommodityService:ISalesCommodityService
    {
        public List<Model.SalesCommodityModel> GetEntities(string sqlWhere)
        {
            string sql = string.Format("select * from v_salesCommodity where 1=1 {0}",sqlWhere);
            List<Model.SalesCommodityModel> listSalesCommodity = new List<Model.SalesCommodityModel>();
            using (OracleDataReader odr=OracleHelper.ExecuteReader(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null))
            {
                while (odr.Read())
                {
                    Model.SalesCommodityModel salesCommodityModel = new Model.SalesCommodityModel();
                    salesCommodityModel.ID = odr.GetInt32(0);
                    salesCommodityModel.SalesOrderID = odr.GetInt32(1);
                    salesCommodityModel.CommodityID = odr.GetInt32(2);
                    salesCommodityModel.CommodityName = odr.IsDBNull(3) ? "" : odr.GetString(3);
                    salesCommodityModel.CommodityType = odr.IsDBNull(4) ? "" : odr.GetString(4);
                    salesCommodityModel.CommodityManufacturer = odr.IsDBNull(5) ? "" : odr.GetString(5);
                    salesCommodityModel.CommodityInventory = odr.IsDBNull(6) ? 0 : odr.GetInt32(6);
                    salesCommodityModel.CommodityUnitPrice = odr.IsDBNull(7) ? 0 : odr.GetInt32(7);
                    salesCommodityModel.CommodityUnit = odr.IsDBNull(8) ? "" : odr.GetString(8);
                    salesCommodityModel.Count = odr.IsDBNull(9) ? 0 : odr.GetInt32(9);
                    salesCommodityModel.SalesPrice = odr.IsDBNull(10) ? 0 : odr.GetDecimal(10);
                    salesCommodityModel.TotalPrice = odr.IsDBNull(11) ? 0 : odr.GetDecimal(11);
                    listSalesCommodity.Add(salesCommodityModel);
                }
            }
            return listSalesCommodity;
        }

        public Model.SalesCommodityModel GetOneEntityByID(int id)
        {
            string sqlWhere = string.Format(" and id={0}",id);
            List<Model.SalesCommodityModel> listSalesCommodity = GetEntities(sqlWhere);
            return listSalesCommodity.SingleOrDefault(u=>u.ID==id);
        }

        private int GetNewID()
        {
            string sql = "select s_salescommodity.nextval from dual";
            return int.Parse(OracleHelper.ExecuteScalar(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null).ToString());
        }

        public Model.SalesCommodityModel AddEntity(Model.SalesCommodityModel entity)
        {
            entity.ID = GetNewID();
            string sql = string.Format("insert into SALESCOMMODITY(ID,SALESORDERID,COMMODITYID,COUNT,SALESPRICE,TOTALPRICE) values({0},{1},{2},{3},{4},{5})",
        entity.ID, entity.SalesOrderID, entity.CommodityID, entity.Count, entity.SalesPrice, entity.TotalPrice);
            if (OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null)>0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateEntity(Model.SalesCommodityModel entity)
        {
            string sql = string.Format("update  SALESCOMMODITY set SALESORDERID={0},COMMODITYID={1},COUNT={2},SALESPRICE={3},TOTALPRICE={4} where id={5}",
               entity.SalesOrderID, entity.CommodityID, entity.Count, entity.SalesPrice, entity.TotalPrice, entity.ID);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null) > 0;
        }

        public bool DeleteEntity(string sqlWhere)
        {
            string sql = string.Format("delete from SalesCommodity where 1=1 {0}",sqlWhere);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null) > 0;
        }

    }
}
