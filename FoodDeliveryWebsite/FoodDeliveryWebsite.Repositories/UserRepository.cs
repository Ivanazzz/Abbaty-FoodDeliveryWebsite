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

        public async Task Register(UserRegistrationDto userRegistrationDto)
        {
            User user = new User
            {
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Gender = userRegistrationDto.Gender,
                Email = userRegistrationDto.Email,
                Password = userRegistrationDto.Password,
                PasswordConfirmation = userRegistrationDto.PasswordConfirmation,
                PhoneNumber = userRegistrationDto.PhoneNumber,
                Addresses = userRegistrationDto.Addresses,
                Role = UserRole.Client
            };

            UserValidator validator = new UserValidator();
            validator.ValidateAndThrow(user);

            bool userExists = await context.User.FirstOrDefaultAsync(u => u.Email == user.Email) != null 
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

            context.User.Add(user);
            await context.SaveChangesAsync();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
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
