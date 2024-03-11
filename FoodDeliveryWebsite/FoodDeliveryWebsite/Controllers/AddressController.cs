using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]es")]
    [AuthorizedClient]
    public class AddressController : ControllerBase
    {
        private IAddressRepository addressRepository { get; set; }

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                var addresses = await addressRepository.GetAddressesAsync(userEmail);

                return Ok(addresses);
            }
            catch(NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                var address = await addressRepository.GetSelectedAddressAsync(userEmail, id);

                return Ok(address);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                var addresses = await addressRepository.AddAddressAsync(userEmail, addressDto);

                return Ok(addresses);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }


        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await addressRepository.UpdateAddressAsync(userEmail, addressDto);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await addressRepository.DeleteAddressAsync(userEmail, id);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }
    }
}
