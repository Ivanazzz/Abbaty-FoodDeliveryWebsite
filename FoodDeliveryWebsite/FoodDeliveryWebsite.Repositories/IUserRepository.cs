using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
