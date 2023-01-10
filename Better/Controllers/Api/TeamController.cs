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
    public class TeamController : BaseController
    {
        private readonly ITeamAppService _teamAppService;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ILogger<TeamController> logger, ITeamAppService teamAppService)
        {
            _teamAppService = teamAppService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Team>>> GetAll()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var result = await _teamAppService.GetAll();

            return ResponseGetAll(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Team>> GetById(Guid guid)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _teamAppService.GetById(guid);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Team>> Post(TeamDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _teamAppService.Post(dto);

                return ResponsePost(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Name")]
        public async Task<ActionResult<Team>> UpdateName(Guid id, string name)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _teamAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _teamAppService.UpdateName(name, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/ShortName")]
        public async Task<ActionResult<Team>> UpdateShortName(Guid id, string shortName)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _teamAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _teamAppService.UpdateShortName(shortName, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/SourceName")]
        public async Task<ActionResult<Team>> UpdateSourceName(Guid id, string sourceName)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _teamAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _teamAppService.UpdateSourceName(sourceName, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Team>> Delete(Guid id)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _teamAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _teamAppService.Delete(id);

                return ResponseDelete(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
