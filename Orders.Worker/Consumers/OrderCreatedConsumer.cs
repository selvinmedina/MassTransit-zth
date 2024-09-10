using MassTransit;
using Orders.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Worker.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            var orderCreated = context.Message;

            Console.WriteLine($"Order created: {orderCreated.OrderId}");

            return Task.CompletedTask;
        }
    }
}
