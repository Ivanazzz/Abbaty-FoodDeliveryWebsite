using FluentValidation;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

namespace FoodDeliveryWebsite.Models.Validations
{
    public class UserUpdateValidator : AbstractValidator<User>
    {
        private const int FirstNameMaxLength = 20;
        private const int LastNameMaxLength = 20;

        private const string NameRegex = @"^[А-я\s]+$";
        private const string PhoneNumberRegex = @"^\+359\d{9}$";

        public UserUpdateValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithState(a => new BadRequestException("First name is required"))
                .MaximumLength(FirstNameMaxLength).WithState(a => new BadRequestException($"First name must not exceed {FirstNameMaxLength} characters"))
                .Matches(NameRegex).WithState(a => new BadRequestException("First name must be written in cyrilic"));

            RuleFor(u => u.LastName)
                .NotEmpty().WithState(a => new BadRequestException("Last name is required"))
                .MaximumLength(LastNameMaxLength).WithState(a => new BadRequestException($"Last name must not exceed {LastNameMaxLength} characters"))
                .Matches(NameRegex).WithState(a => new BadRequestException("Last name must be written in cyrilic"));


            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithState(a => new BadRequestException("Phone number is required"))
                .Matches(PhoneNumberRegex).WithState(a => new BadRequestException("Phone number must be in format: +359 XX XXXX XXX"));

            RuleFor(u => u.Gender)
                .IsInEnum().WithState(a => new BadRequestException("Invalid gender"));
        }
    }
}
