using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Repositories.Address;
using FoodDeliveryWebsite.Repositories.Discount;
using FoodDeliveryWebsite.Repositories.Order;
using FoodDeliveryWebsite.Repositories.OrderItem;
using FoodDeliveryWebsite.Repositories.Product;
using FoodDeliveryWebsite.Repositories.User;

namespace FoodDeliveryWebsite
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IDiscountRepository, DiscountRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IOrderItemRepository, OrderItemRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IValidator<User>, UserValidator>()
                .AddScoped<IValidator<Address>, AddressValidator>()
                .AddScoped<IValidator<Discount>, DiscountValidator>();

            return services;
        }
    }
}
