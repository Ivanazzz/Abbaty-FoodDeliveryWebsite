﻿using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAvailableAsync()
        {
            var discounts = await discountRepository.GetAvailableDiscountsAsync();

            return Ok(discounts);
        }

        [HttpGet("GetUpcoming")]
        public async Task<IActionResult> GetUpcomingAsync()
        {
            var discounts = await discountRepository.GetUpcomingDiscountsAsync();

            return Ok(discounts);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] DiscountDto discountDto)
        {
            await discountRepository.AddDiscountAsync(discountDto);

            return Ok();
        }
    }
}
