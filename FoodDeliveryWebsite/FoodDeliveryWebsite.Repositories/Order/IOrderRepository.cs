using FoodDeliveryWebsite.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderDto orderDto, string userEmail);
    }
}
