using System.Transactions;

using Microsoft.EntityFrameworkCore;
using AutoMapper;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptionMessages;

namespace FoodDeliveryWebsite.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper mapper;
        private readonly FoodDeliveryWebsiteDbContext context;

        public OrderRepository(FoodDeliveryWebsiteDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task AddOrderAsync(string userEmail, OrderDto orderDto)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (orderDto == null)
                    {
                        throw new NotFoundException(ExceptionMessages.InvalidOrder);
                    }

                    var user = await context.Users
                        .FirstOrDefaultAsync(u => u.Email == userEmail
                            && u.IsDeleted == false);

                    if (user == null)
                    {
                        throw new NotFoundException(ExceptionMessages.InvalidUser);
                    }

                    var order = mapper.Map<Order>(orderDto);
                    order.TotalPrice = Math.Round(orderDto.TotalPrice, 2);
                    order.UserId = user.Id;
                    order.OrderItems = null;

                    if (order.AddressId == 0 || order.UserId == 0 || order.TotalPrice <= 0)
                    {
                        throw new NotFoundException(ExceptionMessages.InvalidOrder);
                    }

                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    var orderItemsFromDatabase = await context.OrderItems
                        .Where(oi => oi.UserId == user.Id 
                            && oi.OrderId == null)
                        .ToListAsync();

                    foreach (var orderItem in orderItemsFromDatabase)
                    {
                        orderItem.OrderId = order.Id;
                    }

                    await context.SaveChangesAsync();

                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
