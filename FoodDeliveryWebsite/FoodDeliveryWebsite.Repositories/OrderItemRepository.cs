using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public OrderItemRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderItemDto>> GetOrderItemsAsync(string userEmail)
        {
            var user = await context.Users
                .Include(u => u.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user.");
            }

            var orderItemDtos = new List<OrderItemDto>();

            var orderItemsWithoutOrder = user.OrderItems.Where(oi => oi.OrderId == null);

            foreach (var orderItem in orderItemsWithoutOrder)
            {
                orderItemDtos.Add(new OrderItemDto
                {
                    Id = orderItem.Id,
                    Product = new ProductOrderDto
                    {
                        Id = orderItem.Product.Id,
                        Name = orderItem.Product.Name,
                        Price = orderItem.Product.Price,
                        Image = orderItem.Product.Image,
                        ImageName = orderItem.Product.ImageName,
                        ImageMimeType = orderItem.Product.ImageMimeType
                    },
                    ProductQuantity = orderItem.ProductQuantity,
                    Price = orderItem.Product.Price * orderItem.ProductQuantity
                });
            }

            return orderItemDtos;
        }

        public async Task AddOrderItemAsync(string userEmail, int productId, int quantity)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception("Invalid product.");
            }

            if (quantity < 1)
            {
                throw new Exception("Product quantity must be greater than 0.");
            }

            var user = await context.Users
                .Include(u => u.OrderItems)
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user.");
            }

            var orderItemExisting = user.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if (orderItemExisting != null)
            {
                orderItemExisting.ProductQuantity += quantity;
                context.OrderItems.Update(orderItemExisting);
                await context.SaveChangesAsync();

                return;
            }

            var orderItem = new OrderItem
            {
                UserId = user.Id,
                User = user,
                ProductId = productId,
                Product = product,
                ProductQuantity = quantity,
                Price = product.Price * quantity
            };

            context.OrderItems.Add(orderItem);
            await context.SaveChangesAsync();
        }

        public async Task<OrderItemDto> UpdateOrderItemAsync(int orderItemId, int quantity)
        {
            var orderItem = await context.OrderItems
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
            {
                throw new Exception("Invalid order item.");
            }

            orderItem.ProductQuantity = quantity;
            orderItem.Price = orderItem.Product.Price * orderItem.ProductQuantity;

            context.OrderItems.Update(orderItem);
            await context.SaveChangesAsync();

            var orderItemDto = new OrderItemDto
            {
                Id = orderItem.Id,
                Product = new ProductOrderDto
                {
                    Id = orderItem.Product.Id,
                    Name = orderItem.Product.Name,
                    Price = orderItem.Product.Price,
                    Image = orderItem.Product.Image,
                    ImageName = orderItem.Product.ImageName,
                    ImageMimeType = orderItem.Product.ImageMimeType
                },
                Price = orderItem.Price,
                ProductQuantity = orderItem.ProductQuantity
            };

            return orderItemDto;
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
            {
                throw new Exception("Invalid order item.");
            }

            context.OrderItems.Remove(orderItem);
            await context.SaveChangesAsync();
        }
    }
}
