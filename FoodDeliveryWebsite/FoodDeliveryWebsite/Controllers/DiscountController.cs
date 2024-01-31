using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;
using Microsoft.AspNetCore.Authorization;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DiscountController : ControllerBase
    {
        private IDiscountRepository discountRepository { get; set; }
        private IConfiguration _config;

        public DiscountController(IDiscountRepository discountRepository, IConfiguration config)
        {
            this.discountRepository = discountRepository;
            _config = config;
        }

        [HttpGet("GetAvailable")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetAvailableAsync()
        {
            var discounts = await discountRepository.GetAvailableDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet("GetUpcoming")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetUpcomingAsync()
        {
            var discounts = await discountRepository.GetUpcomingDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet("GetDiscount")]
        [AuthorizedClient]
        public async Task<IActionResult> GetDiscountAsync([FromQuery] string code)
        {
            var discount = await discountRepository.GetDiscountAsync(code);

            return Ok(discount);
        }

        [HttpPost("Add")]
        [AuthorizedAdmin]
        public async Task<IActionResult> AddAsync([FromBody] DiscountDto discountDto)
        {
            await discountRepository.AddDiscountAsync(discountDto);

            return Ok();
        }
    }
}
