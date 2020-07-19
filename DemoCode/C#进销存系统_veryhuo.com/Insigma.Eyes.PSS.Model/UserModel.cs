using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Insigma.Eyes.PSS.Model
{
   /// <summary>
   /// 系统用户信息
   /// </summary>
    [DataContract]
    public class UserModel
    {
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// 用户角色，0=管理员，1=采购员，2=销售员，3=库存管理员
        /// </summary>
        [DataMember]
        public int Role { get; set; }
    }
}
