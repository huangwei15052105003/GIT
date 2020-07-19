using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Insigma.Eyes.PSS.Model
{
    /// <summary>
    /// 采购商品信息
    /// </summary>
    [DataContract]
    public class PurchaseCommodityModel
    {
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 采购订单ID
        /// </summary>
        [DataMember]
        public int PurchaseOrderID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember]
        public int CommodityID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string CommodityName { get; set; }
        /// <summary>
        /// 商品型号
        /// </summary>
        [DataMember]
        public string CommodityType { get; set; }
        /// <summary>
        /// 商品生产厂家
        /// </summary>
        [DataMember]
        public string CommodityManufacturer { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        [DataMember]
        public int CommodityInventory { get; set; }
        /// <summary>
        /// 商品推介销售单价
        /// </summary>
        [DataMember]
        public decimal CommodityUnitPrice { get; set; }
        /// <summary>
        /// 商品单位
        /// </summary>
        [DataMember]
        public string CommodityUnit { get; set; }
        /// <summary>
        /// 商品采购数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
        /// <summary>
        /// 采购单价
        /// </summary>
        [DataMember]
        public decimal PurchasePrice { get; set; }
        /// <summary>
        /// 采购总金额
        /// </summary>
        [DataMember]
        public decimal TotalPrice { get; set; }
    }
}
