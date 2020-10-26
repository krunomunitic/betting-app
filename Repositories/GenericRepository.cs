using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using BettingApp.Data;

namespace BettingApp.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        // async vs sync https://stackoverflow.com/questions/25086866/why-does-the-ef-6-tutorial-use-asynchronous-calls
        // TODO: change all to async

        // Class variables is declared for the database context
        protected readonly BettingAppContext _context;

        // The constructor accepts a database context instance and initializes
        //the entity set variable
        public GenericRepository(BettingAppContext context)
        {
            _context = context;
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
