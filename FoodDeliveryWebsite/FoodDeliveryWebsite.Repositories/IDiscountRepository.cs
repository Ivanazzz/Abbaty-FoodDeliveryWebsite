using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IDiscountRepository
    {
        Task<DiscountDto[]> GetAvailableDiscountsAsync();

        Task<DiscountDto[]> GetUpcomingDiscountsAsync();

        Task AddDiscountAsync(DiscountDto discountDto);
    }
}
