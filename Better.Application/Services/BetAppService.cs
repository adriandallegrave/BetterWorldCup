using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Services
{
    public class BetAppService : IBetAppService
    {
        private readonly IBetService _betService;
        private readonly IAccountAppService _accountAppService;

        public BetAppService(IBetService betService, IAccountAppService accountAppService)
        {
            _betService = betService;
            _accountAppService = accountAppService;
        }

        public async Task<Bet> Delete(Guid id)
        {
            var bet = await GetById(id);

            if (bet is null)
            {
                return bet;
            }

            return await _betService.Delete(bet);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetById(id) != null;
        }

        public async Task<List<Bet>> GetAll()
        {
            return await _betService.GetAll();
        }

        public async Task<Bet> GetById(Guid id)
        {
            return await _betService.GetById(id);
        }

        public async Task<List<Bet>> GetByProperty(Expression<Func<Bet, bool>> expression)
        {
            return await _betService.GetByProperty(expression);
        }

        public async Task<Bet> Post(Guid accountId, Guid gameId, GameScoreDto dto)
        {
            var bet = new Bet()
            {
                Id = Guid.NewGuid(),
                HomeGuess = dto.HomeScore,
                AwayGuess = dto.AwayScore,
                GameId = gameId,
                AccountId = accountId
            };

            var betsSameMatchSameUser = await GetByProperty(x => x.AccountId == bet.AccountId && x.GameId == bet.GameId);

            if (betsSameMatchSameUser.Any())
            {
                return null;
            }

            return await _betService.Post(bet);
        }

        public async Task<Bet> UpdateScore(GameScoreDto dto, Guid id)
        {
            var bet = await GetById(id);

            if (bet is null)
            {
                return bet;
            }

            bet.HomeGuess = dto.HomeScore;
            bet.AwayGuess = dto.AwayScore;

            return await _betService.Update(bet);
        }

        public async Task<bool> DeleteAllByUser(Guid accountId)
        {
            var bets = await GetByProperty(x => x.AccountId == accountId);

            if (bets is null || !bets.Any())
            {
                return false;
            }

            foreach (var bet in bets)
            {
                await Delete(bet.Id);
            };

            await _accountAppService.UpdateHaveBets(false, accountId);

            return true;
        }
    }
}
