using FoodDeliveryWebsite.Models.Dtos.ProductDtos;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Services
{
    public interface IProductService
    {
        Task<List<ProductGetDto>> GetAvailableProductsAsync();

        Task<ProductGetDto> GetProductByIdAsync(int id);

        Task<ProductGetDto> GetSelectedProductAsync(int id);

        Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType);

        Task<List<ProductGetDto>> GetCustomFilteredProductAsync(ProductFilterDto filter);

        Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus);

        Task AddProductAsync(ProductAddDto productDto);

        Task UpdateProductAsync(ProductGetDto productDto);

        Task DeleteProductAsync(int id);
    }
}
