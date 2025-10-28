using DigiShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiShopping.Data
{
    public class DigiShoppingContext : DbContext
    {
        public DigiShoppingContext(DbContextOptions<DigiShoppingContext> options)
            : base(options)
        {
        }

        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PointsPromotion> PointsPromotions { get; set; }
        public DbSet<DiscountPromotion> DiscountPromotions { get; set; }
        public DbSet<DiscountPromotionProduct> DiscountPromotionProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed ProductDetails

            // Seed ProductDetails
            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail { ProductId = "PRD01", ProductName = "Vortex 95", Category = "Fuel", UnitPrice = 1.2m },
                new ProductDetail { ProductId = "PRD02", ProductName = "Vortex 98", Category = "Fuel", UnitPrice = 1.3m },
                new ProductDetail { ProductId = "PRD03", ProductName = "Diesel", Category = "Fuel", UnitPrice = 1.1m },
                new ProductDetail { ProductId = "PRD04", ProductName = "Twix 55g", Category = "Shop", UnitPrice = 2.3m },
                new ProductDetail { ProductId = "PRD05", ProductName = "Mars 72g", Category = "Shop", UnitPrice = 5.1m },
                new ProductDetail { ProductId = "PRD06", ProductName = "SNICKERS 72G", Category = "Shop", UnitPrice = 3.4m },
                new ProductDetail { ProductId = "PRD07", ProductName = "Bounty 3 63g", Category = "Shop", UnitPrice = 6.9m },
                new ProductDetail { ProductId = "PRD08", ProductName = "Snickers 50g", Category = "Shop", UnitPrice = 4.0m }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = Guid.Parse("8e4e8991-aaee-495b-9f24-52d5d0e509c5"),
                    FirstName = "John",
                    LastName = "Doe",
                    LoyaltyCard = "CTX0000001"
                }
            );

            // Seed PointsPromotion
            // Seed PointsPromotions
            modelBuilder.Entity<PointsPromotion>().HasData(
                new PointsPromotion
                {
                    Id = "PP001",
                    PromotionName = "New Year Promo",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 1, 30),
                    Category = "Any",
                    PointsPerDollar = 2m
                },
                new PointsPromotion
                {
                    Id = "PP002",
                    PromotionName = "Fuel Promo",
                    StartDate = new DateTime(2020, 2, 5),
                    EndDate = new DateTime(2020, 2, 15),
                    Category = "Fuel",
                    PointsPerDollar = 3m
                },
                new PointsPromotion
                {
                    Id = "PP003",
                    PromotionName = "Shop Promo",
                    StartDate = new DateTime(2020, 3, 1),
                    EndDate = new DateTime(2020, 3, 20),
                    Category = "Shop",
                    PointsPerDollar = 4m
                }
            );

            // Seed DiscountPromotion
            modelBuilder.Entity<DiscountPromotion>().HasData(
                new DiscountPromotion
                {
                    Id = "DP001",
                    PromotionName = "April Discount",
                    StartDate = new DateTime(2020, 1, 1),
                    EndDate = new DateTime(2020, 2, 15),
                    DiscountPercent = 20    
                },
                 new DiscountPromotion
                 {
                     Id = "DP002",
                     PromotionName = "Happy Promo",
                     StartDate = new DateTime(2020, 3, 2),
                     EndDate = new DateTime(2020, 3, 20),
                     DiscountPercent = 15.0m
                 }
            );

            modelBuilder.Entity<DiscountPromotionProduct>().HasData(

                 new DiscountPromotionProduct
                 {
                     Id=1,
                     DiscountPromotionId = "DP001",
                     ProductId = "PRD02"
                 },
                 new DiscountPromotionProduct
                 {
                     Id=2,
                     DiscountPromotionId = "DP001",
                     ProductId = "PRD01"
                 },
                new DiscountPromotionProduct
                {
                    Id=3,
                    DiscountPromotionId = "DP002",
                    ProductId = "PRD03"
                }
            );

        }

    }
}
