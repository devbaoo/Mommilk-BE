using Domain.Models.Create;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IActionResult> GetOrders(OrderFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetOrder(Guid id);
        Task<IActionResult> CreateOrder(Guid customerId, OrderCreateModel model);
    }
}
