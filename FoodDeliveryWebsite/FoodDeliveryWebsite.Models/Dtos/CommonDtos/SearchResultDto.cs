namespace FoodDeliveryWebsite.Models.Dtos.CommonDtos
{
    public class SearchResultDto<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
