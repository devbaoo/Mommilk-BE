using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string Name { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public string Rank { get; set; } = null!;

        public bool Status { get; set; }
    }
}
