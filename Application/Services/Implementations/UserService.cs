using Microsoft.AspNetCore.Identity.Data;
using static SuaMe88.Data.Login;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using SuaMe88.Data;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using SuaMe88.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Data.Repositories.Interfaces;
using Application.Services;

namespace SuaMe88.Services.UserServices
{
    public class UserService : BaseService, IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public UserService(IAccountRepository AccountRepository, IAuthenticationService jwtService, IMapper mapper, IConfiguration configuration)
            : base(AccountRepository, mapper) // Inject base service dependencies
        {
            _accountRepository = AccountRepository;
            _configuration = configuration;
        }

        public async Task<Response<LoginResult>> Login(Data.LoginRequest request)
        {
            var user = await _accountRepository.GetCustomerByEmailAndPasswordAsync(request.Username, request.Password);

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            var claims = new List<Claim>
        {
            new Claim("UserID", user.Id.ToString()),
            new Claim("Fullname", user.Name),
        };

            var result = GenerateToken(user, claims, DateTime.Now);
            result.UserResult = new UserResult
            {
                UserID = user.Id,
                Username = user.Email,
                Fullname = user.Name,
                Phone = "0" + user.Phone.ToString(),
            };

            return new Response<LoginResult>
            {
                Success = true,
                Message = "Login successfully!",
                Data = result,
                Status = (int)HttpStatusCode.OK
            };
        }

        public LoginResult GenerateToken(Customer user, List<Claim> claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(
                claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value
            );

            var jwtToken = new JwtSecurityToken(
                _configuration["JWT_Configuration:Issuer"],
                shouldAddAudienceClaim ? _configuration["JWT_Configuration:Audience"] : string.Empty,
                claims,
                expires: now.AddMinutes(Convert.ToDouble(_configuration["JWT_Configuration:TokenExpirationMinutes"])),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_Configuration:SecretKey"]!)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            );

            var token = new TokenResult
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpirationMinutes = Convert.ToInt32(_configuration["JWT_Configuration:TokenExpirationMinutes"])
            };

            return new LoginResult
            {
                Token = token,
            };
        }
    }
}
