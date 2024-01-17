using FoodDeliveryWebsite.Models.Common;

namespace FoodDeliveryWebsite.Models.Entities
{
    public class Discount : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Percentage { get; set; }
    }
}
