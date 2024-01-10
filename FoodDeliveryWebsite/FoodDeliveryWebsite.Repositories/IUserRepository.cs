using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto> GetUserAsync(string email);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
