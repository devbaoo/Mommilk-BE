using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class ProductRevenueFilterModel
    {
        public Guid? ProductId { get; set; }
        public string? Search { get; set; }
        public string? Status { get; set; }
    }
}
