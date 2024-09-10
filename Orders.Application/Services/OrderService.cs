using MassTransit;
using Orders.Application.Data;
using Orders.Application.Data.Entities;
using Orders.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPublishEndpoint pulishEndpoint;
        private readonly OrdersDbContext ordersDbContext;

        public OrderService(IPublishEndpoint pulishEndpoint, OrdersDbContext ordersDbContext)
        {
            this.pulishEndpoint = pulishEndpoint;
            this.ordersDbContext = ordersDbContext;
        }
        public async Task<bool> CreateOrder(Order order)
        {
            // save the item in the database
            await ordersDbContext.Orders.AddAsync(order);

            // publish an orderCreated event

            var orderCreatedEvent = new OrderCreated
            {
                OrderId = order.Id,
                CreatedAt = order.CreatedAt
            };

            await pulishEndpoint.Publish(orderCreatedEvent);

            var result = await ordersDbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
