using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class OrderSummaryViewModel
    {
        public decimal Revenue { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal PendingValue { get; set; } = 0;
        public decimal CanceledValue { get; set; } = 0;
        public decimal RevenueFromCash { get; set; } = 0;
        public decimal RevenueFromVNPay { get; set; } = 0;
        public int TotalOrders { get; set; } = 0;
        public int CanceledOrders { get; set; } = 0;
        public int CompletedOrders { get; set; } = 0;
        public int OngoingOrders { get; set; } = 0;
        public int PendingPaymentOrders { get; set; } = 0;
        public string? From { get; set; } = null!;
        public string? To { get; set; } = null!;

    }
}
