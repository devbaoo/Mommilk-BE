using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProductImage
{
    public int Id { get; set; }

    public string Url { get; set; } = null!;

    public int? ProductId { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Product? Product { get; set; }
}
