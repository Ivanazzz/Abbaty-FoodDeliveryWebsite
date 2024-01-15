using FluentValidation;

using FoodDeliveryWebsite.Models;
using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Models.Validations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FoodDeliveryWebsite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDeliveryWebsiteDbContext context;
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserRepository(FoodDeliveryWebsiteDbContext context)
        {
            this.context = context;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            User user = new User
            {
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Gender = userRegistrationDto.Gender,
                Email = userRegistrationDto.Email,
                Password = userRegistrationDto.Password,
                PasswordConfirmation = userRegistrationDto.PasswordConfirmation,
                PhoneNumber = String.Concat(userRegistrationDto.PhoneNumber.Where(c => !Char.IsWhiteSpace(c))),
                Addresses = userRegistrationDto.Addresses,
                Role = UserRole.Client
            };

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrow(user);

            bool userExists = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.IsDeleted == false) != null 
                ? true 
                : false;

            if (userExists)
            {
                throw new Exception("User with the given email already exists.");
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
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email && u.IsDeleted == false);

            if (user == null)
            {
                throw new Exception("There isn't user with the given email.");
            }

            bool isPasswordValid = VerifyPassword(userLoginDto.Password, user.Password, Convert.FromHexString(user.Salt));
            
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password.");
            }

            return user;
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                //Addresses = user.Addresses,
                Role = UserRole.Client,
                //Orders = user.Orders
            };

            return userDto;
        }

        public async Task UpdateUserAsync(UserDto userDto, string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
            
            if (user != null)
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Gender = userDto.Gender;
                user.PhoneNumber = String.Concat(userDto.PhoneNumber.Where(c => !Char.IsWhiteSpace(c)));

                UserUpdateValidator validator = new UserUpdateValidator();
                validator.ValidateAndThrow(user);

                user.PhoneNumber = FormatPhoneNumber(user.PhoneNumber);

                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);

            if (user != null)
            {
                user.IsDeleted = true;
                await context.SaveChangesAsync();
            }
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
