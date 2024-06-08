using Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using SuaMe88.Data;
using System.Security.Claims;
using static SuaMe88.Data.Login;

namespace SuaMe88.Services.UserServices
{
    public interface IUserService
    {
        Task<Response<LoginResult>> Login(Data.LoginRequest request);
        LoginResult GenerateToken(Customer user, List<Claim> claims, DateTime now);
    }
}

