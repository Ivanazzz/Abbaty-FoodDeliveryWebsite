namespace FoodDeliveryWebsite.Models.Dtos
{
    public class ProductOrderDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string ImageName { get; set; }

        public string ImageMimeType { get; set; }
    }
}
