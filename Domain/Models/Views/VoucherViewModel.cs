using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class VoucherViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string From { get; set; }

        public string To { get; set; }

        public int? MinOrderValue { get; set; }

        public int Value { get; set; }

        public int Quantity { get; set; }

        public DateTime CreateAt { get; set; }

        public string Status { get; set; } = null!;
    }
}
