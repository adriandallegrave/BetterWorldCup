using Better.Application.Dtos;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Interfaces
{
    public interface IGameAppService
    {
        public Task<Game> Delete(Guid id);

        public Task<bool> Exists(Guid id);

        public Task<List<Game>> GetAll();

        public Task<Game> GetById(Guid guid);

        public Task<List<Game>> GetByProperty(Expression<Func<Game, bool>> expression);

        public Task<Game> GetByTeamNames(string homeTeam, string awayTeam);

        public Task<Game> Post(GameDto dto);

        public Task<Game> UpdateScore(GameScoreDto dto, Guid id);

        public Task<Game> UpdateStartTimeAndTeams(GameDto dto, Guid id);
    }
}
