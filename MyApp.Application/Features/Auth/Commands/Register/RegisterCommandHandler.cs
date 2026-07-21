using MediatR;
using Microsoft.AspNetCore.Identity;
using MyApp.Application.DTOs.Auth;
using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterCommandHandler(UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponseDto> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _userManager.FindByEmailAsync(request.Email);

            if (exists != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(x => x.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var roles = await _userManager.GetRolesAsync(user);

            Console.WriteLine(string.Join(",", roles));

            return new AuthResponseDto
            {
                Success = true,
                Message = "Registered Successfully"
            };
        }
    }
}
