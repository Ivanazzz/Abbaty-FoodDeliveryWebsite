using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]es")]
    [AuthorizedClient]
    public class AddressController : ControllerBase
    {
        private IAddressService addressService { get; set; }

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var addresses = await addressService.GetAddressesAsync(userEmail);

            return Ok(addresses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var address = await addressService.GetSelectedAddressAsync(userEmail, id);

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var addresses = await addressService.AddAddressAsync(userEmail, addressDto);

            return Ok(addresses);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await addressService.UpdateAddressAsync(userEmail, addressDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await addressService.DeleteAddressAsync(userEmail, id);

            return Ok();
        }
    }
}
