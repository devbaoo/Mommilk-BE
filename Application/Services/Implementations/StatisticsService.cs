using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Models.Filters;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations
{
    public class StatisticsService(IUnitOfWork unitOfWork, IMapper mapper)
        : BaseService(unitOfWork, mapper), IStatisticService
    {
        private readonly IOrderRepository _orderRepository = unitOfWork.Order;
        private readonly IProductRepository _productRepository = unitOfWork.Product;

        public async Task<IActionResult> GetProductRevenues(ProductRevenueFilterModel model)
        {
            var query = _productRepository.GetAll();
            if (model.ProductId != null)
            {
                query = query.Where(p => p.Id.Equals(model.ProductId));
            }
            if (model.Search != null && !model.Search.IsNullOrEmpty())
            {
                query = query.Where(p => p.Name.Contains(model.Search) || p.Origin.Contains(model.Search) || p.Brand.Contains(model.Search));
            }
            if (model.Status != null && model.Status.IsNullOrEmpty())
            {
                query = query.Where(p => p.Status.Equals(model.Status));
            }
            var result = await query
                .ProjectTo<ProductRevenueViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new RevenueViewModel
            {
                Revenue = result.Sum(pl => pl.Revenue),
                ProductRevenues = result
            }.Ok();
        }

        public async Task<IActionResult> GetOrderSummary(OrderSummaryFilterModel model)
        {
            var query = _orderRepository.GetAll();
            if (model.From != null && !model.From.IsNullOrEmpty())
            {
                query = query.Where(or => or.CreateAt >= DateTime.Parse(model.From));
            }
            if (model.To != null && !model.To.IsNullOrEmpty())
            {
                query = query.Where(or => or.CreateAt <= DateTime.Parse(model.To));
            }
            if (model.Status != null && !model.Status.IsNullOrEmpty())
            {
                query = query.Where(or => or.Status.Equals(model.Status));
            }
            var results = await query.ToListAsync();
            //Orders
            var completedOrders = results.Where(re => re.Status.Equals(OrderStatuses.COMPLETED)).ToList();
            var canceledOrders = results.Where(re => re.Status.Equals(OrderStatuses.CANCELED)).ToList();
            var pendingPaymentOrders = results
                .Where(re => re.PaymentMethod.Equals(PaymentMethods.CASH) && !re.Status.Equals(OrderStatuses.PENDING) && !re.Status.Equals(OrderStatuses.CANCELED))
                .ToList();
            var ongoingOrders = results.Where(re => re.Status.Equals(OrderStatuses.COMPLETED) && !re.Status.Equals(OrderStatuses.CANCELED)).ToList();
            var cashOrders = results.Where(re => re.PaymentMethod.Equals(PaymentMethods.CASH) && re.Status.Equals(OrderStatuses.COMPLETED)).ToList();
            var vnPayOrders = results.Where(re => re.PaymentMethod.Equals(PaymentMethods.VNPAY) && re.IsPayment).ToList();
            //Values
            decimal discount = cashOrders.Sum(re => (decimal)re.Discount) + vnPayOrders.Sum(re => (decimal)re.Discount);
            decimal pending = pendingPaymentOrders.Sum(pe => (decimal)pe.Amount) - pendingPaymentOrders.Sum(pe => (decimal)pe.Discount);
            decimal canceled = canceledOrders.Sum(ca => (decimal)ca.Amount);
            decimal cash = cashOrders.Sum(cs => (decimal)cs.Amount);
            decimal vnPay = vnPayOrders.Sum(vn => (decimal)vn.Amount);
            decimal revenue = cash + vnPay;
            var result = new OrderSummaryViewModel
            {
                Revenue = revenue,
                Discount = discount,
                PendingValue = pending,
                CanceledValue = canceled,
                TotalOrders = results.Count,
                CanceledOrders = canceledOrders.Count,
                CompletedOrders = completedOrders.Count,
                OngoingOrders = ongoingOrders.Count,
                PendingPaymentOrders = pendingPaymentOrders.Count,
                RevenueFromCash = cash,
                RevenueFromVNPay = vnPay,
                From = results.Any() ? results.Min(re => re.CreateAt).ToString() : null,
                To = results.Any() ? results.Max(re => re.CreateAt).ToString() : null,
            };
            return result.Ok();
        }
    }
}
