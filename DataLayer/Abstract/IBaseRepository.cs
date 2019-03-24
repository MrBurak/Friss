using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Abstract
{
    public interface IBaseRepository<T> where T : class
    {        
        List<T> GetList(Expression<Func<T, bool>> query = null);

        List<T> GetList(Expression<Func<T, bool>> query = null, int currentPage = 0, int pageSize = 10);

        T GetEntity(Expression<Func<T, bool>> query = null);

        T Add(T entity);        

        bool Update(T newEntity);

        long RecordCount();

        long RecordCount(Expression<Func<T, bool>> query = null);

        bool Delete(Expression<Func<T, bool>> query = null);

        bool DeleteOne(int id);        
    }
}
