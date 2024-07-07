﻿using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService  orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel model)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _orderService.CreateOrder(auth.Id, model);
            }
            catch (Exception ex) {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetOrders([FromBody] OrderFilterModel filter, [FromQuery] PaginationRequestModel pagination)
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
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
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

        [HttpPut]
        [Route("status")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateModel model)
        {
            try
            {
                return await _orderService.UpdateOrderStatus(model);
            }
            catch (Exception ex) 
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("confirm")]
        [Authorize]
        public async Task<IActionResult> ConfirmOrder([FromQuery] Guid orderId)
        {
            try
            {
                return await _orderService.ConfirmOrder(orderId);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("deliver")]
        [Authorize]
        public async Task<IActionResult> DeliverOrder([FromRoute] Guid orderId)
        {
            try
            {
                return await _orderService.DeliverOrder(orderId);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("complete")]
        [Authorize]
        public async Task<IActionResult> CompleteOrder([FromRoute] Guid orderId)
        {
            try
            {
                return await _orderService.CompleteOrder(orderId);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("cancel")]
        [Authorize]
        public async Task<IActionResult> CancelOrder([FromQuery] Guid orderId)
        {
            try
            {
                return await _orderService.CancelOrder(orderId);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
