using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public UserRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task Register(UserRegistrationDto userRegistrationDto)
        {
            User user = new User
            {
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Gender = userRegistrationDto.Gender,
                Email = userRegistrationDto.Email,
                Password = userRegistrationDto.Password,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                Addresses = userRegistrationDto.Addresses,
                Role = UserRole.Client
            };

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrow(user);

            bool userExists = await context.User.FirstOrDefaultAsync(u => u.Email == user.Email) != null 
                ? true 
                : false;

            if (userExists)
            {
                //return "User with the given email already exists.";
            }

            context.User.Add(user);
            await context.SaveChangesAsync();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
