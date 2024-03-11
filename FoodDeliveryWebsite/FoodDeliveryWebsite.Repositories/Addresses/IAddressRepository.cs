using FoodDeliveryWebsite.Models.Dtos.AddressDtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IAddressRepository
    {
        Task<List<AddressDto>> GetAddressesAsync(string userEmail);

        Task<AddressDto> GetSelectedAddressAsync(string userEmail, int id);

        Task<List<AddressDto>> AddAddressAsync(string userEmail, AddressDto addressDto);

        Task UpdateAddressAsync(string userEmail, AddressDto addressDto);

        Task DeleteAddressAsync(string userEmail, int id);
    }
}
