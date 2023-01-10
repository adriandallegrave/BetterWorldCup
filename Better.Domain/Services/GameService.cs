using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Better.Tools.Validations;
using System.Linq.Expressions;

namespace Better.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IRepositoryWrapper _repository;

        public GameService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Game> Delete(Game game)
        {
            if (game is null)
            {
                return game;
            }

            if (await GetById(game.Id) == default)
            {
                return null;
            }

            _repository.Game.Delete(game);
            var commitSuccessful = await _repository.Commit();

            if (!commitSuccessful)
            {
                return null;
            }

            return game;
        }

        public async Task<List<Game>> GetAll()
        {
            return await _repository.Game.Get();
        }

        public async Task<Game> GetById(Guid id)
        {
            return await _repository.Game.GetFirstByProperty(game => game.Id == id);
        }

        public async Task<List<Game>> GetByProperty(Expression<Func<Game, bool>> expression)
        {
            return await _repository.Game.GetAllByProperty(expression);
        }

        public async Task<Game> Post(Game game)
        {
            if (game is null)
            {
                return game;
            }

            if (!Helpers.GuidIsValid(game.Id))
            {
                return null;
            }

            await _repository.Game.Post(game);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return game;
        }

        public async Task<Game> Update(Game game)
        {
            if (game is null)
            {
                return game;
            }

            var old = await GetById(game.Id);

            if (old is null)
            {
                return old;
            }

            old.StartTime = game.StartTime;
            old.HomeTeamId = game.HomeTeamId;
            old.AwayTeamId = game.AwayTeamId;
            old.HomeScored = game.HomeScored;
            old.AwayScored = game.AwayScored;

            _repository.Game.Update(old);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return game;
        }
    }
}
