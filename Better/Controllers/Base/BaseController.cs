using Microsoft.AspNetCore.Mvc;

namespace Better.Controllers.Base
{
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseController : Controller
    {
        protected ActionResult<IEnumerable<T>> ResponseGetAll<T>(IEnumerable<T> result)
        {
            if (result == null || !result.Any())
            {
                return NotFound("Item not found. No results to show");
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (result is null)
            {
                return NotFound("Item not found. No results to show");
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponsePost<T>(T result)
        {
            if (result is null)
            {
                return BadRequest("Not able to post");
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponseUpdate<T>(T result)
        {
            if (result is null)
            {
                return BadRequest("Not able to update");
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponseDelete<T>(T result)
        {
            if (result is null)
            {
                return BadRequest("Not able to delete");
            }

            return Ok(result);
        }

        protected ActionResult ResponseCatch()
        {
            return BadRequest("Try failed");
        }

        protected ActionResult ResponseIdNotFound()
        {
            return NotFound("Id not found");
        }

        protected ActionResult ResponseBool(bool result)
        {
            if (result)
            {
                return Ok("Everything worked as expected");
            }

            return BadRequest("Failure");
        }
    }
}
