using FoodDeliveryWebsite.Models.Enums;

namespace FoodDeliveryWebsite.Models.Dtos.DiscountDtos
{
    public class DiscountDto
    {
        public string Code { get; set; }

        public string StartDate { get; set; }

        public string ExpirationDate { get; set; }

        public int Percentage { get; set; }

        public DiscountStatus Status => GetStatus();

        private DiscountStatus GetStatus()
        {
            DateTime startDate = DateTime.Parse(StartDate);
            DateTime expirationDate = DateTime.Parse(ExpirationDate);

            DateTime startDateUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            DateTime expirationDateUtc = DateTime.SpecifyKind(expirationDate, DateTimeKind.Utc);

            DateTime currentDate = DateTime.UtcNow;

            return currentDate >= startDateUtc
                && currentDate <= expirationDateUtc
                    ? DiscountStatus.Available
                    : DiscountStatus.Unavailable;
        }
    }
}
