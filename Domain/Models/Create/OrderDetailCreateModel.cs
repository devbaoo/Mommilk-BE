namespace Domain.Models.Create
{
    public class OrderDetailCreateModel
    {
        public int ProductId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
