using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Filters
{
    public class OrderFilterModel
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
