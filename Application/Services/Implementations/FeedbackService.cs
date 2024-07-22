using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Application.Services.Implementations
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICustomerRepository _customerRepository;


        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _feedbackRepository = unitOfWork.Feedback;
            _orderRepository = unitOfWork.Order;
            _orderDetailRepository = unitOfWork.OrderDetail;
            _customerRepository = unitOfWork.Customer;
        }

        public async Task<IActionResult> GetFeedbacks(FeedbackFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _feedbackRepository.GetAll();
                if (filter.ProductId != null)
                {
                    query = query.Where(fb => fb.ProductId.Equals(filter.ProductId));
                }
                if (filter.CustomerId != null)
                {
                    query = query.Where(fb => fb.CustomerId.Equals(filter.CustomerId));
                }
                if (filter.From != null)
                {
                    query = query.Where(fb => fb.CreateAt.Date >= filter.From);
                }
                if (filter.To != null)
                {
                    query = query.Where(fb => fb.CreateAt.Date <= filter.To);
                }
                var totalRow = query.Count();
                var feedbacks = await query
                    .ProjectTo<FeedbackViewModel>(_mapper.ConfigurationProvider)
                    .Paginate(pagination)
                    .ToListAsync();
                return feedbacks.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFeedback(Guid id)
        {
            try
            {
                var feedback = await _feedbackRepository
                    .Where(fb => fb.Id.Equals(id))
                    .FirstOrDefaultAsync();
                if (feedback == null)
                {
                    return AppErrors.RECORD_NOT_FOUND.NotFound();
                }
                return feedback.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CanFeedback(Guid orderDetailId)
        {
            try
            {
                var orderDetail = await _orderDetailRepository
                    .Where(od => od.Id.Equals(orderDetailId))
                    .FirstOrDefaultAsync();
                if (orderDetail != null && !orderDetail.HasFeedback)
                {
                    var status = await _orderRepository
                        .Where(order => order.Id.Equals(orderDetail.OrderId))
                        .Select(order => order.Status)
                        .FirstOrDefaultAsync();
                    if (status != null && status == OrderStatuses.COMPLETED) 
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFeedback(Guid customerId, FeedbackCreateModel model)
        {
            try
            {
                //var customer = await _customerRepository
                //    .Where(cs => cs.Id.Equals(customerId))
                //    .Select(cs => cs.Status)
                //    .FirstOrDefaultAsync();
                //if (customer == null || customer.Equals(CustomerStatuses.INACTIVE))
                //{
                //    return AppErrors.INVALID_USER_UNACTIVE.Forbidden();
                //}
                var orderDetail = await _orderDetailRepository
                    .Where(od => od.Id.Equals(model.OrderDetailId))
                    .FirstOrDefaultAsync();
                if (orderDetail == null)
                {
                    return AppErrors.RECORD_NOT_FOUND.NotFound();
                }
                if (await CanFeedback(model.OrderDetailId))
                {
                    var feedback = _mapper.Map<Feedback>(model);
                    feedback.ProductId = orderDetail.ProductId;
                    feedback.CustomerId = customerId;
                    _feedbackRepository.Add(feedback);
                    orderDetail.HasFeedback = true;
                    _orderDetailRepository.Update(orderDetail);
                    var result = await _unitOfWork.SaveChangesAsync();
                    if (result > 0)
                    {
                        return AppNotifications.FEEDBACK_SUCCESSFUL.Ok();
                    }
                    return AppErrors.CREATE_FAIL.UnprocessableEntity();
                }
                return AppErrors.FEEDBACK_ALREADY_EXISTS.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
