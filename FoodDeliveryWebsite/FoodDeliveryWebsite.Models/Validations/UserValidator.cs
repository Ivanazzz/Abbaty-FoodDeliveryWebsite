using FluentValidation;

using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        private const int FirstNameMaxLength = 20;
        private const int LastNameMaxLength = 20;

        private const string NameRegex = @"^[А-я\s-]+$";
        private const string PasswordRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.,#?!@$%^&*-]).{8,}$";
        private const string PhoneNumberRegex = @"^\+359\d{9}$";
        private const string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";

        public UserValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithState(a => new BadRequestException("First name is required"))
                .MaximumLength(FirstNameMaxLength).WithState(a => new BadRequestException($"First name must not exceed {FirstNameMaxLength} characters"))
                .Matches(NameRegex).WithState(a => new BadRequestException("First name must be written in cyrilic"));

            RuleFor(u => u.LastName)
                .NotEmpty().WithState(a => new BadRequestException("Last name is required"))
                .MaximumLength(LastNameMaxLength).WithState(a => new BadRequestException($"Last name must not exceed {LastNameMaxLength} characters"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Last name must be written in cyrilic"));

            RuleFor(u => u.Email)
                .NotEmpty().WithState(a => new BadRequestException("Email address is required"))
                .Matches(emailRegex).WithState(a => new BadRequestException("A valid email address is required"));

            RuleFor(u => u.Password)
                .NotEmpty().WithState(a => new BadRequestException("Password is required"))
                .Matches(PasswordRegex).WithState(a => new BadRequestException(
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
                    + "- At least one special character"));

            RuleFor(u => u.PasswordConfirmation)
                .NotEmpty().WithState(a => new BadRequestException("Password confirmation is required"));

            RuleFor(u => u.PasswordConfirmation.Equals(u.Password))
                .NotEmpty().WithState(a => new BadRequestException("Password confirmation must be equal to password"));

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithState(a => new BadRequestException("Phone number is required"))
                .Matches(PhoneNumberRegex).WithState(a => new BadRequestException("Phone number must be in format: +359 XX XXXX XXX"));

            RuleFor(u => u.Gender)
                .IsInEnum().WithState(a => new BadRequestException("Invalid gender"));

            RuleFor(u => u.Role)
                .IsInEnum().WithState(a => new BadRequestException("Invalid role"));
        }
    }
}
