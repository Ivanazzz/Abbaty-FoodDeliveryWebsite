using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.CustomExceptions;
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

            try
            {
                var addresses = await addressService.GetAddressesAsync(userEmail);

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
                var address = await addressService.GetSelectedAddressAsync(userEmail, id);

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
                var addresses = await addressService.AddAddressAsync(userEmail, addressDto);

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


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AddressDto addressDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await addressService.UpdateAddressAsync(userEmail, addressDto);

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
                await addressService.DeleteAddressAsync(userEmail, id);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }
    }
}
