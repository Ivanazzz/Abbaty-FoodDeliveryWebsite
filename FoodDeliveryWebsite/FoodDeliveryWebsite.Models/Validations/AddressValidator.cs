using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        private const int CityMaxLength = 20;
        private const int StreetMaxLength = 30;
        private const int StreetNoMinValue = 1;
        private const int FloorMinValue = 0;
        private const int ApartmentNoMinValue = 1;

        private const string CityRegex = @"^[А-я\s]+$";
        private const string StreetRegex = @"^[А-я\s]+$";

        public AddressValidator()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(CityMaxLength).WithMessage($"City must not exceed {CityMaxLength} characters.")
                .Matches(CityRegex).WithMessage("City must be written in cyrilic.");

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(StreetMaxLength).WithMessage($"Street must not exceed ${StreetMaxLength} characters.")
                .Matches(StreetRegex).WithMessage("Street must be written in cyrilic.");

            RuleFor(a => a.StreetNo)
                .NotNull().WithMessage("StreetNo is required.")
                .GreaterThanOrEqualTo(StreetNoMinValue).WithMessage($"StreetNo must be greater than or equal to {StreetNoMinValue}.");

            RuleFor(a => a.Floor)
                .GreaterThanOrEqualTo(FloorMinValue).WithMessage($"Floor must be greater than or equal to {FloorMinValue}.");

            RuleFor(a => a.ApartmentNo)
                .GreaterThanOrEqualTo(ApartmentNoMinValue).WithMessage($"ApartmentNo must be greater than or equal to {ApartmentNoMinValue}.");
        }
    }
}
