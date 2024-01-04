namespace FoodDeliveryWebsite.Models.Common
{
    public interface IAuditable
    {
        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }
    }
}
