using HelloApi.Cotracts;
using MassTransit;

namespace HelloApi.Consumers
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> logger;

        public MessageConsumer(
            ILogger<MessageConsumer> logger
            )
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<Message> context)
        {
            throw new ArgumentNullException(nameof(context));
            var message = context.Message;
            logger.LogInformation($"MessageConsumer message: {message.Text}");
            Task.Delay(1000);
            return Task.CompletedTask;
        }
    }
}
