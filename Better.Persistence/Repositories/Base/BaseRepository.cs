using Better.Domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Better.Persistence.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected BetterContext Context { get; }
        protected DbSet<T> DbSet { get; }

        public BaseRepository(BetterContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public T Delete(T entity)
        {
            DbSet.Remove(entity);
            return entity;
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<List<T>> Get()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllByProperty(Expression<Func<T, bool>> expression)
        {
            var result = new List<T>();
            try
            {
                result = await DbSet.Where(expression).ToListAsync();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public async Task<T> GetFirstByProperty(Expression<Func<T, bool>> expression)
        {
            return await DbSet.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<T> Post(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            DbSet.Update(entity);
            return entity;
        }
    }
}
