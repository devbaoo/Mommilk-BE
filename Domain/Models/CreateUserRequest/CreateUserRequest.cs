using System.ComponentModel.DataAnnotations;

namespace Domain.Models.CreateUserRequest
{
    public class CreateUserRequest
    {
        // public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }

        public int? RoleId { get; set; } = 1;
        public int? Status { get; set; } = 1;

        // public string AvatarUrl { get; set; }
        // public string Rank { get; set; }
        // public bool? Status { get; set; } = true;
    }
}
