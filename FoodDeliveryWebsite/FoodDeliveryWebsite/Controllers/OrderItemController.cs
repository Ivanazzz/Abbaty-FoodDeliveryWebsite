using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Repositories;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemRepository orderItemRepository { get; set; }
        private IConfiguration _config;

        public OrderItemController(IOrderItemRepository orderItemRepository, IConfiguration config)
        {
            this.orderItemRepository = orderItemRepository;
            _config = config;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var orderItems = await orderItemRepository.GetOrderItemsAsync(userEmail);

            return Ok(orderItems);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderItemRepository.AddOrderItemAsync(userEmail, productId, quantity);

            return Ok();
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromQuery] int orderItemId, [FromQuery] int quantity)
        {
            var orderItemDto = await orderItemRepository.UpdateOrderItemAsync(orderItemId, quantity);

            return Ok(orderItemDto);
        }

        [HttpDelete("Delete/{orderItemId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderItemId)
        {
            await orderItemRepository.DeleteOrderItemAsync(orderItemId);

            return Ok();
        }
    }
}
