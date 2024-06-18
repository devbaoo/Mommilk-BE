using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using static Domain.Models.Auth.Login;
using Domain.Models.Auth;
using Domain.Models.CreateUserRequest;

using Domain.Models.User;

using Models.Data;

namespace Application.Services.UserServices





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
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);

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
                    new Claim("Name", user.Name),
                };

                var result = _jwtService.GenerateToken(user, claims, DateTime.Now);

                var role = await _context.Roles.FindAsync(user.RoleId);

                result.UserResult = new UserResult
                {
                    UserID = user.Id,
                    Username = user.Email,
                    Name = user.Name,
                    Role = role?.Name,
                    Phone = "0" + user.Phone.ToString(),
                    StoreId = user.StoreId

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

        public async Task<Response<CreateUserRequest>> Register(CreateUserRequest newUser)
        {
            try
            {
                if (newUser.Email == null || newUser.Password == null)
                {
                    return new Response<CreateUserRequest>
                    {
                        Success = false,
                        Message = "Invalid username or password!",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                if (await IsExist(newUser.Email))
                {
                    return new Response<CreateUserRequest>
                    {
                        Success = false,
                        Message = "This username or email exists.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                var userEntity = _mapper.Map<User>(newUser);

                await _context.Users.AddAsync(userEntity);

                

                await _context.SaveChangesAsync();


                return new Response<CreateUserRequest>
                {
                    Success = true,
                    Message = "Register successfully.",
                    Data = newUser,
                    Status = (int)HttpStatusCode.Created
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> IsExist(string userName)
        {
            try
            {
                await Task.CompletedTask;
                bool result = await _context.Users.FirstOrDefaultAsync(u => u.Email == userName) is not null;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Response<string>> ChangePassword(Guid Id, ChangePasswordRequest request)
        {
            try
            {
                await Task.CompletedTask;
                var user = await _context.Users.FindAsync(Id);
                if (user == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = "User does not exists.",
                        Status = (int)HttpStatusCode.OK
                    };
                }
                if (request.OldPassword != user.Password)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = "Old password is not correct.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                user.Password = request.NewPassword;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new Response<string>
                {
                    Success = true,
                    Message = "Change password successfully, please login again.",
                    Status = (int)HttpStatusCode.OK
                };
            }
            catch (Exception )
            {
                throw;
               /* return new Response<string>
                {
                    Success = false,
                    Message = "User service - ChangePassword: " + ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                };*/
            }
        }

        public async Task<Response<UserProfile>> GetProfile(Guid Id)
        {
            try
            {
                await Task.CompletedTask;
                var user = await _context.Users.FindAsync(Id);

                if (user == null)
                {
                    return new Response<UserProfile>
                    {
                        Success = false,
                        Message = "User does not exists.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                var role = await _context.Roles.FindAsync(user.RoleId);
                var userMapping = _mapper.Map<UserProfile>(user);

                userMapping.RoleName = role?.Name;  

                return new Response<UserProfile>
                {
                    Success = true,
                    Message = $"Got profile of user {user.Email}.",
                    Data = userMapping,
                    Status = (int)HttpStatusCode.OK
                };
            }
            catch (Exception )
            {
                throw;
/*                return new Response<UserProfile>
                {
                    Success = false,
                    Message = "UserService - GetProfile: " + ex,
                    Status = (int)HttpStatusCode.InternalServerError
                };*/
            }
        }
        public async Task<Response<List<UserProfile>>> GetUsers(FilterUser? filter)
        {
            try
            {
                await Task.CompletedTask;
                var users = await _context.Users.ToListAsync();

                if (!users.Any())
                {
                    return new Response<List<UserProfile>>
                    {
                        Success = false,
                        Message = "There aren't any users.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                var usersProfile = users.Select(x => _mapper.Map<UserProfile>(x)).ToList();
                foreach (var user in usersProfile)
                {
                    var role = await _context.Roles.FindAsync(user.RoleId);
                    user.RoleName = role?.Name;
                };

/*                if (filter != null) usersProfile = FilterUser(usersProfile, filter);
*/
                return new Response<List<UserProfile>>
                {
                    Success = true,
                    Message = $"Got users. Found {usersProfile.Count()} users!",
                    Data = usersProfile,
                    Status = (int)HttpStatusCode.OK
                };
            }
            catch (Exception )
            {
                throw;
                /*return new Response<List<UserProfile>>
                {
                    Success = false,
                    Message = "UserService - GetUsers: " + ex,
                    Status = (int)HttpStatusCode.InternalServerError
                };*/
            }
        }

        public async Task<Response<UpdateUserRequest>> UpdateUser(Guid UserID, UpdateUserRequest user)
        {
            try
            {
                await Task.CompletedTask;

                var userInData = await GetByID(UserID);
                if (userInData is null)
                {
                    return new Response<UpdateUserRequest>
                    {
                        Success = false,
                        Message = "User does not exist.",
                        Status = (int)HttpStatusCode.OK
                    };
                }

                _mapper.Map(user, userInData);


                _context.Users.Update(userInData);
                await _context.SaveChangesAsync();

                return new Response<UpdateUserRequest>
                {
                    Success = true,
                    Message = $"Updated user {user.Email}.",
                    Data = user,
                    Status = (int)HttpStatusCode.OK
                };
            }
            catch (Exception )
            {
                throw;
                /*return new Response<UpdateUserRequest>
                {
                    Success = false,
                    Message = "UserService - UpdateUser: " + ex,
                    Status = (int)HttpStatusCode.InternalServerError
                };*/
            }
        }

        private async Task<User?> GetByID(Guid UserID)
        {
            try
            {
                await Task.CompletedTask;
                return await _context.Users.FindAsync(UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

/*        public async Task<Response<object>> DeleteUser(Guid UserID)
        {
            try
            {
                await Task.CompletedTask;
                await _context.Database.ExecuteSqlInterpolatedAsync($"sp_delete_user {UserID}");
                await _context.SaveChangesAsync();
                return new Response<object>
                {
                    Success = true,
                    Message = $"Deleted.",
                    Status = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new Response<Object>
                {
                    Success = false,
                    Message = "UserService - DeleteUser: " + ex,
                    Status = (int)HttpStatusCode.InternalServerError
                };
            }
        }
*//*        private List<UserProfile> FilterUser(List<UserProfile> users, FilterUser filter)
        {
            if (filter.Status is not null)
                users = users.Where(x => filter.Status == "activated" ? x.Status : !x.Status).ToList();
            if (filter.Role is not null)
                users = users.Where(u => u.Roles is not null && u.Roles.Contains(filter.Role)).ToList();
            if (filter.SortType is not null)
            {
                PropertyInfo propertyInfo = typeof(UserProfile).GetProperty(char.ToUpper(filter.SortType[0]) + filter.SortType.Substring(1))!;
                switch (filter.SortFrom)
                {
                    case "ascending":
                        users = users.OrderBy(x => propertyInfo.GetValue(x, null)).ToList();
                        break;
                    default:
                        users = users.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
                        break;
                }
            }
            if (filter.SearchText is not null)
                users = users.Where(x =>
                    x.Username.ToLower().Contains(filter.SearchText.ToLower()) ||
                    string.Concat(x.Firstname, x.Lastname).Trim().ToLower().Contains(filter.SearchText.ToLower())
                ).ToList();
            return users;
        }
*/



    }
}
