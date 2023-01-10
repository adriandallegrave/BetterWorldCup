using Better.Domain.Interfaces.Repositories;

namespace Better.Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAccountRepository Account { get; }
        IBetRepository Bet { get; }
        IGameRepository Game { get; }
        ITeamRepository Team { get; }
        ITransactionRepository Transaction { get; }

        Task<bool> Commit();

        void Dispose();
    }
}
