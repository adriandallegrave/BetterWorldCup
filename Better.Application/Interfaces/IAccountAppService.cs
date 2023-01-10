using Better.Application.Dtos;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Interfaces
{
    public interface IAccountAppService
    {
        public Task<Account> Delete(Guid id);

        public Task<bool> Exists(Guid id);

        public Task<List<Account>> GetAll();

        public Task<Account> GetById(Guid guid);

        public Task<List<Account>> GetByProperty(Expression<Func<Account, bool>> expression);

        public Task<Account> IncrementBalance(float amount, Guid id);

        public Task<Account> Post(AccountDto dto);

        public Task<Account> UpdateBalance(float balance, Guid id);

        public Task<Account> UpdateHaveBets(bool hasBets, Guid id);

        public Task<Account> UpdateName(string newName, Guid id);
    }
}
