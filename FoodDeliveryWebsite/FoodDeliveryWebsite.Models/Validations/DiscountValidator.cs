using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

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
                .NotEmpty().WithMessage("Code is required.")
                .Matches(CodeRegex).WithMessage("Not available discount.");

            RuleFor(d => d.Percentage)
                .GreaterThanOrEqualTo(PercentageMinValue).WithMessage($"Discount percentage must be greater than {PercentageMinValue}.");

            RuleFor(d => d.Percentage)
                .LessThanOrEqualTo(PercentageMaxValue).WithMessage($"Discount percentage must not exceed {PercentageMinValue}.");

            RuleFor(d => d.StartDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Discount start date must be greater than today.");

            RuleFor(d => d.ExpirationDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Discount expiration date must be greater than today.");
        }
    }
}
