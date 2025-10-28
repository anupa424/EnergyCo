using DigiShopping.Models;

namespace DigiShopping.Services
{
    public interface IPointsPromotionDAL
    {
        public  Task<List<PointsPromotion>> GetPointPromotions();
        public void InvalidatePointPromotionsCache();

    }
}