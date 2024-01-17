using FoodDeliveryWebsite.Models.Dtos;

namespace FoodDeliveryWebsite.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDto[]> GetProductsAsync();

        Task AddProductAsync(ProductDto productDto);

        Task UpdateProductAsync(ProductDto productDto);

        Task DeleteProductAsync(int id);
    }
}
