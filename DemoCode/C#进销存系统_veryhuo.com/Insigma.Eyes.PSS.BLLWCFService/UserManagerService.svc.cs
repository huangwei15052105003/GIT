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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserManagerService”。
    public class UserManagerService : IUserManagerService
    {

       
        AbstractFactory.DalFactory dataFactory = null;

        public UserManagerService()
        {
            //异常直接在这儿显示
            dataFactory = DefaultProviderDal.DefaultDataProviderFactory;
        }
        
        public Model.UserModel Login(string userName, string password)
        {
            //IUserService userService = new OracleDAL.UserService();
            string sqlWhere = string.Format(" and userName='{0}' and password='{1}'",userName,password);
            //Model.UserModel user = userService.GetOneUser(sqlWhere);
            //return user;
            return dataFactory.UserDal.GetOneUser(sqlWhere);
        }

    }
}
