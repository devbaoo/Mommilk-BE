using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Create
{
    public class FeedbackCreateModel
    {
        public int? ProductId { get; set; }
        public int? RateStar { get; set; }
        public string? Content { get; set; }

    }
}
