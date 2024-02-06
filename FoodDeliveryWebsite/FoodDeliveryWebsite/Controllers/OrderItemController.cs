using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [AuthorizedClient]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemRepository orderItemRepository { get; set; }

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                var orderItems = await orderItemRepository.GetOrderItemsAsync(userEmail);

                return Ok(orderItems);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await orderItemRepository.AddOrderItemAsync(userEmail, productId, quantity);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromQuery] int orderItemId, [FromQuery] int quantity)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                var orderItemDto = await orderItemRepository.UpdateOrderItemAsync(userEmail, orderItemId, quantity);

                return Ok(orderItemDto);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpDelete("{orderItemId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int orderItemId)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await orderItemRepository.DeleteOrderItemAsync(userEmail, orderItemId);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }
    }
}
