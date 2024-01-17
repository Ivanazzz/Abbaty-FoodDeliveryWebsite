using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository { get; set; }
        private IConfiguration _config;

        public ProductController(IProductRepository productRepository, IConfiguration config)
        {
            this.productRepository = productRepository;
            _config = config;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync()
        {
            var products = await productRepository.GetProductsAsync();

            return Ok(products);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto productDto)
        {
            await productRepository.AddProductAsync(productDto);

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto productDto)
        {
            await productRepository.UpdateProductAsync(productDto);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            await productRepository.DeleteProductAsync(id);

            return Ok();
        }
    }
}
