using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class DiscountValidator : AbstractValidator<Discount>
    {
        public DiscountValidator()
        {
            RuleFor(d => d.Code)
                .Matches(@"^[A-z]+[0-9]+$").WithMessage("Not available discount.");
        }
    }
}
