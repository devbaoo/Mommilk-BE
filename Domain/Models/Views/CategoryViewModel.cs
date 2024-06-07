namespace Domain.Models.Views
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string TargetAudience { get; set; } = null!;

        public string AgeRange { get; set; } = null!;

        public string MilkType { get; set; } = null!;

        public string? Icon { get; set; }
    }
}

