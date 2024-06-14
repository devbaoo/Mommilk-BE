using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class ProductFilterModel
    {
        public string? Name { get; set; }

        public string? Origin { get; set; }

        public string? Brand { get; set; }

        public string? Ingredient { get; set; }

        public string? SweetLevel { get; set; }

        public string? Flavour { get; set; }

        public string? Sample { get; set; }

        public string? Capacity { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpireAt { get; set; }

        public Guid StoreId { get; set; }

        public DateTime CreateAt { get; set; }

        public string? Status { get; set; }
    }
}
