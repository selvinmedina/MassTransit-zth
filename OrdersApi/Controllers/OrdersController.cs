using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Services;
using OrdersApi.Infrastructure;
using OrdersApi.Models;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderModel model)
        {
            var createdOrder = await this.orderService.CreateOrder(model.ToOrder());
            return Ok();
        }
    }
}
