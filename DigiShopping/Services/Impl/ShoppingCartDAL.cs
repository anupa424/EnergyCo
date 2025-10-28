using DigiShopping.Data;
using DigiShopping.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DigiShopping.Services.Impl
{
    public class ShoppingCartDAL : IShoppingCartDAL
    {
        private readonly DigiShoppingContext _digitShoppingContext;

        public ShoppingCartDAL(DigiShoppingContext digitShoppingContext)
        {
            _digitShoppingContext = digitShoppingContext;
        }

        public async Task<List<PointsPromotion>> FetchPointPromotions(List<string> productCategories, DateTime transactionDate)
        {
            var pointPromotions = await _digitShoppingContext.PointsPromotions.Where(p => (productCategories.Contains(p.Category) || p.Category.ToLower().Equals("any"))  && transactionDate >= p.StartDate && transactionDate < p.EndDate)
                                        .OrderByDescending(x=>x.Category).ThenByDescending(x=>x.PointsPerDollar).ToListAsync();
            return pointPromotions;
        }
        public async Task<List<DiscountPromotionProductDetail>> FetchDiscountPromotionProductDetails(List<string> productIds, DateTime transactionDate)
        {
            var query = (from dpp in _digitShoppingContext.DiscountPromotionProducts
                        join dp in _digitShoppingContext.DiscountPromotions on dpp.DiscountPromotionId equals dp.Id
                        where productIds.Contains(dpp.ProductId)
                              && dp.StartDate <= transactionDate
                              && dp.EndDate >= transactionDate
                        select new DiscountPromotionProductDetail
                        {
                            ProductId = dpp.ProductId,
                            DiscountPromotionId = dpp.DiscountPromotionId,
                            DiscountPercent = dp.DiscountPercent
                        }).OrderByDescending(x=>x.ProductId).ThenByDescending(x=>x.DiscountPercent);
            return await query.ToListAsync();


        }
        public async Task<Dictionary<string, string>> FetchProductCategory(List<string> productIds)
        {
            var categories = await _digitShoppingContext.ProductDetails.Where(p => productIds.Contains(p.ProductId))
                            .ToDictionaryAsync(p => p.ProductId, p => p.Category);
            return categories;

        }
    }
}