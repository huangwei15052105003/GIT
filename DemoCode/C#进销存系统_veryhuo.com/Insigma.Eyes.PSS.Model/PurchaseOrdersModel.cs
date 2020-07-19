using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Insigma.Eyes.PSS.Model
{
   /// <summary>
   /// 采购订单
   /// </summary>
    [DataContract]
    public class PurchaseOrdersModel
    {
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        /// <summary>
        /// 供应商联系电话
        /// </summary>
        [DataMember]
        public string Tel { get; set; }
        /// <summary>
        /// 供应商联系地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 合同信息
        /// </summary>
        [DataMember]
        public string Contract { get; set; }
        /// <summary>
        /// 订单状态：已入库，未入库
        /// </summary>
        [DataMember]
        public string Status { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [DataMember]
        public DateTime  OrderDate { get; set; }
    }
}
