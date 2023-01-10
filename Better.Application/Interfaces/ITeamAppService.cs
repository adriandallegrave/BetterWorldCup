using Better.Application.Dtos;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Interfaces
{
    public interface ITeamAppService
    {
        public Task<Team> Delete(Guid id);

        public Task<bool> Exists(Guid id);

        public Task<List<Team>> GetAll();

        public Task<Team> GetById(Guid guid);

        public Task<List<Team>> GetByProperty(Expression<Func<Team, bool>> expression);

        public Task<Team> Post(TeamDto dto);

        public Task<Team> UpdateName(string name, Guid id);

        public Task<Team> UpdateShortName(string shortName, Guid id);

        public Task<Team> UpdateSourceName(string sourceName, Guid id);
    }
}
