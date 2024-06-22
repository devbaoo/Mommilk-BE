using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Feedback
{
    public int Id { get; set; }

    public Guid? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public string Content { get; set; } = null!;

    public int RateStar { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
