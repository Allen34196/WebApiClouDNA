using Dapper;
using Microsoft.Data.SqlClient;
using WebApiClouDNA.Models;

namespace WebApiClouDNA
{
    public class DeliveryService : IDeliveryService
    {
        private readonly string? _connectionString;

        public DeliveryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<DeliveryResponseDto> GetRecentOrder(string email, string customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string customerInfoQuery = @"Select 1 from Customers WHERE CustomerId = @CustomerId AND Email = @Email; ";
                var customerInfo = await connection.QueryFirstOrDefaultAsync(customerInfoQuery, new {Email = email, CustomerId = customerId});

                if (customerInfo == null)
                {
                    throw new Exception("Invalid request: customer not found");
                }

                string customerOrderQuery = @"SELECT TOP 1 " +
                    "o.OrderId AS OrderNumber," +
                    "o.OrderDate," +
                    "o.DeliveryExpected," +
                    "CONCAT(c.HouseNo, ',', c.Street, ',', c.Town, ',', c.PostCode) AS DeliveryAddress" +
                    "FROM Orders o" +
                    "JOIN Customers c ON c.CustomerId = o.CustomerId" +
                    "WHERE c.CustomerId = @CustomerId" +
                    "ORDER BY o.OrderDate DESC;";

                var orderInfo = await connection.QueryFirstOrDefaultAsync<Order>(customerOrderQuery, new {CustomerId = customerId});
                if (orderInfo == null)
                {
                    return new DeliveryResponseDto
                    {
                        Customer = customerInfo,
                        Order = new Order()
                    };
                }

                string recentOrderQuery = @"SELECT
                                        CASE
                                           WHEN o.ContainsGift = 1 THEN 'Gift'
                                           ELSE p.ProductName
                                        END AS Product,
                                        oi.Quantity,
                                        oi.Price AS PriceEach
                                        FROM OrderItems oi
                                        JOIN Orders o ON oi.OrderId = o.OrderId
                                        LEFT JOIN Products p ON oi.PrdouctId = p.ProductId
                                        WHERE o.OrderId = @OrderId;";
                var orderItemsInfo = await connection.QueryAsync<OrderItem>(recentOrderQuery, new { OrderId = orderInfo.OrderId });

                return new DeliveryResponseDto
                {
                    Customer = customerInfo,
                    Order = new Order()
                    {
                        OrderId = orderInfo.OrderId,
                        OrderDate = orderInfo.OrderDate,
                        DeliveryExpected = orderInfo.DeliveryExpected,
                        OrderItems = orderItemsInfo.ToList()
                    }
                };
            }
        }
    }
}
