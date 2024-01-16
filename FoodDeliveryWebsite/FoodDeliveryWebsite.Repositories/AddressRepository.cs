using FluentValidation;
using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;

        public AddressRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task<AddressDto[]> GetAddressesAsync(string userEmail)
        {
            var user = await context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            List<AddressDto> addressesDtos = new List<AddressDto>();

            foreach (var address in user.Addresses)
            {
                addressesDtos.Add(new AddressDto
                {
                    Id = address.Id,
                    City = address.City,
                    Street = address.Street,
                    StreetNo = address.StreetNo,
                    Floor = address.Floor,
                    ApartmentNo = address.ApartmentNo
                });
            }

            return addressesDtos.ToArray();
        }

        public async Task AddAddressAsync(AddressDto addressDto, string userEmail)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail && u.IsDeleted == false);

            Address address = new Address
            {
                City = addressDto.City,
                Street = addressDto.Street,
                StreetNo = addressDto.StreetNo,
                Floor = addressDto.Floor,
                ApartmentNo = addressDto.ApartmentNo,
                UserId = user.Id,
                User = user
            };

            AddressValidator validator = new AddressValidator();
            validator.ValidateAndThrow(address);

            context.Address.Add(address);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(AddressDto addressDto)
        {
            var address = await context.Address.FirstOrDefaultAsync(a => a.Id == addressDto.Id);

            address.City = addressDto.City;
            address.Street = addressDto.Street;
            address.StreetNo = addressDto.StreetNo;
            address.Floor = address.Floor;
            address.ApartmentNo = addressDto.ApartmentNo;

            AddressValidator validator = new AddressValidator();
            validator.ValidateAndThrow(address);

            context.Address.Update(address);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int id)
        {
            var address = await context.Address.FirstOrDefaultAsync(a => a.Id == id);

            context.Address.Remove(address);
            context.SaveChanges();
        }
    }
}
