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
    public class AccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var result = await _accountAppService.GetAll();

            return ResponseGetAll(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Account>> GetById(Guid guid)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _accountAppService.GetById(guid);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Account>> Post(AccountDto dto)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _accountAppService.Post(dto);

                return ResponsePost(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Balance")]
        public async Task<ActionResult<Account>> UpdateBalance(Guid id, float balance)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _accountAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _accountAppService.UpdateBalance(balance, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Balance/Increment")]
        public async Task<ActionResult<Account>> UpdateIncrementBalance(Guid id, float amount)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _accountAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _accountAppService.IncrementBalance(amount, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/HaveBets")]
        public async Task<ActionResult<Account>> UpdateEmailConfirmed(Guid id, bool hasBets)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _accountAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _accountAppService.UpdateHaveBets(hasBets, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Name")]
        public async Task<ActionResult<Account>> UpdateName(Guid id, string name)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _accountAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _accountAppService.UpdateName(name, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Account>> Delete(Guid id)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _accountAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _accountAppService.Delete(id);

                return ResponseDelete(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
