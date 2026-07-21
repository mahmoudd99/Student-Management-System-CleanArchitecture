using MediatR;
using Microsoft.AspNetCore.Identity;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTOs.Auth.RefershToken
{
    public class RefreshTokenCommandHandler
       : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(
            UserManager<User> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = _userManager.Users.FirstOrDefault(x =>
                x.RefreshToken == request.RefreshToken);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid Refresh Token"
                };
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Refresh Token Expired"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = await _jwtService.GenerateToken(user, roles);

            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Token Refreshed Successfully",
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
