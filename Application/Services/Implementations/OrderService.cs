using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IProductRepository _productRepository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _mapper = mapper;
            _orderRepository = unitOfWork.Order;
            _orderDetailRepository = unitOfWork.OrderDetail;
            _productRepository = unitOfWork.Product;
        }

        public async Task<IActionResult> GetOrders(OrderFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _orderRepository.GetAll();
                if (filter.CreateAt != null)
                {
                    query = query
                        .Where(x => x.CreateAt.Date == filter.CreateAt.Value.Date);
                }
                var list = await query
                    .Paginate(pagination)
                    .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                return list.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateOrder(OrderCreateModel order)
        {
            try
            {
                var tempOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = order.CustomerId,
                    Address = order.Address,
                    Phone = order.Phone,
                    Recipient = order.Recipient,
                    Amount = order.Amount,
                    PaymentMethod = order.PaymentMethod,
                    Status = order.Status,
                    CreateAt = DateTime.Now
                };
                _orderRepository.Add(tempOrder);
                var createdOrder = await _unitOfWork.SaveChangesAsync();
                if(createdOrder > 0)
                {
                    foreach (OrderDetailCreateModel orderDetail in order.OrderDetails) {
                        _orderDetailRepository.Add(new OrderDetail
                        {
                            
                        }
                        );
                    }
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}