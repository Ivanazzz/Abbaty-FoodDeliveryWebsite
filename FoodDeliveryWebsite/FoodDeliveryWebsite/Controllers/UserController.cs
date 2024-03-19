using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Models.Dtos.TokenDtos;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;
using FoodDeliveryWebsite.Services;
using Microsoft.Extensions.Configuration;
using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private IUserService userService { get; set; }
        private IConfiguration _config;

        public UserController(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                await userService.RegisterAsync(userRegistrationDto);

                return Ok();
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var user = await userService.LoginAsync(userLoginDto);

                return Ok(GenerateToken(user));
            }
            catch (NotFoundException nfe)
            {
                return BadRequest(nfe.Message);
            }
        }

        [HttpPut]
        [AuthorizedClient]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDto userDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await userService.UpdateUserAsync(userEmail, userDto);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return BadRequest(nfe.Message);
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }

        [HttpDelete]
        [AuthorizedClient]
        public async Task<IActionResult> DeleteAsync()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await userService.DeleteUserAsync(userEmail);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return BadRequest(nfe.Message);
            }
        }

        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetAsync()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            var user = await userService.GetUserAsync(email);

            return Ok(user);
        }

        private ResponseTokenDto GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiresInMinutes")),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string userToken = tokenHandler.WriteToken(token);

            return new ResponseTokenDto(userToken);
        }
    }
}
