using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DiscountController : ControllerBase
    {
        private IDiscountRepository discountRepository { get; set; }

        public DiscountController(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        [HttpGet("Available")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetAvailableAsync()
        {
            var discounts = await discountRepository.GetAvailableDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet("Upcoming")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetUpcomingAsync()
        {
            var discounts = await discountRepository.GetUpcomingDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet]
        [AuthorizedClient]
        public async Task<IActionResult> GetDiscountAsync([FromQuery] string code)
        {
            var discount = await discountRepository.GetDiscountAsync(code);

            return Ok(discount);
        }

        [HttpPost]
        [AuthorizedAdmin]
        public async Task<IActionResult> AddAsync([FromBody] DiscountDto discountDto)
        {
            try
            {
                await discountRepository.AddDiscountAsync(discountDto);

                return Ok();
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }
    }
}
