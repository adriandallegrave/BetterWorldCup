using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountService _accountService;

        public AccountAppService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Account> Delete(Guid id)
        {
            var account = await GetById(id);

            if (account is null)
            {
                return account;
            }

            return await _accountService.Delete(account);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetById(id) != null;
        }

        public async Task<List<Account>> GetAll()
        {
            return await _accountService.GetAll();
        }

        public async Task<Account> GetById(Guid id)
        {
            return await _accountService.GetById(id);
        }

        public async Task<List<Account>> GetByProperty(Expression<Func<Account, bool>> expression)
        {
            return await _accountService.GetByProperty(expression);
        }

        public async Task<Account> IncrementBalance(float amount, Guid id)
        {
            var account = await GetById(id);

            if (account is null)
            {
                return account;
            }

            account.Balance += amount;

            return await _accountService.Update(account);
        }

        public async Task<Account> Post(AccountDto dto)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                HaveBets = false,
                Balance = 0.00F,
                Name = dto.Name
            };

            var accountsWithSameMail = await GetByProperty(x => x.Email == account.Email);

            if (accountsWithSameMail.Any())
            {
                return null;
            }

            var accountsWithSameName = await GetByProperty(x => x.Name == account.Name);

            if (accountsWithSameName.Any())
            {
                return null;
            }

            return await _accountService.Post(account);
        }

        public async Task<Account> UpdateBalance(float balance, Guid id)
        {
            var account = await GetById(id);

            if (account is null)
            {
                return account;
            }

            account.Balance = balance;

            return await _accountService.Update(account);
        }

        public async Task<Account> UpdateHaveBets(bool hasBets, Guid id)
        {
            var account = await GetById(id);

            if (account is null)
            {
                return account;
            }

            account.HaveBets = hasBets;

            return await _accountService.Update(account);
        }

        public async Task<Account> UpdateName(string newName, Guid id)
        {
            var account = await GetById(id);

            if (account is null)
            {
                return account;
            }

            account.Name = newName;

            return await _accountService.Update(account);
        }
    }
}
