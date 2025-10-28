using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    public class DiscountPromotion
    {
        [Key]
        public string Id { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal DiscountPercent { get; set; }
    }
}
