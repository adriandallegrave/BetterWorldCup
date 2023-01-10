using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Application.Objects;
using Better.Domain.Models;
using Better.Tools.Configuration;
using Microsoft.AspNetCore.Http;

namespace Better.Application.Services
{
    public class WebAppService : IWebAppService
    {
        private readonly IAccountAppService _accountAppService;
        private readonly IBetAppService _betAppService;
        private readonly IGameAppService _gameAppService;
        private readonly ITeamAppService _teamAppService;
        private readonly ITransactionAppService _transactionAppService;

        public WebAppService(IBetAppService betAppService,
                             IGameAppService gameAppService,
                             ITeamAppService teamAppService,
                             IAccountAppService accountAppService,
                             ITransactionAppService transactionAppService)
        {
            _betAppService = betAppService;
            _gameAppService = gameAppService;
            _teamAppService = teamAppService;
            _accountAppService = accountAppService;
            _transactionAppService = transactionAppService;
        }

        public async Task<bool> ClearBetsAndDeleteAccount(Guid accountId)
        {
            var bets = await _betAppService.DeleteAllByUser(accountId);
            var account = await _accountAppService.Delete(accountId);

            return bets && account != null;
        }

        public async Task<List<BetSelection>> GenerateBets()
        {
            var betSelections = new List<BetSelection>();
            var games = await _gameAppService.GetAll();

            foreach (var game in games)
            {
                betSelections.Add(new BetSelection()
                {
                    MatchId = game.Id,
                    HomeScored = 0,
                    AwayScored = 0
                });
            }

            return betSelections;
        }

        public async Task<List<BetsTableItem>> GenerateBetsTable()
        {
            var tableItems = new List<BetsTableItem>();
            var games = await _gameAppService.GetAll();
            games = games.OrderBy(game => game.Id).ToList();

            foreach (var game in games)
            {
                var homeTeam = await _teamAppService.GetById(game.HomeTeamId);
                var awayTeam = await _teamAppService.GetById(game.AwayTeamId);

                tableItems.Add(new BetsTableItem()
                {
                    MatchId = game.Id,
                    Date = game.StartTime,
                    HomeTeam = homeTeam.Name,
                    AwayTeam = awayTeam.Name
                });
            }

            return tableItems;
        }

        public async Task<List<HomeTableItem>> GenerateHomeTable()
        {
            var tableItems = new List<HomeTableItem>();
            var games = await _gameAppService.GetAll();
            var bets = await _betAppService.GetAll();
            games = games.OrderBy(game => game.StartTime).ToList();

            foreach (var game in games)
            {
                var matchBets = bets.Where(bet => bet.GameId == game.Id).OrderByDescending(bet => bet.AccountId).ToList();
                var matchBetsResults = new List<string>();

                foreach (var bet in matchBets)
                {
                    matchBetsResults.Add($"{bet.HomeGuess}x{bet.AwayGuess}");
                };

                var officialResult = " x ";
                var amountWon = 0.00F;
                var amountLost = 0.00F;

                if (game.AwayScored != -1)
                {
                    var pot = Constants.MatchBetAmount * matchBets.Count;
                    officialResult = $"{game.HomeScored}x{game.AwayScored}";
                    var hits = matchBetsResults.Where(bet => bet == officialResult).Count();
                    amountWon = hits == 0 ? amountWon : ((pot - (hits * Constants.MatchBetAmount)) / hits);
                    amountLost = hits == 0 ? amountLost : (Constants.MatchBetAmount * -1);
                }

                var homeTeam = await _teamAppService.GetById(game.HomeTeamId);
                var awayTeam = await _teamAppService.GetById(game.AwayTeamId);

                var newItem = new HomeTableItem()
                {
                    Date = game.StartTime,
                    HomeTeam = homeTeam.Name,
                    Result = officialResult,
                    AwayTeam = awayTeam.Name,
                    AmountLost = amountLost,
                    AmountWon = amountWon,
                    OrderedBets = Constants.ShowResults ? matchBetsResults : new List<string>()
                };

                tableItems.Add(newItem);
            }

            return tableItems;
        }

        public async Task<List<ResultsTableItem>> GenerateResults()
        {
            var resultTable = new List<ResultsTableItem>();
            var participants = await _accountAppService.GetByProperty(x => x.HaveBets == true);

            foreach (var user in participants)
            {
                resultTable.Add(new ResultsTableItem()
                {
                    Name = user.Name,
                    Balance = user.Balance
                });
            }

            var sortedTable = resultTable.OrderByDescending(x => x.Balance).ToList();

            return sortedTable;
        }

