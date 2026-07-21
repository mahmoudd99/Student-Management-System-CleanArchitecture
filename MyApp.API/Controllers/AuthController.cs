using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Auth;
using MyApp.Application.DTOs.Auth.RefershToken;
using MyApp.Application.Features.Auth.Commands.Login;
using MyApp.Application.Features.Auth.Commands.Register;
namespace MyApp.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var command = new RegisterCommand(
                dto.UserName,
                dto.Email,
                dto.Password);

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var command = new LoginCommand(
                dto.Email,
                dto.Password);

            var result = await _mediator.Send(command);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
    RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }

}