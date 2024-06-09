using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
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
        private readonly IOrderRepository _orderService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _orderService = unitOfWork.Order;
        }

        public async Task<IActionResult> GetOrders(OrderFilterModel filter,PaginationRequestModel pagination)
        {
            try
            {
                var query = _orderService.GetAll();

                if(filter.Id != null)
                {
                    query = query
                        .Where(x => x.Id == filter.Id);
                }
                if(filter.CustomerId!= null)
                {
                    query = query
                        .Where(x => x.CustomerId == filter.CustomerId);
                }
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

    }
}
