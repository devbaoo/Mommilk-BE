using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Auth.Login;

namespace Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        LoginResult GenerateToken(User user, List<Claim> claims, DateTime now);

    }
}
