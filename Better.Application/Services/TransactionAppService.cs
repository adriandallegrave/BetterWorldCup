using Better.Application.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionService _transactionService;

        public TransactionAppService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<Transaction> Delete(Guid id)
        {
            var transaction = await GetById(id);

            if (transaction is null)
            {
                return transaction;
            }

            return await _transactionService.Delete(transaction);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetById(id) != null;
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _transactionService.GetAll();
        }

        public async Task<Transaction> GetById(Guid id)
        {
            return await _transactionService.GetById(id);
        }

        public async Task<List<Transaction>> GetByProperty(Expression<Func<Transaction, bool>> expression)
        {
            return await _transactionService.GetByProperty(expression);
        }

        public async Task<Transaction> Post(Guid accountId, float amount)
        {
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Timestamp = DateTime.Now,
                AccountId = accountId
            };

            return await _transactionService.Post(transaction);
        }

        public async Task<Transaction> UpdateAccountId(Guid accountId, Guid id)
        {
            var transaction = await GetById(id);

            if (transaction is null)
            {
                return transaction;
            }

            transaction.AccountId = accountId;

            return await _transactionService.Update(transaction);
        }

        public async Task<Transaction> UpdateAmount(float amount, Guid id)
        {
            var transaction = await GetById(id);

            if (transaction is null)
            {
                return transaction;
            }

            transaction.Amount = amount;

            return await _transactionService.Update(transaction);
        }
    }
}
