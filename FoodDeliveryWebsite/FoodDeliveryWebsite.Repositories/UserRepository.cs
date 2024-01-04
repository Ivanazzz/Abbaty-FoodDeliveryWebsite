using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public async Task AddUser(User user)
        {
            UserValidator validator = new UserValidator();
            validator.ValidateAndThrow(user);

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
