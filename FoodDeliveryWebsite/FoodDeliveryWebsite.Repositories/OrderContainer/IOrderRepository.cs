using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderDto orderDto, string userEmail);
    }
}
