using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel Customer { get; set; }
        public ProductViewModel Product { get; set; }
        public int RateStar { get; set; }
        public string? Content { get; set; }
    }
}
