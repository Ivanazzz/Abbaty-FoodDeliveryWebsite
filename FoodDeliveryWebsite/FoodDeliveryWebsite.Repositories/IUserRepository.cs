using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IUserRepository
    {
        Task Register(UserRegistrationDto userRegistrationDto);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
