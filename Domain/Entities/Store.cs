using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
