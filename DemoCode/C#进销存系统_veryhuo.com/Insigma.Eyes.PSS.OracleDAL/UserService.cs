using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace Insigma.Eyes.PSS.OracleDAL
{
    public class UserService:IDAL.IUserService
    {
        public Model.UserModel GetOneUser(string sqlWhere)
        {
            string sql = string.Format("select * from Users where 1=1 {0}",sqlWhere);
            using (OracleDataReader odr=OracleHelper.ExecuteReader(OracleHelper.ConnectionString,System.Data.CommandType.Text,sql,null))
            {
                if (odr.Read())
                {
                    Model.UserModel user = new Model.UserModel();
                    user.ID = odr.GetInt32(0);
                    user.UserName = odr.GetString(1);
                    user.Password = odr.GetString(2);
                    user.Role = odr.GetInt32(3);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
