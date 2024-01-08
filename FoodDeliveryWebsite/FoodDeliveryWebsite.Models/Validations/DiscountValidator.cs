using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class DiscountValidator : AbstractValidator<Discount>
    {
        private const string codeRegex = @"^[A-z]+[0-9]+$";

        public DiscountValidator()
        {
            RuleFor(d => d.Code)
                .Matches(codeRegex).WithMessage("Not available discount.");
        }
    }
}
