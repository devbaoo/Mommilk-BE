using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Auth;
using Domain.Models.CreateUserRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mommilk88.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _userService.Login(request);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            try
            {
                var result = await _userService.Register(request);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }


        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {

            var change = User.FindFirstValue("Id");
            if (change == null)
            {
                return Unauthorized();
            }

            var result = await _userService.ChangePassword(Guid.Parse(change), request);
            return StatusCode(result.Status, result);
        }
    }
}
