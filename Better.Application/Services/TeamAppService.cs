using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Services
{
    public class TeamAppService : ITeamAppService
    {
        private readonly ITeamService _teamService;

        public TeamAppService(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<Team> Delete(Guid id)
        {
            var team = await GetById(id);

            if (team is null)
            {
                return team;
            }

            return await _teamService.Delete(team);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetById(id) != null;
        }

        public async Task<List<Team>> GetAll()
        {
            return await _teamService.GetAll();
        }

        public async Task<Team> GetById(Guid id)
        {
            return await _teamService.GetById(id);
        }

        public async Task<List<Team>> GetByProperty(Expression<Func<Team, bool>> expression)
        {
            return await _teamService.GetByProperty(expression);
        }

        public async Task<Team> Post(TeamDto dto)
        {
            var team = new Team()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ShortName = dto.ShortName,
                SourceName = dto.SourceName
            };

            var teamsWithSameName = await GetByProperty(x => x.Name == team.Name);

            if (teamsWithSameName.Any())
            {
                return null;
            }

            return await _teamService.Post(team);
        }

        public async Task<Team> UpdateName(string name, Guid id)
        {
            var team = await GetById(id);

            if (team is null)
            {
                return team;
            }

            team.Name = name;

            return await _teamService.Update(team);
        }

        public async Task<Team> UpdateShortName(string shortName, Guid id)
        {
            var team = await GetById(id);

            if (team is null)
            {
                return team;
            }

            team.ShortName = shortName;

            return await _teamService.Update(team);
        }

        public async Task<Team> UpdateSourceName(string sourceName, Guid id)
        {
            var team = await GetById(id);

            if (team is null)
            {
                return team;
            }

            team.SourceName = sourceName;

            return await _teamService.Update(team);
        }
    }
}
