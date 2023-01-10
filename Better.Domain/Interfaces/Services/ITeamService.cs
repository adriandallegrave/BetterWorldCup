using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<Team> Delete(Team team);

        public Task<List<Team>> GetAll();

        public Task<Team> GetById(Guid id);

        public Task<List<Team>> GetByProperty(Expression<Func<Team, bool>> expression);

        public Task<Team> Post(Team team);

        public Task<Team> Update(Team team);
    }
}
