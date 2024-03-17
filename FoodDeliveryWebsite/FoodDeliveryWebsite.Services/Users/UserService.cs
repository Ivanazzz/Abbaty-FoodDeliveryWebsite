using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IMapper mapper;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public UserService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            User user = mapper.Map<User>(userRegistrationDto);

            UserValidator validator = new UserValidator();
            var result = validator.Validate(user);

            foreach (var failure in result.Errors)
            {
                if (failure.CustomState is BadRequestException bre)
                {
                    throw bre;
                }
            }

            bool userExists = await repository.All<User>()
                .SingleOrDefaultAsync(u => u.Email == user.Email && !u.IsDeleted) != null
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

            await repository.AddAsync(user);
            await repository.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await repository.All<User>()
                .SingleOrDefaultAsync(u => u.Email == userLoginDto.Email 
                    && !u.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            bool isPasswordValid = VerifyPassword(userLoginDto.Password, user.Password, Convert.FromHexString(user.Salt));

            if (!isPasswordValid)
            {
                throw new NotFoundException(ExceptionMessages.InvalidUser);
            }

            return user;
        }

        public async Task<UserDto?> GetUserAsync(string email)
        {
            var user = await repository.All<User>()
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
