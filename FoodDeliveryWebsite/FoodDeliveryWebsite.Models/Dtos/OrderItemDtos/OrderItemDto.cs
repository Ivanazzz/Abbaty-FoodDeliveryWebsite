using FoodDeliveryWebsite.Models.Dtos.ProductDtos;

namespace FoodDeliveryWebsite.Models.Dtos.OrderItemDtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public ProductOrderDto Product { get; set; }

        public int ProductQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
