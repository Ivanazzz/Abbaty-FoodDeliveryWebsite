﻿using Microsoft.EntityFrameworkCore;

using AutoMapper;
using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IMapper mapper;
        private readonly FoodDeliveryWebsiteDbContext context;

        public AddressRepository(FoodDeliveryWebsiteDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<AddressDto>> GetAddressesAsync(string userEmail)
        {
            var user = await context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                return null;
            }

            var userAddresses = user.Addresses
                .Where(a => a.IsDeleted == false)
                .Select(a => mapper.Map<AddressDto>(a))
                .ToList();

            return userAddresses;
        }

        public async Task<AddressDto> GetSelectedAddressAsync(string userEmail, int id)
        {
            var user = await context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            var address = user.Addresses
                .FirstOrDefault(a => a.Id == id && a.IsDeleted == false);

            if (address == null)
            {
                throw new Exception("Invalid address");
            }

            return mapper.Map<AddressDto>(address);
        }

        public async Task<List<AddressDto>> AddAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            var address = mapper.Map<Address>(addressDto);
            address.UserId = user.Id;

            AddressValidator validator = new AddressValidator();
            validator.ValidateAndThrow(address);

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            return await GetAddressesAsync(userEmail);
        }

        public async Task UpdateAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == addressDto.Id && a.IsDeleted == false);

            if (address == null)
            {
                throw new Exception("Invalid address");
            }

            address.City = addressDto.City;
            address.Street = addressDto.Street;
            address.StreetNo = addressDto.StreetNo;
            address.Floor = address.Floor;
            address.ApartmentNo = addressDto.ApartmentNo;

            AddressValidator validator = new AddressValidator();
            validator.ValidateAndThrow(address);

            context.Addresses.Update(address);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(string userEmail, int id)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);

            if (address == null)
            {
                throw new Exception("Invalid address");
            }

            context.Addresses.Update(address);
            context.SaveChanges();
        }
    }
}
