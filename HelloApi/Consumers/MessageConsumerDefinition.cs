using MassTransit;

namespace HelloApi.Consumers
{
    public class MessageConsumerDefinition : ConsumerDefinition<MessageConsumer>
    {
        public MessageConsumerDefinition()
        {
            Endpoint(x => x.Name = "message.consumer");
        }

        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<MessageConsumer> consumerConfigurator, 
            IRegistrationContext context)
        {
            consumerConfigurator.ConcurrentMessageLimit = 2;
            consumerConfigurator.UseMessageRetry(x => x.Interval(5, TimeSpan.FromSeconds(3)));
        }
    }
}
