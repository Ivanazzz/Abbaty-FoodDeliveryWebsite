using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IProductRepository
    {
        Task<ProductGetDto[]> GetProductsAsync();

        Task<ProductGetDto> GetSelectedProductAsync(int id);

        Task AddProductAsync(ProductAddDto productDto);

        Task UpdateProductAsync(ProductGetDto productDto);

        Task DeleteProductAsync(int id);
    }
}
