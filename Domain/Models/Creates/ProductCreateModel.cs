using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class ProductCreateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Origin { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? Ingredient { get; set; } = null!;

        public string? SweetLevel { get; set; } = null!;

        public string? Flavour { get; set; } = null!;

        public string? Sample { get; set; } = null!;

        public string? Capacity { get; set; } = null!; 

        public string Description { get; set; } = null!;

        public double Price { get; set; } 

        public int Quantity { get; set; } 

        public DateTime ExpireAt { get; set; } 

        public Guid StoreId { get; set; } 

        public DateTime CreateAt { get; set; } 

        public string Status { get; set; } = null!;
    }
}
