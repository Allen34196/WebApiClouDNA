namespace WebApiClouDNA.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId {  get; set; } = string.Empty;
        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryExpected { get; set; }
        public byte ContainsGift {  get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

    }
}
