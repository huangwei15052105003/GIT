using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insigma.Eyes.PSS.Model;

namespace Insigma.Eyes.PSS.IDAL
{
    public interface IBaseService<T> where T :class
    {
        List<T> GetEntities(string sqlWhere);
        T GetOneEntityByID(int id);
        T AddEntity(T entity);
        bool UpdateEntity(T entity);
        bool DeleteEntity(string sqlWhere);
    }
}

// Downloads By http://www.veryhuo.com