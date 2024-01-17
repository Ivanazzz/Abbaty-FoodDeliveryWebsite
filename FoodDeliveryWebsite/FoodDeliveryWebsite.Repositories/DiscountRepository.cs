﻿using Microsoft.EntityFrameworkCore;

using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public DiscountRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task<DiscountDto[]> GetAvailableDiscountsAsync()
        {
            var currentDiscounts = await context.Discount.ToListAsync();

            List<DiscountDto> discounts = new List<DiscountDto>();
            foreach (var discount in currentDiscounts)
            {
                discounts.Add(new DiscountDto
                {
                    Code = discount.Code,
                    StartDate = discount.StartDate.ToShortDateString(),
                    ExpirationDate = discount.ExpirationDate.ToShortDateString(),
                    Percentage = discount.Percentage
                });
            }

            discounts = discounts.Where(d => d.Status == DiscountStatus.Available).ToList();

            return discounts.ToArray();
        }

        public async Task<DiscountDto[]> GetUpcomingDiscountsAsync()
        {
            var upcomingDiscounts = await context.Discount.Where(d => d.StartDate > DateTime.UtcNow).ToListAsync();

            List<DiscountDto> discounts = new List<DiscountDto>();
            foreach (var discount in upcomingDiscounts)
            {
                discounts.Add(new DiscountDto
                {
                    Code = discount.Code,
                    StartDate = discount.StartDate.ToShortDateString(),
                    ExpirationDate = discount.ExpirationDate.ToShortDateString(),
                    Percentage = discount.Percentage
                });
            }

            return discounts.ToArray();
        }

        public async Task AddDiscountAsync(DiscountDto discountDto)
        {
            if (discountDto.StartDate == null || discountDto.ExpirationDate == null)
            {
                throw new Exception("Start date and Expiration date are required.");
            }

            DateTime startDate = DateTime.Parse(discountDto.StartDate);
            DateTime expirationDate = DateTime.Parse(discountDto.ExpirationDate);

            if (startDate > expirationDate)
            {
                throw new Exception("Start date should not be greater than Expiration date.");
            }

            Discount discount = new Discount
            {
                Code = discountDto.Code,
                StartDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc),
                ExpirationDate = DateTime.SpecifyKind(expirationDate, DateTimeKind.Utc),
                Percentage = discountDto.Percentage
            };

            DiscountValidator validator = new DiscountValidator();
            validator.ValidateAndThrow(discount);

            context.Discount.Add(discount);
            await context.SaveChangesAsync();
        }
    }
}