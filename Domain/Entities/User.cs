using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string Name { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public int? StoreId { get; set; }

    public string Rank { get; set; } = null!;

    public int? RoleId { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<MembershipTransaction> MembershipTransactions { get; set; } = new List<MembershipTransaction>();

    public virtual ICollection<OrderTransaction> OrderTransactions { get; set; } = new List<OrderTransaction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual Store? Store { get; set; }
}
