using SagaOrders.Application.Data.Entities;

namespace SagaOrders.Application.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(Order order);
    }
}