using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductGetDto>> GetAvailableProductsAsync();

        Task<List<ProductGetDto>> GetAllProductsAsync();

        Task<ProductGetDto> GetSelectedProductAsync(int id);

        Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType);

        Task<List<ProductGetDto>> GetCustomFilteredProductAsync(ProductFilterDto filter);

        Task<List<ProductGetDto>> GetProductsWithStatusAsync(ProductStatus productStatus);

        Task AddProductAsync(ProductAddDto productDto);

        Task UpdateProductAsync(ProductGetDto productDto);

        Task DeleteProductAsync(int id);
    }
}
