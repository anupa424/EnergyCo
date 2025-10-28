using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    public class DiscountPromotionProduct
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string DiscountPromotionId { get; set; }

    }

    [NotMapped]
    public class DiscountPromotionProductDetail
    {
        public string ProductId { get; set; }
        public string DiscountPromotionId { get; set; }
        public decimal DiscountPercent { get; set; }


    }
}
