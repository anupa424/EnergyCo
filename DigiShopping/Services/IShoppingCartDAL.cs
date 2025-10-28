using DigiShopping.Models;

namespace DigiShopping.Services
{
    public interface IShoppingCartDAL
    {
        Task<List<PointsPromotion>> FetchPointPromotions(List<string> productCategories, DateTime transactionDate);
        Task<Dictionary<string, string>> FetchProductCategory(List<string> productIds);
        Task<List<DiscountPromotionProductDetail>> FetchDiscountPromotionProductDetails(List<string> productIds, DateTime transactionDate);

    }
}