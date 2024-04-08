namespace FoodDeliveryWebsite.Models.Common
{
    public interface IPassword
    {
        public string HashPasword(string password, out byte[] salt);

        public bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
