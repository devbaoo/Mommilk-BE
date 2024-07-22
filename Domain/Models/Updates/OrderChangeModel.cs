using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Updates
{
    public class OrderChangeModel
    {
        public Guid OrderId { get; set; }
        public string? Note { get; set; }
    }
}
