using FoodDeliveryWebsite.Models.Dtos.OrderDtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(string userEmail, OrderDto orderDto);
    }
}
