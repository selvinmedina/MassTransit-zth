using MassTransit;
using SagaOrders.Application.Data.Entities;
using SagaOrders.Application.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrders.Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly IPublishEndpoint publishEndpoint;

        public OrderService(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            var message = new OrderCreated()
            {
                CreatedAt = order.CreatedAt,
                OrderId = order.Id,
                Amount = 100

            };
            await publishEndpoint.Publish(message);
            return true;
        }

    }
}
