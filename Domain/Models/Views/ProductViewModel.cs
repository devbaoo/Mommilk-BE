using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Origin { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? Ingredient { get; set; }

        public string? SweetLevel { get; set; }

        public string? Flavour { get; set; }

        public string? Sample { get; set; }

        public string? Capacity { get; set; }

        public string Description { get; set; } = null!;

        public double Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpireAt { get; set; }

        public DateTime CreateAt { get; set; }

        public string Status { get; set; } = null!;
        public ICollection<ProductImageViewModel> ProductImages { get; set; } = new List<ProductImageViewModel>();

    }
}
