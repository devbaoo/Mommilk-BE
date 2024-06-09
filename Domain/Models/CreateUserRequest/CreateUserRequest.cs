using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.CreateUserRequest
{
    internal class CreateUserRequest
    {
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? RoleCode { get; set; }

        public bool? IsActive { get; set; } = true;
        public string Phone { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
