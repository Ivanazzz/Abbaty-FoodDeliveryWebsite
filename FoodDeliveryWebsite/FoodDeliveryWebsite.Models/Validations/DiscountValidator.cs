using FluentValidation;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.DiscountConstants;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class DiscountValidator : AbstractValidator<Discount>
    {
        public DiscountValidator()
        {
            RuleFor(d => d.Code)
                .NotEmpty().WithState(a => new BadRequestException("Кодът е задължителен"))
                .Matches(CodeRegex).WithState(a => new BadRequestException("Невалиден формат на кода"));

            RuleFor(d => d.Percentage)
                .GreaterThanOrEqualTo(PercentageMinValue).WithState(a => new BadRequestException($"Процентът отстъпка трябва да бъде по-голям или равен на {PercentageMinValue}"))
                .LessThanOrEqualTo(PercentageMaxValue).WithState(a => new BadRequestException($"Процентът отстъпка не трябва да надвишава {PercentageMaxValue}"));

            RuleFor(d => d.StartDate)
                .GreaterThan(DateTime.UtcNow).WithState(a => new BadRequestException("Началната дата на отсъпката не може да бъде минала или днешна дата"));

            RuleFor(d => d.ExpirationDate)
                .GreaterThan(DateTime.UtcNow).WithState(a => new BadRequestException("Датата на изтичане на отсъпката не може да бъде минала или днешна дата"));
        }
    }
}
