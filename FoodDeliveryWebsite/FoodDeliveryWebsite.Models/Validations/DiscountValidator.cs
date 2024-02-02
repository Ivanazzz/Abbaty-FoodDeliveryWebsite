using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class DiscountValidator : AbstractValidator<Discount>
    {
        private const int PercentageMinValue = 1;
        private const int PercentageMaxValue = 100;

        private const string CodeRegex = @"^[A-z]+[0-9]+$";

        public DiscountValidator()
        {
            RuleFor(d => d.Code)
                .NotEmpty().WithState(a => new BadRequestException("Code is required"))
                .Matches(CodeRegex).WithState(a => new BadRequestException("Invalid code format"));

            RuleFor(d => d.Percentage)
                .GreaterThanOrEqualTo(PercentageMinValue).WithState(a => new BadRequestException($"Discount percentage must be greater than {PercentageMinValue}"));

            RuleFor(d => d.Percentage)
                .LessThanOrEqualTo(PercentageMaxValue).WithState(a => new BadRequestException($"Discount percentage must not exceed {PercentageMaxValue}"));

            RuleFor(d => d.StartDate)
                .GreaterThan(DateTime.UtcNow).WithState(a => new BadRequestException("Discount start date must be greater than today"));

            RuleFor(d => d.ExpirationDate)
                .GreaterThan(DateTime.UtcNow).WithState(a => new BadRequestException("Discount expiration date must be greater than today"));
        }
    }
}
