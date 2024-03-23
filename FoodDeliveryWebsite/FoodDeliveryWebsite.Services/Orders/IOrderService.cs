using FoodDeliveryWebsite.Models.Dtos.CommonDtos;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;

namespace FoodDeliveryWebsite.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(string userEmail, OrderDto orderDto);

        Task<SearchResultDto<OrderInfoDto>> GetOrdersAsync(string userEmail, int currentPage, int pageSize);
    }
}
