using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class ProductLineCreateModel
    {
        public int Quantity { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
