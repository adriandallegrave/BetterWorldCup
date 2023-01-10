using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Repositories;
using Better.Persistence.Repositories;

namespace Better.Persistence
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BetterContext _context;

        private IAccountRepository _accountRepository;
        private IBetRepository _betRepository;
        private IGameRepository _gameRepository;
        private ITeamRepository _teamRepository;
        private ITransactionRepository _transactionRepository;

        public RepositoryWrapper(BetterContext context)
        {
            _context = context;
        }

        public IAccountRepository Account
        {
            get
            {
                _accountRepository ??= new AccountRepository(_context);
                return _accountRepository;
            }
        }

        public IBetRepository Bet
        {
            get
            {
                _betRepository ??= new BetRepository(_context);
                return _betRepository;
            }
        }

        public IGameRepository Game
        {
            get
            {
                _gameRepository ??= new GameRepository(_context);
                return _gameRepository;
            }
        }

        public ITeamRepository Team
        {
            get
            {
                _teamRepository ??= new TeamRepository(_context);
                return _teamRepository;
            }
        }

        public ITransactionRepository Transaction
        {
            get
            {
                _transactionRepository ??= new TransactionRepository(_context);
                return _transactionRepository;
            }
        }

        public async Task<bool> Commit()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
