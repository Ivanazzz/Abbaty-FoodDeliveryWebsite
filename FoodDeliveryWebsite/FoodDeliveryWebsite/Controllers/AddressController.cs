using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]es")]
    [AuthorizedClient]
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
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var address = await addressRepository.GetSelectedAddressAsync(userEmail, id);

            return Ok(address);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var addresses = await addressRepository.AddAddressAsync(userEmail, addressDto);

            return Ok(addresses);
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await addressRepository.UpdateAddressAsync(userEmail, addressDto);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await addressRepository.DeleteAddressAsync(userEmail, id);

            return Ok();
        }
    }
}
