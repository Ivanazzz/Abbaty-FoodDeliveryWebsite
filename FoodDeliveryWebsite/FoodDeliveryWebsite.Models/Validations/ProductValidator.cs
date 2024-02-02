using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 500;
        private const decimal PriceMinValue = 0.01m;
        private const int GramsMinValue = 1;
        private const int GramsMaxValue = 2001;

        private const string NameRegex = @"^[А-яA-z\s]+$";
        private const string DescriptionRegex = @"^[\(\)-.,А-яA-z1-9\s]+$";

        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithState(a => new BadRequestException("Name is required"))
                .MaximumLength(NameMaxLength).WithState(a => new BadRequestException($"Name must not exceed {NameMaxLength} characters"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Name must be written in cyrilic"));

            RuleFor(p => p.Description)
                .NotEmpty().WithState(a => new BadRequestException("Description is required"))
                .MaximumLength(DescriptionMaxLength).WithState(a => new BadRequestException($"Description must not exceed ${DescriptionMaxLength} characters"))
                .Matches(DescriptionRegex).WithState(a => new BadRequestException("Description must be written in cyrilic"));

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(PriceMinValue).WithState(a => new BadRequestException($"Price must be greater than or equal to {PriceMinValue}"));

            RuleFor(p => p.Grams)
                .GreaterThan(GramsMinValue).WithState(a => new BadRequestException($"Grams must be greater than or equal to {GramsMinValue}"));

            RuleFor(p => p.Grams)
                .LessThan(GramsMinValue).WithState(a => new BadRequestException($"Grams must not exceed {GramsMaxValue}"));

            RuleFor(p => p.Type)
                .IsInEnum().WithState(a => new BadRequestException("Invalid product type"));

            RuleFor(u => u.Status)
                .IsInEnum().WithState(a => new BadRequestException("Invalid status"));

            // Image Validator?????
            //RuleFor(u => u.Image)
            //    .NotEmpty().WithMessage("Image is required.");
        }
    }
}
