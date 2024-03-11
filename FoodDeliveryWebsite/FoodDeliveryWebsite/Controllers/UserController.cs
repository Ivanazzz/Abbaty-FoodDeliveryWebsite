using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.TokenDtos;
using FoodDeliveryWebsite.Models.Dtos.UserDtos;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository { get; set; }
        private IConfiguration _config;

        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                await userRepository.RegisterAsync(userRegistrationDto);

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
                var user = await userRepository.LoginAsync(userLoginDto);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "http://localhost:10001",
                    Audience = "http://localhost:10001",
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                    Expires = DateTime.Now.AddMinutes(120),
                    SigningCredentials = credentials
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                string userToken = tokenHandler.WriteToken(token);

                ResponseTokenDto accessToken = new ResponseTokenDto(userToken);

                return Ok(accessToken);
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

        [HttpPost("Update")]
        [AuthorizedClient]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDto userDto)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                await userRepository.UpdateUserAsync(userEmail, userDto);

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
                await userRepository.DeleteUserAsync(userEmail);

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

            var user = await userRepository.GetUserAsync(email);

            return Ok(user);
        }
    }
}
