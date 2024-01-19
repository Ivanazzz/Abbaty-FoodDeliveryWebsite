using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IDiscountRepository
    {
        Task<List<DiscountDto>> GetAvailableDiscountsAsync();

        Task<List<DiscountDto>> GetUpcomingDiscountsAsync();

        Task AddDiscountAsync(DiscountDto discountDto);
    }
}
