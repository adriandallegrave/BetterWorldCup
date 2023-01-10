using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Application.Services
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameService _gameService;

        public GameAppService(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<Game> Delete(Guid id)
        {
            var game = await GetById(id);

            if (game is null)
            {
                return game;
            }

            return await _gameService.Delete(game);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await GetById(id) != null;
        }

        public async Task<List<Game>> GetAll()
        {
            return await _gameService.GetAll();
        }

        public async Task<Game> GetById(Guid id)
        {
            return await _gameService.GetById(id);
        }

        public async Task<List<Game>> GetByProperty(Expression<Func<Game, bool>> expression)
        {
            return await _gameService.GetByProperty(expression);
        }

        public async Task<Game> GetByTeamNames(string homeTeamName, string awayTeamName)
        {
            var game = await GetByProperty(x => x.HomeTeam.Name == homeTeamName &&
                                                x.AwayTeam.Name == awayTeamName);

            if (!game.Any())
            {
                return null;
            }

            return game.First();
        }

        public async Task<Game> Post(GameDto dto)
        {
            var game = new Game()
            {
                Id = Guid.NewGuid(),
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                StartTime = dto.StartTime
            };

            return await _gameService.Post(game);
        }

        public async Task<Game> UpdateScore(GameScoreDto dto, Guid id)
        {
            var game = await GetById(id);

            if (game is null)
            {
                return game;
            }

            game.HomeScored = dto.HomeScore;
            game.AwayScored = dto.AwayScore;

            return await _gameService.Update(game);
        }

        public async Task<Game> UpdateStartTimeAndTeams(GameDto dto, Guid id)
        {
            var game = await GetById(id);

            if (game is null)
            {
                return game;
            }

            game.StartTime = dto.StartTime;
            game.HomeTeamId = dto.HomeTeamId;
            game.AwayTeamId = dto.AwayTeamId;

            return await _gameService.Update(game);
        }
    }
}
