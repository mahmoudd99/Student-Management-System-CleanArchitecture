using MediatR;
using MyApp.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        string UserName,
        string Email,
        string Password
    ) : IRequest<AuthResponseDto>;
}
