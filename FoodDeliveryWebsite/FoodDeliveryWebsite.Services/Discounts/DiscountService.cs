using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public DiscountService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<DiscountDto>> GetAvailableDiscountsAsync()
        {
            var discounts = await repository.All<Discount>()
                .ProjectTo<DiscountDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            var availableDiscounts = discounts
                .Where(d => d.Status == DiscountStatus.Available)
                .ToList();

            return availableDiscounts;
        }

        public async Task<List<DiscountDto>> GetUpcomingDiscountsAsync()
        {
            var upcomingDiscounts = await repository.All<Discount>()
                .Where(d => d.StartDate > DateTime.UtcNow)
                .ProjectTo<DiscountDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return upcomingDiscounts;
        }

        public async Task<DiscountOrderDto> GetDiscountAsync(string code)
        {
            var discount = await repository.All<Discount>()
                .ProjectTo<DiscountOrderDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(d => d.Code == code);

            return discount != null 
                ? discount 
                : new DiscountOrderDto();
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

            var discount = mapper.Map<Discount>(discountDto);

            DiscountValidator validator = new DiscountValidator();
            var result = validator.Validate(discount);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            await repository.AddAsync(discount);
            await repository.SaveChangesAsync();
        }
    }
}
