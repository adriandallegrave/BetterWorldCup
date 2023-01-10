using Better.Application.Dtos;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Interfaces
{
    public interface IBetAppService
    {
        public Task<Bet> Delete(Guid id);

        public Task<bool> DeleteAllByUser(Guid accountId);

        public Task<bool> Exists(Guid id);

        public Task<List<Bet>> GetAll();

        public Task<Bet> GetById(Guid guid);

        public Task<List<Bet>> GetByProperty(Expression<Func<Bet, bool>> expression);

        public Task<Bet> Post(Guid accountId, Guid gameId, GameScoreDto dto);

        public Task<Bet> UpdateScore(GameScoreDto dto, Guid id);
    }
}
