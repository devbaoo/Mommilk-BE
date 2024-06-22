using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Create;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly new IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderTransactionRepository _orderTransactionRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _mapper = mapper;
            _orderRepository = unitOfWork.Order;
            _orderDetailRepository = unitOfWork.OrderDetail;
            _orderTransactionRepository = unitOfWork.OrderTransaction;
            _productRepository = unitOfWork.Product;
        }

        public async Task<IActionResult> GetOrders(OrderFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _orderRepository.GetAll();
                if (filter.Id != null)
                {
                    query = query
                        .Where(x => x.Id == filter.Id);
                }
                if (filter.CustomerId != null)
                {
                    query = query
                        .Where(x => x.CustomerId == filter.CustomerId);
                }
                if (filter.DeliveryDate.HasValue)
                {
                    query = query
                    .Where(x => x.DeliveryDate.HasValue && x.DeliveryDate.Value.Date == filter.DeliveryDate.Value.Date);
                }
                var totalRow = query.Count();
                var orders = await query
                    .Paginate(pagination)
                    .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.DeliveryDate)
                    .ToListAsync();
                return orders.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var order = await _orderRepository.Where(x => x.Id.Equals(id))
                    .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return order != null ? order.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateOrder(Guid customerId, OrderCreateModel model)
        {
            try
            {
                var order = _mapper.Map<Order>(model);
                order.CustomerId = customerId;
                _orderRepository.Add(order);
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    return await GetOrder(order.Id);
                }
                return AppErrors.CREATE_FAIL.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}