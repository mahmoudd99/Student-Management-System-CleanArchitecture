using MediatR;
using Microsoft.AspNetCore.Identity;
using MyApp.Application.DTOs.Auth;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Auth.Commands.Login
{


    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(
            UserManager<User> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                };
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!validPassword)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);


            var token = await _jwtService.GenerateToken(user, roles);
            

            var accessToken = await _jwtService.GenerateToken(user, roles);

            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);
            return new AuthResponseDto
            {
                Success = true,
                Message = "Login Successfully",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }


}
