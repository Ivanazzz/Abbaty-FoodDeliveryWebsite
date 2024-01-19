using FoodDeliveryWebsite.Models.Dtos;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductGetDto>> GetProductsAsync();

        Task<ProductGetDto> GetSelectedProductAsync(int id);

        Task<List<ProductGetDto>> GetFilteredProductAsync(ProductType productType);

        Task AddProductAsync(ProductAddDto productDto);

        Task UpdateProductAsync(ProductGetDto productDto);

        Task DeleteProductAsync(int id);
    }
}
