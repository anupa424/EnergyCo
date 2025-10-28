using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    public class PointsPromotion
    {
        [Key]
        public string Id { get; set; }
        public string PromotionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PointsPerDollar { get; set; }
    }
}
