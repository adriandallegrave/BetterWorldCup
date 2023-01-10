using Better.Domain.Models;
using System.Linq.Expressions;

namespace Better.Domain.Interfaces.Services
{
    public interface IBetService
    {
        public Task<Bet> Delete(Bet bet);

        public Task<List<Bet>> GetAll();

        public Task<Bet> GetById(Guid id);

        public Task<List<Bet>> GetByProperty(Expression<Func<Bet, bool>> expression);

        public Task<Bet> Post(Bet bet);

        public Task<Bet> Update(Bet bet);
    }
}
