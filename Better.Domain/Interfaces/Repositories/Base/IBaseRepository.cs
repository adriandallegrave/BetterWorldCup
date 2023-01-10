using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        T Delete(T entity);

        Task<List<T>> Get();

        Task<List<T>> GetAllByProperty(Expression<Func<T, bool>> expression);

        Task<T> GetFirstByProperty(Expression<Func<T, bool>> expression);

        Task<T> Post(T entity);

        T Update(T entity);
    }
}
