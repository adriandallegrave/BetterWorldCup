using Better.Application.Dtos;
using Better.Application.Interfaces;
using Better.Controllers.Base;
using Better.Domain.Models;
using Better.Tools.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Better.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BetController : BaseController
    {
        private readonly IBetAppService _betAppService;
        private readonly ILogger<BetController> _logger;

        public BetController(ILogger<BetController> logger, IBetAppService betAppService)
        {
            _betAppService = betAppService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Bet>>> GetAll()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var result = await _betAppService.GetAll();

            return ResponseGetAll(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Bet>> GetById(Guid guid)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _betAppService.GetById(guid);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Bet>> Post(Guid accountId, Guid gameId, GameScoreDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _betAppService.Post(accountId, gameId, dto);

                return ResponsePost(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Score")]
        public async Task<ActionResult<Bet>> UpdateScore(Guid id, GameScoreDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _betAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _betAppService.UpdateScore(dto, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Bet>> Delete(Guid id)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _betAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _betAppService.Delete(id);

                return ResponseDelete(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
