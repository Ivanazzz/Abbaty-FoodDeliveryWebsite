using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IAddressRepository
    {
        Task<List<AddressDto>> GetAddressesAsync(string userEmail);

        Task AddAddressAsync(AddressDto addressDto, string userEmail);

        Task UpdateAddressAsync(AddressDto addressDto);

        Task DeleteAddressAsync(int id);
    }
}
