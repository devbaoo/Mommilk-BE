using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Auth;
using Domain.Models.CreateUserRequest;
using Domain.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mommilk88.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

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
            var userAuthID = User.FindFirstValue("UserID");
            if (userAuthID == null)
            {
                return Unauthorized();
            }
            var result = await _userService.GetProfile(Guid.Parse(userAuthID));
            return StatusCode(result.Status, result);
        }
        [HttpGet("user-by-id/{userID}")]
        public async Task<IActionResult> GetUserByID(Guid userID)
        {
            var result = await _userService.GetProfile(userID);
            return StatusCode(result.Status, result);
        }

        [HttpGet("users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers([FromQuery] FilterUser? filterObject)
        {
            var result = await _userService.GetUsers(filterObject);
            return StatusCode(result.Status, result);
        }

        [HttpPut("update-user/{UserID}")]
        public async Task<IActionResult> UpdateUser(Guid UserID, UpdateUserRequest request)
        {
            var result = await _userService.UpdateUser(UserID, request);
            return StatusCode(result.Status, result);
        }

/*        [HttpDelete("delete-user/{UserID}")]
        public async Task<IActionResult> DeleteUser(Guid UserID)
        {
            var result = await _userService.DeleteUser(UserID);
            return StatusCode(result.Status, result);
        }*/
    }
}
