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
        [HttpGet("user-by-id/{userID}")]
        public async Task<IActionResult> GetUserByID(Guid userID)
        {
            try
            {
                var result = await _userService.GetProfile(userID);
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

        [HttpPut("update-user/{UserID}")]
        public async Task<IActionResult> UpdateUser(Guid UserID, UpdateUserRequest request)
        {
            try
            {
                var result = await _userService.UpdateUser(UserID, request);
                return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

/*        [HttpDelete("delete-user/{UserID}")]
        public async Task<IActionResult> DeleteUser(Guid UserID)
        {
            var result = await _userService.DeleteUser(UserID);
            return StatusCode(result.Status, result);
        }*/
    }
}
