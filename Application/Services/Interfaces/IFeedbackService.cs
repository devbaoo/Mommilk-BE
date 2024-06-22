using Domain.Models.Create;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<IActionResult> GetFeedbacks(int? id, PaginationRequestModel pagination);
        Task<IActionResult> GetFeedback(Guid id);
        Task<IActionResult> CreateFeedback(FeedbackCreateModel model, Guid customerId);
        Task<IActionResult> GetFeedbacksByCustomerId(Guid id, PaginationRequestModel pagination);
    }
}