        public async Task<List<string>> GenerateUsers()
        {
            var accounts = await _accountAppService.GetAll();
            var users = accounts.OrderByDescending(account => account.Id).Select(x => x.Name).ToList();

            return users;
        }

        public async Task<bool> RecalculateTransactions()
        {
            var accounts = await _accountAppService.GetAll();

            foreach (var account in accounts)
            {
                await _accountAppService.UpdateBalance(0.00F, account.Id);
            }

            var transactions = await _transactionAppService.GetAll();
            foreach (var transaction in transactions)
            {
                await _accountAppService.IncrementBalance(transaction.Amount, transaction.AccountId);
            }

            return true;
        }

        public async Task<(bool, string)> SaveBets(IFormCollection form)
        {
            var userMail = form["UserMail"];
            var userName = form["UserName"];
            var games = await _gameAppService.GetAll();

            var account = new AccountDto()
            {
                Name = userName,
                Email = userMail
            };

            var accountCreated = await _accountAppService.Post(account);

            if (accountCreated is null)
            {
                return (false, "Conta não foi criada");
            }

            var bet = new Bet();
            var gameScore = new GameScoreDto();

            foreach (var game in games)
            {
                gameScore.HomeScore = int.Parse(form[$"({game.Id})A"].ToString());
                gameScore.AwayScore = int.Parse(form[$"({game.Id})B"].ToString());

                bet = await _betAppService.Post(accountCreated.Id, game.Id, gameScore);

                if (bet is null)
                {
                    var response = await ClearBetsAndDeleteAccount(accountCreated.Id);
                    return (response, "Aposta não foi salva");
                }
            };

            var isValidToRules = await IsValid(accountCreated.Id);

            if (!isValidToRules)
            {
                var response = await ClearBetsAndDeleteAccount(accountCreated.Id);
                return (response, "Aposta não está de acordo com as regras");
            }

            await _accountAppService.UpdateHaveBets(true, accountCreated.Id);

            return (true, "");
        }

        public async Task<bool> SetResult(SetResultDto dto)
        {
            var gameExists = await _gameAppService.Exists(dto.GameId);

            if (!gameExists)
            {
                return false;
            }

            (var hits, var misses) = await GetHitsAndMisses(dto.GameId, dto.HomeScored, dto.AwayScored);
            var pot = Constants.MatchBetAmount * misses.Count;
            var amountWon = pot / hits.Count;
            var amountLost = -1 * Constants.MatchBetAmount;

            var gameScore = new GameScoreDto()
            {
                HomeScore = dto.HomeScored,
                AwayScore = dto.AwayScored
            };
            await _gameAppService.UpdateScore(gameScore, dto.GameId);

            foreach (var winner in hits)
            {
                await _transactionAppService.Post(winner.AccountId, amountWon);
                await _accountAppService.IncrementBalance(amountWon, winner.AccountId);
            }

            foreach (var loser in misses)
            {
                if (hits.Count > 0)
                {
                    await _transactionAppService.Post(loser.AccountId, amountLost);
                    await _accountAppService.IncrementBalance(amountLost, loser.AccountId);
                }
            }

            return true;
        }

        public async Task<bool> UserHaveBets(string email)
        {
            var account = await _accountAppService.GetByProperty(x => x.Email == email);

            if (account == null || !account.Any())
            {
                return false;
            }

            return account.First().HaveBets;
        }

        private async Task<(List<Bet>, List<Bet>)> GetHitsAndMisses(Guid gameId, int home, int away)
        {
            var bets = await _betAppService.GetByProperty(x => x.GameId == gameId);
            var hits = new List<Bet>();
            var misses = new List<Bet>();

            foreach (var bet in bets)
            {
                if (bet.HomeGuess == home && bet.AwayGuess == away)
                {
                    hits.Add(bet);
                }
                else
                {
                    misses.Add(bet);
                }
            }

            return (hits, misses);
        }

        private async Task<bool> IsValid(Guid accountId)
        {
            var bets = await _betAppService.GetByProperty(x => x.AccountId == accountId);
            var guesses = bets.Select(x => $"{(x.HomeGuess > x.AwayGuess ? x.HomeGuess : x.AwayGuess)}x{(x.HomeGuess > x.AwayGuess ? x.AwayGuess : x.HomeGuess)}")
                              .OrderByDescending(x => x)
                              .ToList();

            if (guesses.Count != Constants.NumberOfMatches)
            {
                return false;
            }

            var distinctGuesses = guesses.Distinct();

            foreach (var result in distinctGuesses)
            {
                if (guesses.Count(item => item == result) > Constants.MaxRepeatResult)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
