using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Interfaces
{
    public interface ITransactionAppService
    {
        public Task<Transaction> Delete(Guid id);

        public Task<bool> Exists(Guid id);

        public Task<List<Transaction>> GetAll();

        public Task<Transaction> GetById(Guid guid);

        public Task<List<Transaction>> GetByProperty(Expression<Func<Transaction, bool>> expression);

        public Task<Transaction> Post(Guid accountId, float amount);

        public Task<Transaction> UpdateAccountId(Guid accountId, Guid id);

        public Task<Transaction> UpdateAmount(float amount, Guid id);
    }
}
