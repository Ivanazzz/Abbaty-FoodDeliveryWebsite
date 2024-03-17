using AutoMapper;

using FoodDeliveryWebsite.Models.Dtos.AddressDtos;
using FoodDeliveryWebsite.Models.Dtos.DiscountDtos;
using FoodDeliveryWebsite.Models.Dtos.OrderDtos;
using FoodDeliveryWebsite.Models.Dtos.OrderItemDtos;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Address
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.StreetNo, opt => opt.MapFrom(src => src.StreetNo))
                .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Floor))
                .ForMember(dest => dest.ApartmentNo, opt => opt.MapFrom(src => src.ApartmentNo))
                .ReverseMap();

            // Discount
            CreateMap<Discount, DiscountDto>()
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToShortDateString()))
               .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToShortDateString()))
               .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage));

            CreateMap<DiscountDto, Discount>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => 
                    DateTime.SpecifyKind(DateTime.Parse(src.StartDate), DateTimeKind.Utc)))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => 
                    DateTime.SpecifyKind(DateTime.Parse(src.ExpirationDate), DateTimeKind.Utc)))
                .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage));

            CreateMap<Discount, DiscountOrderDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
               .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.Percentage));

            // Order
            CreateMap<OrderDto, Order>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.DiscountId, opt => opt.MapFrom(src => src.DiscountId))
               .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
               .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => Math.Round(src.TotalPrice, 2)))
               .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.DeliveryPrice));

            // OrderItem
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.ProductQuantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price * src.ProductQuantity))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            // Product
            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Grams, opt => opt.MapFrom(src => src.Grams))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageMimeType, opt => opt.MapFrom(src => src.ImageMimeType));

            CreateMap<ProductAddDto, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Grams, opt => opt.MapFrom(src => src.Grams))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Product, ProductOrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.ImageMimeType, opt => opt.MapFrom(src => src.ImageMimeType));

            // User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<UserRegistrationDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PasswordConfirmation, opt => opt.MapFrom(src => src.PasswordConfirmation))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => string.Concat(src.PhoneNumber.Where(c => !char.IsWhiteSpace(c)))))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.Client));
        }
    }
}
