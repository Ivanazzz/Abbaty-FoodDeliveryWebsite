using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IRepository repository;

        public OrderService(IRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
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

                    var user = await repository.All<User>()
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

                    await repository.AddAsync(order);
                    await repository.SaveChangesAsync();

                    var orderItemsFromDatabase = await repository.All<OrderItem>()
                        .Where(oi => oi.UserId == user.Id 
                            && oi.OrderId == null)
                        .ToListAsync();

                    foreach (var orderItem in orderItemsFromDatabase)
                    {
                        orderItem.OrderId = order.Id;
                    }

                    await repository.SaveChangesAsync();

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
