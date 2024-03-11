namespace FoodDeliveryWebsite.Models.Dtos.AddressDtos
{
    public class AddressDto
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNo { get; set; }

        public int? Floor { get; set; }

        public int? ApartmentNo { get; set; }
    }
}
