using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BettingApp.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
