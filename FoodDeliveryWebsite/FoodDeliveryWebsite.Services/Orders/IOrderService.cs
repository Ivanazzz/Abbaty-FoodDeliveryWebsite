using FoodDeliveryWebsite.Models.Dtos.OrderDtos;

namespace FoodDeliveryWebsite.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(string userEmail, OrderDto orderDto);
    }
}
