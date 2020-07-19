using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insigma.Eyes.PSS.IDAL
{
    public interface IUserService
    {
        Model.UserModel GetOneUser(string sqlWhere);
    }
}
