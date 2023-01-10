using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Services
{
    public interface ITransactionService
    {
        public Task<Transaction> Delete(Transaction transaction);

        public Task<List<Transaction>> GetAll();

        public Task<Transaction> GetById(Guid id);

        public Task<List<Transaction>> GetByProperty(Expression<Func<Transaction, bool>> expression);

        public Task<Transaction> Post(Transaction transaction);

        public Task<Transaction> Update(Transaction transaction);
    }
}
