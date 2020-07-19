using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Insigma.Eyes.PSS.Model
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [DataContract]
    public class CommodityModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 商品型号
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        [DataMember]
        public string Manufacturer { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [DataMember]
        public int Inventory { get; set; }
        /// <summary>
        /// 推介销售价格，不一定是真实价格
        /// </summary>
        [DataMember]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [DataMember]
        public string Unit { get; set; }
    }
}
