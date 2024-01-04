using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private const int NameMaxLength = 30;
        private const int DescriptionMaxLength = 500;
        private const int PriceMinValue = 1;
        private const int QuantityMinValue = 1;
        private const int GramsMinValue = 1;

        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(NameMaxLength).WithMessage($"Name must not exceed {NameMaxLength} characters.")
                .Matches(@"^[А-я\s]+$").WithMessage("Name must be written in cyrilic.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(DescriptionMaxLength).WithMessage($"Description must not exceed ${DescriptionMaxLength} characters.")
                .Matches(@"^[А-я\s]+$").WithMessage("Description must be written in cyrilic.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(PriceMinValue).WithMessage($"Price must be greater than or equal to {PriceMinValue}.");

            RuleFor(p => p.Quantity)
                .GreaterThan(QuantityMinValue).WithMessage($"Quantity must be greater than or equal to {QuantityMinValue}.");

            RuleFor(p => p.Grams)
                .GreaterThan(GramsMinValue).WithMessage($"Grams must be greater than or equal to {GramsMinValue}.");

            RuleFor(p => p.Type)
                .IsInEnum().WithMessage("Invalid product type.");

            RuleFor(u => u.Status)
                .IsInEnum().WithMessage("Invalid status.");

            // Image Validator?????
            //RuleFor(u => u.Image)
            //    .NotEmpty().WithMessage("Image is required.");
        }
    }
}
