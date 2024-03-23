using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class OrderController : ControllerBase
    {
        private const int DefaultCurrentPage = 1;
        private const int DefaultPageSize = 10;
        
        private IOrderService orderService { get; set; }

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [AuthorizedClient]
        public async Task<IActionResult> AddAsync([FromBody] OrderDto orderDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await orderService.AddOrderAsync(userEmail, orderDto);

            return Ok();
        }

        [HttpGet]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetAsync([FromQuery] int currentPage = DefaultCurrentPage, [FromQuery] int pageSize = DefaultPageSize)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var orders = await orderService.GetOrdersAsync(userEmail, currentPage, pageSize);

            return Ok(orders);
        }
    }
}
