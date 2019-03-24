using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Abstract
{
    public interface ISqlRepository<T> : IBaseRepository<T> where T : class
    {
        bool Delete(T entity);
    }
}
