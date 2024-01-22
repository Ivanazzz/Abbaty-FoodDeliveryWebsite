using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.Models.Enums;

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
            var products = await productRepository.GetAvailableProductsAsync();

            return Ok(products);
        }

        [HttpGet("GetSelected/{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var product = await productRepository.GetSelectedProductAsync(id);

            return Ok(product);
        }

        [HttpGet("GetFiltered")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] ProductType productType)
        {
            var products = await productRepository.GetFilteredProductAsync(productType);

            return Ok(products);
        }

        [HttpGet("GetProductsWithStatus")]
        public async Task<IActionResult> GetProductsWithStatusAsync([FromQuery] ProductStatus productStatus)
        {
            var products = await productRepository.GetProductsWithStatusAsync(productStatus);

            return Ok(products);
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

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await productRepository.DeleteProductAsync(id);

            return Ok();
        }

        [HttpGet("{id:int}/File")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var products = await productRepository.GetAllProductsAsync();

            var image = products.Single(e => e.Id == id).Image;
            var mimeType = products.Single(e => e.Id == id).ImageMimeType;
            var name = products.Single(e => e.Id == id).ImageName;

            return File(image, mimeType, name);
        }
    }
}
