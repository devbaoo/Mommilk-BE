using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class OrderSummaryFilterModel
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Status { get; set; }
    }
}
