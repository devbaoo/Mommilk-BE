using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Create;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _feedbackRepository = unitOfWork.Feedback;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetFeedbacks(int? id, PaginationRequestModel pagination)
        {
            var query = _feedbackRepository.GetAll();
            if (id.HasValue) { 
                query = query.Where(x => x.ProductId == id);
            }
            var feedbacks = await query
                .Paginate(pagination)
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var totalRow = feedbacks.Count;
            if (feedbacks.Count > 0)
            {
                return feedbacks.ToPaged(pagination, totalRow).Ok();
            }
            return AppErrors.NOT_FOUND.NotFound();
        }

        public async Task<IActionResult> GetFeedbacksByCustomerId(Guid id, PaginationRequestModel pagination)
        {
            var feedbacks = await _feedbackRepository.GetAll()
                .Where(x => x.CustomerId == id)
                .Paginate(pagination)
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var totalRow = feedbacks.Count;
            if (feedbacks.Count > 0)
            {
                return feedbacks.ToPaged(pagination, totalRow).Ok();
            }
            return AppErrors.NOT_FOUND.NotFound();
        }
        public async Task<IActionResult> GetFeedback (Guid id)
        {
            var feedback = await _feedbackRepository
                .Where(x => x.Id.Equals(id))
                .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (feedback == null)
            {
                return AppErrors.NOT_FOUND.NotFound();
            }
            return feedback.Ok();
        }

        public async Task<IActionResult> CreateFeedback(FeedbackCreateModel model, Guid customerId)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(model);
                feedback.Id = Guid.NewGuid();
                feedback.CustomerId = customerId;
                feedback.CreateAt = DateTime.Now;
                _feedbackRepository.Add(feedback);
                var result = await _unitOfWork.SaveChangesAsync();
                if(result > 0)
                {
                    return await GetFeedback(feedback.Id);
                }
                return AppErrors.CREATE_FAIL.BadRequest();
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
