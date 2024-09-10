
using SagaOrders.Application.Data.Entities;
using SagaOrders.Models;
namespace SagaOrders.Infrastructure
{
    public static class Extensions
    {
        public static Application.Data.Entities.Order ToOrder(this OrderModel model)
        {
            return new Order
            {
                Comments = model.Comments,
                CreatedAt = DateTime.UtcNow,
                Total = model.Total,
                Id = Guid.NewGuid()

            };
        }
    }
}
