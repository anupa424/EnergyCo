
using DigiShopping.Data;
using DigiShopping.Models;
using DigiShopping.Services;
using DigiShopping.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace DigiShopping.Tests
{
    public class ShoppingCartBALTests
    {
        private readonly Mock<IShoppingCartDAL> _shoppingCartDALMock;
        private readonly Mock<IPointsPromotionDAL> _pointsPromotionDALMock;
        private readonly ShoppingCartBAL _shoppingCartBAL;

        public ShoppingCartBALTests()
        {
            _shoppingCartDALMock = new Mock<IShoppingCartDAL>();
            _pointsPromotionDALMock = new Mock<IPointsPromotionDAL>();
            _shoppingCartBAL = new ShoppingCartBAL(_shoppingCartDALMock.Object, _pointsPromotionDALMock.Object);
        }

        [Fact]
        public async Task Checkout_ReturnsCorrectTotals_WhenValidRequest()
        {
            // Arrange
            var request = new ShoppingRequest
            {
                CustomerId = new Guid(),
                TransactionDate = new DateTime(2020, 01, 04),
                LoyaltyCard = "LC456",
                Basket = new List<Basket>
            {
                new Basket { ProductId = "PRD01", UnitPrice = 1.20m, Quantity = 3 },
              // new Basket { ProductId = "PRD02", UnitPrice = 2.0m, Quantity = 2 },
              // new Basket { ProductId = "PRD03", UnitPrice = 5.0m, Quantity = 5 }
            }
            };

            var productCategories = new Dictionary<string, string>
        {
            { "PRD01", "Fuel" },
            { "PRD02", "Shop" }
        };

            var productDiscounts = new List<DiscountPromotionProductDetail>
        {
            new DiscountPromotionProductDetail { ProductId = "PRD01", DiscountPercent = 10 },
            new DiscountPromotionProductDetail { ProductId = "PRD02", DiscountPercent = 20 }
        };

            var pointsPromotions = new List<PointsPromotion>
        {
            new PointsPromotion { Category = "Fuel", PointsPerDollar = 1, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue },
            new PointsPromotion { Category = "Shop", PointsPerDollar = 2, StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue }
        };

            _shoppingCartDALMock.Setup(x => x.FetchProductCategory(It.IsAny<List<string>>()))
                .ReturnsAsync(productCategories);

            _shoppingCartDALMock.Setup(x => x.FetchDiscountPromotionProductDetails(It.IsAny<List<string>>(), It.IsAny<DateTime>()))
                .ReturnsAsync(productDiscounts);

            _pointsPromotionDALMock.Setup(x => x.GetPointPromotions())
                .ReturnsAsync(pointsPromotions);

            var result = await _shoppingCartBAL.Checkout(request);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(result.Result);


            // Assert
            var expectedTotal = 1.20m * 3; // 3.60
            var expectedDiscount = expectedTotal * 0.10m; // 0.36
            var expectedPoints = expectedTotal * 1; // 3.60 → 3 points (rounded down)

            Assert.Equal(expectedTotal, result.Result.TotalAmount);
            Assert.Equal(expectedDiscount, result.Result.DiscountApplied);
            Assert.Equal((int)expectedPoints, result.Result.PointsEarned);
            Assert.Equal(expectedTotal - expectedDiscount, result.Result.GrandTotal);

        }
    }
}
