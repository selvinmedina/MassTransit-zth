using HelloApi.Cotracts;
using MassTransit;

namespace HelloApi.Consumers
{
    public class SayByeConsumer : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            var message = context.Message;
            Console.WriteLine($"SayBye message: {message.Text}");
            Task.Delay(1000);
            return Task.CompletedTask;
        }
    }
}
