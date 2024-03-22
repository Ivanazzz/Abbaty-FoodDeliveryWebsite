using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Services;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private IProductService productService { get; set; }

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await productService.GetAvailableProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            var product = await productService.GetSelectedProductAsync(id);

            return Ok(product);
        }

        [HttpGet("Filtered")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] ProductType productType)
        {
            var products = await productService.GetFilteredProductAsync(productType);

            return Ok(products);
        }

        [HttpGet("CustomFilter")]
        public async Task<IActionResult> GetCustomFilteredAsync([FromQuery] ProductFilterDto productDto)
        {
            List<ProductGetDto> products = await productService.GetCustomFilteredProductAsync(productDto);

            return Ok(products);
        }

        [HttpGet("ProductsWithStatus")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetProductsWithStatusAsync([FromQuery] ProductStatus productStatus)
        {
            var products = await productService.GetProductsWithStatusAsync(productStatus);

            return Ok(products);
        }

        [HttpPost]
        [AuthorizedAdmin]
        public async Task<IActionResult> AddAsync([FromForm] ProductAddDto productDto)
        {
            await productService.AddProductAsync(productDto);

            return Ok();
        }

        [HttpPut]
        [AuthorizedAdmin]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductGetDto productDto)
        {
            await productService.UpdateProductAsync(productDto);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [AuthorizedAdmin]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await productService.DeleteProductAsync(id);

            return Ok();
        }

        [HttpGet("{id:int}/File")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var product = await productService.GetSelectedProductAsync(id);

            return File(product.Image, product.ImageMimeType, product.ImageName);
        }
    }
}
