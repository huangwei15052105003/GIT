using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.IDAL;
using System.Data.OracleClient;

namespace Insigma.Eyes.PSS.OracleDAL
{
     public class PurchaseCommodityService:IPurchaseCommodityService
    {
        public List<Model.PurchaseCommodityModel> GetEntities(string sqlWhere)
        {
            string sql = string.Format("select * from v_purchasecommodity where 1=1 {0}",sqlWhere);
            List<Model.PurchaseCommodityModel> listPurchaseCommodities = new List<Model.PurchaseCommodityModel>();
            using (OracleDataReader odr=OracleHelper.ExecuteReader(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null))
            {
                while (odr.Read())
                {
                    Model.PurchaseCommodityModel purchaseCommodityModel = new Model.PurchaseCommodityModel();
                    purchaseCommodityModel.ID = odr.GetInt32(0);
                    //由于数据库在该字段设置了值太大，所以溢出
                    purchaseCommodityModel.PurchaseOrderID = odr.GetInt32(1);
                    purchaseCommodityModel.CommodityID = odr.GetInt32(2);
                    purchaseCommodityModel.CommodityName = odr.IsDBNull(3) ? "" : odr.GetString(3);
                    purchaseCommodityModel.CommodityType = odr.IsDBNull(4) ? "" : odr.GetString(4);
                    purchaseCommodityModel.CommodityManufacturer = odr.IsDBNull(5) ? "" : odr.GetString(5);
                    purchaseCommodityModel.CommodityInventory = odr.IsDBNull(6) ? 0 : odr.GetInt32(6);
                    purchaseCommodityModel.CommodityUnitPrice = odr.IsDBNull(7) ? 0 : odr.GetDecimal(7);
                    purchaseCommodityModel.CommodityUnit = odr.IsDBNull(8) ? "": odr.GetString(8);
                    purchaseCommodityModel.Count = odr.IsDBNull(9) ? 0 : odr.GetInt32(9);
                    purchaseCommodityModel.PurchasePrice = odr.IsDBNull(10) ? 0 : odr.GetInt32(10);
                    purchaseCommodityModel.TotalPrice = odr.IsDBNull(11) ? 0 : odr.GetInt32(11);
                    listPurchaseCommodities.Add(purchaseCommodityModel);
                }
            }
            return listPurchaseCommodities;
        }

        public Model.PurchaseCommodityModel GetOneEntityByID(int id)
        {
            string sqlWhere = string.Format(" and ID={0}",id);
            List<Model.PurchaseCommodityModel> list = GetEntities(sqlWhere);
            return list.SingleOrDefault(p => p.ID == id);
        }

        private int GetNewID()
        {
            string sql = "select s_purchasecommodity.nextval from dual";
            return int.Parse(OracleHelper.ExecuteScalar(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null).ToString());
        }

        public Model.PurchaseCommodityModel AddEntity(Model.PurchaseCommodityModel entity)
        {
            entity.ID = GetNewID();
            string sql = string.Format("insert into purchasecommodity(id,purchaseorderid,commodityid,count,purchaseprice,totalprice) values({0},{1},{2},{3},{4},{5})",
                entity.ID, entity.PurchaseOrderID, entity.CommodityID, entity.Count, entity.PurchasePrice, entity.TotalPrice);
            if (OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null) > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateEntity(Model.PurchaseCommodityModel entity)
        {
            string sql = string.Format("update purchasecommodity set purchaseorderid={0},commodityid={1},count={2},purchaseprice={3},totalprice={4} where id={5}",
                entity.PurchaseOrderID, entity.CommodityID, entity.Count, entity.PurchasePrice, entity.TotalPrice, entity.ID);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null)>0;
        }

        public bool DeleteEntity(string sqlWhere)
        {
            string sql = string.Format("delete from purchasecommodity where 1=1 {0}",sqlWhere);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null)>0;
        }
    }
}
