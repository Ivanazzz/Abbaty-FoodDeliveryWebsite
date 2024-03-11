using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [AuthorizedClient]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository { get; set; }

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] OrderDto orderDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await orderRepository.AddOrderAsync(userEmail, orderDto);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }
    }
}
