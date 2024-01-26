using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using Microsoft.AspNetCore.Builder;

namespace FoodDeliveryWebsite.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public OrderRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task AddOrderAsync(OrderDto orderDto, string userEmail)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (orderDto == null)
            {
                throw new Exception("Invalid order.");
            }

            if (user == null)
            {
                throw new Exception("Invalid user.");
            }

            Order order = new Order
            {
                UserId = user.Id,
                AddressId = orderDto.AddressId,
                DiscountId = orderDto.DiscountId,
                TotalPrice = orderDto.TotalPrice,
                DeliveryPrice = orderDto.DeliveryPrice
            };

            context.Order.Add(order);
            await context.SaveChangesAsync();

            var orderItemsFromDatabase = await context.OrderItems
                .Where(oi => oi.UserId == user.Id)
                .ToListAsync();

            foreach (var orderItem in orderItemsFromDatabase)
            {
                orderItem.OrderId = order.Id;
            }

            await context.SaveChangesAsync();
        }
    }
}
