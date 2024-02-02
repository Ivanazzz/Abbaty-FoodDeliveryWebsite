using Microsoft.AspNetCore.Mvc;

using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Enums;
using FoodDeliveryWebsite.Repositories;
using static FoodDeliveryWebsite.Repositories.ValidatorContainer.ValidatorRepository;
using FoodDeliveryWebsite.CustomExceptions;
using FoodDeliveryWebsite.Repositories.CustomExceptions;

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

        [HttpGet("GetFiltered")]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] ProductType productType)
        {
            var products = await productRepository.GetFilteredProductAsync(productType);

            return Ok(products);
        }

        [HttpGet("GetProductsWithStatus")]
        [AuthorizedAdmin]
        public async Task<IActionResult> GetProductsWithStatusAsync([FromQuery] ProductStatus productStatus)
        {
            var products = await productRepository.GetProductsWithStatusAsync(productStatus);

            return Ok(products);
        }

        [HttpPost("Add")]
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

        [HttpDelete("Delete/{id:int}")]
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
