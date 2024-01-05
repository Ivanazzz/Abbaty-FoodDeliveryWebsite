﻿using FluentValidation;
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
                //.AddScoped<IBookRepository, BookRepository>()
                //.AddScoped<ILibraryRepository, LibraryRepository>()
                .AddScoped<IValidator<User>, UserValidator>();
                //.AddScoped(typeof(IGenderNGenreNRepository<>), typeof(GenderNRepository<>));

            return services;
        }
    }
}
