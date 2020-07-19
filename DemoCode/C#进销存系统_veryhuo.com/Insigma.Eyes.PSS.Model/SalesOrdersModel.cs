using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Insigma.Eyes.PSS.Model
{

    /// <summary>
    /// 销售订单信息
    /// </summary>
    [DataContract]
    public class SalesOrdersModel
    {
        [DataMember]
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 销售订单编号
        /// </summary>
        [DataMember]
        public string OrderNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        [DataMember]
        public string Tel
        {
            get;
            set;
        }

        /// <summary>
        /// 送货地址
        /// </summary>
        [DataMember]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 销售合同信息
        /// </summary>
        [DataMember]
        public string Contract
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态，已出库，未出库
        /// </summary>
        [DataMember]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// 订单时间
        /// </summary>
        [DataMember]
        public DateTime OrderDate
        {
            get;
            set;
        }
    }
}
