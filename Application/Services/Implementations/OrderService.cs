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
                if (filter.Id != null) {
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
                var list = await query
                    .Paginate(pagination)
                    .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.DeliveryDate)
                    .ToListAsync();
                return list.Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetOrderDetails(Guid target)
        {
            try
            {
                var order = _orderRepository.FirstOrDefault(x => x.Id == target);
                var details = await _orderDetailRepository.GetAll()
                    .Where(x => x.OrderId == target)
                    .ToListAsync();
                var result = new OrderViewModel
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    DeliveryDate = order.DeliveryDate,
                    Address = order.Address,
                    Phone = order.Phone,
                    Recipient = order.Recipient,
                    Amount = order.Amount,
                    PaymentMethod = order.PaymentMethod,
                    Status = order.Status,
                    OrderDetails = details,
                };
                return result.Ok();
            } catch (Exception)
            {
                throw;
            }
        }

        //public async Task<IActionResult> CreateOrder(OrderCreateModel model)
        //{
        //    try
        //    {
        //        var order = new Order
        //        {
        //            Id = Guid.NewGuid(),
        //            CustomerId = model.CustomerId,
        //            Phone = model.Phone,
        //            Address = model.Address,
        //            Recipient = model.Recipient,
        //            PaymentMethod = model.PaymentMethod,
        //            Amount = model.Amount,
        //            Status = model.Status,
        //            DeliveryDate = DateTime.Now,
        //        };
        //        _orderRepository.Add(order);
        //        await _unitOfWork.SaveChangesAsync();
        //        foreach (OrderDetailCreateModel detail in model.OrderDetails)
        //        {
        //            _orderDetailRepository.Add(new OrderDetail
        //            {
        //                Id = Guid.NewGuid(),
        //                OrderId = order.Id,
        //                ProductId = detail.ProductId,
        //                Price = detail.Price,

        //            });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}