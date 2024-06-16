
namespace Domain.Models.Views
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; } = null!;
    }
}
