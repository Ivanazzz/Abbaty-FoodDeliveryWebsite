using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Entities;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos
{
    public class ProductFilterDto : IFilter<Product>
    {
        public decimal PriceMin { get; set; }

        public decimal PriceMax { get; set; }

        public ProductType? Type { get; set; }

        public IQueryable<Product> WhereBuilder(IQueryable<Product> query)
        {
            if (PriceMin != 0)
            {
                query = query.Where(p => p.Price >= PriceMin);
            }

            if (PriceMax != 0)
            {
                query = query.Where(p => p.Price <= PriceMax);
            }

            if (Type.HasValue)
            {
                query = query.Where(p => p.Type == Type);
            }

            query = query.Where(p => p.Status == ProductStatus.Available);

            return query;
        }
    }
}
