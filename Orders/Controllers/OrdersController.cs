using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SagaOrders.Application.Events;
using SagaOrders.Application.Services;
using SagaOrders.Infrastructure;
using SagaOrders.Models;

namespace SagaOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase

    {
        private readonly IOrderService service;
        private readonly IPublishEndpoint publishEndpoint;

        public OrdersController(IOrderService service, IPublishEndpoint publishEndpoint)
        {
            this.service = service;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderModel model)
        {
            var createdOrder = await service.CreateOrder(model.ToOrder());
            return Accepted();
        }

        [HttpPost("/pay")]
        public async Task<IActionResult> PayOrder(Guid orderId, decimal amount)
        {
            var message = new PayOrder
            {
                OrderId = orderId,
                Amount = amount
            };

            await publishEndpoint.Publish(message);
            return Accepted();
        }

        [HttpPost("/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            var message = new CancelOrder
            {
                OrderId = orderId
            };

            await publishEndpoint.Publish(message);
            return Accepted();
        }
    }
}
