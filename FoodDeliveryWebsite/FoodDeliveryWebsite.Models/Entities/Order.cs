using FoodDeliveryWebsite.Models.Common;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Order : IEntity, IAuditable
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int? DiscountId { get; set; }

        public Discount Discount { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
