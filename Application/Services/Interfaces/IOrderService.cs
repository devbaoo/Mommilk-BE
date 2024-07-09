﻿using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IActionResult> GetOrders(OrderFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetOrder(Guid id);
        Task<IActionResult> UpdateOrderStatus(OrderStatusUpdateModel model);
        Task<IActionResult> CreateOrder(Guid customerId, OrderCreateModel model);
        Task<IActionResult> ConfirmOrder(Guid id);
        Task<IActionResult> DeliverOrder(Guid orderId);
        Task<IActionResult> CompleteOrder(Guid orderId);
        Task<IActionResult> CancelOrder(OrderChangeModel model);
        Task<IActionResult> NoteDeliveringOrder(OrderChangeModel model);
    }
}
