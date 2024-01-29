using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Repositories.User
{
    public interface IUserRepository
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto> GetUserAsync(string email);

        Task UpdateUserAsync(UserDto userDto, string email);

        Task DeleteUserAsync(string email);
    }
}
