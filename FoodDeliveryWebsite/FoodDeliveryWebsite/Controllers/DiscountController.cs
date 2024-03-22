using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DiscountController : ControllerBase
    {
        private IDiscountService discountService { get; set; }

        public DiscountController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }

        [HttpGet("Available")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetAvailableAsync()
        {
            var discounts = await discountService.GetAvailableDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet("Upcoming")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetUpcomingAsync()
        {
            var discounts = await discountService.GetUpcomingDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet]
        [AuthorizedClient]
        public async Task<IActionResult> GetDiscountAsync([FromQuery] string code)
        {
            var discount = await discountService.GetDiscountAsync(code);

            return Ok(discount);
        }

        [HttpPost]
        [AuthorizedAdmin]
        public async Task<IActionResult> AddAsync([FromBody] DiscountDto discountDto)
        {
            await discountService.AddDiscountAsync(discountDto);

            return Ok();
        }
    }
}
