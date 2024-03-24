using FluentValidation;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Entities;
using static FoodDeliveryWebsite.Models.Constants.UserConstants;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserUpdateValidator : AbstractValidator<User>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithState(a => new BadRequestException("Името е задължително"))
                .MaximumLength(FirstNameMaxLength).WithState(a => new BadRequestException($"Името не трябва да надвишава {FirstNameMaxLength} символа"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Името трябва да бъде написано на кирирлица"));

            RuleFor(u => u.LastName)
                .NotEmpty().WithState(a => new BadRequestException("Фамилията е задължителна"))
                .MaximumLength(LastNameMaxLength).WithState(a => new BadRequestException($"Фамилията не трябва да надвишава {LastNameMaxLength} символа"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Фамилията трябва да бъде написана на кирилица"));


            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithState(a => new BadRequestException("Телефонният номер е задължителен"))
                .Matches(PhoneNumberRegex).WithState(a => new BadRequestException("Телефонният номер трябва да бъде във формат: +359XXXXXXXXX"));

            RuleFor(u => u.Gender)
                .IsInEnum().WithState(a => new BadRequestException("Невалиден пол"));
        }
    }
}
