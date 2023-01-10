using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Models;
using Better.Tools.Validations;
using System.Linq.Expressions;

namespace Better.Domain.Services
{
    public class BetService : IBetService
    {
        private readonly IRepositoryWrapper _repository;

        public BetService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Bet> Delete(Bet bet)
        {
            if (bet is null)
            {
                return bet;
            }

            if (await GetById(bet.Id) == default)
            {
                return null;
            }

            _repository.Bet.Delete(bet);
            var commitSuccessful = await _repository.Commit();

            if (!commitSuccessful)
            {
                return null;
            }

            return bet;
        }

        public async Task<List<Bet>> GetAll()
        {
            return await _repository.Bet.Get();
        }

        public async Task<Bet> GetById(Guid id)
        {
            return await _repository.Bet.GetFirstByProperty(bet => bet.Id == id);
        }

        public async Task<List<Bet>> GetByProperty(Expression<Func<Bet, bool>> expression)
        {
            return await _repository.Bet.GetAllByProperty(expression);
        }

        public async Task<Bet> Post(Bet bet)
        {
            if (bet is null)
            {
                return bet;
            }

            if (!Helpers.GuidIsValid(bet.Id))
            {
                return null;
            }

            await _repository.Bet.Post(bet);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return bet;
        }

        public async Task<Bet> Update(Bet bet)
        {
            if (bet is null)
            {
                return bet;
            }

            var old = await GetById(bet.Id);

            if (old is null)
            {
                return old;
            }

            old.HomeGuess = bet.HomeGuess;
            old.AwayGuess = bet.AwayGuess;

            _repository.Bet.Update(old);
            var commitSucessful = await _repository.Commit();

            if (!commitSucessful)
            {
                return null;
            }

            return bet;
        }
    }
}
