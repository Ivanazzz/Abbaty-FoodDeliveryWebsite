using Microsoft.EntityFrameworkCore;

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

        public async Task<List<DiscountDto>> GetAvailableDiscountsAsync()
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

            return discounts;
        }

        public async Task<List<DiscountDto>> GetUpcomingDiscountsAsync()
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

            return discounts;
        }

        public async Task<DiscountOrderDto> GetDiscountAsync(string code)
        {
            var discount = await context.Discount.FirstOrDefaultAsync(d => d.Code == code);

            var discountOrderDto = new DiscountOrderDto();

            if (discount != null)
            {
                discountOrderDto.Id = discount.Id;
                discountOrderDto.Code = discount.Code;
                discountOrderDto.Percentage = discount.Percentage;
            }

            return discountOrderDto;
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

            //var discounts = await context.Discount.FirstOrDefaultAsync(d => d.Code == discountDto.Code);

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
