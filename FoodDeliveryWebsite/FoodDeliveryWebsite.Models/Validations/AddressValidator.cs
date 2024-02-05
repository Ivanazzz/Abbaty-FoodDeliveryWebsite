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
                .NotEmpty().WithState(a => new BadRequestException("Градът е задължителен"))
                .MaximumLength(CityMaxLength).WithState(a => new BadRequestException($"Градът не трява да надвишава {CityMaxLength} символа"))
                .Matches(CityRegex).WithState(a => new BadRequestException("Градът трябва да бъде написан на кирирлица"));

            RuleFor(a => a.Street)
                .NotEmpty().WithState(a => new BadRequestException("Улицата е задължителна"))
                .MaximumLength(StreetMaxLength).WithState(a => new BadRequestException($"Улицата не трява да надвишава ${StreetMaxLength} символа"))
                .Matches(StreetRegex).WithState(a => new BadRequestException("Улицата трябва да бъде написана на кирирлица"));

            RuleFor(a => a.StreetNo)
                .NotNull().WithState(a => new BadRequestException("Номерът на улицата е задължителен"))
                .GreaterThanOrEqualTo(StreetNoMinValue).WithState(a => new BadRequestException($"Номерът на улицата трябва да бъде по-голям или равен на {StreetNoMinValue}"));

            RuleFor(a => a.Floor)
                .GreaterThanOrEqualTo(FloorMinValue).WithState(a => new BadRequestException($"Етажът трябва да бъде по-голям или равен на {FloorMinValue}"));

            RuleFor(a => a.ApartmentNo)
                .GreaterThanOrEqualTo(ApartmentNoMinValue).WithState(a => new BadRequestException($"Номерът на апартамента трябва да бъде по-голям или равен на  {ApartmentNoMinValue}"));
        }
    }
}
