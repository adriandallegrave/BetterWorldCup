using Better.Application.Dtos;
using Better.Application.Objects;
using Microsoft.AspNetCore.Http;

namespace Better.Application.Interfaces
{
    public interface IWebAppService
    {
        public Task<bool> ClearBetsAndDeleteAccount(Guid accountId);

        public Task<List<BetSelection>> GenerateBets();

        public Task<List<BetsTableItem>> GenerateBetsTable();

        public Task<List<HomeTableItem>> GenerateHomeTable();

        public Task<List<ResultsTableItem>> GenerateResults();

        public Task<List<string>> GenerateUsers();

        public Task<bool> RecalculateTransactions();

        public Task<(bool, string)> SaveBets(IFormCollection form);

        public Task<bool> SetResult(SetResultDto dto);

        public Task<bool> UserHaveBets(string email);
    }
}
