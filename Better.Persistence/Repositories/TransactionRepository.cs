using Better.Domain.Interfaces.Repositories;
using Better.Domain.Models;
using Better.Persistence.Repositories.Base;

namespace Better.Persistence.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BetterContext context) : base(context)
        {
        }
    }
}
