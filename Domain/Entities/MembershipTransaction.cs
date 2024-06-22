using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MembershipTransaction
{
    public int Id { get; set; }

    public int? MembershipId { get; set; }

    public Guid? StoreOwnerId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual Membership? Membership { get; set; }

    public virtual User? StoreOwner { get; set; }
}
