using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [AuthorizedClient]
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
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var orderItemDto = await orderItemRepository.UpdateOrderItemAsync(userEmail, orderItemId, quantity);

            return Ok(orderItemDto);
        }

        [HttpDelete("Delete/{orderItemId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderItemId)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderItemRepository.DeleteOrderItemAsync(userEmail, orderItemId);

            return Ok();
        }
    }
}
