using DigiShopping.Data;
using DigiShopping.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
namespace DigiShopping.Services.Impl
{
    public class PointsPromotionDAL: IPointsPromotionDAL
    {
        private readonly IMemoryCache _cache;
        private readonly DigiShoppingContext _digitShoppingContext;

        public PointsPromotionDAL(IMemoryCache cache, DigiShoppingContext digitShoppingContext)
        {
            _cache = cache;
            _digitShoppingContext = digitShoppingContext;
        }
        /// <summary>
        /// method to cache all the data in pointpromotions to cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<PointsPromotion>> GetPointPromotions()
        {
            if (_cache.TryGetValue($"pointpromotions", out List<PointsPromotion> cachedPoints))
            {
                return cachedPoints;
            }

            var points = await _digitShoppingContext.PointsPromotions.ToListAsync();
               

            _cache.Set($"pointpromotions", points, TimeSpan.FromMinutes(3600));
            return points;
        }
        /// <summary>
        /// method to incvalidate the cache
        /// </summary>
        public void InvalidatePointPromotionsCache()
        {
            _cache.Remove($"pointpromotions");
        }

        public async Task<List<PointsPromotion>> FetchPointPromotions(List<string> productCategories, DateTime transactionDate)
        {
            var pointPromotions = await _digitShoppingContext.PointsPromotions.Where(p => (productCategories.Contains(p.Category) || p.Category.ToLower().Equals("any")) && transactionDate >= p.StartDate && transactionDate < p.EndDate)
                                        .OrderByDescending(x => x.Category).ThenByDescending(x => x.PointsPerDollar).ToListAsync();
            return pointPromotions;
        }
    }
}