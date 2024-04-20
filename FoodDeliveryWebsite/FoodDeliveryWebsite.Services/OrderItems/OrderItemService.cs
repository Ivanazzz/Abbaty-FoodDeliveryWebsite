using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.OrderItemDtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public OrderItemService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<OrderItemDto>> GetOrderItemsAsync(string userEmail)
        {
            var user = await repository.AllReadOnly<User>()
                .Include(u => u.OrderItems)
                .ThenInclude(oi => oi.Product)
                .SingleOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var orderItems = user.OrderItems
                .AsQueryable()
                .Where(oi => oi.OrderId == null)
                .ProjectTo<OrderItemDto>(mapper.ConfigurationProvider)
                .ToList();

            return orderItems;
        }

        public async Task AddOrderItemAsync(string userEmail, int productId, int quantity)
        {
            var user = await repository.All<User>()
                .Include(u => u.OrderItems)
                .SingleOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var product = await repository.AllReadOnly<Product>()
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidProduct);
            }

            if (quantity < 1)
            {
                throw new BadRequestException(ExceptionMessages.ProductQuantityLessThan1);
            }

            var orderItemExisting = user.OrderItems
                .SingleOrDefault(oi => oi.ProductId == productId 
                    && oi.OrderId == null);

            if (orderItemExisting != null)
            {
                orderItemExisting.ProductQuantity += quantity;

                await repository.SaveChangesAsync();

                return;
            }

            var orderItem = new OrderItem
            {
                UserId = user.Id,
                User = user,
                ProductId = productId,
                ProductQuantity = quantity,
                Price = product.Price * quantity
            };

            await repository.AddAsync(orderItem);
            await repository.SaveChangesAsync();
        }

        public async Task<OrderItemDto> UpdateOrderItemAsync(string userEmail, int orderItemId, int quantity)
        {
            var user = await repository.AllReadOnly<User>()
                .SingleOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var orderItem = await repository.All<OrderItem>()
                .Include(oi => oi.Product)
                .SingleOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidOrderItem);
            }

            if (orderItem.OrderId != null)
            {
                throw new BadRequestException(ExceptionMessages.InvalidOrderItem);
            }

            if (user.Id != orderItem.UserId)
            {
                throw new NotFoundException(ExceptionMessages.InvalidOrderItemForUser);
            }

            orderItem.ProductQuantity = quantity;
            orderItem.Price = orderItem.Product.Price * orderItem.ProductQuantity;

            await repository.SaveChangesAsync();

            var orderItemDto = mapper.Map<OrderItemDto>(orderItem);

            return orderItemDto;
        }

        public async Task DeleteOrderItemAsync(string userEmail, int orderItemId)
        {
            var orderItem = await repository.All<OrderItem>()
                .SingleOrDefaultAsync(oi => oi.Id == orderItemId);

            if (orderItem == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidOrderItem);
            }

            if (orderItem.OrderId != null)
            {
                throw new BadRequestException(ExceptionMessages.InvalidOrderItem);
            }

            var user = await repository.AllReadOnly<User>()
                .SingleOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            if (user.Id != orderItem.UserId)
            {
                throw new NotFoundException(ExceptionMessages.InvalidOrderItemForUser);
            }

            repository.Delete(orderItem);
            await repository.SaveChangesAsync();
        }
    }
}
