using Domain.Models.Filters;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IActionResult> GetOrders(OrderFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetOrderDetails(Guid target);
    }
}
