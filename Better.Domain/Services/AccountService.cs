using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Better.Tools.Validations;
using System.Linq.Expressions;

namespace Better.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryWrapper _repository;

        public AccountService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Account> Delete(Account account)
        {
            if (account is null)
            {
                return account;
            }

            if (await GetById(account.Id) == default)
            {
                return null;
            }

            _repository.Account.Delete(account);
            var commitSuccessful = await _repository.Commit();

            if (!commitSuccessful)
            {
                return null;
            }

            return account;
        }

        public async Task<List<Account>> GetAll()
        {
            return await _repository.Account.Get();
        }

        public async Task<Account> GetById(Guid id)
        {
            return await _repository.Account.GetFirstByProperty(account => account.Id == id);
        }

        public async Task<List<Account>> GetByProperty(Expression<Func<Account, bool>> expression)
        {
            return await _repository.Account.GetAllByProperty(expression);
        }

        public async Task<Account> Post(Account account)
        {
            if (account is null)
            {
                return account;
            }

            if (!Helpers.GuidIsValid(account.Id))
            {
                return null;
            }

            await _repository.Account.Post(account);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return account;
        }

        public async Task<Account> Update(Account account)
        {
            if (account is null)
            {
                return account;
            }

            var old = await GetById(account.Id);

            if (old is null)
            {
                return old;
            }

            old.Name = account.Name;
            old.HaveBets = account.HaveBets;
            old.Balance = account.Balance;

            _repository.Account.Update(old);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return account;
        }
    }
}
