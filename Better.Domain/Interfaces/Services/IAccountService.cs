using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<Account> Delete(Account account);

        public Task<List<Account>> GetAll();

        public Task<Account> GetById(Guid id);

        public Task<List<Account>> GetByProperty(Expression<Func<Account, bool>> expression);

        public Task<Account> Post(Account account);

        public Task<Account> Update(Account account);
    }
}
