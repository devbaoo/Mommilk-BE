﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class OrderTransaction
{
    public int Id { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? OrderId { get; set; }

    public string Status { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual User Customer { get; set; }

    public virtual Order Order { get; set; }
}