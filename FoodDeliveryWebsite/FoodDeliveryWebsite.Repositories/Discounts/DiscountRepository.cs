using Microsoft.EntityFrameworkCore;

using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptionMessages;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;

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
            var currentDiscounts = await context.Discounts.ToListAsync();

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

            discounts = discounts
                .Where(d => d.Status == DiscountStatus.Available)
                .ToList();

            return discounts;
        }

        public async Task<List<DiscountDto>> GetUpcomingDiscountsAsync()
        {
            var upcomingDiscounts = await context.Discounts
                .Where(d => d.StartDate > DateTime.UtcNow)
                .ToListAsync();

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
            var discount = await context.Discounts
                .FirstOrDefaultAsync(d => d.Code == code);

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
                throw new BadRequestException(ExceptionMessages.StartExpirationDateRequired);
            }

            DateTime startDate = DateTime.Parse(discountDto.StartDate);
            DateTime expirationDate = DateTime.Parse(discountDto.ExpirationDate);

            if (startDate > expirationDate)
            {
                throw new BadRequestException(ExceptionMessages.StartDateGreaterThanExpirationDate);
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
            var result = validator.Validate(discount);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            context.Discounts.Add(discount);
            await context.SaveChangesAsync();
        }
    }
}
