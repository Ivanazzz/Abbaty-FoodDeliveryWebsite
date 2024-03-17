using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public AddressService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<AddressDto>> GetAddressesAsync(string userEmail)
        {
            var user = await repository.All<User>()
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var userAddresses = user.Addresses
                .Where(a => !a.IsDeleted)
                .AsQueryable()
                .ProjectTo<AddressDto>(mapper.ConfigurationProvider)
                .ToList();

            return userAddresses;
        }

        public async Task<AddressDto> GetSelectedAddressAsync(string userEmail, int id)
        {
            var user = await repository.All<User>()
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = user.Addresses
                .FirstOrDefault(a => a.Id == id 
                    && !a.IsDeleted);

            if (address == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidAddress);
            }

            return mapper.Map<AddressDto>(address);
        }

        public async Task<List<AddressDto>> AddAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await repository.All<User>()
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

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

            await repository.AddAsync(address);
            await repository.SaveChangesAsync();

            return await GetAddressesAsync(userEmail);
        }

        public async Task UpdateAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await repository.All<User>()
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = await repository.All<Address>()
                .FirstOrDefaultAsync(a => a.Id == addressDto.Id 
                    && !a.IsDeleted);

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

            await repository.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(string userEmail, int id)
        {
            var user = await repository.All<User>()
                .FirstOrDefaultAsync(u => u.Email == userEmail 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            var address = await repository.All<Address>()
                .FirstOrDefaultAsync(a => a.Id == id 
                    && !a.IsDeleted);

            if (address == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidAddress);
            }

            address.IsDeleted = true;

            await repository.SaveChangesAsync();
        }
    }
}
