using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Better.Tools.Validations;
using System.Linq.Expressions;

namespace Better.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryWrapper _repository;

        public TransactionService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Delete(Transaction transaction)
        {
            if (transaction is null)
            {
                return transaction;
            }

            if (await GetById(transaction.Id) == default)
            {
                return null;
            }

            _repository.Transaction.Delete(transaction);
            var commitSuccessful = await _repository.Commit();

            if (!commitSuccessful)
            {
                return null;
            }

            return transaction;
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _repository.Transaction.Get();
        }

        public async Task<Transaction> GetById(Guid id)
        {
            return await _repository.Transaction.GetFirstByProperty(transaction => transaction.Id == id);
        }

        public async Task<List<Transaction>> GetByProperty(Expression<Func<Transaction, bool>> expression)
        {
            return await _repository.Transaction.GetAllByProperty(expression);
        }

        public async Task<Transaction> Post(Transaction transaction)
        {
            if (transaction is null)
            {
                return transaction;
            }

            if (!Helpers.GuidIsValid(transaction.Id))
            {
                return null;
            }

            await _repository.Transaction.Post(transaction);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return transaction;
        }

        public async Task<Transaction> Update(Transaction transaction)
        {
            if (transaction is null)
            {
                return transaction;
            }

            var old = await GetById(transaction.Id);

            if (old is null)
            {
                return old;
            }

            old.Amount = transaction.Amount;
            old.AccountId = transaction.AccountId;

            _repository.Transaction.Update(old);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return transaction;
        }
    }
}
