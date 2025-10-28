using DigiShopping.Data;
using DigiShopping.Models;

namespace DigiShopping.Services.Impl
{
    public class ShoppingCartBAL : IShoppingCartBAL
    {
        private readonly IShoppingCartDAL _shoppingCartDAL;
        private readonly IPointsPromotionDAL _pointsPromotionsDAL;

        public ShoppingCartBAL(IShoppingCartDAL shoppingCartDAL, IPointsPromotionDAL pointsPromotionsDAL)
        {
            _shoppingCartDAL = shoppingCartDAL;
            _pointsPromotionsDAL = pointsPromotionsDAL;

        }
        /// <summary>
        /// service to compute the shopping cart -points ,discounts
        /// </summary>
        /// <param name="shoppingRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<ShoppingResponse>> Checkout(ShoppingRequest shoppingRequest)
        {
            ApiResponse<ShoppingResponse> apiResponse = new ApiResponse<ShoppingResponse>();
            decimal totalAmount = 0;
            decimal totalDiscount = 0;
            decimal totalPoints = 0;
            if (shoppingRequest is not null)
            
            {
                List<string> productIds = shoppingRequest.Basket.Select(p => p.ProductId).ToList();
                Dictionary<string, string> productCategories = await _shoppingCartDAL.FetchProductCategory(productIds);
                List<DiscountPromotionProductDetail> productDiscounts = await _shoppingCartDAL.FetchDiscountPromotionProductDetails(productIds,shoppingRequest.TransactionDate);
                List<PointsPromotion> pointsPromotions = new();
                if (productCategories is not null && productCategories.Count > 0)
                {
                    pointsPromotions = await FetchPointPromotions(productCategories.Values.ToList(), shoppingRequest.TransactionDate);
                }

      
                foreach (var item in shoppingRequest.Basket)
                {
                    if (productCategories.TryGetValue(item.ProductId, out string category))
                    {
                        decimal itemPrice = item.UnitPrice * item.Quantity;

                        PointsPromotion applicablePromotion = pointsPromotions.FirstOrDefault(x => (x.Category.Equals(category) || x.Category.ToLower().Equals("any")));
                        if (applicablePromotion is not null) totalPoints += itemPrice * applicablePromotion.PointsPerDollar;

                        totalAmount += itemPrice;

                        DiscountPromotionProductDetail? discountPromotion = productDiscounts?.FirstOrDefault(x=>x.ProductId==item.ProductId);
                        if (discountPromotion is not null)
                        {
                            decimal discountAmount = (itemPrice * discountPromotion.DiscountPercent) / 100;
                            totalDiscount += discountAmount;
                        }

                    }

                }
                ShoppingResponse shoppingResponse = new ShoppingResponse()
                {
                    TotalAmount = totalAmount,
                    DiscountApplied = totalDiscount,
                    PointsEarned = (int)totalPoints,
                    CustomerId = shoppingRequest.CustomerId,
                    TransactionDate = shoppingRequest.TransactionDate,
                    GrandTotal = totalAmount - totalDiscount,
                    LoyaltyCard = shoppingRequest.LoyaltyCard
                };
                apiResponse.Result = shoppingResponse;
                apiResponse.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                apiResponse.StatusCode = StatusCodes.Status400BadRequest;
                apiResponse.Errors.Add( "Invalid shopping request.");
                 
            }
                
            return apiResponse;

        }

        public async Task<List<PointsPromotion>> FetchPointPromotions(List<string> productCategories, DateTime transactionDate)
        {
            List<PointsPromotion> pointPromotions = null;

            var points = await _pointsPromotionsDAL.GetPointPromotions();
            if(points is not null && points.Count >0)
                 pointPromotions =  points.Where(p => (productCategories.Contains(p.Category) || p.Category.ToLower().Equals("any")) && transactionDate >= p.StartDate && transactionDate < p.EndDate)
                                        .OrderByDescending(x => x.Category).ThenByDescending(x => x.PointsPerDollar).ToList();
            return pointPromotions;
        }
    }
}