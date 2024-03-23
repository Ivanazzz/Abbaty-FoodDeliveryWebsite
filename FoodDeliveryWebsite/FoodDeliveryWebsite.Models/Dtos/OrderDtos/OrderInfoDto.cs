using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.Models.Dtos.OrderDtos
{
    public class OrderInfoDto
    {
        public string UserFullName { get; set; }

        public DateTime OrderDate { get; set; }

        public bool HaveUsedDiscount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
