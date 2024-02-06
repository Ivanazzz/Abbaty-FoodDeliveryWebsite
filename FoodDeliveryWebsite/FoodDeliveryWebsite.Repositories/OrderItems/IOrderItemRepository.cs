using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemDto>> GetOrderItemsAsync(string userEmail);

        Task AddOrderItemAsync(string userEmail, int productId, int quantity);

        Task<OrderItemDto> UpdateOrderItemAsync(string userEmail, int productId, int quantity);

        Task DeleteOrderItemAsync(string userEmail, int productId);
    }
}
