using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.IDAL;
using System.Data.OracleClient;
using System.Data;

namespace Insigma.Eyes.PSS.OracleDAL
{
    public class PurchaseOrderService:IPurchaseOrdersService
    {

        public List<Model.PurchaseOrdersModel> GetEntities(string sqlWhere)
        {
            string sql = string.Format("select * from PurchaseOrders where 1=1 {0}", sqlWhere);
            List<Model.PurchaseOrdersModel> listPurchaseOrders = new List<Model.PurchaseOrdersModel>();
            using (OracleDataReader odr = OracleHelper.ExecuteReader(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null))
            {
                while (odr.Read())
                {
                    Model.PurchaseOrdersModel purchaseOrderModel = new Model.PurchaseOrdersModel();
                    purchaseOrderModel.ID = odr.GetInt32(0);
                    purchaseOrderModel.OrderNumber = odr.IsDBNull(1) ? "" : odr.GetString(1);
                    purchaseOrderModel.SupplierName = odr.IsDBNull(2) ? "" : odr.GetString(2);
                    purchaseOrderModel.Tel = odr.IsDBNull(3) ? "" : odr.GetString(3);
                    purchaseOrderModel.Address = odr.IsDBNull(4) ? "" : odr.GetString(4);
                    purchaseOrderModel.Contract = odr.IsDBNull(5) ? "" : odr.GetString(5);
                    purchaseOrderModel.Status = odr.IsDBNull(6) ? "" : odr.GetString(6);
                    purchaseOrderModel.OrderDate = odr.IsDBNull(7) ? DateTime.Now : odr.GetDateTime(7);
                    listPurchaseOrders.Add(purchaseOrderModel);
                }
            }
            return listPurchaseOrders;
        }

        public Model.PurchaseOrdersModel GetOneEntityByID(int id)
        {
            string sqlWhere = string.Format(" and id={0}", id);
            List<Model.PurchaseOrdersModel> listPurchaseOrders = GetEntities(sqlWhere);
            return listPurchaseOrders.SingleOrDefault(u => u.ID == id);
        }

        private int GetNewID()
        {
            string sql = "select s_purchaseOrders.nextval from dual";
            return int.Parse(OracleHelper.ExecuteScalar(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null).ToString());
        }
        public Model.PurchaseOrdersModel AddEntity(Model.PurchaseOrdersModel entity)
        {
            entity.ID = GetNewID();
            string sql = string.Format("insert into purchaseOrders(ID,OrderNumber,SupplierName,Tel,Address,Contract,Status,OrderDate) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',to_date('{7}','yyyy-mm-dd'))",
             entity.ID, entity.OrderNumber, entity.SupplierName, entity.Tel, entity.Address, entity.Contract, entity.Status, entity.OrderDate.ToString("yyyy-MM-dd"));
            if (OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateEntity(Model.PurchaseOrdersModel entity)
        {
            string sql = string.Format("Update PurchaseOrders Set OrderNumber='{0}',SupplierName='{1}',Tel='{2}',Address='{3}',Contract='{4}',Status='{5}',OrderDate=to_date('{6}','yyyy-MM-dd') Where ID={7}",
               entity.OrderNumber, entity.SupplierName, entity.Tel, entity.Address, entity.Contract, entity.Status, entity.OrderDate.ToString("yyyy-MM-dd"), entity.ID);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0;
        }

        public bool DeleteEntity(string sqlWhere)
        {
            string sql = string.Format("delete from PurchaseOrders where 1=1  {0}", sqlWhere);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0;
        }
    }
}
