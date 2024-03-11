using Microsoft.AspNetCore.Mvc;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Repositories;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptions;
using FoodDeliveryWebsite.Attributes;
using FoodDeliveryWebsite.Models.Dtos.ProductDtos;

namespace FoodDeliveryWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository { get; set; }

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await productRepository.GetAvailableProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedAsync([FromRoute] int id)
        {
            try
            {
                var product = await productRepository.GetSelectedProductAsync(id);

                return Ok(product);
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpGet("Filtered")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] ProductType productType)
        {
            var products = await productRepository.GetFilteredProductAsync(productType);

            return Ok(products);
        }

        [HttpGet("CustomFilter")]
        public async Task<IActionResult> GetCustomFilteredAsync([FromQuery] ProductFilterDto productDto)
        {
            List<ProductGetDto> products = await productRepository.GetCustomFilteredProductAsync(productDto);

            return Ok(products);
        }

        [HttpGet("ProductsWithStatus")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetProductsWithStatusAsync([FromQuery] ProductStatus productStatus)
        {
            var products = await productRepository.GetProductsWithStatusAsync(productStatus);

            return Ok(products);
        }

        [HttpPost]
        [AuthorizedAdmin]
        public async Task<IActionResult> AddAsync([FromForm] ProductAddDto productDto)
        {
            try
            {
                await productRepository.AddProductAsync(productDto);

                return Ok();
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }

        [HttpPost("Update")]
        [AuthorizedAdmin]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductGetDto productDto)
        {
            try
            {
                await productRepository.UpdateProductAsync(productDto);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch (BadRequestException bre)
            {
                return BadRequest(bre.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [AuthorizedAdmin]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await productRepository.DeleteProductAsync(id);

                return Ok();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
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
