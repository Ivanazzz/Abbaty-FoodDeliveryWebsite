using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Services
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegistrationDto userRegistrationDto);

        Task<User> LoginAsync(UserLoginDto userLoginDto);

        Task<UserDto> GetUserAsync(string email);

        Task UpdateUserAsync(string email, UserDto userDto);

        Task DeleteUserAsync(string email);
    }
}
