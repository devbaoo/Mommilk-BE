using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Create;
using Domain.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks([FromQuery] int? productId, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _feedbackService.GetFeedbacks(productId, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("/api/customer{customerId}")]
        public async Task<IActionResult> GetFeedbacksByCustomerId([FromRoute] Guid customerId, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _feedbackService.GetFeedbacksByCustomerId(customerId, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFeedback([FromRoute] Guid id)
        {
            try
            {
                return await _feedbackService.GetFeedback(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeedback ([FromBody] FeedbackCreateModel model)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue("UserId")!);                
                return await _feedbackService.CreateFeedback(model, userId);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
