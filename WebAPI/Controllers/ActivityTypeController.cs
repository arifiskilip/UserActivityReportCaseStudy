using Application.Features.ActivityType.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ActivityTypeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllActivityTypeQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
