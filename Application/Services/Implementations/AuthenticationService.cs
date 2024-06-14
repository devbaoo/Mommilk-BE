using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Auth.Login;

namespace Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SuaMe88Context _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(SuaMe88Context context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public LoginResult GenerateToken(User user, List<Claim> claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(
                claims?.FirstOrDefault(x => x.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value
            );

            var jwtToken = new JwtSecurityToken(
                _configuration["JWT_Configuration:Issuer"],
                shouldAddAudienceClaim ? _configuration["JWT_Configuration:Audience"] : String.Empty,
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
