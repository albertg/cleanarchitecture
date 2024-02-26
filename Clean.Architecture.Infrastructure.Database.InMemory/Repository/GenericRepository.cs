using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Infrastructure.Database.InMemory.Repository
{
    internal class GenericRepository<T> where T : class
    {
        internal ParishDBContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(ParishDBContext context) { 
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual T Get(Guid id)
        {
            return dbSet.Find(id);
        }
    }
}
