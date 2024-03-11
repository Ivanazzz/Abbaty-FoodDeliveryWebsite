namespace FoodDeliveryWebsite.Models.Dtos.TokenDtos
{
    public class ResponseTokenDto
    {
        public ResponseTokenDto(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
