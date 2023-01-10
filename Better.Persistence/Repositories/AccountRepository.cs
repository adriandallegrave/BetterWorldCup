using Better.Domain.Interfaces.Repositories;
using Better.Domain.Models;
using Better.Persistence.Repositories.Base;

namespace Better.Persistence.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(BetterContext context) : base(context)
        {
        }
    }
}
