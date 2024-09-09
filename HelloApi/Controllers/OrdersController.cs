using HelloApi.Cotracts;
using HelloApi.Orders.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestClient<VerifyOrder> requestClient;

        public OrdersController(IRequestClient<VerifyOrder> requestClient)
        {
            this.requestClient = requestClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var response = await requestClient.GetResponse<OrderResult, OrderNotFoundResult, Email>(new
            {
                Id = id,
                __Header_Promotion = "some-promotion-id"
            });

            if (response.Is(out Response<OrderResult> order))
            {
                return Ok(order.Message);
            }

            if (response.Is(out Response<OrderNotFoundResult> notFound))
            {
                return NotFound(notFound.Message);
            }

            return BadRequest();
        }
    }
}
