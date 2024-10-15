namespace WebApiClouDNA.Models
{
    public class Customer
    {
        public string CustomerId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HouseNo {  get; set; } = string.Empty;
        public string Street {  get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
    }
}
