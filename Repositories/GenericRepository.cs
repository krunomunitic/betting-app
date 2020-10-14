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
        // Class variables are declared for the database context and for the entity set that the repository is instantiated for
        protected readonly BettingAppContext _context;
        // internal DbSet<T> dbSet;

        // The constructor accepts a database context instance and initializes the entity set variable
        public GenericRepository(BettingAppContext context)
        {
            _context = context;
            // this.dbSet = context.Set<T>();
        }

        public T GetById(int id)
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
