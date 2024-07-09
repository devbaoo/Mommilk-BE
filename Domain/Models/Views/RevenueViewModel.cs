using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class RevenueViewModel
    {
        public int Revenue { get; set; }
        public List<ProductRevenueViewModel> ProductRevenues { get; set; } = null!;
    }
}
