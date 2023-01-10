using Better.Domain.Interfaces.Repositories;
using Better.Domain.Models;
using Better.Persistence.Repositories.Base;

namespace Better.Persistence.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(BetterContext context) : base(context)
        {
        }
    }
}
