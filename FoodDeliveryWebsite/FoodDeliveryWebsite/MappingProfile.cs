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

            // Order
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            //CreateMap<OrderItem, OrderItemDto>()
                //.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            // Product
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductAddDto, Product>();
            CreateMap<Product, ProductAddDto>();
            CreateMap<Product, ProductOrderDto>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
