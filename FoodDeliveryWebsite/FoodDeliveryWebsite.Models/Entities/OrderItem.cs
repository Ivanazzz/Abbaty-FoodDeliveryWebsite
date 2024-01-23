using FoodDeliveryWebsite.Models.Common;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class OrderItem : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ProductQuantity { get; set; }

        public decimal Price { get; set; }
    }
}
