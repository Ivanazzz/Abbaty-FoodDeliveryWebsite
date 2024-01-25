using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]es")]
    public class AddressController : ControllerBase
    {
        private IAddressRepository addressRepository { get; set; }
        private IConfiguration _config;

        public AddressController(IAddressRepository addressRepository, IConfiguration config)
        {
            this.addressRepository = addressRepository;
            _config = config;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var addresses = await addressRepository.GetAddressesAsync(userEmail);

            return Ok(addresses);
        }

        [HttpGet("GetSelected/{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var product = await addressRepository.GetSelectedAddressAsync(id);

            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var addresses = await addressRepository.AddAddressAsync(addressDto, userEmail);

            return Ok(addresses);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto addressDto)
        {
            await addressRepository.UpdateAddressAsync(addressDto);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            await addressRepository.DeleteAddressAsync(id);

            return Ok();
        }
    }
}
