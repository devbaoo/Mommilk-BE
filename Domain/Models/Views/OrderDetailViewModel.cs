using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public Double Price { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; } = null!;
    }
}
