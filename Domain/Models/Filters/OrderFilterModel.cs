namespace Domain.Models.Filters
{
    public class OrderFilterModel
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
