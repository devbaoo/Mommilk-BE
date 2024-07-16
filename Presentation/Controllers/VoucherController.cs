using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService) { 
            _voucherService = voucherService;
        }

        [HttpPost]  
        [Route("filter")]
        public async Task<IActionResult> GetVouchers([FromBody]VoucherFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _voucherService.GetVouchers(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("get-valid")]
        public async Task<IActionResult> GetValidVouchers()
        {
            try
            {
                return await _voucherService.GetValidVouchers();
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetVoucher([FromRoute] Guid id)
        {
            try
            {
                return await _voucherService.GetVoucher(id);
            }
            catch (Exception ex) 
            { 
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(UserRoles.STAFF)]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherCreateModel model)
        {
            try
            {
                return await _voucherService.CreateVoucher(model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.STAFF)]
        [Route("{id}")]
        public async Task<IActionResult> UpdateVoucher([FromRoute] Guid id, [FromBody] VoucherUpdateModel model)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _voucherService.UpdateVoucher(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
