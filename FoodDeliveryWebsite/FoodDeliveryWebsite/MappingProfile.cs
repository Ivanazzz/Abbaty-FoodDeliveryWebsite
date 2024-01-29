using AutoMapper;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Address
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();

            // Discount
            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountDto, Discount>();
        }
    }
}
