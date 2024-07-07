﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Origin { get; set; } = null!;

    public string ThumbnailUrl { get; set; } = null!;

    public string MadeIn { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public int AgeFrom { get; set; }

    public int AgeTo { get; set; }

    public int Price { get; set; }

    public int? PromotionPrice { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductLine> ProductLines { get; set; } = new List<ProductLine>();
}
