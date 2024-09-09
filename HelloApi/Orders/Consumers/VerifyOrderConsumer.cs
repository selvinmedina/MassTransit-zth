using HelloApi.Cotracts;
using HelloApi.Orders.Contracts;
using MassTransit;

namespace HelloApi.Orders.Consumers
{
    public class VerifyOrderConsumer : IConsumer<VerifyOrder>
    {
        public Task Consume(ConsumeContext<VerifyOrder> context)
        {
            if(!context.IsResponseAccepted<Email>())
            {
                throw new InvalidOperationException("The response was not accepted");
            }

            var order = context.Message;
            if (order.Id == 42)
            {
                return context.RespondAsync<OrderResult>(new OrderResult
                {
                    Id = order.Id,
                    Amount = 100.00m,
                    CustomerName = "Test Customer"
                });
                
            }

            return context.RespondAsync<OrderNotFoundResult>(new
            {
                Message = $"Order {order.Id} was not found"
            });

        }
    }
}
