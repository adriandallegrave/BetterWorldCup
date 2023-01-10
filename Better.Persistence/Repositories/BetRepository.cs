using Better.Domain.Interfaces.Repositories;
using Better.Domain.Models;
using Better.Persistence.Repositories.Base;

namespace Better.Persistence.Repositories
{
    public class BetRepository : BaseRepository<Bet>, IBetRepository
    {
        public BetRepository(BetterContext context) : base(context)
        {
        }
    }
}
