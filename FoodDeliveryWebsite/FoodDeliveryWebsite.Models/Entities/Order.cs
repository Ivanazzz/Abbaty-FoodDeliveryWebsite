using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Order : IEntity, IAuditable
    {
        private const decimal deliveryPrice = 7;

        public Order()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public OrderStatus Status { get; set; }

        public int OrderNo { get; set; }

        public decimal DeliveryPrice => deliveryPrice;

        public int UserId { get; set; }

        public User User { get; set; }

        public Discount Discount { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
