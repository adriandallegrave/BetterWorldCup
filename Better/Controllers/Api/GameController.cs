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
    public class GameController : BaseController
    {
        private readonly IGameAppService _gameAppService;
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger, IGameAppService gameAppService)
        {
            _gameAppService = gameAppService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var result = await _gameAppService.GetAll();

            return ResponseGetAll(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Game>> GetById(Guid guid)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _gameAppService.GetById(guid);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpGet("GetByTeamNames")]
        public async Task<ActionResult<Game>> GetByTeamNames(string homeTeam, string awayTeam)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _gameAppService.GetByTeamNames(homeTeam, awayTeam);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Game>> Post(GameDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _gameAppService.Post(dto);

                return ResponsePost(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Score")]
        public async Task<ActionResult<Game>> UpdateScore(Guid id, GameScoreDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _gameAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _gameAppService.UpdateScore(dto, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/StartTimeAndTeams")]
        public async Task<ActionResult<Game>> UpdateStartTimeAndTeams(Guid id, GameDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _gameAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _gameAppService.UpdateStartTimeAndTeams(dto, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Game>> Delete(Guid id)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _gameAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _gameAppService.Delete(id);

                return ResponseDelete(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
