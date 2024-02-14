namespace FoodDeliveryWebsite.Models.Common
{
    public interface IFilter<T>
    {
        IQueryable<T> WhereBuilder(IQueryable<T> query);
    }
}
