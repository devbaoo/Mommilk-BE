using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UpdateUserRequest
    {

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } 
    }
}
