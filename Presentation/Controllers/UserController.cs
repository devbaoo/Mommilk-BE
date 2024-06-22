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

namespace Presentation.Controllers
{
    [Route("api/users")]
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
            try
            {
                var userAuthID = User.FindFirstValue("UserID");
                if (userAuthID == null)
                {
                    return Unauthorized();
                }
                var result = await _userService.GetProfile(Guid.Parse(userAuthID));
                return StatusCode(result.Status, result);

            } catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
        [HttpGet("user-by-id/{userId}")]
        public async Task<IActionResult> GetUserByID(Guid userId)
        {
            try
            {
                var result = await _userService.GetProfile(userId);
                return StatusCode(result.Status, result);
            } 
            catch (Exception ex) {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet("users")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers([FromQuery] FilterUser? filterObject)
        {
            try
            {
                var result = await _userService.GetUsers(filterObject);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserRequest request)
        {
            try
            {
                var result = await _userService.UpdateUser(userId, request);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
