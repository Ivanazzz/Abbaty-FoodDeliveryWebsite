using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Repositories;

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
                .AddScoped<IValidator<User>, UserValidator>()
                .AddScoped<IValidator<Address>, AddressValidator>()
                .AddScoped<IValidator<Discount>, DiscountValidator>();

            return services;
        }
    }
}
