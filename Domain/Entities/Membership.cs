using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Membership
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime ExpireDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<MembershipTransaction> MembershipTransactions { get; set; } = new List<MembershipTransaction>();
}
