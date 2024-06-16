using Domain.Entities;
using Domain.Models.Auth;
using Domain.Models.CreateUserRequest;
using Domain.Models.User;

using Models.Data;



using System.Security.Claims;
using static Domain.Models.Auth.Login;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<LoginResult>> Login(LoginRequest request);
        Task<Response<CreateUserRequest>> Register(CreateUserRequest newUser);
        Task<Response<string>> ChangePassword(Guid Id, ChangePasswordRequest request);

        Task<Response<UserProfile>> GetProfile(Guid Id);
        Task<Response<List<UserProfile>>> GetUsers(FilterUser? filter);
        Task<Response<UpdateUserRequest>> UpdateUser(Guid UserID, UpdateUserRequest user);
/*        Task<Response<Object>> DeleteUser(Guid UserID);
*/


    }
}
