﻿using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilterModel filter,[FromQuery] PaginationRequestModel pagination)
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
    }
}
