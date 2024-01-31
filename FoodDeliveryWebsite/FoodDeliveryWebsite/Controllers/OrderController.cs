using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [AuthorizedClient]
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

            await orderRepository.AddOrderAsync(userEmail, orderDto);

            return Ok();
        }
    }
}
