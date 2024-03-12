using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped<IRepository, Repository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAddressService, AddressService>()
                .AddScoped<IDiscountService, DiscountService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderItemService, OrderItemService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IValidator<User>, UserValidator>()
                .AddScoped<IValidator<Address>, AddressValidator>()
                .AddScoped<IValidator<Discount>, DiscountValidator>();

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }
    }
}
