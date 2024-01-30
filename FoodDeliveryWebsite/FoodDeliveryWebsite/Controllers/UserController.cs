using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;

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
            await userRepository.RegisterAsync(userRegistrationDto);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
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
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string userToken = tokenHandler.WriteToken(token);

            ResponseTokenDto accessToken = new ResponseTokenDto(userToken);

            return Ok(accessToken);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserDto userDto)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await userRepository.UpdateUserAsync(userDto, email);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            await userRepository.DeleteUserAsync(email);

            return Ok();
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
