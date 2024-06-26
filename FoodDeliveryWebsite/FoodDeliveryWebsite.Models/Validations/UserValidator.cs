﻿using FluentValidation;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using static FoodDeliveryWebsite.Models.Constants.UserConstants;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithState(a => new BadRequestException("Името е задължително"))
                .MaximumLength(FirstNameMaxLength).WithState(a => new BadRequestException($"Името не трябва да надвишава {FirstNameMaxLength} символа"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Името трябва да бъде написано на кирилица"));

            RuleFor(u => u.LastName)
                .NotEmpty().WithState(a => new BadRequestException("Фамилията е задължителна"))
                .MaximumLength(LastNameMaxLength).WithState(a => new BadRequestException($"Фмаилията не трябва да надвишава {LastNameMaxLength} символа"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Фамилията трябва да бъде написано на кирилица"));

            RuleFor(u => u.Email)
                .NotEmpty().WithState(a => new BadRequestException("Имейлът е задължителен"))
                .Matches(EmailRegex).WithState(a => new BadRequestException("Имейлът е в невалиден формат"));

            RuleFor(u => u.Password)
                .NotEmpty().WithState(a => new BadRequestException("Паролата е задължителна"))
                .Matches(PasswordRegex).WithState(a => new BadRequestException(
                    "Паролата трябва:"
                    + Environment.NewLine
                    + "- да бъде поне 8 символа"
                    + Environment.NewLine
                    + "- да съдържа поне една главна буква"
                    + Environment.NewLine
                    + "- да съдържа поне една малка буква"
                    + Environment.NewLine
                    + "- да съдържа поне една цифра"
                    + Environment.NewLine
                    + "- да съдържа поне един специален символ"));

            RuleFor(u => u.PasswordConfirmation)
                .NotEmpty().WithState(a => new BadRequestException("Потвърждението на паролата е задължително"));

            RuleFor(u => u.PasswordConfirmation.Equals(u.Password))
                .NotEmpty().WithState(a => new BadRequestException("Паролите трябвва да съвпадат"));

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithState(a => new BadRequestException("Телефонният номер е задължителен"))
                .Matches(PhoneNumberRegex).WithState(a => new BadRequestException("Телефонният номер трябва да бъде във формат: +359XXXXXXXXX"));

            RuleFor(u => u.Gender)
                .IsInEnum().WithState(a => new BadRequestException("Невалиден пол"));
        }
    }
}
