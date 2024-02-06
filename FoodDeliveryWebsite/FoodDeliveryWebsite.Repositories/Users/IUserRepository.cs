using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto> GetUserAsync(string email);

        Task UpdateUserAsync(string email, UserDto userDto);

        Task DeleteUserAsync(string email);
    }
}
