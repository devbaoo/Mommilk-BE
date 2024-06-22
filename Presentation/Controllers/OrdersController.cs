using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Create;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _orderService.GetOrders(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderDetails([FromRoute] Guid id)
        {
            try
            {
                return await _orderService.GetOrder(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel model)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue("UserID")!);

                return await _orderService.CreateOrder(userId, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
