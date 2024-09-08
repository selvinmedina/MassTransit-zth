using HelloApi.Cotracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public HelloController(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var messageToSend = new Message() { Text = "Hello, World!" };

            //await _publishEndpoint.Publish(messageToSend);
            await _publishEndpoint.Publish<Message>(messageToSend, publishContext =>
            {
                publishContext.SetRoutingKey("my-direct-router-key");
                publishContext.Headers.Set("message-id", "12345");
            });

            //var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:hello"));

            ////await sendEndpoint.Send(messageToSend);
            //await sendEndpoint.Send<Message>(new
            //{
            //    messageToSend.Text
            //},
            //sendcontext =>
            //{
            //    sendcontext.Headers.Set("message-id", "12345"); 
            //});

            return Ok();
        }
    }
}
