using FoodDeliveryWebsite.Models.Common;
using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Discount : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public string Code { get; set; }

        public DiscountStatus Status { get; set; }

        public int Percentage { get; set; }
    }
}
