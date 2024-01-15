﻿using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserUpdateValidator : AbstractValidator<User>
    {
        private const int FirstNameMaxLength = 20;
        private const int LastNameMaxLength = 20;

        private const string nameRegex = @"^[А-я\s]+$";
        private const string phoneNumberRegex = @"^\+359\d{9}$";

        public UserUpdateValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FirstNameMaxLength).WithMessage($"First name must not exceed {FirstNameMaxLength} characters.")
                .Matches(nameRegex).WithMessage("First name must be written in cyrilic.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(LastNameMaxLength).WithMessage($"Last name must not exceed {LastNameMaxLength} characters.")
                .Matches(nameRegex).WithMessage("Last name must be written in cyrilic.");


            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(phoneNumberRegex).WithMessage("Phone number must be in format: +359 XX XXXX XXX.");

            RuleFor(u => u.Gender)
                .IsInEnum().WithMessage("Invalid gender.");
        }
    }
}