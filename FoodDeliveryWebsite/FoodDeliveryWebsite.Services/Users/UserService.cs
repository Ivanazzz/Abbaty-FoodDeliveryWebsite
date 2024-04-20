using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using System.Text.RegularExpressions;

using FoodDeliveryWebsite.CustomExceptionMessages;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Validations;

namespace FoodDeliveryWebsite.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly IPassword password;
        private readonly IMapper mapper;

        public UserService(IRepository repository, IPassword password, IMapper mapper)
        {
            this.repository = repository;
            this.password = password;
            this.mapper = mapper;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            userRegistrationDto.PhoneNumber = string.Concat(userRegistrationDto.PhoneNumber.Where(c => !char.IsWhiteSpace(c)));

            UserValidator validator = new UserValidator();
            var result = validator.Validate(userRegistrationDto);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            User user = mapper.Map<User>(userRegistrationDto);

            bool userExists = await repository.AllReadOnly<User>()
                .SingleOrDefaultAsync(u => u.Email == user.Email && !u.IsDeleted) != null
                    ? true
                    : false;

            if (userExists)
            {
                throw new BadRequestException(ExceptionMessages.AlreadyExistingUser);
            }

            user.PhoneNumber = FormatPhoneNumber(user.PhoneNumber);

            var hashedPassword = password.HashPasword(user.Password, out var salt);
            user.Password = hashedPassword;
            user.Salt = Convert.ToHexString(salt);

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await repository.AllReadOnly<User>()
                .SingleOrDefaultAsync(u => u.Email == userLoginDto.Email 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            bool isPasswordValid = password.VerifyPassword(userLoginDto.Password, user.Password, Convert.FromHexString(user.Salt));

            if (!isPasswordValid)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            return user;
        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            var user = await repository.AllReadOnly<User>()
                .Where(u => u.Email == email && !u.IsDeleted)
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return user;
        }

        public async Task UpdateUserAsync(string email, UserDto userDto)
        {
            var user = await repository.All<User>()
                .SingleOrDefaultAsync(u => u.Email == email 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            userDto.PhoneNumber = string.Concat(userDto.PhoneNumber.Where(c => !char.IsWhiteSpace(c)));

            UserUpdateValidator validator = new UserUpdateValidator();
            var result = validator.Validate(userDto);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Gender = userDto.Gender;
            user.PhoneNumber = FormatPhoneNumber(userDto.PhoneNumber);

            await repository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await repository.All<User>()
                .SingleOrDefaultAsync(u => u.Email == email 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            user.IsDeleted = true;
            await repository.SaveChangesAsync();
        }

        private static string FormatPhoneNumber(string phoneNumber)
        {
            string pattern = @"(\+359)(\d{2})(\d{4})(\d{3})";

            return Regex.Replace(phoneNumber, pattern, "$1 $2 $3 $4");
        }
    }
}
