using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public const int CityMaxLength = 20;
        internal const int StreetMaxLength = 30;
        private const int StreetNoMinValue = 1;
        private const int FloorMinValue = 0;
        private const int ApartmentNoMinValue = 1;

        private const string CityRegex = @"^[А-я\s]+$";
        private const string StreetRegex = @"^[А-я\s]+$";

        public AddressValidator()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithState(a => new BadRequestException("City is required"))
                .MaximumLength(CityMaxLength).WithState(a => new BadRequestException($"City must not exceed {CityMaxLength} characters"))
                .Matches(CityRegex).WithState(a => new BadRequestException("City must be written in cyrilic"));

            RuleFor(a => a.Street)
                .NotEmpty().WithState(a => new BadRequestException("Street is required"))
                .MaximumLength(StreetMaxLength).WithState(a => new BadRequestException($"Street must not exceed ${StreetMaxLength} characters"))
                .Matches(StreetRegex).WithState(a => new BadRequestException("Street must be written in cyrilic"));

            RuleFor(a => a.StreetNo)
                .NotNull().WithState(a => new BadRequestException("Street No is required"))
                .GreaterThanOrEqualTo(StreetNoMinValue).WithState(a => new BadRequestException($"StreetNo must be greater than or equal to {StreetNoMinValue}"));

            RuleFor(a => a.Floor)
                .GreaterThanOrEqualTo(FloorMinValue).WithState(a => new BadRequestException($"Floor must be greater than or equal to {FloorMinValue}"));

            RuleFor(a => a.ApartmentNo)
                .GreaterThanOrEqualTo(ApartmentNoMinValue).WithState(a => new BadRequestException($"ApartmentNo must be greater than or equal to {ApartmentNoMinValue}"));
        }
    }
}
