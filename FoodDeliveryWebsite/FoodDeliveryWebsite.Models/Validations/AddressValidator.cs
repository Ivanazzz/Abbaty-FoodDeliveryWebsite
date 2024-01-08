using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class AddressValidator : AbstractValidator<Address>
    {
        private const int CityMaxLength = 20;
        private const int StreetMaxLength = 30;
        private const int StreetNoMinValue = 1;
        private const int FloorMinValue = 1;
        private const int AppartmentNoMinValue = 1;

        private const string cityRegex = @"^[А-я\s]+$";
        private const string streetRegex = @"^[А-я\s]+$";

        public AddressValidator()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(CityMaxLength).WithMessage($"City must not exceed {CityMaxLength} characters.")
                .Matches(cityRegex).WithMessage("City must be written in cyrilic.");

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(StreetMaxLength).WithMessage($"Street must not exceed ${StreetMaxLength} characters.")
                .Matches(streetRegex).WithMessage("Street must be written in cyrilic.");

            RuleFor(a => a.StreetNo)
                .GreaterThanOrEqualTo(StreetNoMinValue).WithMessage($"StreetNo must be greater than or equal to {StreetNoMinValue}.");

            RuleFor(a => a.Floor)
                .GreaterThanOrEqualTo(FloorMinValue).WithMessage($"Floor must be greater than or equal to {FloorMinValue}.");

            RuleFor(a => a.ApartmentNo)
                .GreaterThanOrEqualTo(AppartmentNoMinValue).WithMessage($"ApartmentNo must be greater than or equal to {AppartmentNoMinValue}.");
        }
    }
}
