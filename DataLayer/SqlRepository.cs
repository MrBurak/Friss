using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataLayer.Abstract;


namespace DataLayer
{
    public class SqlRepository<T> : ISqlRepository<T> where T : class, new()
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public SqlRepository(/*EfPostgreSqlDbContext context*/)
        {
            //_context = context ?? throw new ArgumentNullException("_context can not be null.");

            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public long RecordCount()
        {
            return _dbSet.Count();
        }

        public long RecordCount(Expression<Func<T, bool>> query = null)
        {
            return _dbSet.Where(query).Count();
        }

        public bool DeleteOne(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            return _context.SaveChanges() != 0;
        }

        public bool Delete(Expression<Func<T, bool>> query = null)
        {
            T entityToDelete = _dbSet.Where(query).SingleOrDefault();
            Delete(entityToDelete);

            return _context.SaveChanges() != 0;
        }

        public bool Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            return _context.SaveChanges() != 0;
        }

        public T GetEntity(Expression<Func<T, bool>> query = null)
        {
            var entity = _dbSet.FirstOrDefault(query);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;
            return _dbSet.FirstOrDefault(query);
        }


        public List<T> GetList(Expression<Func<T, bool>> query = null)
        {
            var entities = _dbSet.Where(query);
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Detached;
                }
            }
            return _dbSet.Where(query).AsQueryable().ToList();
        }



        public List<T> GetList(Expression<Func<T, bool>> query = null, int currentPage = 0, int pageSize = 10)
        {
            var entities = _dbSet.Where(query);
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    _context.Entry(entity).State = EntityState.Detached;
                }
            }
            return _dbSet.Where(query).Skip(pageSize * currentPage).Take(pageSize).AsQueryable().ToList();
        }


        public bool Update(T newEntity)
        {
           
            _dbSet.Update(newEntity);
            _context.Entry(newEntity).State = EntityState.Modified;
            return _context.SaveChanges() != 0;
            
        }

      

    }
}
