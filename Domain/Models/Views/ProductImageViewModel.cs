using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }

        public string Url { get; set; } = null!;

        public bool IsPrimary { get; set; }
    }
}
