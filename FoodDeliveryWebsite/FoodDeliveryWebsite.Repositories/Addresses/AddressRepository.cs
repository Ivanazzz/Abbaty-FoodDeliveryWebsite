using Microsoft.EntityFrameworkCore;

using AutoMapper;
using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptionMessages;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

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
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
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
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = user.Addresses
                .FirstOrDefault(a => a.Id == id 
                    && a.IsDeleted == false);

            if (address == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidAddress);
            }

            return mapper.Map<AddressDto>(address);
        }

        public async Task<List<AddressDto>> AddAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = mapper.Map<Address>(addressDto);
            address.UserId = user.Id;

            AddressValidator validator = new AddressValidator();
            var result = validator.Validate(address);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            return await GetAddressesAsync(userEmail);
        }

        public async Task UpdateAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == addressDto.Id 
                    && a.IsDeleted == false);

            if (address == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidAddress);
            }

            address.City = addressDto.City;
            address.Street = addressDto.Street;
            address.StreetNo = addressDto.StreetNo;
            address.Floor = address.Floor;
            address.ApartmentNo = addressDto.ApartmentNo;

            AddressValidator validator = new AddressValidator();
            var result = validator.Validate(address);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            context.Addresses.Update(address);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(string userEmail, int id)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = await context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id 
                    && a.IsDeleted == false);

            if (address == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidAddress);
            }

            address.IsDeleted = true;

            context.Addresses.Update(address);
            context.SaveChanges();
        }
    }
}
