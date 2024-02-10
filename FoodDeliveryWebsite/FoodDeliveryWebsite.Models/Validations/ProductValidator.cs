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
        private const string DescriptionRegex = @"^[\(\)-.',А-яA-z1-9\s]+$";

        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithState(a => new BadRequestException("Името е задължително"))
                .MaximumLength(NameMaxLength).WithState(a => new BadRequestException($"Името не трябва да надвишава {NameMaxLength} символа"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Името трябва да бъде написано на кирилица"));

            RuleFor(p => p.Description)
                .NotEmpty().WithState(a => new BadRequestException("Описанието е задължително"))
                .MaximumLength(DescriptionMaxLength).WithState(a => new BadRequestException($"Описанието не трябва да надвишава ${DescriptionMaxLength} символа"))
                .Matches(DescriptionRegex).WithState(a => new BadRequestException("Описанието трябва да бъде написано на кирилица"));

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(PriceMinValue).WithState(a => new BadRequestException($"Цената трябва да бъде по-голяма или равна на {PriceMinValue}"));

            RuleFor(p => p.Grams)
                .GreaterThan(GramsMinValue).WithState(a => new BadRequestException($"Грамажът трябва да бъде по-голям или равен на {GramsMinValue}"));

            RuleFor(p => p.Grams)
                .LessThan(GramsMaxValue).WithState(a => new BadRequestException($"Грамажът не тряба да надвишава {GramsMaxValue}"));

            RuleFor(p => p.Type)
                .IsInEnum().WithState(a => new BadRequestException("Невалиден тип на продукта"));

            RuleFor(u => u.Status)
                .IsInEnum().WithState(a => new BadRequestException("Невалиден статус на продукта"));

            // Image Validator?????
            //RuleFor(u => u.Image)
            //    .NotEmpty().WithMessage("Image is required.");
        }
    }
}
