using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Create
{
    public class OrderCreateModel
    {
        public Guid CustomerId { get; set; }
        public string? Address { get; set; }
        //[RegularExpression(@"^0\d{9}$", ErrorMessage = "Please provide a valid phone number ! (Example: 0901234567")]
        public string? Phone { get; set; }
        public string? Recipient { get; set; }
        public float Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string Status { get; set; }
        public ICollection<OrderDetailCreateModel> OrderDetails { get; set; } = null!;
    }
}
