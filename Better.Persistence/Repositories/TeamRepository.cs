using Better.Domain.Interfaces.Repositories;
using Better.Domain.Models;
using Better.Persistence.Repositories.Base;

namespace Better.Persistence.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(BetterContext context) : base(context)
        {
        }
    }
}
