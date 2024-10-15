using WebApiClouDNA.Models;

namespace WebApiClouDNA
{
    public class DeliveryResponseDto
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }

        public DeliveryResponseDto()
        {
            Customer = new Customer();
            Order = new Order();
        }

    }
}
