using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Mommilk88.Data;
using static Domain.Models.Auth.Login;
using Domain.Models.Auth;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Mommilk88.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly SuaMe88Context _context;
        private readonly IAuthenticationService _jwtService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(SuaMe88Context context, IAuthenticationService jwtService, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _jwtService = jwtService;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<Response<LoginResult>> Login(LoginRequest request)
        {
            try
            {
                await Task.CompletedTask;
                var user = await _context.Customers.FirstOrDefaultAsync(x => x.Email == request.Username && x.Password == request.Password);

                if (user == null)
                {
                    return new Response<LoginResult>
                    {
                        Success = false,
                        Message = "User does not exists.",
                        Status = (int)HttpStatusCode.OK
                    };
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
            catch (Exception)
            {
                throw;
            }
        }

        public LoginResult GenerateToken(Customer user, List<Claim> claims, DateTime now)
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
        /*public async Task<ApiResponse<CreateUserRequest>> Register(CreateUserRequest newUser)
        {
            try
            {
                if (newUser.Username == null || newUser.Password == null)
                {
                    return new ApiResponse<CreateUserRequest>
                    {
                        Success = false,
                        Message = "Invalid username or password!",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                if (await IsExist(newUser.Username))
                {
                    return new ApiResponse<CreateUserRequest>
                    {
                        Success = false,
                        Message = "This username or email exists.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                var userEntity = _mapper.Map<User>(newUser);

                await _context.Users.AddAsync(userEntity);
                bool checkAnyUserExists = await IsAnyUserInSystem();

                var roleCode = newUser.RoleCode != null ? newUser.RoleCode : 300;
                var role = await _context.Roles.FirstOrDefaultAsync(checkAnyUserExists ? x => x.Code == roleCode : x => x.Code == 100);
                if (role == null)
                {
                    return new ApiResponse<CreateUserRequest>
                    {
                        Success = false,
                        Message = "There aren't any role to create new account.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                await _context.SaveChangesAsync();

                await RelateRole(role.RoleID, userEntity.UserID);

                return new ApiResponse<CreateUserRequest>
                {
                    Success = true,
                    Message = "Register successfully.",
                    Data = newUser,
                    Status = (int)HttpStatusCode.Created
                };
            }
            catch (Exception )
            {
                throw;
            }
        }*/

    }
}
