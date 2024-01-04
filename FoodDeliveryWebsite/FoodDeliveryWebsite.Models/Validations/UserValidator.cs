using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        private const int FirstNameMaxLength = 20;
        private const int LastNameMaxLength = 20;

        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FirstNameMaxLength).WithMessage($"First name must not exceed {FirstNameMaxLength} characters.")
                .Matches(@"^[А-я\s]+$").WithMessage("First name must be written in cyrilic.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(LastNameMaxLength).WithMessage($"Last name must not exceed {LastNameMaxLength} characters.")
                .Matches(@"^[А-я\s]+$").WithMessage("Last name must be written in cyrilic.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$").WithMessage(
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

            //RuleFor(u => u.PhoneNumber)
            //    .NotEmpty().WithMessage("Phone number is required.")
            //    .Matches().WithMessage("Phone number must be in format: +359 XXX XXX XXX.");

            RuleFor(u => u.Gender)
                .IsInEnum().WithMessage("Invalid gender.");

            RuleFor(u => u.Role)
                .IsInEnum().WithMessage("Invalid role.");
        }
    }
}
