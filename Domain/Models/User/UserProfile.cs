﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserProfile
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string Rank { get; set; }

        public int? RoleId { get; set; }
        public string? RoleName { get; set; }

    }
}
