using HelloApi.Cotracts;
using HelloApi.Filters;
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
        private readonly Tenant _tenant;
        private readonly IRequestClient<MyRequest> _requestClient;

        public HelloController(
            IPublishEndpoint publishEndpoint, 
            ISendEndpointProvider sendEndpointProvider,
            Tenant tenant,
            IRequestClient<MyRequest> requestClient)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
            _tenant = tenant;
            _requestClient = requestClient;
            _tenant.MyValue = "tenant-1";
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var messageToSend = new Message() { Text = "Hello, World!" };

            //await _publishEndpoint.Publish(new Email
            //{
            //    Destination = "",
            //    Subject = "Hello from an email!"
            //});
            await _publishEndpoint.Publish(messageToSend, publishContext =>
            {
                //publishContext.SetRoutingKey("my-direct-router-key");
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

        [HttpGet("/request")]
        public async Task<IActionResult> GetRequest()
        {
            var response = await _requestClient.GetResponse<MyResponse>(new MyRequest
            {
                Id = Guid.NewGuid(),
                RequestBody = "Hello, World!"
            });

            return Ok(response.Message);
        }
    }
}
