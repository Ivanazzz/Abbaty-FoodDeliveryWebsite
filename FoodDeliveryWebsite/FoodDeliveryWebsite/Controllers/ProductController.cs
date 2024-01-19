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

        [HttpGet("GetSelected/{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var product = await productRepository.GetSelectedProductAsync(id);

            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromForm] ProductAddDto productDto)
        {
            await productRepository.AddProductAsync(productDto);

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductGetDto productDto)
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

        [HttpGet("{id:int}/File")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var products = await productRepository.GetProductsAsync();

            var image = products.Single(e => e.Id == id).Image;
            var mimeType = products.Single(e => e.Id == id).ImageMimeType;
            var name = products.Single(e => e.Id == id).ImageName;

            return File(image, mimeType, name);
        }
    }
}
