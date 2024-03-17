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
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public OrderService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
                        .SingleOrDefaultAsync(u => u.Email == userEmail
                            && !u.IsDeleted);

                    if (user == null)
                    {
                        throw new NotFoundException(ExceptionMessages.InvalidUser);
                    }

                    var order = mapper.Map<Order>(orderDto);
                    order.UserId = user.Id;
                    order.OrderItems = null;

                    if (order.AddressId == 0 || order.UserId == 0 || order.TotalPrice <= 0)
                    {
                        throw new NotFoundException(ExceptionMessages.InvalidOrder);
                    }

                    await repository.AddAsync(order);
                    await repository.SaveChangesAsync();

                    await repository.All<OrderItem>()
                        .Where(oi => oi.UserId == user.Id
                            && oi.OrderId == null)
                        .ForEachAsync(oi => oi.OrderId = order.Id);

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
