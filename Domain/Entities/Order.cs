using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? CustomerId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Recipient { get; set; }

    public double Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DeliveryDate { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderTransaction> OrderTransactions { get; set; } = new List<OrderTransaction>();
}
