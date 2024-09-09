using HelloApi.Cotracts;
using MassTransit;

namespace HelloApi.Consumers
{
    public class MyRequestConsumer : IConsumer<MyRequest>
    {
        public Task Consume(ConsumeContext<MyRequest> context)
        {
            var request = context.Message;
            var response = new MyResponse
            {
                ResponseContent = $"Received: {request.RequestBody}",
                InitialRequestId = request.Id
            };

            return context.RespondAsync(response);
        }
    }
}
