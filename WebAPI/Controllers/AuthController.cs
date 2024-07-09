using Application.Features.Auth.Commands.EmailVerified;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Queries.IsEmailVerified;
using Application.Features.Auth.Queries.SendVerificationCode;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AuthController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> IsEmailVerified([FromQuery] IsEmailVerifiedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> VerificationCode([FromQuery] SendVerificationCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> EmailVerified([FromBody] EmailVerifiedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
