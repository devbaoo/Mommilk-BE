using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class ProductCategoryFilterModel
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        public int? ProductId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}
