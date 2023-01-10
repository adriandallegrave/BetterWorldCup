using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Controllers.Base;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ManagementController : BaseController
    {
        private readonly IWebAppService _webAppService;
        private readonly IGameAppService _gameAppService;
        private readonly IBetAppService _betAppService;
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(ILogger<ManagementController> logger,
                                    IWebAppService webAppService,
                                    IGameAppService gameWebService,
                                    IBetAppService betAppService)
        {
            _webAppService = webAppService;
            _gameAppService = gameWebService;
            _logger = logger;
            _betAppService = betAppService;
        }

        [HttpPost("MatchResult")]
        public async Task<ActionResult> SetResult(SetResultDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _gameAppService.Exists(dto.GameId))
                {
                    return ResponseIdNotFound();
                }

                var result = await _webAppService.SetResult(dto);

                return ResponseBool(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("RecalculateBalance")]
        public async Task<ActionResult> RecalculateTransactions()
        {
            try
            {
                _logger.LogWarning("{txt}", Helpers.LogThis(""));

                var result = await _webAppService.RecalculateTransactions();

                return ResponseBool(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("DeleteAllBetsByUser")]
        public async Task<ActionResult> DeleteAllByUser(Guid accountId)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _webAppService.ClearBetsAndDeleteAccount(accountId);

                return ResponseBool(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
