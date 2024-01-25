﻿using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IAddressRepository
    {
        Task<List<AddressDto>> GetAddressesAsync(string userEmail);

        Task<AddressDto> GetSelectedAddressAsync(int id);

        Task<List<AddressDto>> AddAddressAsync(AddressDto addressDto, string userEmail);

        Task UpdateAddressAsync(AddressDto addressDto);

        Task DeleteAddressAsync(int id);
    }
}
