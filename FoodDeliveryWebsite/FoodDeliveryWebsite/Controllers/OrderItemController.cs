using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [AuthorizedClient]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemService orderItemService { get; set; }

        public OrderItemController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var orderItems = await orderItemService.GetOrderItemsAsync(userEmail);

            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderItemService.AddOrderItemAsync(userEmail, productId, quantity);

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery] int orderItemId, [FromQuery] int quantity)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var orderItemDto = await orderItemService.UpdateOrderItemAsync(userEmail, orderItemId, quantity);

            return Ok(orderItemDto);
        }

        [HttpDelete("{orderItemId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderItemId)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderItemService.DeleteOrderItemAsync(userEmail, orderItemId);

            return Ok();
        }
    }
}
