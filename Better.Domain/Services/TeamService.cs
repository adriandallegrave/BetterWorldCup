using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Better.Tools.Validations;
using System.Linq.Expressions;

namespace Better.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepositoryWrapper _repository;

        public TeamService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Team> Delete(Team team)
        {
            if (team is null)
            {
                return team;
            }

            if (await GetById(team.Id) == default)
            {
                return null;
            }

            _repository.Team.Delete(team);
            var commitSuccessful = await _repository.Commit();

            if (!commitSuccessful)
            {
                return null;
            }

            return team;
        }

        public async Task<List<Team>> GetAll()
        {
            return await _repository.Team.Get();
        }

        public async Task<Team> GetById(Guid id)
        {
            return await _repository.Team.GetFirstByProperty(team => team.Id == id);
        }

        public async Task<List<Team>> GetByProperty(Expression<Func<Team, bool>> expression)
        {
            return await _repository.Team.GetAllByProperty(expression);
        }

        public async Task<Team> Post(Team team)
        {
            if (team is null)
            {
                return team;
            }

            if (!Helpers.GuidIsValid(team.Id))
            {
                return null;
            }

            await _repository.Team.Post(team);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return team;
        }

        public async Task<Team> Update(Team team)
        {
            if (team is null)
            {
                return team;
            }

            var old = await GetById(team.Id);

            if (old is null)
            {
                return old;
            }

            old.Name = team.Name;
            old.ShortName = team.ShortName;
            old.SourceName = team.SourceName;

            _repository.Team.Update(old);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return team;
        }
    }
}
