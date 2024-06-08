using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuaMe88.Data;
using SuaMe88.Services.UserServices;
using static SuaMe88.Data.Login;
using System.Net;
using Common.Extensions;

namespace SuaMe88.Controllers
{


    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Data.LoginRequest request)
        {
            try
            {
                var response = await _userService.Login(request);
                return response.Ok();
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<LoginResult>
                {
                    Success = false,
                    Message = "User service - Login: " + ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError
                };
                return errorResponse.InternalServerError();
            }
        }
    }
}
