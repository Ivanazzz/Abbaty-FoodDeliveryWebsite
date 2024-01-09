using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task LoginAsync(UserLoginDto userLoginDto);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
