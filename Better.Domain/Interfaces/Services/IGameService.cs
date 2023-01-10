using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Services
{
    public interface IGameService
    {
        public Task<Game> Delete(Game game);

        public Task<List<Game>> GetAll();

        public Task<Game> GetById(Guid id);

        public Task<List<Game>> GetByProperty(Expression<Func<Game, bool>> expression);

        public Task<Game> Post(Game game);

        public Task<Game> Update(Game game);
    }
}
