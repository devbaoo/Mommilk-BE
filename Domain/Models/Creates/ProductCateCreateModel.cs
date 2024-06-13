﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class ProductCateCreateModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
