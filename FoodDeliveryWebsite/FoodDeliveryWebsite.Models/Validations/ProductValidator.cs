using FluentValidation;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.ProductConstants;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
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
        }
    }
}
