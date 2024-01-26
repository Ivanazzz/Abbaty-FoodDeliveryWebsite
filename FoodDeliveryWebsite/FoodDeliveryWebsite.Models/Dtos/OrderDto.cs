using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Dtos
{
    public class OrderDto
    {
        public int UserId { get; set; }

        public int? DiscountId { get; set; }

        public int AddressId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
