namespace WebApiClouDNA
{
    public interface IDeliveryService
    {
        Task<DeliveryResponseDto> GetRecentOrder(string email, string customerId);
    }
}
