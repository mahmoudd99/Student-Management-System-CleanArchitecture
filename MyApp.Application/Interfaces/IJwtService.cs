using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface IJwtService
    {
       
        Task<string> GenerateToken(User user, IList<string> roles);

        string GenerateRefreshToken();
    }
    }
