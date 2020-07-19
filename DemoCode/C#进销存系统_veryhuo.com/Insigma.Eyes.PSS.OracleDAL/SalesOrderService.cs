using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.IDAL;
using Insigma.Eyes.PSS.Model;
using System.Data.OracleClient;
using System.Data;

namespace Insigma.Eyes.PSS.OracleDAL
{
    public class SalesOrderService:ISalesOrdersService
    {


        private int GetNewID()
        {
            string sql = "select s_SalesOrders.nextval from dual";
            return int.Parse(OracleHelper.ExecuteScalar(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null).ToString());
        }



 

        public List<SalesOrdersModel> GetEntities(string sqlWhere)
        {
            string sql = string.Format("select * from SalesOrders where 1=1 {0}", sqlWhere);
            List<Model.SalesOrdersModel> listSalesOrders = new List<SalesOrdersModel>();
            using (OracleDataReader odr = OracleHelper.ExecuteReader(OracleHelper.ConnectionString, System.Data.CommandType.Text, sql, null))
            {
                while (odr.Read())
                {
                    Model.SalesOrdersModel salesOrdersModel = new SalesOrdersModel();
                    salesOrdersModel.ID = odr.GetInt32(0);
                    salesOrdersModel.OrderNumber = odr.IsDBNull(1) ? "" : odr.GetString(1);
                    salesOrdersModel.CustomerName = odr.IsDBNull(2) ? "" : odr.GetString(2);
                    salesOrdersModel.Tel = odr.IsDBNull(3) ? "" : odr.GetString(3);
                    salesOrdersModel.Address = odr.IsDBNull(4) ? "" : odr.GetString(4);
                    salesOrdersModel.Contract = odr.IsDBNull(5) ? "" : odr.GetString(5);
                    salesOrdersModel.Status = odr.IsDBNull(6) ? "" : odr.GetString(6);
                    salesOrdersModel.OrderDate = odr.IsDBNull(7) ? DateTime.Now : odr.GetDateTime(7);
                    listSalesOrders.Add(salesOrdersModel);
                }
            }
            return listSalesOrders;
        }

        public SalesOrdersModel GetOneEntityByID(int id)
        {
            string sqlWhere = string.Format(" and id={0}", id);
            List<Model.SalesOrdersModel> listSalesOrders = GetEntities(sqlWhere);
            return listSalesOrders.SingleOrDefault(u => u.ID == id);
        }

        public SalesOrdersModel AddEntity(SalesOrdersModel entity)
        {
            entity.ID = GetNewID();
            string sql = string.Format("insert into salesOrders(ID,OrderNumber,CustomerName,Tel,Address,Contract,Status,OrderDate) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',to_date('{7}','yyyy-mm-dd'))",
             entity.ID, entity.OrderNumber, entity.CustomerName, entity.Tel, entity.Address, entity.Contract, entity.Status, entity.OrderDate.ToString("yyyy-MM-dd"));
            if (OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateEntity(SalesOrdersModel entity)
        {
            //OrderNumber 这儿写上吧，实际业务里不能被修改
            //Oracle时间类型都是to_date()
            string sql = string.Format("Update SalesOrders Set OrderNumber='{0}',CustomerName='{1}',Tel='{2}',Address='{3}',Contract='{4}',Status='{5}',OrderDate=to_date('{6}','yyyy-MM-dd') Where ID={7}",
                entity.OrderNumber, entity.CustomerName, entity.Tel, entity.Address, entity.Contract, entity.Status, entity.OrderDate.ToString("yyyy-MM-dd"), entity.ID);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0;
        }

        /// <summary>
        /// 删除订单信息之前必须要先删除订单对应的商品信息，不然有垃圾数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>

        public bool DeleteEntity(string sqlWhere)
        {
            string sql = string.Format("delete from SalesOrders where 1=1  {0}", sqlWhere);
            return OracleHelper.ExecuteNonQuery(OracleHelper.ConnectionString, CommandType.Text, sql, null) > 0;
        }
    }
}
