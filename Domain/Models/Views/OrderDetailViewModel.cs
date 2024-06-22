
namespace Domain.Models.Views
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; } = null!;
    }
}
