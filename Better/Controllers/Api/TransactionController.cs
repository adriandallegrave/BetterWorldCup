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
    public class TransactionController : BaseController
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            var result = await _transactionAppService.GetAll();

            return ResponseGetAll(result);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Transaction>> GetById(Guid guid)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _transactionAppService.GetById(guid);

                return ResponseGet(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPost("Post")]
        public async Task<ActionResult<Transaction>> Post(Guid accountId, float amount)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                var result = await _transactionAppService.Post(accountId, amount);

                return ResponsePost(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Amount")]
        public async Task<ActionResult<Transaction>> UpdateAmount(Guid id, float amount)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _transactionAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _transactionAppService.UpdateAmount(amount, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpPut("Update/Account")]
        public async Task<ActionResult<Transaction>> UpdateUserId(Guid id, Guid accountId)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _transactionAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _transactionAppService.UpdateAccountId(accountId, id);

                return ResponseUpdate(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Transaction>> Delete(Guid id)
        {
            _logger.LogWarning("{txt}", Helpers.LogThis(""));

            try
            {
                if (!await _transactionAppService.Exists(id))
                {
                    return ResponseIdNotFound();
                }

                var result = await _transactionAppService.Delete(id);

                return ResponseDelete(result);
            }
            catch
            {
                return ResponseCatch();
            }
        }
    }
}
