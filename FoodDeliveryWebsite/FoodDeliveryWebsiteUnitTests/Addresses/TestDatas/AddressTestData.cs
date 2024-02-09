using FoodDeliveryWebsite.Models.Entities;

namespace FoodDeliveryWebsite.UnitTests.Addresses
{
    public static class AddressTestData
    {
        public static List<Address> GetTestAddresses()
        {
            return new List<Address>
            {
                new Address { Id = 1, City = "София", Street = "Шипка", StreetNo = 34, Floor = 1, ApartmentNo = null },
                new Address { Id = 2, City = "Варна", Street = "Морска", StreetNo = 13, Floor = 2, ApartmentNo = 6 },
                new Address { Id = 2, City = "София", Street = "Христо Ботев", StreetNo = 56, Floor = null, ApartmentNo = null }
            };
        }
    }
}