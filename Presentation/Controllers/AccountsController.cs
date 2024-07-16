using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController (IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("admins")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateModel model)
        {
            try
            {
                return await _accountService.CreateAdmin(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("admins/filter")]
        public async Task<IActionResult> GetAdmins([FromBody] AdminFilterModel model, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _accountService.GetAdmins(model, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Authorize(UserRoles.ADMIN)]
        [Route("admins/{id}")]
        public async Task<IActionResult> CreateAdmin([FromRoute] Guid id)
        {
            try
            {
                return await _accountService.GetAdmin(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateModel model)
        {
            try
            {
                return await _accountService.CreateCustomer(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers/{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            try
            {
                return await _accountService.GetCustomer(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers/filter")]
        public async Task<IActionResult> GetCustomers([FromBody] CustomerFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _accountService.GetCustomers(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers/update/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, CustomerUpdateModel model)
        {
            try
            {
                return await _accountService.UpdateCustomer(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPatch]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers/change-password/{id}")]
        public async Task<IActionResult> ChangeCustomerPassword([FromRoute] Guid id, [FromBody] PasswordUpdateModel model)
        {
            try
            {
                return await _accountService.ChangeCustomerPassword(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPatch]
        [Authorize(UserRoles.ADMIN)]
        [Route("customers/change-status")]
        public async Task<IActionResult> ChangeCustomerStatus([FromBody] CustomerStatusUpdateModel model)
        {
            try
            {
                return await _accountService.ChangeCustomerStatus(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs/filter")]
        public async Task<IActionResult> GetStaffs([FromBody] StaffFilterModel model,[FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _accountService.GetStaffs(model, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs/{id}")]
        public async Task<IActionResult> GetStaff([FromRoute] Guid id)
        {
            try
            {
                return await _accountService.GetStaff(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs")]
        public async Task<IActionResult> CreateStaff([FromBody]StaffCreateModel model)
        {
            try
            {
                return await _accountService.CreateStaff(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs/update/{id}")]
        public async Task<IActionResult> UpdateStaff([FromRoute]Guid id, [FromBody]StaffUpdateModel model)
        {
            try
            {
                return await _accountService.UpdateStaff(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPatch]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs/change-password/{id}")]
        public async Task<IActionResult> ChangeStaffPassword([FromRoute]Guid id, [FromBody]PasswordUpdateModel model)
        {
            try
            {
                return await _accountService.ChangeStaffPassword(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPatch]
        [Authorize(UserRoles.ADMIN)]
        [Route("staffs/change-status/")]
        public async Task<IActionResult> ChangeStaffStatus([FromBody]StaffStatusUpdateModel model)
        {
            try
            {
                return await _accountService.ChangeStaffStatus(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
