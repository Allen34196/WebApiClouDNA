using Microsoft.AspNetCore.Mvc;

namespace WebApiClouDNA.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class EstimatedDeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public EstimatedDeliveryController(IDeliveryService deliveryService)
        {
               _deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecentOrderInfo([FromBody] CustomerRequest customerRequest)
        {
            if (string.IsNullOrEmpty(customerRequest.User) || string.IsNullOrEmpty(customerRequest.CustomerId))
            {
                return BadRequest();
            }
            var orderInfo = await _deliveryService.GetRecentOrder(customerRequest.User, customerRequest.CustomerId);

            if (orderInfo == null)
            {
                return NotFound();
            }
            return Ok(orderInfo);
        }
    }
}
