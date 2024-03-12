using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;

namespace FoodDeliveryWebsite.Services
{
    public interface IDiscountService
    {
        Task<List<DiscountDto>> GetAvailableDiscountsAsync();

        Task<List<DiscountDto>> GetUpcomingDiscountsAsync();

        Task<DiscountOrderDto> GetDiscountAsync(string code);

        Task AddDiscountAsync(DiscountDto discountDto);
    }
}
