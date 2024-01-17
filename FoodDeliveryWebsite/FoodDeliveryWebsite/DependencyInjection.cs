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
                .AddScoped<IValidator<User>, UserValidator>();
                //.AddScoped(typeof(IGenderNGenreNRepository<>), typeof(GenderNRepository<>));

            return services;
        }
    }
}
