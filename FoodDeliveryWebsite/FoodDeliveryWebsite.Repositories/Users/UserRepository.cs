using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptionMessages;

namespace FoodDeliveryWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        private readonly FoodDeliveryWebsiteDbContext context;
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserRepository(FoodDeliveryWebsiteDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            User user = mapper.Map<User>(userRegistrationDto);
            user.PhoneNumber = string.Concat(userRegistrationDto.PhoneNumber.Where(c => !char.IsWhiteSpace(c)));
            user.Role = UserRole.Client;

            UserValidator validator = new UserValidator();
            var result = validator.Validate(user);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            bool userExists = await context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.IsDeleted == false) != null
                    ? true
                    : false;

            if (userExists)
            {
                throw new BadRequestException(ExceptionMessages.AlreadyExistingUser);
            }

            user.PhoneNumber = FormatPhoneNumber(user.PhoneNumber);

            var hashedPassword = HashPasword(user.Password, out var salt);
            user.Password = hashedPassword;
            user.PasswordConfirmation = hashedPassword;
            user.Salt = Convert.ToHexString(salt);

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == userLoginDto.Email 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.NonExistentUser);
            }

            bool isPasswordValid = VerifyPassword(userLoginDto.Password, user.Password, Convert.FromHexString(user.Salt));

            if (!isPasswordValid)
            {
                throw new BadRequestException(ExceptionMessages.InvalidUserPassword);
            }

            return user;
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email 
                    && u.IsDeleted == false);

            if (user == null)
            {
                return null;
            }

            var userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task UpdateUserAsync(string email, UserDto userDto)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Gender = userDto.Gender;
            user.PhoneNumber = string.Concat(userDto.PhoneNumber.Where(c => !char.IsWhiteSpace(c)));

            UserUpdateValidator validator = new UserUpdateValidator();
            var result = validator.Validate(user);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            user.PhoneNumber = FormatPhoneNumber(user.PhoneNumber);

            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email 
                    && u.IsDeleted == false);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            user.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        private static string FormatPhoneNumber(string phoneNumber)
        {
            string pattern = @"(\+359)(\d{2})(\d{4})(\d{3})";

            return Regex.Replace(phoneNumber, pattern, "$1 $2 $3 $4");
        }

        private string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
            salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
