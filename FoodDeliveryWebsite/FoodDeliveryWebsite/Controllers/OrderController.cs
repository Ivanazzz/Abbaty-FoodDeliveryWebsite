using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository { get; set; }
        private IConfiguration _config;

        public OrderController(IOrderRepository orderRepository, IConfiguration config)
        {
            this.orderRepository = orderRepository;
            _config = config;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] OrderDto orderDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderRepository.AddOrderAsync(orderDto, userEmail);

            return Ok();
        }
    }
}
