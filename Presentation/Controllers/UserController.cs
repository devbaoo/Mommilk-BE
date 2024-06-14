using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Auth;
using Domain.Models.CreateUserRequest;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mommilk88.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + "Firebase")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("profile")]

        public async Task<IActionResult> GetProfile()
        {
            var userAuthID = Guid.Parse(User.FindFirstValue("Id")!);
            var result = await _userService.GetProfile(userAuthID);
            return StatusCode(result.Status, result);
        }
    }
}
