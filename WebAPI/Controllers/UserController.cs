using Application.Features;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdUserQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] AddImageUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
