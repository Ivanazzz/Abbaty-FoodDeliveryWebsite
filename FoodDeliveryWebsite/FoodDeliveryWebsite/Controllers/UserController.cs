using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository { get; set; }

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await userRepository.AddUser(user);
            return Ok();
        }
    }
}
