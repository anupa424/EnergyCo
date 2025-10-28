using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShopping.Models
{
    public class ProductDetail
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal UnitPrice { get; set; }
    }
}

