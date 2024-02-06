using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(string userEmail, OrderDto orderDto);
    }
}
