﻿using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        private const int FirstNameMaxLength = 20;
        private const int LastNameMaxLength = 20;

        private const string NameRegex = @"^[А-я\s]+$";
        private const string PasswordRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.,#?!@$%^&*-]).{8,}$";
        private const string PhoneNumberRegex = @"^\+359\d{9}$";
        // private const string emailRegex = @"/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/$";

        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FirstNameMaxLength).WithMessage($"First name must not exceed {FirstNameMaxLength} characters.")
                .Matches(NameRegex).WithMessage("First name must be written in cyrilic.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(LastNameMaxLength).WithMessage($"Last name must not exceed {LastNameMaxLength} characters.")
                .Matches(NameRegex).WithMessage("Last name must be written in cyrilic.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(PasswordRegex).WithMessage(
                    "Password must have:"
                    + Environment.NewLine
                    + "- Minimum 8 characters in length"
                    + Environment.NewLine
                    + "- At least one uppercase letter"
                    + Environment.NewLine
                    + "- At least one lowercase letter"
                    + Environment.NewLine
                    + "- At least one digit"
                    + Environment.NewLine
                    + "- At least one special character");

            RuleFor(u => u.PasswordConfirmation)
                .NotEmpty().WithMessage("Password confirmation is required.");

            RuleFor(u => u.PasswordConfirmation.Equals(u.Password))
                .NotEmpty().WithMessage("Password confirmation must be equal to password.");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(PhoneNumberRegex).WithMessage("Phone number must be in format: +359 XX XXXX XXX.");

            RuleFor(u => u.Gender)
                .IsInEnum().WithMessage("Invalid gender.");

            RuleFor(u => u.Role)
                .IsInEnum().WithMessage("Invalid role.");
        }
    }
}
